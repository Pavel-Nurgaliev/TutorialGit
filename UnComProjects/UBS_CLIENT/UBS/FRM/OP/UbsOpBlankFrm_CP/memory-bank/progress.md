# Memory Bank: Progress

## Summary

Phase 1 and Phase 2 BUILD complete. Constants partial and channel contract in place; Blank_ud UI and logic (TabControl, main fields, add-fields, InitDoc, ListKey, btnSave, FillCombo) implemented in UbsOpBlankFrm. Linter clean. Build in VS/.NET 2.0 recommended to verify references (UbsParam, UbsChannel_*, UbsCtrlFields).

## BUILD Phase 1 & 2 (2026-03-16)

- **Phase 1:** UbsOpBlankFrm.Constants.cs (LoadResource, commands, params, messages); form uses LoadResource and FormTitle; csproj updated.
- **Phase 2:** TabControl + main tab (date, ser, num, name, kind, state combo) + add-fields tab; ListKey, InitDoc, LoadFromParams, FillCombo, btnSave (Blank_Save, MsgSaved); UbsCtrlFields reference and UbsCtrlFieldsSupportCollection.Add("Набор параметров", ubsCtrlAddFields).

## Creative: Constants & Channel (2026-03-16)

- **creative-blank-constants-channel.md** — Design options for Phase 1; **Option B** selected: Constants partial + channel contract doc. Guidelines: regions (Resource, Commands, Params, Messages), exact VB6 param/command names, no magic strings.
- **ubsopblankfrm-channel-contract.md** — LoadResource, Get_Data (in/out), Blank_Save (in), init flow. Single reference for BUILD and ASM alignment.

---

## Plan Implementation (2026-03-16)

### Completed

- **projectbrief.md** — Updated with conversion goal (Blank_ud → UbsOpBlankFrm), objectives, scope, success criteria.
- **techContext.md** — Created (stack, key files, legacy path, encoding).
- **productContext.md** — Created (product, users, constraints).
- **plan-blank-conversion-goals.md** — Main goal, legacy inventory, VB6→.NET mapping, phased roadmap (Phase 1–3).
- **plan-blank-legacy-source-conversion.md** — Legacy↔target roles, inventory table, next steps.
- **creative/creative-blank-conversion-architecture.md** — IUbs/ListKey mapping, control mapping, DDX replacement, LoadResource, keyboard notes.
- **creative/creative-blank-ud-conversion.md** — Layout (Option B), control replacements, data binding, channel, FillCombo logic, implementation guidelines.
- **systemPatterns.md** — Architecture, conventions, patterns (explicit binding, ListKey, state combo, creative-before-build).
- **tasks.md** — Current task, links to plan-* and creative-*, Phase 1–3 checklists, status.
- **progress.md** — This log.

### Next

- Phase 1: Constants partial, channel contract.
- Phase 2: UI and wiring (TabControl, fields, InitDoc, ListKey, btnSave, LoadResource, constants).
- Phase 3: Partials, reflection, systemPatterns update.

## Earlier

- **VAN initialization** — Memory Bank created; Blank_ud conversion task registered; complexity Level 3.
- **Creative (earlier)** — Design decisions for layout, DDX migration, channel, keyboard UX recorded and merged into creative-* docs above.
