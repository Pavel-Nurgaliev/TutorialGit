# CREATIVE: Payment instruction — `UbsControlAccount` → `UbsCtrlAccount`, side indexes, TabIndex

**Scope:** Tab 5 «Оплата», nested `tabControlInstr` (`tabPageInstr1` = Покупатель, `tabPageInstr2` = Продавец).

---

## 1. Control mapping (VB6 → .NET)

| VB6 (`UBSCTRLLibCtl`) | .NET | Notes |
|----------------------|------|--------|
| `UbsControlAccount` (RS / KS) | `UbsCtrlAccount` | Not `TextBox`. Add assembly reference in `.csproj` when implementing. |

**Field fill/clear:** `UbsCtrlAccount` **derives from `System.Windows.Forms.TextBox`** (assembly `UbsCtrlAccount.dll`, type `UbsControl.UbsCtrlAccount`). Use **`.Text`** (and inherited **`.ReadOnly` / `.Enabled` / `.TabStop`**) like the legacy placeholders; optional **`.AccountFormat`** / **`.MaxLength`** for display rules.

---

## 2. Side index (business meaning)

| Constant | Value | Sub-tab |
|----------|-------|---------|
| `InstrSideBuyer` | 0 | `tabPageInstr1` — Покупатель |
| `InstrSideSeller` | 1 | `tabPageInstr2` — Продавец |

Use these names in C# instead of raw `0`/`1` for instruction-side branching.

---

## 3. TabIndex (WinForms — **per parent container**)

**Container:** each of `tabPageInstr1` and `tabPageInstr2` is its own tab-order scope. Values **restart** inside each page; do not copy legacy VB6 form-wide numbers (e.g. 47/53/54).

**Canonical interactive order** (top → bottom, matches current corrected `Designer.cs` intent):

| Order | Control role | TabIndex | TabStop (typical) |
|------|----------------|----------|-------------------|
| 1 | `chkCash` | 1 | true |
| 2 | `linkListInstr` (pick instruction) | 2 | true |
| 3 | `txtBIK` | 3 | true |
| 4 | **`ucaKS`** (корр. счёт) | 4 | **false** when read-only/disabled like legacy |
| 5 | `txtName` (bank name) | 5 | false (display) |
| 6 | **`ucaRS`** (расч. счёт) | 6 | **false** when set only via picker/cash fill |
| 7 | `linkAccountPayment` | 7 | true |
| 8 | `txtClient` | 8 | true |
| 9 | `txtNote` | 9 | true |
| 10 | `txtINN` | 10 | true |
| 11 | `chkNotAkcept` | 11 | true |

**Rules:**

- **Labels** stay at `TabIndex = 0` with `TabStop = false` (default).
- **`UbsCtrlAccount`:** assign **`TabIndex` on the host control** in the **TabPage** (same slots as former `txtKS` / `txtRS`). If the user control exposes multiple inner editors, align inner `TabStop` with product behavior: prefer **one logical stop** or **none** when the host is display-only, so keyboard order matches the table above.
- When a **`cmdAccount`-style** button is added to the right of RS (per screen layout), give it the next index **after** `ucaRS` and **before** or **after** `linkAccountPayment` consistently on both sub-tabs; renumber siblings so indices stay **dense 1…N** with no duplicates.

---

## 4. BUILD checklist

1. ~~Add `UbsCtrlAccount` reference to `UbsPmTradeFrm.csproj`.~~ **Done** (HintPath `UbsCtrlAccount.dll` alongside other Ubs controls).
2. ~~Replace `txtRS0/1`, `txtKS0/1` with `ucaRS0/1`, `ucaKS0/1` in `Designer.cs`; wire `FillControlInstrPayment` / `ClearPayment`~~ **Done** (control type `UbsControl.UbsCtrlAccount`; `.Text` fill/clear unchanged — inherits `TextBox`).
3. **TabIndex / TabStop:** unchanged from prior pass (KS=4, RS=6, `TabStop=false`); re-verify after any new `cmdAccount` control.
4. **Manual keyboard walk** on target workstation when exercising Tab 5.

---

## 5. Open points

- Linked **account id** (if any) beyond displayed **`.Text`** — confirm when Save / picker integration is implemented.
- Whether account picker opens from `linkAccountPayment` only or also from `cmdAccount` when added.
