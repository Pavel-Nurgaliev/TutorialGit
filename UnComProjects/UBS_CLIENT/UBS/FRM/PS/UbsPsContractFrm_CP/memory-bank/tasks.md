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

- [x] Designer: `tabPageMain` fields, recipient block, commission tab + `UbsCtrlFields` tab; `UbsPsContractFrm.InitDoc.cs`.
- [x] `UbsPsContractFrm.cs`: `ListKey`/`Load` hybrid, `AddFields` registration, `m_addFields`, browse/save message stubs.
- [x] `UbsPsContractFrm.csproj`: UBS control DLL references + compile `InitDoc.cs`.
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

- [ ] Phases D–E per `plan-ubspcontractfrm-conversion.md` (add-fields depth, save/validation).

## Design decisions (2026-04-02)

| Topic | Decision | Doc |
|-------|----------|-----|
| IUbs init | **CommandLine** = ADD/EDIT; **ListKey** = id / composite array; extend if host sends full `InitParamForm` blob | `creative-ubspcontractfrm-conversion-architecture.md` |
| DDX | **D2** — explicit change/build of param list; v1 can send full field set like post–`MembersValue` | same |
| Channel reference | Single inventory for all `Run` + `STRCOMMAND` variants | `creative-ubspcontractfrm-channel-contract.md` |

## Reference

- **`plan-ubspcontractfrm-conversion.md`** — master roadmap
- **`plan-phase-b-main-tab.md`** — Phase B (main tab + InitDoc + pickers)
- `plan-conversion-goals-revised.md` — short summary
- `.cursor/rules/` — designer, style, arrays

## Status

**CREATIVE phase complete** for Phase A design. **Phase B BUILD complete.** **Phase C BUILD complete** (commission `EnableSum` parity + channel doc note). **Phase F BUILD complete** (tab polish + `Keys.cs`).

**Automated tests:** No test project in this solution; **verification = MSBuild** (Debug + Release).

Build verification:
- [x] `MSBuild` **Debug** — `UbsPsContractFrm.sln` (2026-04-02, after Phase C).
- [x] `MSBuild` **Release** — same; XML doc comments added for ctor/`Dispose` so **CS1591** does not fail Release (`DocumentationFile`).

## Reflection (2026-04-02)

- [x] **Phases A/B** — **`memory-bank/reflection/reflection-phase-a-b-ubspcontractfrm.md`** (foundation, main tab, naming, build).
- [x] **Phase C** — **`memory-bank/reflection/reflection-phase-c-ubspcontractfrm.md`** (commission `EnableSum`, channel §7, `Load`/`m_addFields` regression).
- [x] **Phase F** — **`memory-bank/reflection/reflection-phase-f-ubspcontractfrm.md`** (tab order, Esc between tabs, footer focus).
- **Next:** `/archive` when ready to fold milestones into `memory-bank/archive/` and trim ephemeral checklist above.
