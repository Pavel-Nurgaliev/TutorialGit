# Memory Bank: Tasks

Ephemeral checklist — merge into archive and clear when a major milestone completes.

## Active

**Goal:** VB6 `Contract.dob` → `UbsPsContractFrm` — follow **`plan-ubspcontractfrm-conversion.md`**.

### Phase A

- [x] **CREATIVE** — Channel + architecture design docs:
  - `memory-bank/creative/creative-ubspcontractfrm-channel-contract.md` — all `Run` calls, params, messages, LoadResource placeholder.
  - `memory-bank/creative/creative-ubspcontractfrm-conversion-architecture.md` — IUbs mapping (CommandLine + ListKey), DDX strategy, control/tab mapping.
- [x] `UbsPsContractFrm.Constants.cs` — `LoadResource`, commands, param keys, UI messages (initial Phase A set added).
- [ ] Map `InitParamForm` / `RetFromGrid` in code (**in progress**: CommandLine/ListKey bootstrap implemented; host-specific RetFromGrid command naming pending integration confirmation).
- [x] Designer: `TabControl` (3 tabs), `panelMain`, buttons + info, approximate client size from legacy twips.

### Phase B

Follow **`plan-phase-b-main-tab.md`** (B.1 Designer → B.7 helpers).  
**CREATIVE:** `memory-bank/creative/creative-phase-b-main-tab.md` (InitDoc entry hybrid, param mapping, browse stubs, add-fields scope).

- [x] Designer: `tabPageMain` fields, recipient block, commission tab + `UbsCtrlFields` tab; `UbsPsContractFrm.Initialization.cs`.
- [x] `UbsPsContractFrm.cs`: `ListKey`/`Load` hybrid, `AddFields` registration, `m_addFields`, browse/save message stubs.
- [x] `UbsPsContractFrm.csproj`: UBS control DLL references + compile `Initialization.cs`.
- [x] `MSBuild` Debug for `UbsPsContractFrm.sln`.

### Phase C

- [x] **`UbsPsContractFrm.Commission.cs`:** `EnableSumCommissionControls` (legacy `EnableSum`: list indices **0** and **3** disable/zero percent editors).
- [x] Designer: `SelectedIndexChanged` on payer/recipient commission type combos.
- [x] **`InitDoc`:** call `EnableSumCommissionControls` after EDIT/ADD branch before `ucfAdditionalFields.Refresh`.
- [x] Constants: `CommissionComboIndexDisablePercentFirst` / `Second`.
- [x] **`creative-ubspcontractfrm-channel-contract.md` §7** — Phase C note (`EnableSum`, save deferred to Phase E).
- [x] Restored **`UbsPsContractFrm_Load`** / **`m_addFields`** in `UbsPsContractFrm.cs` (fixes `m_isInitialized` CS0414 and IUbs field registration).

### Phase F

- [x] Designer: main-tab **`TabIndex`** / label **`TabStop`**, recipient block order, commission tab labels, `tblActions` footer order.
- [x] `UbsPsContractFrm.Keys.cs` — Esc navigates add-fields → commission → main; ctor `KeyPreview`, `AcceptButton`.
- [x] `reflection-phase-f-ubspcontractfrm.md`; plan + `progress.md` Phase F markers.

### Then

- [x] Phases D–E per `plan-ubspcontractfrm-conversion.md` (save + add-fields keyboard / validation) — см. Waves D–E в плане обработчиков.

### Recipient helpers (2026-04-03)

- [x] **CREATIVE** — `memory-bank/creative/creative-enablefieldscl-recipient.md` — `EnableFieldsCl`: INN + address enabled iff `m_idClient <= 0` (VB6 parity); no `m_blnArbitrary` branch.
- [x] **BUILD** — `EnableFieldsCl` in `UbsPsContractFrm.Initialization.cs` (replaces stub).
- [x] **CREATIVE** — `memory-bank/creative/creative-getbanknameacc-bik-lookup.md` — `GetBankNameAcc` + `SetSignOurBik`: `ReadBankBIK`, matrix/flat `ParamOut`, `m_blnIsOurBik`, link enable rules.
- [x] **BUILD** — `GetBankNameAcc` / `SetSignOurBik` in `UbsPsContractFrm.Initialization.cs` + `m_blnIsOurBik` in `UbsPsContractFrm.cs`; channel §4.10 updated.

### Plan: All event handlers (2026-04-03)

**Complexity:** Level 3 (host integration, `RetFromGrid`, channel ordering). **Planning status:** complete — execute in `/build` waves below.

#### 1. Inventory (current workspace)

| State | Control / surface | Handler (code) | Legacy (`Contract.dob`) |
|-------|-------------------|----------------|-------------------------|
| **Wired** | `btnSave` | `btnSave_Click` — save stub | `btnSave_Click` (full save) |
| **Wired** | `btnExit` | `btnExit_Click` — `Close()` | `Parent.CloseWindow NumWin` |
| **Wired** | `btnRecipientClientClear` | `btnRecipientClientClear_Click` — `ClearBankFields` only | `btnDelClient_Click` — also `idClient=0`, `EnableFieldsCl` |
| **Wired** | `cmbPayerCommissionType`, `cmbRecipientCommissionType` | `*_SelectedIndexChanged` → `EnableSumCommissionControls` | `cboTypeSend_Click` / `cboTypeRec_Click` → `EnableSum` |
| **Wired** | `linkLabel1` («Клиент банка») | `LinkClicked` → `OpenBrowseClientList` (`Ubs_ActionRun` `UBS_LIST_COMMON_CLIENT`) | `btnClient_Click` |
| **Wired** | `linkPaymentKind` («Вид платежа») | `LinkClicked` → `OpenBrowseKindPaymentList` | `btnKind_Click` |
| **Wired** | `linkRecipientClient` («Р/с») | `LinkClicked` → `OpenBrowseAccountList` | `btnAcc_Click` |
| **Wired** | `cmbContractStatus` | `SelectedIndexChanged` → `ApplyContractCloseVisibilityFromStatus` | `cmbStatus_Click` |
| **Wired** | `udcRecipientBik` | `KeyDown` (Enter) → `OnRecipientBikEnterKey` | `UserDocument_KeyPress` on BIC |
| **Form** | `KeyPreview`, **`UbsPsContractFrm.Keys.cs`** | Esc: add-fields → commission → main; add-fields `KeyPress` Enter → `btnSave` | `AddProperties_KeyPress`, shell Ctrl+Tab (не переносился) |
| **Host** | Modal `Ubs_ActionRun` | `BrowseShell` + `Ubs_ActionRunBegin` | Legacy async `RetFromGrid` / `NotifyCloseWindow` (superseded by modal lists in Wave C) |

#### 2. Implementation waves (recommended order)

**Wave A — Designer wiring + thin handlers (no host yet)**  
1. [x] **`LinkClicked`** on `linkLabel1`, `linkPaymentKind`, `linkRecipientClient` → delegate to existing browse stubs (`linkRecipientBankClient_LinkClicked`, `linkPaymentKind_LinkClicked`, `linkRecipientClient_LinkClicked`).  
2. [x] **`cmbContractStatus.SelectedIndexChanged`** → `ApplyContractCloseVisibilityFromStatus()` when `ContractComboItem.Id == 1` (legacy `cmbStatus_Click`).  
3. [x] **`btnRecipientClientClear_Click`** — legacy `btnDelClient`: only if `m_idClient > 0`: clear id, name, `ClearBankFields`, `EnableFieldsCl`, `SetSignOurBik`.  
4. [x] No new messages this wave (stubs unchanged); constants unchanged.

**Wave B — BIK / focus parity (channel only, no modal)**  
5. [x] **`udcRecipientBik` + Enter:** `KeyDown` on form (`ContainsFocus` on BIK) → `OnRecipientBikEnterKey`: `GetBankNameAcc`, strict gate `!m_blnIsPublicPayments || (!m_blnArbitrary && EDIT)`, nested guards for message (legacy ~1614–1628), `MessageBox` `MsgBikNotFound` / `MsgBikNotFoundTitle`, main tab + focus; success: focus INN then account, `ApplyRecipientAccountAfterOurBikRule` (placeholder vs `SearchOneAccClient` via `CmdSearchAccClient`).  
6. [x] **`TryValidateBikForSave()`** — same strict + guards as save block (~891–901); called from `btnSave_Click` before save stub. **Leave/Validated** on BIC not added (legacy Enter-only).

**Wave C — Host / shell child windows**  
7. **Client list** — **`Ubs_ActionRun("UBS_LIST_COMMON_CLIENT", this, true)`** + **`Ubs_ActionRunBegin`**: `UbsItemSet` «Тип» = 1 (legacy grid where); on return **`ReadClient`**, `GetBankNameAcc`, `EnableFieldsCl`, **`SearchAccClient`** when our BIK (see `UbsPsContractFrm.BrowseShell.cs` / `Initialization.cs`).  
8. **Payment kind** — `GetNameFilterKindPaym` (`FILTERCONTRACT` from `m_contractListFilterName`, fallback **`UBS_FLT\PS\KIND_PAYM_ALL.flt`**), then **`Ubs_ActionRun`/`Ubs_ActionRunBegin`** on resolved filter; on selection **`Contract` `CHANGEKIND`**, `ReadKind` / add-fields / `EnableSumCommissionControls`.  
9. **Account list** — **`Ubs_ActionRun("UBS_LIST_OD_ACCOUNT0", this, true)`** + Begin: client id + «Балансовый счет» hidden row (legacy); on return **`ReadAcc`** → `ucaRecipientAccount`.  
10. **Notify-close** — not required for **modal** `Ubs_ActionRun(..., true)` (same pattern as `UbsBgContractFrm` / `UbsOpRetoperFrm`); legacy `NumChildWin*` refocus superseded by modal shell.

**Wave D — Save / exit / keyboard polish**  
11. [x] **`btnSave_Click`** — `ExecuteSaveContract` in **`UbsPsContractFrm.Save.cs`**: arbitrary flags, `TryValidateBikForSave`, дата закрытия, `CheckArbitraryAddFl` / `FillDependFields`, **`TryValidateContractBusinessRules`** (legacy `Check`), `Contract` **`READF`** / **`ADD`/`EDIT`**, сообщения `strError`/`StrError`, **`ApplyContractKindAndCodeEditability`**.  
12. [x] **`btnExit_Click`** — `Close()` как **`UbsBgContractFrm`** / **`UbsPmTradeFrm`** (без `CloseWindow` до подтверждения API).  
13. [x] **Esc** — **`UbsPsContractFrm.Keys.cs`**: с вкладки доп. полей → комиссия (фокус процент/тип), с комиссии → основная.

**Wave E — Add-fields keyboard**  
14. [x] **`ucfAdditionalFields.KeyPress`** — Enter → фокус **`btnSave`**; Esc → тот же цикл, что и форма (см. п.13).

#### 3. Creative / spike flags (before Wave C)

- [x] **CREATIVE** — `memory-bank/creative/creative-ubspcontractfrm-child-host-and-bik-validation.md`  
  - **Child lists:** Target **Option A** (`UbsFormBase`/loader API after spike); interim **Option C** stub acceptable; VB contract summarized for `GetWindow` / `InitParamGrid` / notify-close.  
  - **BIK:** **Option A** — shared **`TryValidateBikForForm(BikValidationContext)`** wrapping **`GetBankNameAcc()`**; Enter vs Save branches inside helper.

#### 4. Files to touch (expected)

- `UbsPsContractFrm.Designer.cs` — event hookups for links + status combo.  
- `UbsPsContractFrm.cs` — browse/save/exit handlers; optional `Keys.cs` partial.  
- `UbsPsContractFrm.Initialization.cs` — shared helpers (`SearchAccClient`, `ReadAcc` fill) called from return path.  
- `UbsPsContractFrm.Constants.cs` — window names (`UBS_LIST_COMMON_CLIENT`, …), messages (`БИК не найден`, …).  
- `creative-ubspcontractfrm-channel-contract.md` — any new `Run` / param keys discovered when wiring return paths.

#### 5. Success criteria

- Every interactive control on the main tab that had a VB analogue either **fires a handler** or is explicitly documented as intentional no-op.  
- No duplicate modal child for the same list if already open (legacy `NumChildWin* <> 0` → focus existing).  
- `MSBuild` Debug/Release clean; channel calls documented.

- [x] **BUILD** — **Wave A** (2026-04-03): link `LinkClicked`, status combo close-date visibility, clear-client parity.  
- [x] **BUILD** — **Wave B** (2026-04-03): BIK Enter + `TryValidateBikForSave`, `SearchAccClient` fill, constants `CmdReadBankBik`/`ParamBic`/search keys/messages.  
- [x] **BUILD** — **Wave C** (2026-04-03): `Ubs_ActionRun` client/kind/account lists, `Ubs_ActionRunBegin` filter setup, `ReadClient`/`ReadAcc`/`CHANGEKIND`+`ReadKind` return paths (`BrowseShell` + `Initialization`).  
- [x] **BUILD** — **Waves D–E** (2026-04-03): полное сохранение (`Save.cs`), `Keys.cs` + `KeyPress` на `ucfAdditionalFields`, `ApplyContractKindAndCodeEditability` из `InitDoc`.

## Design decisions (2026-04-02)

| Topic | Decision | Doc |
|-------|----------|-----|
| IUbs init | **CommandLine** = ADD/EDIT; **ListKey** = id / composite array; extend if host sends full `InitParamForm` blob | `creative-ubspcontractfrm-conversion-architecture.md` |
| DDX | **D2** — explicit change/build of param list; v1 can send full field set like post–`MembersValue` | same |
| Channel reference | Single inventory for all `Run` + `STRCOMMAND` variants | `creative-ubspcontractfrm-channel-contract.md` |
| `EnableFieldsCl` | **Option A** — `udcRecipientInn` / `txtRecipientAddress` enabled only when `m_idClient <= 0`; no arbitrary override | `creative-enablefieldscl-recipient.md` |
| `GetBankNameACC` | **Option A** — `ReadBankBIK` + `NUM` gate; `Parameters` matrix then flat `BANKNAME`/`CORRACC`; `SetSignOurBik` in `finally`; respect `m_blnArbitrary` for link | `creative-getbanknameacc-bik-lookup.md` |
| Host child grids | **A + C** — implement via `UbsFormBase`/host once API spiked; stubs OK until then; one child id per list type | `creative-ubspcontractfrm-child-host-and-bik-validation.md` |
| BIK Enter vs save | **Shared helper** with context (`Enter` / `Save`) calling `GetBankNameAcc`; legacy message/focus rules centralized | same |

## Reference

- **`plan-ubspcontractfrm-conversion.md`** — master roadmap
- **`plan-phase-b-main-tab.md`** — Phase B (main tab + InitDoc + pickers)
- `plan-conversion-goals-revised.md` — short summary
- `.cursor/rules/` — designer, style, arrays

## Status

**CREATIVE phase complete** for Phase A design. **Phase B BUILD complete.** **Phase C BUILD complete** (commission `EnableSum` parity + channel doc note). **Phase F:** archived as complete in reflection; if `UbsPsContractFrm.Keys.cs` is missing from the tree, re-add per **Plan: All event handlers** wave D.13.

**Automated tests:** No test project in this solution; **verification = MSBuild** (Debug + Release).

Build verification:
- [x] `MSBuild` **Debug** — `UbsPsContractFrm.sln` (2026-04-02, after Phase C).
- [x] `MSBuild` **Release** — same; XML doc comments added for ctor/`Dispose` so **CS1591** does not fail Release (`DocumentationFile`).

## Reflection (2026-04-02)

- [x] **Phases A/B** — **`memory-bank/reflection/reflection-phase-a-b-ubspcontractfrm.md`** (foundation, main tab, naming, build).
- [x] **Phase C** — **`memory-bank/reflection/reflection-phase-c-ubspcontractfrm.md`** (commission `EnableSum`, channel §7, `Load`/`m_addFields` regression).
- [x] **Phase F** — **`memory-bank/reflection/reflection-phase-f-ubspcontractfrm.md`** (tab order, Esc between tabs, footer focus).
- [x] **Phases D-E** — **`memory-bank/reflection/reflection-phase-d-e-ubspcontractfrm.md`** (full save path parity, add-fields Enter/Esc keyboard flow, post-save state normalization).
- **Next:** `/archive` when ready to fold milestones into `memory-bank/archive/` and trim ephemeral checklist above.
