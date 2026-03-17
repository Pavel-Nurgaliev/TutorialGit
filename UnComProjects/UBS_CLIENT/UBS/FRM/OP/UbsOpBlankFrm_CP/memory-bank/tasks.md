# Memory Bank: Tasks

## Current Task

**Main goal:** Convert Blank_ud (VB6 UserDocument) → UbsOpBlankFrm (.NET WinForm)

**Plans:**
- `memory-bank/plan-blank-conversion-goals.md` — main goal, conversion roadmap
- `memory-bank/plan-blank-legacy-source-conversion.md` — legacy source (Blank_ud.dob) → target (UbsOpBlankFrm)

**Phase 1 (Prep):**
- [x] Constants partial (UbsOpBlankFrm.Constants.cs), channel contract doc (ubsopblankfrm-channel-contract.md)

**Creative phase:** ✅ Complete
- **Docs:** `memory-bank/creative/creative-blank-conversion-architecture.md`, `memory-bank/creative/creative-blank-ud-conversion.md` — IUbs/ListKey, control mapping, DDX→explicit binding, FillCombo, layout
- **Constants & channel:** `memory-bank/creative/creative-blank-constants-channel.md` — Option B: Constants partial + channel contract now; `memory-bank/creative/ubsopblankfrm-channel-contract.md` — LoadResource, Get_Data, Blank_Save, param in/out

**Phase 2 (Main goal — Conversion):**
- [x] Implement UI: TabControl, txbDateCalc, txbSer, txbNum, txbNameVal, txbKindVal, cmbState, add-fields, btnSave, btnExit, info
- [x] Implement InitDoc (Get_Data, LoadFromParams, FillCombo)
- [x] Implement ListKey (pass ID; call InitDoc)
- [x] Implement btnSave (ParamIn Id/State, Blank_Save, show MsgSaved)
- [x] LoadResource and constants used (VBS path, combo text, messages); channel commands/params as explicit string values in code

**Phase 3 (Post-conversion):**
- [ ] Split partials when form grows
- [x] Reflection doc (`memory-bank/reflection/reflection-blank-conversion.md`)
- [ ] Update systemPatterns if needed
- [ ] Archiving

## Status

- [x] Initialization complete
- [x] Planning complete
- [x] Creative phases complete
- [x] Implementation complete (Phase 1 & 2)
- [x] Reflection complete
- [ ] Archiving

**Reflection highlights:** See `memory-bank/reflection/reflection-blank-conversion.md`. What went well: phased plan + creative first, Constants partial (resource/UI/messages only) + channel contract, channel commands/params as **explicit string values** at call sites, explicit binding/ListKey, FillCombo logic. Lessons: do not put channel command/param names in Constants — use explicit values; align UI/key names with creative; document optional behavior (e.g. keyboard) as Phase 3/follow-up; add brief verify step in tasks. Next: ARCHIVE mode; Phase 3 partials, systemPatterns, optional tab key alignment and keyboard behavior.

- **Memory-bank structure** — Implemented per `plan-future-projects-using-memory-bank.md` (projectbrief, techContext, productContext, plan-*, creative-*, systemPatterns, tasks, progress). Ready for ARCHIVE.
