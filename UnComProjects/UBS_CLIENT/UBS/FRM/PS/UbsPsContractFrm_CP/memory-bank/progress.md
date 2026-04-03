# Memory Bank: Progress

## Summary

**2026-04-02:** Phase A BUILD started. Added `UbsPsContractFrm.Constants.cs`, template controls with **prefix-based** names (`btnSave`, `btnExit`, `uciContract`, `tabContract`, … per creative §7), and a 3-tab designer shell. IUbs bootstrap (`CommandLine`, `ListKey`) updated with basic EDIT selection validation.

**2026-04-02:** Phase B BUILD: main tab + recipient block + commission tab controls in designer; `UbsPsContractFrm.InitDoc.cs` with `InitFormContract`, `Contract` READ, `ReadKind`/`ReadClient`, executors/commission combos, `UbsCtrlFields` registration; `ListKey`/`Load` hybrid + browse/save stubs; project references `UbsCtrlDecimal`/`UbsCtrlDate`/`UbsCtrlAccount`/`UbsCtrlFields`. `MSBuild` Debug succeeds.

**2026-04-02:** `/build` sweep: **Debug + Release** `MSBuild` OK; **Release** had CS1591 until XML comments on `UbsPsContractFrm()` and `Dispose(bool)` (documentation output enabled in csproj).

**2026-04-02:** **Phase C BUILD:** `UbsPsContractFrm.Commission.cs` — `EnableSumCommissionControls` matching `Contract.dob` `EnableSum` (combo indices 0 and 3 disable/zero `udcPayerCommissionPercent` / `udcRecipientCommissionPercent`); `SelectedIndexChanged` on commission type combos; `InitDoc` calls enable logic after load. Restored `Load` + `m_addFields` on main form (was missing; fixed CS0414 on `m_isInitialized`). Channel creative §7 documents Phase C; save of `Метод расчета комиссии с получателя` left for Phase E.

**2026-04-02:** **Phase F BUILD:** Designer tab order on main tab (code → OI → number → dates → kind → status → close → recipient block → comment), recipient inner order, commission labels `TabStop = false`, `tblActions` 0/1/2; `UbsPsContractFrm.Keys.cs` (Esc: add-fields → commission → main); ctor `KeyPreview`, `AcceptButton = btnSave`, `Load` hybrid. Reflection `reflection-phase-f-ubspcontractfrm.md`. MSBuild Debug + Release OK.

## Completed

- [x] Form/solution/project named **UbsPsContractFrm** (v2.0).
- [x] Memory bank: core files + **detailed conversion plan** (`plan-ubspcontractfrm-conversion.md`).
- [x] **CREATIVE:** `creative-ubspcontractfrm-channel-contract.md`, `creative-ubspcontractfrm-conversion-architecture.md`.
- [x] **CREATIVE (Phase B):** `creative-phase-b-main-tab.md` — InitDoc entry, param mapping, browse/add-fields scope.
- [x] **Phase A BUILD (partial):** constants file + designer shell + naming convention alignment + initial IUbs command bootstrap.
- [x] **Phase B BUILD:** main/commission/add-fields UI, `InitDoc`, channel wiring, stubs, csproj control refs, build OK.
- [x] **Phase C BUILD:** commission tab `EnableSum` parity, doc note; save param for recipient commission method → Phase E.
- [x] **Phase F BUILD:** tabindex/focus polish, `Keys.cs`, reflection doc.

## Not Started

- [ ] Phase A BUILD: host-specific `RetFromGrid` mapping and integration command names confirmation.
- [ ] Phases D–E: add-fields depth, save/validation, `GetBankNameACC` / `EnableFieldsCl` full port.

## Milestones

- [ ] Phase A complete (host integration pending)
- [x] Phase B (main tab + InitDoc shell)
- [x] Phase C (commission `EnableSum` + load of recipient commission method)
- [ ] Phases D–E (add-fields + save)
- [ ] Phase E (save)
- [x] Phase F (tab order, Esc between tabs, partial `Keys`, reflect)
