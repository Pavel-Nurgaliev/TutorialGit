# Memory Bank: Progress

## Summary
Phase 2 (Commission_ud → UbsOpCommissionFrm conversion) complete. Form implements ADD/EDIT, Get_Data, Com_Save, CheckData, TabControl with main data and add-fields. Form appearance aligned with legacy-form-screens (2026-03-16).

## Form Appearance — Legacy Screens (2026-03-16)

### Completed
- **Designer (UbsOpCommissionFrm.Designer.cs):** Form title «Добавить новую»; tab «Комиссия» (was «Основные»), tab «Набор параметров» (was «Доп. поля»); txbDesc Multiline = true, ScrollBars.Vertical, Size (475, 60), Anchor Top|Left|Right|Bottom.
- **Code-behind (UbsOpCommissionFrm.cs):** `UbsCtrlFieldsSupportCollection.Add("Набор параметров", ubsCtrlAddFields)` (key matches second tab).
- **Linter:** No errors. Verification: build in Visual Studio / .NET 2.0 environment and visual check against legacy-form-screens recommended.

## Phase 2 Implementation (2026-03-14)

### Completed
- **Constants:** AddCommand, EditCommand, GetDataAction, ComSaveAction, ParamAction, ParamName, ParamDesc, ParamId, Msg*
- **UI:** TabControl (tabPageMain: txbName, txbDesc; tabPageAddFields: ubsCtrlAddFields), btnSave, btnExit, ubsCtrlInfo
- **InitDoc:** Get_Data for EDIT, clear for ADD; Load event fallback when ListKey not called
- **ListKey:** Extract m_id from param_in; call InitDoc; handle empty list for EDIT
- **btnSave:** CheckData, Com_Save with params Наименование/Описание
- **m_addFields:** ParamName, ParamDesc bound to txbName, txbDesc
- **UbsCtrlFields:** Added to UbsCtrlFieldsSupportCollection
- **LoadResource:** ASM path for OP Commission

## Phase 1 Implementation (2026-03-14)

### Completed
- **UbsOpCommissionFrm.Constants.cs** created with:
  - `#region Ресурс канала` — `LoadResource`
  - `#region Имена полей` — `FieldNameExample`
- **UbsOpCommissionFrm.cs** updated:
  - `IUbsChannel.LoadResource = LoadResource`
  - `IUbsFieldCollection.Add(FieldNameExample, ...)` and `IUbsFieldCollection[FieldNameExample].ReadOnly = true`
- **UbsOpCommissionFrm.csproj** — added `<Compile Include="UbsOpCommissionFrm.Constants.cs" />`
- **Linter:** No errors on `UbsOpCommissionFrm.cs` or `UbsOpCommissionFrm.Constants.cs`

### Build verification
- **Automated build:** Not run successfully in this environment (MSBuild not in PATH; `dotnet build` failed with "reference assemblies for .NETFramework,Version=v2.0 were not found").
- **Recommendation:** Build in Visual Studio or a developer prompt with .NET 2.0 targeting pack installed to confirm compile.

## Milestones
- [x] Phase 1: Constants partial + literal replacement
- [ ] Phase 2: Split partials when form grows (~300+ lines)
- [ ] Phase 3: VB6 conversion (if source exists)
