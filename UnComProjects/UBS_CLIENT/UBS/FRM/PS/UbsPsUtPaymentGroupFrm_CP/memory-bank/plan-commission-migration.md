# PLAN: Commission calculation logic migration — UbsPsUtPaymentGroupFrm

Source: `legacy-form\UtPaymentGroup\UtPaymentGroup_F.dob` — `CalcSumCommiss`, `curSumma_TextChange`, data load in `FindContractbyId` / `UtReadContract`.  
Related channel keys: `creative/creative-ubspsutpaymentgroupfrm-channel-contract.md` (`UtReadContract` outs).

## 1. Purpose

Reproduce payer commission (`udcSummaRateSend`) and total to pay (`udcSummaTotal`) from payment amount (`udcSumma`) using the same rules as VB6, including tiered tariffs and min/max caps.

## 2. Input data (`m_arrRateSend` in VB6)

Populated when `UtReadContract` succeeds (`FindContractbyId`):

| Index | VB6 source (`objParamOut`) | Role |
|-------|---------------------------|------|
| 0 | `RateTypeSend` | Base commission type: `0` = none, `1` = percent of payment, `2` = fixed sum |
| 1 | `RatePercentSend` | Percent or fixed amount (when no tier grid overrides `curPerSend`) |
| 2 | `MinSumSend` | Minimum commission (if &gt; 0, floor applied) |
| 3 | `MaxSumSend` | Maximum commission (if &gt; 0, cap applied) |
| 4 | `Комиссии с плательщика-признак ставки` | Russian tariff label string |
| 5 | `Комиссии с плательщика-тарифная сетка` | Variant matrix for brackets (may be empty / non-array) |

**.NET storage (recommended):**

- `int m_rateTypeSend`, `decimal m_ratePercentSend`, `decimal m_minSumSend`, `decimal m_maxSumSend`, `string m_strTarifCommission`, `object[,] m_arrStavka` (nullable / null when no tiers).
- Or keep a loose `object[]` of length 6 only if you must mirror VB6 literally; prefer typed fields + optional matrix for clarity.

**Matrix layout (VB6 → C#):** `arrStavka(0, i)` = bracket threshold (payment amount), `arrStavka(1, i)` = rate value for that row. Map to `object[row, 0]`, `object[row, 1]` with `row == i` (same convention as project array-rule).

## 3. Trigger (when to recalculate)

- **Primary:** Whenever the payment amount control changes and **`m_IdContract > 0`** (VB6 `curSumma_TextChange`).
- **Not** automatically re-run on every contract field change unless legacy did (it did not); contract change reloads `m_arrRateSend` via `UtReadContract`, then user editing amount fires recalc.
- After **`ReadContract`**, amounts come from server (`SummaPaym`, `SummaRateSend`, `Summa`); still call **`CalcSumCommiss`** once if parity requires sync when user then edits only amount — legacy relies on TextChange; opening edit on sum will fire. If .NET loads without touching sum, optionally call **`CalcSumCommiss`** after filling `udcSumma` when `m_IdContract > 0` to align totals (CREATIVE/BUILD: compare side-by-side with VB6).

## 4. Algorithm (`CalcSumCommiss`) — order preserved

```
curSumPaym = payment amount (decimal)

intTypeSend = m_rateTypeSend (from index 0)
curPerSend = m_ratePercentSend (from index 1)
curMinSumSend, curMaxSumSend from 2,3
arrStavka = m_arrStavka, strTarif = m_strTarif (index 4)

If arrStavka is non-null array with length > 0 in 2nd dimension:
    If strTarif == "ставка, %"      → intTypeSend = 1
    Else If strTarif == "фиксированная сумма" → intTypeSend = 2
    Else → EXIT without updating commission/total controls (VB6 Exit Function)
    If tier data empty → EXIT without updating
    curPerSend = 0
    i = upper row index of arrStavka (second dimension)
    blnFind = false
    Do
        If (decimal)arrStavka[0,i] <= curSumPaym → curPerSend = (decimal)arrStavka[1,i], blnFind = true
        If i == 0 → blnFind = true
        i--
    Loop While !blnFind

Switch intTypeSend:
    0 → curSumRateSend = 0
    1 → curSumRateSend = Round2(curSumPaym * curPerSend / 100)
    2 → curSumRateSend = Round2(curPerSend)

If intTypeSend > 0:
    If curSumRateSend < curMinSumSend && curMinSumSend > 0 → curSumRateSend = Round2(curMinSumSend)
    If curSumRateSend > curMaxSumSend && curMaxSumSend > 0 → curSumRateSend = Round2(curMaxSumSend)

curSumTotal = Round2(curSumPaym + curSumRateSend)

Assign to udcSummaRateSend and udcSummaTotal (use control API: Text or Value property as in `UbsCtrlDecimal` — follow `UbsPsContractFrm` or sibling forms).
```

**Early exit:** If the tier block exits on `Case Else` or empty `arrStavka`, VB6 **does not** assign commission/total (leaves previous UI). .NET must **return immediately** without overwriting `udcSummaRateSend` / `udcSummaTotal` in those branches.

## 5. Rounding (`objFormat.Round` vs .NET 2.0)

VB6 uses `Lib3.IUbsFormat` **`Round(..., 2)`** for principal amounts; min/max clamp uses VB **`Round(..., 2)`** (not `objFormat`) on lines 1967–1970.

- **PLAN default:** Use **`decimal`** for all money math; **`Math.Round(decimal, 2)`** for two decimal places.
- **Risk:** VB6 `Currency` / `Round` may differ from .NET default rounding (banker’s vs away-from-zero).  
- **Mitigation:** During BUILD/REFLECT, compare outputs for sample contracts (percent only, fixed only, tiered, min/max hit). If drift &gt; 0.01, switch to **`MidpointRounding.AwayFromZero`** on `decimal` after scaling, or locate a **UBS library** rounding helper if the product exposes one.
- Document chosen rule in `Constants.cs` comment or one-line in `Commission.cs` (no essay).

## 6. File placement

| Artifact | Location |
|----------|----------|
| `CalcSumCommiss` (private method) | `UbsPsUtPaymentGroupFrm.Commission.cs` |
| Amount-changed handler wiring | Same file: subscribe in ctor after `InitializeComponent` or in `OnLoad` — match `UbsCtrlDecimal` event name from DLL |
| `m_arrRateSend` / tier fields | `UbsPsUtPaymentGroupFrm.cs` (shared fields) **or** `Commission.cs` as `partial` fields — prefer **main `.cs`** if other partials need contract state |

**Populate rate bundle** in **`Initialization.cs`** inside `FindContractbyId` immediately after successful `UtReadContract` (same lines as VB6 `ReDim m_arrRateSend(5)` and assignments).

## 7. Dependencies

- No extra channel calls inside `CalcSumCommiss` (pure client-side from cached `UtReadContract` outputs).
- **Penalty** (`udcPeny`): not part of `CalcSumCommiss` in VB6; total is **sum + commission only**. Do not add penalty into this formula unless product confirms.

## 8. Test matrix (REFLECT)

| Case | Expectation |
|------|-------------|
| `intTypeSend == 0` | Commission 0, total = payment |
| Percent, no tiers | `amount * rate / 100`, rounded, then min/max |
| Fixed, no tiers | `Round(rate, 2)`, then min/max |
| Tier grid + «ставка, %» | Bracket search selects `curPerSend`, then type 1 |
| Tier grid + «фиксированная сумма» | Bracket search, then type 2 |
| `strTarif` unknown with array present | No UI update (early exit) |
| Min/max with zero bound | VB6 only applies floor/ceiling when bound &gt; 0 |

## 9. Constants (`Constants.cs`)

Add string literals (exact UTF-8 Russian as in VB6):

- `TarifLabelPercent = "ставка, %"`
- `TarifLabelFixed = "фиксированная сумма"`

Avoid scattering these in `Commission.cs`.

---

**Summary:** Cache six outputs from `UtReadContract` in typed fields + optional `object[,]` tiers; on amount change with contract selected, run `CalcSumCommiss` with preserved order, early-exit parity, and `decimal` + explicit two-decimal rounding; wire UI in `Commission.cs` and data load in `Initialization.cs`.
