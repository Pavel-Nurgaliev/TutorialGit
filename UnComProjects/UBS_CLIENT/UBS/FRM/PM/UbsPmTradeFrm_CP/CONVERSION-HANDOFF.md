# UbsPmTradeFrm — conversion handoff (attach this for future sessions)

**Purpose:** One file to load into context instead of full memory-bank + plans. Reduces tokens and avoids repeating known mistakes (DOB vs screenshots, tab swap, naming).

**Project root (this folder):** `UnComProjects/UBS_CLIENT/UBS/FRM/PM/UbsPmTradeFrm_CP`

---

## 1. What we are building

- **Legacy:** VB6 UserDocument `legacy-form/Pm_Trade/Pm_Trade_ud.dob` (~5.3k lines); helper `legacy-form/Pm_Trade/frmInstr.frm`.
- **Target:** .NET Framework 2.0 WinForms `UbsPmTradeFrm` — PM trade form **Сделка**, UBS channel integration.
- **Screens (ground truth for UI):** `legacy-form/screens/1.png` … `7.png`.

---

## 2. Canonical paths

| Role | Path |
|------|------|
| Form code | `UbsPmTradeFrm/UbsPmTradeFrm.cs` |
| Designer | `UbsPmTradeFrm/UbsPmTradeFrm.Designer.cs` |
| Project | `UbsPmTradeFrm/UbsPmTradeFrm.csproj` |
| Reference OP conversion | `../OP/UbsOpRetoperFrm_CP` (designer + channel patterns) |

---

## 3. CRITICAL: do not trust DOB alone for tabs 4–5

`plan-trade-designer-conversion.md` was **first pass from DOB only** and was **wrong** on tab titles and **swapped** tab 4 vs tab 5 content.

**Authoritative tab captions (from screenshots):**

| # | Text |
|---|------|
| 1 | Основные |
| 2 | Обязательства |
| 3 | Данные |
| 4 | Поставка |
| 5 | Оплата |
| 6 | Дополнительные |

**Content swap vs naive DOB read:**

- **Tab 4 (Поставка):** storage / delivery style content (what older plans put on “хранилище”).
- **Tab 5 (Оплата):** payment instruction UI (what older plans put on “инструкция”).

**Designer source of truth:** `memory-bank/plan-trade-designer-revision.md` (pixel/layout detail).  
**Inventory / steps (with DOB line refs):** `memory-bank/plan-trade-designer-conversion.md` — use for control lists, verify against revision + PNGs.

---

## 4. Confirmed control / product decisions

- **ucpParam (UbsControlProperty):** use **`UbsControl.UbsCtrlFields`** (namespace `UbsControl`).
- **Control arrays:** named pairs `*_0` / `*_1` + helper arrays in code-behind.
- **Tab 5 nested tabs:** index **0 = Покупатель**, **1 = Продавец**.
- **Комиссия** (`lblCommission` / `cmbComission`): **Visible = true** (shown on screen).
- **chkCash:** **Visible = true**.
- **Accounts:** `lblAccounts` / `cmdAccounts` live on **tab 3** as e.g. `lblAccountsOblig` / `cmdAccountsOblig`, not only bottom strip.
- **Bottom strip:** simplified layout (3 columns vs 5) per revision plan.
- **cmdExitOblig.Text:** **"Отмена"** (not "Выход").
- **Form ClientSize:** ~`470×530` to match legacy width (~451px).
- **`lblCheckInstr`:** long text is a **Label** beside instruction UI, not CheckBox text.

---

## 5. Stack and base class

- .NET **2.0**, C#, **WinForms**, base **`UbsFormBase`**.
- Channel: **`IUbsChannel`** (`this.IUbsChannel`), not OCX.
- **DDX** in VB6 → **`LoadFromParams` / `BuildSaveParams`** (explicit mapping).

---

## 6. Channel commands (short reference)

Document full In/Out keys in `memory-bank/techContext.md` and (when created) `memory-bank/creative/ubspmatradefrm-channel-contract.md`.

| Command | Role |
|---------|------|
| `GetOneTrade` | EDIT load (`ID_TRADE` in → fields + obligations + instr out) |
| `ModifyTrade` | Save / create |
| `TradeCombo_FillPM` | Init combos (`ID_PATTERN=1`) |
| `FillOurBIK` | Own BIK |
| `PMCheckOperationByTrade` | `Was_Operation` → lock form |
| `FillBaseCurrency` | ADD default currency |
| `GetContractPM` | Contract by `ID_CONTRACT` |
| `TradeFillInstr` | Instructions by contract |
| `GetInstructionOplataCash` | Cash instruction |
| `GetObjectPM` | Object / metal |
| `GetStorage` | Storage |
| `GetRate_CB` / `GetRateForPM` | Rates |
| `TradeCheckKey` / `TradeCheckINN` | Validation |
| `FillRekv` | Bank by BIK |
| `DefineCodCurrency` | ISO code for checks |

**LoadResource:** VB6 `"VBS:UBS_VBD\PM\Pm_Trade.vbs"` → .NET ASM string TBD (follow OP pattern).

**Coding rule:** channel **command names and param keys** stay **explicit string literals** in code; user-facing UI strings go in **`UbsPmTradeFrm.Constants.cs`** partial.

---

## 7. VB6 OCX → .NET (summary)

| VB6 | .NET |
|-----|------|
| SSActiveTabs | `TabControl` |
| UbsControlMoney | `UbsCtrlDecimal` |
| UbsControlDate | `UbsCtrlDate` (or `DateTimePicker`) |
| UbsControlProperty / ucpParam | `UbsCtrlFields` / AddFields support |
| UbsInfo | `UbsCtrlInfo` |
| MSCOMCTL ListView | `System.Windows.Forms.ListView` |

**References to add if missing:** `UbsCtrlDecimal`, `UbsCtrlDate`, `UbsCtrlAddFields` (see `techContext.md` HintPaths).

---

## 8. Init entry (legacy parity)

- **`ListKey` / `CommandLine`:** `RSIdent(0) = ID_TRADE`, `strRunParam` = `"EDIT"` or `"ADD#<vidTrade>"`.
- **InitDoc order (high level):** FillCombos → FillOurBIK → EDIT: GetOneTrade + LoadFromParams + obligations list; ADD: defaults → enable/disable by mode → if EDIT and `Was_Operation`, lock UI.

---

## 9. Phased work (do in order)

1. **Prep:** `Constants.cs`, channel contract doc, csproj references, LoadResource ASM string.
2. **Designer:** match **`plan-trade-designer-revision.md`** + PNGs (not raw DOB layout for tabs 4–5).
3. **Logic:** InitDoc, validation, Save (`ModifyTrade`), obligation editor state machine, sub-forms (CREATIVE).

---

## 10. CREATIVE still open

- Sub-forms: contract / instruction / account / object pickers (modal vs inline).
- Obligations model: `DataTable` vs `BindingList` vs list items.
- Tab-disable: `Panel.Enabled` vs broader patterns.
- `UbsCtrlFieldsSupportCollection.Add("Параметры", ucpParam)` in ctor — follow **UbsOpBlankFrm**-style pattern.

---

## 11. Designer hygiene (avoid repeat mistakes)

- **TabIndex:** unique **per parent container**; do not reuse form-wide sequence inside `GroupBox` children; set **TabStop = false** on read-only/display labels/fields as appropriate; watch **duplicate index 0** on sibling controls (`chkCash`, links, etc.). Audit pass documented in `memory-bank/plan-tabindex-order.md` / `progress.md` (2026-03-24).

---

## 12. Deeper detail (load only when needed)

| Topic | File |
|-------|------|
| Tasks / checklists | `memory-bank/tasks.md` |
| Channel + mapping tables | `memory-bank/techContext.md` |
| InitDoc / Save / state machine | `memory-bank/systemPatterns.md` |
| Session state / last focus | `memory-bank/activeContext.md` |
| Designer revision (screens) | `memory-bank/plan-trade-designer-revision.md` |
| Designer inventory (DOB) | `memory-bank/plan-trade-designer-conversion.md` |

---

*Last consolidated: 2026-03-24. Update this file when authoritative decisions change so one attachment stays enough.*
