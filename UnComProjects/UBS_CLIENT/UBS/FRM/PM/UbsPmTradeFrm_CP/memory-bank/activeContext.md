# Memory Bank: Active Context

## Current Phase

**PLAN Complete — Designer Revision (Screen-Based)**  
Date: 2026-03-24

Analyzed 7 legacy screenshots. Found significant differences vs. DOB-based v1 plan. Full correction plan written as `plan-trade-designer-revision.md`. Designer.cs needs to be rebuilt from scratch following the revised plan.

## Current Focus

**BUILD (Designer v2)** — recreate `UbsPmTradeFrm.Designer.cs` following `plan-trade-designer-revision.md`.  
The previous `Designer.cs` was lost (Cursor workspace sync issue), so a full rewrite is required.

## What Was Just Done

- Read all 7 legacy screen PNGs (`1.png`–`7.png` from `legacy-form/screens/`).
- Found and documented all discrepancies between DOB-based plan and actual form appearance.
- Created `plan-trade-designer-revision.md` with:
  - Corrected tab names: Основные | Обязательства | Данные | Поставка | Оплата | Дополнительные
  - Tab 4/5 content SWAP: Поставка = storage/delivery; Оплата = payment instruction
  - Per-tab pixel-level coordinates for all control changes
  - 14 new Label controls (for Оплата sub-tab labels + Tab 3 accounts)
  - Bottom strip simplified from 5 cols → 3 cols
  - `cmdExitOblig.Text = "Отмена"` (was "Выход")
  - Tab 5 sub-tab order: Покупатель first, Продавец second
  - 8 ordered build steps

## Key Decisions Made

- **ucpParam** → `UbsControl.UbsCtrlFields` ✅ confirmed
- **Control arrays** → named pairs (`*_0` / `*_1`) + helper arrays in code-behind ✅
- **Tab 5 sub-tabs** → index 0 = Покупатель, index 1 = Продавец (corrected from DOB plan)
- **Комиссия** → Visible=**true** (shown in screen 1)
- **chkCash** → Visible=**true** (shown in screens 5/6)
- **lblAccounts/cmdAccounts** → inside tabPage3 as `lblAccountsOblig`/`cmdAccountsOblig`, NOT bottom strip
- **Form size** → `ClientSize = new Size(470, 530)` (matches VB6 ~451px width)

## Immediate Next Actions

1. **BUILD (Designer v2):** Rewrite `UbsPmTradeFrm.Designer.cs` from scratch following `plan-trade-designer-revision.md`
2. Verify build: 0 errors, 0 warnings
3. (Later) CREATIVE phase: sub-form strategy, obligations model, tab-disable mechanism
4. (Later) Phase 2 logic: InitDoc, ListKey, FillCombos, Save, event handlers

## Open Questions / Risks

- **Sub-form strategy:** Contract lookup, instruction picker, account picker, object picker — modal dialog vs inline. Decision needed in CREATIVE.
- **`EnableWindow` equivalent:** VB6 disables entire panels with API. .NET: `panel.Enabled = false`. Confirm scope.
- **Rate calculation precision:** `ucdRateCurOblig.Value` — confirm `UbsCtrlDecimal` supports 10-decimal precision.
- **`UbsCtrlFieldsSupportCollection`:** Should `base.UbsCtrlFieldsSupportCollection.Add("Параметры", ucpParam)` be called in constructor? (Yes — per UbsOpBlankFrm pattern.)
- **`lblCheckInstr` label text:** Screen shows "Выбор платежной инструкции по покупателю/продавцу" — this is a LABEL, not the CheckBox text. The current approach (label to right of cmdListInstr button) is correct per screen 5/6.
