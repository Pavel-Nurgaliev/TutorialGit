# Task Reflection: Blank_ud → UbsOpBlankFrm Conversion

**Feature ID:** blank-conversion  
**Date of Reflection:** 2026-03-17  
**Complexity Level:** 3

---

## 1. Brief Feature Summary

Converted the VB6 UserDocument `Blank_ud` (Принятая ценность) to a .NET Windows Form `UbsOpBlankFrm`. Delivered:

- **Phase 1:** Constants partial (`UbsOpBlankFrm.Constants.cs`) with LoadResource, combo state labels, messages, FormTitle (no channel command/param constants); channel contract doc (`ubsopblankfrm-channel-contract.md`).
- **Phase 2:** Full UI (TabControl with main tab and add-fields tab), main fields (date, series, number, name, kind, state combo), InitDoc (Get_Data → LoadFromParams, FillCombo), ListKey (ID from param, InitDoc), btnSave (ParamIn Id/State, Blank_Save, MsgSaved), add-fields control registered in `UbsCtrlFieldsSupportCollection`. Channel commands and parameters are used as **explicit string values** at call sites (e.g. `"Идентификатор"`, `"Состояние"`, `"Blank_Save"`, `"Get_Data"`); Constants hold only resource path, combo text, messages, form title.

Phase 3 (split partials when form grows; update systemPatterns) is deferred.

---

## 2. Overall Outcome & Requirements Alignment

- **Requirements met:** Plan and creative docs were followed. UI matches legacy (tabs, fields, state combo, add-fields, Save/Exit/Info). Channel usage matches contract (Get_Data, Blank_Save; param names in Russian). Channel command and parameter names are **explicit string values** in code (not in Constants); Constants partial holds only LoadResource, combo labels, messages, FormTitle. Channel contract doc remains the single source of truth for channel semantics.
- **Deviations:** Tab label for add-fields is "Доп. поля" in code vs "Набор параметров" in creative; functionally equivalent, naming can be aligned later if needed.
- **Assessment:** Feature is successful for Phase 1 and Phase 2 scope. Build/linter clean; runtime verification (VS/.NET 2.0, UbsParam/UbsChannel_*/UbsCtrlFields) recommended.

---

## 3. Planning Phase Review

- **Effectiveness:** `plan-blank-conversion-goals.md` and `plan-blank-legacy-source-conversion.md` gave a clear legacy inventory, VB6→.NET mapping, and phased roadmap. Phase 1 (prep) and Phase 2 (main conversion) were accurately scoped.
- **Accuracy:** Component breakdown (Constants, channel contract, TabControl, fields, InitDoc, ListKey, btnSave, FillCombo) matched implementation. No material scope creep.
- **Could improve:** Explicit checklist for "keyboard behavior" (Enter/Esc) was in plan but not implemented in Phase 2; could be called out in Phase 3 or a follow-up task.

---

## 4. Creative Phase(s) Review

- **Right aspects in CREATIVE:** Layout (Option B — TabControl + layout containers), DDX→explicit binding, channel (Get_Data/Blank_Save, param names as explicit values), FillCombo logic (state 17 vs KindVal 3/4), and Constants (resource, UI/messages only; not command/param names) + channel contract (Option B) were all appropriate for design-first decisions.
- **Design → implementation:** Creative decisions translated well. LoadFromParams/BuildSaveParams (via ParamIn + Run), KeyValuePair<int,string> for combo, InitDoc flow, and Constants regions are implemented as designed. One small naming variance: tab key "Доп. поля" vs doc "Набор параметров".
- **Style/guidelines:** No project style-guide.md was required for this task; creative docs and channel contract were sufficient.

---

## 5. Implementation Phase Review

- **Successes:** Constants partial and channel contract done first, which kept BUILD free of magic strings. ListKey → InitDoc and FillCombo (state 17, KindVal 3/4, common states) implemented without rework. Regions and partial structure kept the form readable.
- **Challenges:** Ensuring param keys and command names exactly matched VB6/contract; addressed by using explicit string values at call sites and the channel contract doc for reference. Add-fields integration relied on existing `UbsCtrlFieldsSupportCollection` pattern (e.g. Commission).
- **Adherence:** Coding followed existing base (UbsFormBase, IUbs, UbsChannel_*), Russian param/command names as explicit literals where used; Constants partial only for resource path, combo text, messages, form title (not for channel commands/params).

---

## 6. Testing Phase Review

- **Strategy:** Linter clean; no automated tests in repo for this form. Manual build in VS with .NET 2.0 and real UbsParam/UbsChannel_*/UbsCtrlFields is the recommended verification.
- **Improvement:** For similar features, a short manual test checklist (ListKey with ID, InitDoc load, state combo, Save, add-fields refresh) could be added to progress or tasks.

---

## 7. What Went Well?

1. **Phased plan + creative first:** Phase 1 (Constants + channel contract) and creative docs (layout, binding, channel, FillCombo) made Phase 2 BUILD straightforward and consistent.
2. **Constants partial + channel contract:** Constants for resource path, combo labels, messages, form title; channel command/param names as **explicit values** at call sites (no constants for those). Channel contract doc for semantics; explicit values keep call sites readable and avoid extra indirection.
3. **Explicit binding and ListKey pattern:** LoadFromParams/ParamIn + Run matched creative design and kept logic clear.
4. **FillCombo logic:** State 17 (disabled), KindVal 3/4 branching, and common states implemented as specified without backtracking.
5. **Memory Bank usage:** tasks.md, progress.md, plan-*, and creative-* provided clear traceability from goal to implementation.

---

## 8. What Could Have Been Done Differently?

1. **Tab key naming:** Align code ("Доп. поля") with creative ("Набор параметров") during BUILD to avoid minor doc/code drift.
2. **Keyboard behavior:** Plan mentioned Enter/Esc; either implement in Phase 2 or explicitly move to Phase 3/follow-up in tasks.
3. **Build verification:** Add one-line note in tasks or progress: "Verify build in VS with UbsParam, UbsChannel_*, UbsCtrlFields" to make post-BUILD check explicit.
4. **Add-fields stub:** Document in channel contract or creative whether add-fields params are passed in Blank_Save in this phase or left for later.

---

## 9. Key Lessons Learned

- **Technical:** Constants partial for **resource path, UI/combo text, and messages only**; **do not** put channel command and parameter names in constants — use **explicit string values** at the call site (e.g. `UbsChannel_ParamIn("Идентификатор", m_id)`, `UbsChannel_Run("Blank_Save")`). Channel contract doc stays the single source of truth for semantics; constants only for user-facing and resource strings.
- **Process:** Level 3 flow (VAN → PLAN → CREATIVE → BUILD) fit this conversion: creative locked layout, binding, and channel before coding; BUILD stayed within design.
- **Estimation:** Phase 1 + Phase 2 scope was accurate; Phase 3 (partials, reflection, systemPatterns) correctly left for after core conversion.

---

## 10. Actionable Improvements for Future L3 Features

1. **Naming consistency:** When creative docs name a UI/key (e.g. tab key), add it to tasks as a checklist item so BUILD matches exactly.
2. **Optional behavior in plan:** If plan mentions behavior (e.g. keyboard) not in Phase 2 scope, add an explicit "Phase 3 / follow-up" line so reflection doesn’t flag it as missing.
3. **Post-BUILD verification:** In tasks or progress, include a one-sentence "Verify: build + [critical refs]" so the handoff to QA or next phase is clear.
4. **Channel contract completeness:** For forms with add-fields, document in the channel contract whether and how add-fields params are sent (e.g. Blank_Save) so future ASM changes are obvious.
5. **Constants vs explicit values:** Keep channel command and parameter names as **explicit string values** in code; reserve Constants partial for LoadResource, combo/UI text, messages, FormTitle. Avoid constants for param/command names to keep call sites clear and avoid unnecessary indirection.

---

## Next Steps

- Proceed to **ARCHIVE** mode to finalize task documentation.
- Phase 3: split partials when form grows; update systemPatterns if needed; optionally align tab key name and add keyboard behavior.
