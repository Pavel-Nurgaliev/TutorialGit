# PLAN: Group payment cycle implementation — UbsPsUtPaymentGroupFrm

Source: `legacy-form\UtPaymentGroup\UtPaymentGroup_F.dob` — `UBSChild_ParamInfo` (`InitParamForm`), `InitDoc`, `btnSave_Click`, `NewRecord`, `InitFormGroup` out `EndGroup`.  
Channel: `Payment_Save` outs — `creative/creative-ubspsutpaymentgroupfrm-channel-contract.md`.

## 1. Business meaning

- **Group payment:** Multiple payments share one **group**; after each successful save, the user may **add another** payment into the same group via host script `PsPaymentIncomingReception.vbs`.
- **Commands:** `StrCommand` drives init: **`ADD`** (new group line), **`GROUP_EDIT`** (edit existing group payment / continue group), **`GROUP_ADD`** (add to group — see `InitDoc` branch). Item array carries payment id when not `ADD`.

## 2. Form open (`InitParamForm` / .NET `CommandLine`)

| VB6 | Meaning |
|-----|---------|
| `RHS(0)` | Parent window id |
| `RHS(1)` | `ItemArray` — payment id list (first element = `m_IdPayment` when not ADD) |
| `RHS(2)` | `StrCommand` |

**Logic:**

- `StrCommand = "ADD"` → `m_IdPayment = 0`.
- Else → require non-empty `ItemArray`; `m_IdPayment = ItemArray(0)`; if empty → message «Список групповых платежей пуст!», close form.

Then **`InitChannel`** (channel host, stub, `ucfAddFields`), **`InitDoc`** (`InitFormGroup`).

**.NET:** Map host `CommandLine` / `InitParamForm` payload to the same three values; same guards and messages in `Constants.cs`.

## 3. `InitDoc` / `InitFormGroup` (group state)

| Out | Use |
|-----|-----|
| `EndGroup` | If true → `DisableAllFields` after `ReadContract` when `StrCommand = "GROUP_EDIT"` |
| `GroupContract` | Fill `cmbCode` (ids + captions) |
| `ChoiceClient`, `SummaPeni`, `strError` | As in channel contract |

**Branches:**

- **`GROUP_EDIT`:** `ReadContract`; if `blnEndGroup` then disable all inputs.
- **`GROUP_ADD`:** `EnableAllFields`.

**.NET:** After full layout exists, mirror enable/disable lists from VB6 `DisableAllFields` / `EnableAllFields`.

## 4. Save success path — UI that always follows `Payment_Save` (VB6)

Inside the same `m_blnSave` block, **after** `Run("Payment_Save")` and success/error info updates:

1. `btnClient.Enabled = false`
2. Select **main tab** (`Tabs(1)` in VB6)
3. `m_blnSave = false`; `btnExit.Enabled = true`
4. **Group continuation** `MsgBox`: «Вы хотите продолжить ввод платежей в группу?» / title «Ввод группы платежей», **Yes/No**, default button 1.

**If Yes:**

1. `m_IdPayment = objParamOut("IdPaym")`
2. Create **`URunScr.IUbsRunScript`**, set:
   - `UbsChannel` = same channel instance as form
   - `Parameter("Parent")` = host parent (VB6 `Parent` object — **interop mapping required**)
   - `Parameter("Key")` = array of one element: `m_IdPayment`
   - `Parameter("Идентификатор группового платежа")` = `PAYMENTGROUPID` from save out
   - `Parameter("Идентификатор основного платежа")` = `m_IdPayment`
3. `LoadFiles("UBS_VBS\PS\PsPaymentIncomingReception.vbs")` — path string as VB6 (constant in `Constants.cs`)
4. `ExecuteScript`
5. `ReleaseUbsChannel`
6. If `objScript1.Parameter("EndGroup")` → `btnSave.Enabled = false`; else `btnSave.Enabled = true`

**Then (always after save block, Yes or No):**

- `StrCommand = "GROUP_EDIT"`
- `btnClient.Enabled = true`
- `cmbCode.Enabled = true`

**Parity warning:** In VB6 this tail runs even when `StrError` was set on `Payment_Save` (user still gets group question and `GROUP_EDIT`). **Recommendation for .NET:** run steps 1–3 and script **only** when `string.IsNullOrEmpty(StrError)`; still refresh `StrCommand`/buttons per product. Document any intentional deviation in archive.

## 5. Interop: `IUbsRunScript`

| Topic | Plan |
|-------|------|
| ProgId | `URunScr.IUbsRunScript` — late-bind via `Type.GetTypeFromProgID` + `Activator.CreateInstance`, or add COM reference if interop assembly exists in shop standard |
| Channel | Assign same `IUbsChannel` / wrapper the form uses so script sees same session |
| `Parent` | VB6 `UBSParrent` — .NET must pass object the host expects (often `IUbs` or a known dispatch). **CREATIVE/BUILD:** confirm with `UbsFormBase` / host docs or mirror another PS form that calls `IUbsRunScript` |
| Lifecycle | Always call **`ReleaseUbsChannel`** after `ExecuteScript` (match VB6 `finally`-style) |
| Errors | `try`/`catch` → `Ubs_ShowError`; optionally re-enable Save/Exit |

## 6. Constants (`Constants.cs`)

- `ScriptGroupContinuation = @"UBS_VBS\PS\PsPaymentIncomingReception.vbs"` (or exact literal from VB6)
- `MsgGroupContinue = "Вы хотите продолжить ввод платежей в группу?"`
- `CaptionGroupInput = "Ввод группы платежей"`
- `MsgGroupPaymentListEmpty = "Список групповых платежей пуст!"`
- Param keys for script: `"Идентификатор группового платежа"`, `"Идентификатор основного платежа"`, `"Parent"`, `"Key"` (string literals)

## 7. Code placement

| Concern | File |
|---------|------|
| Init routing, `StrCommand`, `m_IdPayment` from host | `UbsPsUtPaymentGroupFrm.cs` (`CommandLine`) |
| `InitDoc`, `InitChannel`, `ReadContract`, group branches | `UbsPsUtPaymentGroupFrm.Initialization.cs` |
| `btnSave_Click`, `Payment_Save`, group MsgBox, script, post-save flags | `UbsPsUtPaymentGroupFrm.Save.cs` |
| `NewRecord`, `PAYMENT` CLEAR, re-init | `Save.cs` or `Initialization.cs` (keep next to `InitDoc` if shared) |

## 8. `NewRecord` (reference)

VB6: `StrCommand = "CLEAR"`, `Run("PAYMENT")`, clear purpose, `m_IdPayment = 0`, `InitDoc`, `clearRecFieldsSend`, `clearRecFields`. Not active in current `btnSave` path (commented). **BUILD:** if host exposes “new line in group” via `NewRecord`, wire same sequence.

## 9. Testing (REFLECT)

- Open as **ADD** vs **GROUP_EDIT** with valid id list.
- Save with empty `StrError` → info line → group question → **No** → `StrCommand` still `GROUP_EDIT`, client/combo re-enabled.
- Save → **Yes** → script runs → `EndGroup` true/false toggles Save.
- `EndGroup` from `InitFormGroup` → fields disabled on edit open.

## 10. Checklist summary

- [ ] Host init params match VB6 three-tuple.
- [ ] `InitFormGroup` + `GROUP_EDIT` / `GROUP_ADD` / `ReadContract` + `blnEndGroup`.
- [ ] `Payment_Save` → read `IdPaym`, `PAYMENTGROUPID`, `StrError`.
- [ ] Group MsgBox + `IUbsRunScript` with all parameters and `ReleaseUbsChannel`.
- [ ] `StrCommand = GROUP_EDIT` and re-enable client + contract combo after block.
- [ ] Decide StrError vs continuation behavior (parity vs fix).
- [ ] `Parent` reference type resolved with host team.

---

**Next:** CREATIVE/BUILD resolve `Parent` type and COM activation; implement in `Save.cs` with constants for all strings and script path.
