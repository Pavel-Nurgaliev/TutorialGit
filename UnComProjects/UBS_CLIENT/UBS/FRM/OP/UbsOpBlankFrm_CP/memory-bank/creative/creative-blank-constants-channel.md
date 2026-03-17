# 🎨 CREATIVE PHASE: UbsOpBlankFrm — Constants & Channel Contract

**Scope:** Design decisions for constants structure and channel contract documentation for Blank_ud conversion.  
**Context:** WinForms, UBS channel (ASM); align with UbsOpCommissionFrm pattern (Constants partial + channel contract doc).

---

## 🎨🎨🎨 ENTERING CREATIVE PHASE: Architecture (Constants & Channel Contract)

### 1️⃣ PROBLEM

**Description:** Phase 1 calls for "Constants partial, channel contract doc." We need a consistent approach for (1) where and how to define constants for Blank (param names, command names, LoadResource, messages), and (2) what to document as the channel contract so BUILD and future maintenance stay aligned with the legacy VB6 contract and with other UBS forms (e.g. Commission).

**Requirements:**
- No magic strings in form logic; all channel/UI literals in named constants
- Param and command names must match VB6 exactly (Идентификатор, Состояние, Get_Data, Blank_Save, etc.)
- One place documenting channel: LoadResource, commands, ParamIn/ParamOut
- Scalable when more params or messages are added

**Constraints:**
- Must match legacy Blank_ud.dob channel usage (Get_Data, Blank_Save; param keys in Russian)
- .NET resource path = ASM equivalent of `VBS:UBS_VBD\OP\Blank.vbs`
- Same namespace/partial class as main form

---

### 2️⃣ OPTIONS

**Option A: Inline constants in main form**  
Add `#region Constants` and const strings inside `UbsOpBlankFrm.cs`. No separate file; no channel contract doc until later.

**Option B: Constants partial + channel contract now (recommended)**  
Create `UbsOpBlankFrm.Constants.cs` with regions (Resource, Commands, Param names, Messages). Create `ubsopblankfrm-channel-contract.md` (in memory-bank/creative or memory-bank/) documenting LoadResource, Get_Data, Blank_Save, and param in/out. Mirror Commission’s Option C.

**Option C: Full placeholder set**  
Constants partial with every possible param/command predeclared; channel contract doc fully filled with placeholders. High upfront cost, risk of drift.

**Option D: Channel contract doc only**  
Document channel in a single doc; keep string literals in code until Phase 2 BUILD replaces them with constants in one pass.

---

### 3️⃣ ANALYSIS

| Criterion              | Option A | Option B | Option C | Option D |
|-----------------------|----------|----------|----------|----------|
| Consistency with Commission | ⭐⭐   | ⭐⭐⭐⭐⭐  | ⭐⭐⭐⭐   | ⭐⭐      |
| No magic strings (after Phase 2) | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ | ⭐⭐      |
| Single source of truth for channel | ⭐ | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐  | ⭐⭐⭐⭐   |
| Implementation cost   | ⭐⭐⭐⭐   | ⭐⭐⭐⭐   | ⭐⭐      | ⭐⭐⭐     |

**Insights:** Option B gives the same pattern as Commission (Constants partial + channel contract doc), clear naming for BUILD, and one place to look up param/command names. Option A/D leave magic strings or defer structure; Option C adds unused clutter.

---

### 4️⃣ DECISION

**Selected:** **Option B — Constants partial + channel contract now.**

**Rationale:**
- **Constants:** Add `UbsOpBlankFrm.Constants.cs` (partial class) with regions, e.g.:
  - **Ресурс канала:** `LoadResource` (ASM path for OP Blank)
  - **Команды канала:** `GetDataCommand`, `BlankSaveCommand`
  - **Параметры:** `ParamId`, `ParamDate`, `ParamNameVal`, `ParamKindVal`, `ParamKindId`, `ParamSer`, `ParamNum`, `ParamState` (Russian values as in VB6)
  - **Сообщения:** `MsgSaved`, `MsgEmptyList`, etc.
- **Channel contract:** Create `ubsopblankfrm-channel-contract.md` with LoadResource, Get_Data (in: Идентификатор; out: Дата учета, Наименование ценности, Вид ценности, Идентификатор вида, Серия, Номер, Состояние), Blank_Save (in: Идентификатор, Состояние; add-fields via stub). Update when contract changes.

---

### 5️⃣ IMPLEMENTATION GUIDELINES

**Constants file (`UbsOpBlankFrm.Constants.cs`):**
- Same namespace and `partial class UbsOpBlankFrm`.
- Use Russian region names if the rest of the form does; otherwise English.
- Define only constants that will be used in Phase 2 (no speculative placeholders).
- Param constants = exact VB6 strings: `"Идентификатор"`, `"Дата учета"`, `"Наименование ценности"`, `"Вид ценности"`, `"Идентификатор вида"`, `"Серия"`, `"Номер"`, `"Состояние"`.
- Command constants: `"Get_Data"`, `"Blank_Save"`.
- Message constants: e.g. `"Данные сохранены!"`, `"Список принятых ценностей пуст!"` (for error/title as in VB6).

**Channel contract doc (`memory-bank/creative/ubsopblankfrm-channel-contract.md`):**
- **Resource:** LoadResource value (ASM equivalent of `VBS:UBS_VBD\OP\Blank.vbs`).
- **Get_Data:** ParamIn (Идентификатор); ParamOut (Дата учета, Наименование ценности, Вид ценности, Идентификатор вида, Серия, Номер, Состояние).
- **Blank_Save:** ParamIn (Идентификатор, Состояние; add-fields via stub).
- **Init:** ListKey delivers ID; form calls Get_Data in InitDoc.

**Verification:**
- After BUILD Phase 1: no literals for resource, commands, or param names in main form; build and linter clean.
- Channel contract doc is the single reference for backend/ASM alignment.

---

## 🎨🎨🎨 EXITING CREATIVE PHASE

**Design decision:** Constants partial + channel contract doc (Option B). Use param/command names from legacy Blank_ud.dob; document in channel contract for BUILD and future reference.
