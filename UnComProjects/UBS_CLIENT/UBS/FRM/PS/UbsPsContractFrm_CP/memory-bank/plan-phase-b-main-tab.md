# PLAN: Phase B — Main tab (data entry + recipient)

**Date:** 2026-04-02  
**Parent roadmap:** `plan-ubspcontractfrm-conversion.md` § Phase B  
**Design references:** `creative/creative-ubspcontractfrm-conversion-architecture.md` (§7 names, IUbs), `creative/creative-ubspcontractfrm-channel-contract.md` (Run/params).  
**Phase B CREATIVE (decisions):** `creative/creative-phase-b-main-tab.md` — InitDoc entry, param mapping, `VARSTATE`, `RetFromGrid`, add-fields scope, layout.

---

## 1. Goal

Implement the **Main** tab (`tabPageMain`) of `UbsPsContractFrm` so that:

- All legacy **main-area** controls from `Contract.dob` (`TabMain` + recipient frame) exist in the Designer with **.NET naming** from the creative doc.
- **`InitDoc`** loads data for **EDIT** and clears/defaults for **ADD**, using the same channel sequence as VB (`InitFormContract`, `Contract` READ, `ReadKind`, `ReadClient`, etc.).
- **Browse/clear** buttons invoke the host or stubs for list returns, then apply **`ReadClient` / `ReadAcc` / `Contract` `CHANGEKIND`** as in legacy `RetFromGrid`.
- **Phase B explicitly excludes** full **Save** (`btnSave` parity) and full **Check** — those stay **Phase E**, except minimal wiring needed to show InitDoc (e.g. `Load` event calling `InitDoc` after `CommandLine` + `ListKey`).

---

## 2. Prerequisites

| Item | Status |
|------|--------|
| Phase A shell (`tabContract`, `btnSave`/`btnExit`/`uciContract`, `LoadResource` constant) | Done |
| `CommandLine` + `ListKey` storing `m_command`, `m_idContract` | Done |
| Correct **ASM `LoadResource`** for PS Contract | Confirm with deployment; update constant when known |
| **Project references** for `UbsCtrlDate`, `UbsCtrlDecimal`, account control, `UbsCtrlFields` | Add to `.csproj` when first used (match `UbsOpCommissionFrm_CP` / `UbsPmTradeFrm_CP` HintPath pattern) |

---

## 3. Work packages (recommended order)

### B.1 — Designer: `tabPageMain` layout

- [ ] Add a **layout container** on `tabPageMain` (e.g. `TableLayoutPanel` or nested panels) so controls align with legacy proportions; use **`legacy-form/screens/`** when available.
- [ ] Place controls per creative §7 **Main tab** table, with **semantic `Label`** names (e.g. `ContractCodeLabel`, `RecipientBikLabel`).
- [ ] Set **read-only** / **Enabled** defaults matching VB: `RecipientClientNameTextBox`, `BankNameTextBox`, `PaymentKindCodeTextBox`, `PaymentKindCommentTextBox` disabled where legacy had `Enabled = False`.
- [ ] Set **`TabIndex`** order for main flow (code → num → kind → recipient → …) — full keyboard parity is **B.6** optional.
- [ ] Resize form **`ClientSize`** toward legacy twips (8175×6015) converted to pixels if needed; keep **`panelMain`** and bottom **`ActionsTableLayoutPanel`** per `designer-rules.mdc`.

### B.2 — Constants expansion

- [ ] Extend `UbsPsContractFrm.Constants.cs` with:
  - Channel **command** strings: `InitFormContract`, `Contract`, `ReadClient`, `ReadAcc`, `ReadKind`, `GetNameFilterKindPaym` (if used from main tab).
  - **Param keys** used in Phase B (`IDCONTRACT`, `STRCOMMAND`, `IDCLIENT`, `IDACC`, `IDKINDPAYMENT`, DDX keys from channel contract, Cyrillic keys as required).
  - **User messages** for errors already thrown in legacy InitDoc / RetFromGrid paths.

### B.3 — Field collection and state fields

- [ ] Replace placeholder `m_addFields` with **`IUbsFieldCollection`** entries for each bound field key (match DDX member names / channel keys).
- [ ] Add **private state** mirroring VB: `idClient`, `idAcc`, `idKind`, flags for arbitrary contract, our BIK, etc. (see `Contract.dob` module-level `Dim`).

### B.4 — Init entry point

- [ ] After **`CommandLine`** and **`ListKey`** have run, call **`InitDoc`** once:
  - Option A: `Load` event on the form (pattern used in other UBS conversions when ListKey may not fire first).
  - Option B: end of `ListKey` when params are complete.
- [ ] Guard: do not double-initialize; respect `Ubs_CommandLock` if needed.

### B.5 — `InitDoc` implementation (EDIT / ADD)

Order aligned with legacy `InitDoc`:

1. [ ] Clear ids / flags; init channel params objects (`ParamIn`/`ParamOut` via `IUbsChannel`).
2. [ ] `IUbsChannel.Run("InitFormContract", ...)`; read `BIKBANK`, `nIdOI`, `varExecutors`, `VARSTATE`.
3. [ ] Fill **`PayerCommissionTypeComboBox`** / **`RecipientCommissionTypeComboBox`** from `VARSTATE` (remember **.NET `object[row,column]`** vs VB variant indexing).
4. [ ] Configure **status** combo (`ContractStatusComboBox`) and visibility of close date like legacy (`blnIsPublicPayments` branch).
5. [ ] **If EDIT:** `ParamIn[ParamIdContract]`, `ParamIn[ParamCommand]=ActionRead`, `Run("Contract", ...)`, loop `ParamOut` / parameters to fill text and combos; apply **arbitrary contract** UI state; `ReadKind`, `ReadClient`; set `RecipientClientNameTextBox` from `NAME`.
6. [ ] **If ADD:** `ClearBankFields`-equivalent; set default **OI** from `m_nIdOI` when applicable; `FillExecutors` equivalent for **`ExecutorComboBox`**.
7. [ ] **Add-fields stub:** wire **`ucfAdditionalFields`** on `tabPageAddFields` with stub + `Refresh` when Phase B requires parity with VB `AddProperties.Refresh` after load (minimal wiring acceptable if Phase D completes the rest).

### B.6 — Browse buttons and `RetFromGrid` parity

- [ ] **RecipientClientBrowseButton:** open client list (host `IUbs` / modal) → on return set `idClient` → `ReadClient` → fill recipient fields; `GetBankNameACC` / `EnableFieldsCl` if present in legacy path.
- [ ] **RecipientAccountBrowseButton:** → `ReadAcc`.
- [ ] **PaymentKindBrowseButton:** → `GetNameFilterKindPaym` / filter + `Contract` `CHANGEKIND` + `FillKind` equivalent.
- [ ] **RecipientClientClearButton:** clear client + dependent fields per VB `btnDelClient_Click`.
- [ ] Map host callback to a **single** handler (e.g. `RetFromGrid` delegate) if the shell uses that name; otherwise mirror **`UbsPmTradeFrm`** child-window return pattern.

### B.7 — BIK / bank helpers (if time in Phase B)

- [ ] Port **`GetBankNameACC`**, **`SearchOneAccClient`**, **`EnableFieldsCl`** from `Contract.dob` in dependency order.
- [ ] Defer deep **KeyPress** / **Enter** / **Esc** routing to **B.7b** or Phase F if schedule slips.

---

## 4. Success criteria (Phase B done)

- [ ] Main tab visually complete; labels/read-only states match legacy intent.
- [ ] **EDIT:** opening form with list id loads contract row via channel; kind and client lines populate.
- [ ] **ADD:** fields clear/default; no null ref in `InitDoc`.
- [ ] Browse buttons either functional with host or clearly stubbed with TODO and no crash.
- [ ] Build succeeds; no new linter issues; constants hold command/param strings (no scattered magic strings for new code).
- [ ] `memory-bank/progress.md` updated; optional **CREATIVE** addendum only if channel keys differ from doc.

---

## 5. Out of scope (later phases)

| Topic | Phase |
|-------|--------|
| Commission tab logic (`EnableSum`, percent enable) | C |
| Add-fields full keyboard + `CheckExistAddFieldContract` | D |
| Save, `READF`, uniqueness, `Check` | E |
| Screenshots polish, partial file split | F |

---

## 6. Related files (implementation)

| File | Role |
|------|------|
| `UbsPsContractFrm.Designer.cs` | Main tab controls |
| `UbsPsContractFrm.cs` | `InitDoc`, handlers, `ListKey`/`CommandLine` integration |
| `UbsPsContractFrm.Constants.cs` | Strings for Run/params/messages |
| `legacy-form/Contract/Contract.dob` | Source of truth for behavior |

---

## 7. Risks

- **Host API** for grid returns may differ from VB `Parent.CloseWindow`; allocate time for debugging integration.
- **`VARSTATE` / `varExecutors`** array shapes must be validated at runtime (logging in DEBUG if needed).
