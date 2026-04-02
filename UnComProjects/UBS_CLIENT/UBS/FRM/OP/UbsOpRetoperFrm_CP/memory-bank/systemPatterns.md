# Memory Bank: System Patterns

## Architecture

- **Legacy → .NET conversion:** One primary plan for conversion goals (`plan-*-conversion-goals.md`), one for legacy↔target mapping (`plan-*-legacy-source-conversion.md`). Phases: Prep (constants, channel contract) → Conversion (UI + InitDoc + ListKey + Save) → Post (partials, reflection).
- **Form pattern:** UbsFormBase (or equivalent); IUbs (ListKey for ID/init); panelMain; bottom strip (Save, Exit, Info). TabControl for multi-page content; add-fields on second tab with named key in field collection.

## Conventions

- All user-facing (e.g. messagebox) strings in a **Constants partial** (e.g. `UbsOpBlankFrm.Constants.cs`). No magic strings in code-behind (except the channel commands and params).
- **Channel contract** documented (LoadResource, commands, in/out params) in a dedicated doc or section.
- **Progress** logged in `progress.md` with date and phase; **tasks** in `tasks.md` with checkboxes and links to plan/creative docs.

## Patterns in Use

- **Explicit data binding:** Replace VB6 DDX with LoadFromParams(param_out) and BuildSaveParams() for form↔channel param mapping.
- **ListKey for list-opened forms:** When form is opened from a list, ListKey delivers ID (or selection); InitDoc uses it for Get_Data. No ADD mode for Blank (ID required).
- **State combo by KindVal:** FillCombo logic depends on Идентификатор вида (KindVal) and current Состояние; state IDs 10–17; combo disabled when state = 17.
- **Creative before build:** Architecture and layout decisions in `creative/creative-*-conversion-architecture.md` and `creative-blank-ud-conversion.md`; reference Commission creative docs for consistency.
