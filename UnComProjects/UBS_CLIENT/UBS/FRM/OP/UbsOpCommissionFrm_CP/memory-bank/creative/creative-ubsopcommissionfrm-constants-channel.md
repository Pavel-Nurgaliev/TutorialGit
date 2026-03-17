# 🎨 CREATIVE PHASE: UbsOpCommissionFrm — Constants & Channel Contract

**Scope:** Design decisions for constants structure and channel contract documentation.  
**Context:** WinForms, .NET 2.0, UBS channel (ASM), IUbs command pattern; align with UbsBgContractFrm goals.

---

## 📌 CREATIVE PHASE START: UbsOpCommissionFrm (Constants & Channel Contract)

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

### 1️⃣ PROBLEM

**Description:** The form is a template with few literals today (LoadResource, one field name). We need a consistent approach for (1) where and how to define constants, and (2) what to document as the channel contract so that future growth and UBS integration stay maintainable and aligned with UbsBgContractFrm.

**Requirements:**
- No magic strings in form logic; all channel/UI literals in named constants
- Structure scalable when commands, params, and messages are added
- One place documenting channel: commands, ParamIn/ParamOut, control→param mapping
- Match UBS form patterns (UbsFormBase, IUbs) and existing BG Contract approach

**Constraints:**
- Must remain compatible with `UbsFormBase`, IUbs, and existing channel
- No breaking changes to `CommandLine` / `ListKey` signatures or channel parameter names
- .NET 2.0 and Windows Forms only

---

### 2️⃣ OPTIONS

**Option A: Full UbsBgContractFrm parity from start**  
Introduce `UbsOpCommissionFrm.Constants.cs` with the same region layout as BG (Channel/Commands/Messages/Field names) and predefine placeholder constants for future commands/params. Channel contract doc is full template with empty sections.

**Option B: Minimal constants only**  
Add a small const region inside `UbsOpCommissionFrm.cs` for the two current literals (LoadResource, field name). No separate Constants file until the form grows. No channel contract doc yet.

**Option C: Constants partial + channel contract now (recommended)**  
Create `UbsOpCommissionFrm.Constants.cs` with regions (Resource, Commands, Field names; Messages when needed). Define only constants for current literals; add new constants as features are implemented. Create `ubsopcommissionfrm-channel-contract.md` documenting current LoadResource, commands (CommandLine, ListKey), and field collection — extend as channel usage grows.

**Option D: Channel contract only, constants later**  
Write the channel contract doc only; keep literals in code until the first “real” channel integration (e.g. init, list keys). Then introduce Constants partial.

---

### 3️⃣ ANALYSIS

| Criterion              | Option A (Full parity) | Option B (Minimal) | Option C (Constants + doc) | Option D (Doc only) |
|------------------------|------------------------|--------------------|----------------------------|---------------------|
| Consistency with BG    | ⭐⭐⭐⭐⭐                | ⭐⭐                 | ⭐⭐⭐⭐                      | ⭐⭐                 |
| No magic strings now   | ⭐⭐⭐⭐⭐                | ⭐⭐⭐                | ⭐⭐⭐⭐⭐                     | ⭐                  |
| Scalability            | ⭐⭐⭐⭐                 | ⭐⭐                 | ⭐⭐⭐⭐                      | ⭐⭐⭐               |
| Implementation cost    | ⭐⭐                    | ⭐⭐⭐⭐⭐              | ⭐⭐⭐⭐                      | ⭐⭐⭐⭐              |
| Onboarding / clarity   | ⭐⭐⭐                   | ⭐⭐                 | ⭐⭐⭐⭐⭐                     | ⭐⭐⭐               |

**Insights:**
- Option C gives immediate removal of magic strings, a clear place for future constants, and a single channel contract doc — without inventing placeholders (A) or deferring structure (B, D).
- Option A risks unused constants and drift; Option B/D delay the pattern and make later refactor larger.
- Option C matches the plan (“Constants + channel contract”) and UbsBgContractFrm’s Option C–first approach.

---

### 4️⃣ DECISION

**Selected:** **Option C — Constants partial + channel contract now.**

**Rationale:**
- **Constants:** Add `UbsOpCommissionFrm.Constants.cs` with regions: Resource (LoadResource), Field names, and (when needed) Commands and Messages. Today: only define constants for the two existing literals.
- **Channel contract:** Add `memory-bank/creative/ubsopcommissionfrm-channel-contract.md` describing current LoadResource, command flow (CommandLine, ListKey), and field collection. Update when new commands or params are added.
- Aligns with UbsBgContractFrm goals and plan; minimal work, clear structure, no placeholder clutter.

---

### 5️⃣ IMPLEMENTATION NOTES

**Constants file (`UbsOpCommissionFrm.Constants.cs`):**
- Same namespace and `partial class UbsOpCommissionFrm`.
- Regions (Russian or English, match main file): e.g. `#region Ресурс канала` / `#region Имена полей`.
- Current constants:
  - `LoadResource` = `"ASM:UBS_ASM\\Business\\DllName.dll->UbsBusiness.NameClass"` (or correct OP-specific value when known).
  - `FieldNameExample` (or similar) = `"Имя поля"` for the example field in `m_addFields`.
- Add Commands region when real command names are used; Messages region when user-facing strings appear.

**Main form:**
- Replace the literal in `IUbsChannel.LoadResource = ...` with the constant.
- Replace `"Имя поля"` in `IUbsFieldCollection.Add` and the indexer with the constant.

**Channel contract doc (`memory-bank/creative/ubsopcommissionfrm-channel-contract.md`):**
- **Resource:** LoadResource value and meaning.
- **Commands:** CommandLine (param_in = command string), ListKey (param_in/param_out as used).
- **Field collection:** Current fields (name, control, property); extend when more fields are bound.
- **ParamIn/ParamOut:** “To be added when channel init/list/actions are implemented.”
- Keep one page; expand with new sections when channel usage grows.

**Verification:**
- Build succeeds; no linter errors.
- No remaining literals for channel resource or field names in `UbsOpCommissionFrm.cs`.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━  
📌 CREATIVE PHASE END

---

## CREATIVE PHASE VERIFICATION

- [x] Problem clearly defined  
- [x] Multiple options considered (4)  
- [x] Decision made with rationale  
- [x] Implementation guidance provided  
- [x] Document saved to `memory-bank/creative/`

**→ NEXT RECOMMENDED MODE: BUILD MODE** (implement Phase 1: Constants + channel contract)
