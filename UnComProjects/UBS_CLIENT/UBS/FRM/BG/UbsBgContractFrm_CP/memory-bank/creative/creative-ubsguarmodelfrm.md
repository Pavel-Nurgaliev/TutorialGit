# 🎨 CREATIVE PHASE: UbsGuarModelFrm (Guarantee Model Form)

**Scope:** Design decisions for the Guarantee Model form — structure, validation, UX, and maintainability.  
**Context:** WinForms, .NET 2.0, UBS channel (VBS/ASM), IUbs command pattern.

---

## 📌 CREATIVE PHASE START: UbsGuarModelFrm

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

### 1️⃣ PROBLEM

**Description:** The form handles CRUD for guarantee models (name, model, executor, state, client type) with channel calls, combo init, rights checks, and dynamic add-fields. Design choices affect readability, testability, and consistency with other UBS forms.

**Requirements:**
- Clear separation between UI, validation, and channel logic
- Consistent error messaging and user feedback
- Maintainable init/load flow (models, executors, states, client types)
- Safe handling of Cyrillic resource keys and legacy channel contracts

**Constraints:**
- Must remain compatible with `UbsFormBase`, IUbs, and existing VBS channel scripts
- No breaking changes to `CommandLine` / `ListKey` signatures or channel parameter names
- .NET 2.0 and Windows Forms only

---

### 2️⃣ OPTIONS

**Option A: Extract service facade (thin form)**  
Form delegates all channel calls and data shaping to a dedicated “GuarModelService” or helper class; form only binds UI and handles events.

**Option B: Keep current structure, add validation layer**  
Leave channel usage in the form but extract validation (Check, IsAddFieldFilled, rights) into a small validator class and centralize message keys.

**Option C: Minimal refactor + constants/documentation**  
Keep structure as-is; introduce named constants for all magic strings (channel params, messages), fix encoding for comments, and add a one-page “form contract” doc (channel in/out, commands).

**Option D: Presenter / passive view**  
Introduce a presenter that owns command and init logic; form becomes a passive view (setters/getters, no channel calls). Requires more refactor and may diverge from other UBS forms.

---

### 3️⃣ ANALYSIS

| Criterion           | Option A (Facade) | Option B (Validator) | Option C (Constants) | Option D (Presenter) |
|---------------------|-------------------|------------------------|------------------------|----------------------|
| Consistency with UBS | ⭐⭐⭐              | ⭐⭐⭐⭐                 | ⭐⭐⭐⭐⭐                | ⭐⭐                  |
| Testability         | ⭐⭐⭐⭐            | ⭐⭐⭐                  | ⭐⭐                    | ⭐⭐⭐⭐⭐              |
| Risk / regression   | ⭐⭐⭐              | ⭐⭐⭐⭐                 | ⭐⭐⭐⭐⭐                | ⭐⭐                  |
| Implementation cost | ⭐⭐⭐              | ⭐⭐⭐⭐                 | ⭐⭐⭐⭐⭐                | ⭐⭐                  |
| Maintainability     | ⭐⭐⭐⭐            | ⭐⭐⭐⭐                 | ⭐⭐⭐                   | ⭐⭐⭐⭐               |

**Insights:**
- Option C is lowest risk and aligns with “do not break channel contract”; good first step.
- Option B improves clarity without moving channel usage out of the form; pairs well with C.
- Option A gives a clear boundary for future tests and reuse without a full MVP refactor.
- Option D is powerful but inconsistent with typical UBS form style and higher cost.

---

### 4️⃣ DECISION

**Selected:** **Option C first, then Option B** (incremental).

**Rationale:**
- **Phase 1 (Option C):** Replace magic strings with constants, fix comment encoding, document channel contract (params in/out, commands). No behavior change, minimal risk.
- **Phase 2 (Option B):** Extract validation (Check, IsAddFieldFilled, and optionally rights-result interpretation) into a `UbsGuarModelFrmValidator` (or similar) used by the form. Centralize message keys there or in a small resource/constants class. Form remains the single place that talks to the channel, so UBS patterns stay intact while validation becomes easier to test and reuse.

Defer Option A (facade) until a second form is refactored the same way to avoid one-off architecture. Defer Option D unless the product adopts presenter/MVP for all forms.

---

### 5️⃣ IMPLEMENTATION NOTES

**Phase 1 – Constants and documentation**
- Add static class or const region for: channel param names (e.g. `StrCommand`, `Наименование`, `Модель`, `Код`, `Состояние`, `Тип клиента`), command names (`GuarModelEdit`, `GuarModelInit`, etc.), and user-facing message keys.
- Replace all literal channel and message strings in `UbsGuarModelFrm.cs` with these constants.
- Add `memory-bank/creative/ubsguarmodelfrm-channel-contract.md` (or section in techContext): list commands, ParamIn/ParamOut per command, and which controls map to which params.

**Phase 2 – Validation layer**
- Create a validator that takes form-provided data (name, selected model, executor, state, client type, add-fields snapshot) and returns result + message key or message text.
- Form calls validator in `btnSave_Click` and before other submit paths; keeps all `UbsChannel_*` and `IUbsChannel.*` calls in the form.
- Keep `CheckRights()` in form (or move to a small “rights” helper) since it uses channel; validator can consume the result (e.g. “read-only” vs “edit”) for message choice.
- Preserve existing behavior: same messages, same order of checks, same focus/visibility logic.

**UX / messaging**
- Keep `ubsCtrlInfo` for success; keep `MessageBox` for errors until a product-wide UX decision is made.
- Consider one constant per user-visible string (including DataCheckingMessage, NameOperation, etc.) to simplify future localization.

**Verification**
- After Phase 1: build, run form (Add/Edit), confirm all channel calls still work (VBS/ASM unchanged).
- After Phase 2: same manual test; consider a small unit test for validator with stub data (no channel).

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━  
📌 CREATIVE PHASE END

---

## CREATIVE PHASE VERIFICATION

- [x] Problem clearly defined  
- [x] Multiple options considered (4)  
- [x] Decision made with rationale  
- [x] Implementation guidance provided (phased)  
- [x] Document saved to `memory-bank/creative/`

**→ NEXT RECOMMENDED MODE: BUILD MODE** (when implementing Phase 1 or 2)
