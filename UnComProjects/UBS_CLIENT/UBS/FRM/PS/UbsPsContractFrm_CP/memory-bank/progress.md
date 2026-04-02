# Memory Bank: Progress

## Summary

**2026-04-02:** Memory bank initialized. **UbsPsContractFrm** project uses named form files (`UbsPsContractFrm.cs`, `.Designer.cs`, `.resx`), `UbsPsContractFrm.csproj` / `.sln`, `TargetFrameworkVersion` v2.0, assembly name `UbsPsContractFrm`. VB6 conversion of `Contract.dob` is **not** implemented yet.

## Completed

- [x] Form/class/solution renamed from template (`UbsForm1` / `UbsFormProject1`) to **UbsPsContractFrm**.
- [x] Memory bank core files created under `memory-bank/`.

## Not Started

- [ ] Full inventory of `legacy-form/Contract/Contract.dob` (controls, events, channel contract).
- [ ] `UbsPsContractFrm.Constants.cs` and creative channel contract document.
- [ ] Designer layout vs `legacy-form/screens/` (folder may be populated separately).
- [ ] InitDoc / ListKey / save path parity with legacy.

## Milestones (target)

- [ ] Phase 1: Constants partial + channel contract doc + inventory
- [ ] Phase 2: Main conversion (UI + logic from `Contract.dob`)
- [ ] Phase 3: Partial splits, appearance polish, reflection
