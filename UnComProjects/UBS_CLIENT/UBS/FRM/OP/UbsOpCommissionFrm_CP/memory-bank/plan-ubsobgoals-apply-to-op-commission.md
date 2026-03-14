# PLAN: Apply UbsBgContractFrm Goals to UbsOpCommissionFrm

**Source:** Analysis of `D:\Repositories\TutorialGit\UnComProjects\UBS_CLIENT\UBS\FRM\BG\UbsBgContractFrm_CP`  
**Target:** `UbsOpCommissionFrm_CP` (OP Commission form)  
**Date:** 2026-03-14

---

## 1. MAIN GOALS OF UbsBgContractFrm_CP (Source Project)

### 1.1 Project Structure & Documentation
- **Memory Bank** with tasks, projectbrief, activeContext, progress, techContext, systemPatterns, productContext
- **Archive** for completed tasks
- **Creative phase docs** for design decisions (creative-*.md)
- **Refactoring plans** for large methods (refactoring-plan-*.md)
- **Channel contract documentation** (ubsguarmodelfrm-channel-contract.md)

### 1.2 Code Quality & Architecture
- **Constants file:** `UbsBgContractFrm.Constants.cs` — all magic strings (channel params, commands, messages, field names) moved to `private const`
- **Partial class split:** Large form split into partials:
  - `UbsBgContractFrm.Constants.cs` — constants
  - `UbsBgContractFrm.ListKey.cs` — ListKey + helpers
  - `UbsBgContractFrm.InitDoc.cs` — InitDoc + command-based init
  - `UbsBgContractFrm.Helper.cs` — UI/combo helpers
  - `UbsBgContractFrm.Rates.cs` — rates logic
- **Refactoring:** InitDoc, ListKey, ReReadDoc broken into smaller methods per refactoring-plan-*.md

### 1.3 UBS Form Pattern Compliance
- All forms inherit from `UbsFormBase` (not `Form`)
- Implement IUbs: `m_addCommand()`, `CommandLine()`, `ListKey()`, `m_addFields()`
- Layout: `panelMain`, `tableLayoutPanel`, `ubsCtrlInfo`, `btnApply`, `btnExit`
- Use `Ubs_ShowError` for errors
- Set `IUbsChannel.LoadResource` in constructor

### 1.4 VB6 to .NET Conversion (where applicable)
- VB6 forms in `source/BG_CONTRACT/` converted to .NET Framework 2.0 WinForms
- UBS controls: `UbsCtrlDate`, `UbsCtrlDecimal`, etc.
- Encoding: Windows-1251 for .cs/.Designer.cs, UTF-8 for .resx
- Russian text preserved; Cyrillic comments fixed

### 1.5 Creative Phase Process
- Option C first: constants + channel contract docs (no behavior change)
- Option B second: validation layer if needed
- Incremental refactor; no breaking channel contract

---

## 2. APPLICABLE GOALS FOR UbsOpCommissionFrm_CP

| UbsBgContractFrm Goal                    | Applicable to UbsOpCommissionFrm? | Notes |
|----------------------------------------|-----------------------------------|------|
| Memory Bank structure                  | ✅ Yes                            | Already created (VAN mode) |
| Project brief (objectives, scope)      | ✅ Yes                            | Update to match style |
| Constants file (magic strings)         | ✅ Yes                            | Introduce early; few strings now |
| Partial class split                   | ⏳ Later                          | Form is ~104 lines; split when it grows |
| Channel contract documentation        | ✅ Yes                            | Document LoadResource, commands, params |
| Refactoring plans                     | ⏳ Later                          | When InitDoc/ListKey grow large |
| UBS form pattern compliance           | ✅ Yes                            | Already follows; verify/align |
| VB6 conversion                        | ❓ If source exists               | No `source/OP_*` found yet |
| Creative phase for design decisions   | ✅ Yes                            | Use when adding features/validation |
| Encoding (1251/UTF-8)                 | ✅ Yes                            | Apply when Russian text is added |

---

## 3. IMPLEMENTATION ROADMAP FOR UbsOpCommissionFrm_CP

### Phase 1: Foundation (Now)
1. **Update project brief** — objectives, scope, success criteria (same structure as UbsBgContractFrm)
2. **Create `UbsOpCommissionFrm.Constants.cs`** — constants for:
   - `LoadResource` string
   - Field names (e.g. "Имя поля")
   - Commands (if any — CommandLine, ListKey are stubs)
   - Messages (when added)
3. **Replace literals in `UbsOpCommissionFrm.cs`** with constants
4. **Create channel contract doc** — `memory-bank/creative/ubsopcommissionfrm-channel-contract.md` (commands, params in/out)

### Phase 2: As Form Grows
5. **Split partial classes** — when main file exceeds ~300 lines:
   - `UbsOpCommissionFrm.Constants.cs` (already in Phase 1)
   - `UbsOpCommissionFrm.ListKey.cs` — ListKey + helpers
   - `UbsOpCommissionFrm.InitDoc.cs` or similar — if InitDoc/command logic appears
6. **Refactoring plans** — create refactoring-plan-*.md when methods exceed ~100 lines

### Phase 3: If VB6 Source Exists
7. **Add `source/OP_COMMISSION/`** with VB6 forms (if available)
8. **VB6 conversion** — same pattern as BG_CONTRACT (forms, controls, validation)

---

## 4. SUCCESS CRITERIA (Aligned with UbsBgContractFrm)

- Form integrates with UBS channel system
- Commands (CommandLine, ListKey) function correctly
- Field collection and validation work as expected
- No magic strings in main logic; all in Constants
- Channel contract documented
- Zero linter errors
- 100% architecture compliance (UbsFormBase, IUbs, layout pattern)

---

## 5. NEXT STEPS

**Immediate (Phase 1):**
- [ ] Update `memory-bank/projectbrief.md` with full objectives/scope
- [ ] Create `UbsOpCommissionFrm.Constants.cs`
- [ ] Replace literals in `UbsOpCommissionFrm.cs` with constants
- [ ] Create `memory-bank/creative/ubsopcommissionfrm-channel-contract.md`

**When ready:** Type **BUILD** to implement Phase 1, or **CREATIVE** to design further.
