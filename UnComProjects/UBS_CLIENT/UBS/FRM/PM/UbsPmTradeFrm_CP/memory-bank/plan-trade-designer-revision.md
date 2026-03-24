# PLAN: UbsPmTradeFrm — Designer Revision (Screen-Based)

**Date:** 2026-03-24  
**Scope:** Correct `UbsPmTradeFrm.Designer.cs` to match actual legacy form screenshots  
**Reference screens:** `legacy-form/screens/1.png` … `7.png`  
**Prior plan:** `plan-trade-designer-conversion.md` (first-pass, from DOB source only — had errors)

---

## 1. MASTER DIFF: SCREENS vs. CURRENT DESIGNER

### 1.1 Tab Names — ALL WRONG in prior plan

| Tab # | Old (wrong) | Correct (from screens) | Screen |
|-------|-------------|------------------------|--------|
| 1 | "Основные данные" | **"Основные"** | 1.png |
| 2 | "Обязательства" | "Обязательства" ✓ | 2.png |
| 3 | "Детали" | **"Данные"** | 3_1.png |
| 4 | "Инструкция оплаты" | **"Поставка"** | 4.png |
| 5 | "Хранилище" | **"Оплата"** | 5.png/6.png |
| 6 | "Параметры" | **"Дополнительные"** | 7.png |

### 1.2 Tab Contents — Tabs 4 and 5 are SWAPPED

| Position | Old content | Correct content |
|----------|-------------|-----------------|
| tabPage4 | Payment instruction (tabControlInstr) | **Storage/delivery (was tabPage5)** |
| tabPage5 | Storage controls | **Payment instruction (was tabPage4)** |

---

## 2. TAB-BY-TAB CORRECTIONS

---

### 2.1 Tab 1 — "Основные" (screen: `1.png`)

#### GroupBox "Сделка" (grpTrade) — layout changes

**Old layout (3 rows):**
```
Row 1: Дата [date] | Номер [text] | □ Составная?
Row 2: Поставка [combo]
Row 3: Тип [combo] ← hidden
```

**New layout (2 rows, Тип visible):**
```
Row 1: Дата [date] | Номер [text] | □ Составная?
Row 2: Поставка [combo] | Тип [combo]   ← same row, both visible
```

**Changes:**
- Remove separate hidden row for `lblTradeType` / `cmbTradeType`
- Place `lblTradeType` + `cmbTradeType` on same row as `lblDelivery` + `cmbKindSupplyTrade`
- `cmbTradeType.Visible = true` (shown in screenshot)
- GroupBox height: ~72px (was 98px)

**Approximate positions inside grpTrade (Width=680):**
| Control | X | Y | W | H |
|---------|---|---|---|---|
| lblDate | 8 | 24 | 30 | 15 |
| txtTradeDate | 44 | 20 | 110 | 21 |
| lblNum | 165 | 24 | 50 | 15 |
| txtTradeNum | 218 | 20 | 90 | 20 |
| chkIsComposit | 320 | 21 | 100 | 17 |
| lblDelivery | 8 | 50 | 55 | 15 |
| cmbKindSupplyTrade | 68 | 46 | 150 | 21 |
| lblTradeType | 230 | 50 | 28 | 15 |
| cmbTradeType | 264 | 46 | 150 | 21 |

#### GroupBox "Стороны сделки" (grpContracts) — layout change

**Old layout (each contract on 1 wide row):**
```
Row: Продавец [combo] [...] [code] [name—long]
Row: Покупатель [combo] [...] [code] [name—long]
```

**New layout (each contract on 2 rows):**
```
Row A1: Продавец  [combo] [...]
Row A2:           [code]  [name—long]
Row B1: Покупатель [combo] [...]
Row B2:            [code]  [name—long]
Row C:  □ НДС  □ Экспортная
```

GroupBox height: ~115px (was 90px)

**Approximate positions inside grpContracts (Width=680):**
| Control | X | Y | W | H | Notes |
|---------|---|---|---|---|-------|
| lblSeller | 8 | 18 | 70 | 15 | |
| cmbContractType1 | 85 | 14 | 95 | 21 | |
| cmdContract1 | 185 | 14 | 26 | 21 | Text="..." |
| txtContractCode1 | 8 | 40 | 80 | 20 | ReadOnly |
| txtClientName1 | 94 | 40 | 575 | 20 | ReadOnly, Anchor=TLR |
| lblBuyer | 8 | 68 | 75 | 15 | |
| cmbContractType2 | 85 | 64 | 95 | 21 | |
| cmdContract2 | 185 | 64 | 26 | 21 | Text="..." |
| txtContractCode2 | 8 | 90 | 80 | 20 | ReadOnly |
| txtClientName2 | 94 | 90 | 575 | 20 | ReadOnly, Anchor=TLR |
| chkNDS | 8 | 95 | 45 | 17 | Visible=false |
| chkExport | 60 | 95 | 100 | 17 | Visible=false |

**Комиссия** (below grpContracts, Visible=true in screenshot):
- lblCommission: Visible=**true** (not false as before)
- cmbComission: Visible=**true** (not false as before)

---

### 2.2 Tab 2 — "Обязательства" (screen: `2.png`)

**No layout changes.** Just tab name correction.  
Controls unchanged: `lstViewOblig` (11 cols) + `cmdAddOblig`/`cmdEditOblig`/`cmdDelOblig`.

---

### 2.3 Tab 3 — "Данные" (screens: `3_1.png`, `3_2.png`)

#### Sub-tab 1 "Обязательство" — control order and GroupBox names

**Old control order inside tabPageOblig1:**
```
Row: [cmbNaprTrade] | [cmbCurOblig]   ← side by side
Row: chkRate + ucdRateCurOblig
Row: chkSumInCurValue + ucdCostCurOpl
Row: [cmbUnit] | [ucdCostUnit]        ← side by side
GroupBox "Характеристики металла"
GroupBox "Характеристики металла (поставка)"
lblObligInfo1, lblObligInfo2
```

**New control order (from screen 3_1):**
```
Row: Направление сделки     [cmbNaprTrade ▼]              ← full row
Row: Валюта обязательства   [cmbCurOblig ▼]               ← full row
Row: Единица измерения веса [cmbUnit ▼]                   ← full row
Row: Цена за ед. в валюте обязательства   [ucdCostUnit]   ← full row
Row: Коэф. пересчета валюты обязательства [□chkRate][ucdRateCurOblig]
Row: Цена за ед. в валюте оплаты          [□chkSum][ucdCostCurOpl]
GroupBox "Обязательство поставки"
  Row: Дата поставки [txtDatePost]
  Row: Масса металла в ед. измерения [ucdMassa] | Масса металла в граммах [ucdMassaGramm]
GroupBox "Обязательство оплаты"
  Row: Дата оплаты [txtDateOpl]
  Row: Сумма в валюте обязательства [ucdSumOblig]
  Row: Сумма в валюте оплаты [ucdSumOpl]
```

**GroupBox name changes:**
| Old name | New name |
|----------|----------|
| "Характеристики металла" | **"Обязательство поставки"** |
| "Характеристики металла (поставка)" | **"Обязательство оплаты"** |

**Massa fields side-by-side** (in "Обязательство поставки"):
- `lblMassa` + `ucdMassa` on left half
- `lblMassaGramm` + `ucdMassaGramm` on right half — SAME ROW

**Approximate positions inside tabPageOblig1 (Width≈672):**
| Control | X | Y | W | H | Notes |
|---------|---|---|---|---|-------|
| lblNaprTrade | 6 | 11 | 160 | 15 | |
| cmbNaprTrade | 220 | 7 | 440 | 21 | Anchor=TLR |
| lblCurOblig | 6 | 37 | 160 | 15 | |
| cmbCurOblig | 220 | 33 | 440 | 21 | Anchor=TLR |
| lblUnit | 6 | 63 | 160 | 15 | |
| cmbUnit | 220 | 59 | 440 | 21 | Anchor=TLR |
| lblCostUnit | 6 | 89 | 250 | 15 | |
| ucdCostUnit | 440 | 86 | 220 | 20 | Anchor=TR |
| lblRateCoef (new name of chkRate label) | 6 | 113 | 250 | 15 | Text="Коэф. пересчета валюты обязательства" |
| chkRate | 420 | 110 | 18 | 17 | checkbox only |
| ucdRateCurOblig | 440 | 110 | 220 | 20 | Enabled=false, Anchor=TR |
| lblCostCurOpl (new name of chkSum label) | 6 | 137 | 250 | 15 | Text="Цена за ед. в валюте оплаты" |
| chkSumInCurValue | 420 | 134 | 18 | 17 | checkbox only |
| ucdCostCurOpl | 440 | 134 | 220 | 20 | Enabled=false, Anchor=TR |
| grpMetalChar (now "Обязательство поставки") | 6 | 162 | 330 | 85 | |
| grpMetalCharPost (now "Обязательство оплаты") | 344 | 162 | 322 | 105 | |

Inside **"Обязательство поставки"** (grpMetalChar):
| Control | X | Y | W | H |
|---------|---|---|---|---|
| lblDatePost | 8 | 22 | 85 | 15 |
| txtDatePost | 100 | 18 | 110 | 21 |
| lblMassa | 8 | 50 | 130 | 15 |
| ucdMassa | 145 | 47 | 100 | 20 |
| lblMassaGramm | 170 | 50 | 80 | 15 |
| ucdMassaGramm | 253 | 47 | 70 | 20 |

Inside **"Обязательство оплаты"** (grpMetalCharPost):
| Control | X | Y | W | H |
|---------|---|---|---|---|
| lblDateOpl | 8 | 22 | 85 | 15 |
| txtDateOpl | 100 | 18 | 110 | 21 |
| lblSumOblig | 8 | 48 | 175 | 15 |
| ucdSumOblig | 182 | 45 | 130 | 20 |
| lblSumOpl | 8 | 74 | 175 | 15 |
| ucdSumOpl | 182 | 71 | 130 | 20 |

#### Sub-tab 2 "Объекты" — add info labels at top

**New controls in tabPageOblig2:**
- `lblObligInfo1`: at top, centered, Text="" (shows "Дата оплаты, Дата поставки" when obligation selected)
- `lblObligInfo2`: below lblObligInfo1, centered, Text="" (shows "Цена,Масса,Сумма")
- Then `lstViewObject` below the labels
- `cmdAddObject`, `cmdDelObject` to the right

**Approximate positions inside tabPageOblig2:**
| Control | X | Y | W | H | Notes |
|---------|---|---|---|---|-------|
| lblObligInfo1 | 6 | 6 | 660 | 15 | Anchor=TLR, TextAlign=Center |
| lblObligInfo2 | 6 | 24 | 660 | 15 | Anchor=TLR, TextAlign=Center |
| lstViewObject | 6 | 44 | 578 | 330 | Anchor=All |
| cmdAddObject | 594 | 44 | 72 | 26 | Anchor=TR |
| cmdDelObject | 594 | 76 | 72 | 26 | Anchor=TR |

#### Bottom of tabPage3 — "Счета по обязательству" + buttons

**Remove** `lblAccounts` / `cmdAccounts` from bottom strip (`tableLayoutPanel`).  
**Add** to `tabPage3` level (below `tabControlOblig`):

| Control | Name | X | Y | W | H | Notes |
|---------|------|---|---|---|---|-------|
| Label | lblAccountsOblig | 6 | 432 | 140 | 15 | Text="Счета по обязательству", Anchor=BL, Visible=false |
| Button | cmdAccountsOblig | 152 | 428 | 26 | 22 | Text="...", Anchor=BL, Visible=false |
| Button | cmdApplayOblig | 504 | 428 | 82 | 26 | Text="Применить", Anchor=BR, Visible=false |
| Button | cmdExitOblig | 592 | 428 | 82 | 26 | Text="Отмена", Anchor=BR, Visible=false |

> **Note:** `cmdExitOblig.Text = "Отмена"` (was incorrectly set to "Выход")

**Bottom strip** (`tableLayoutPanel`) — simplify to 3 columns (remove lblAccounts/cmdAccounts):
```
[ubsCtrlInfo — fill] [btnSave — 88px] [btnExit — 88px]
```

---

### 2.4 Tab 4 — "Поставка" (screen: `4.png`)

**Content:** Delivery instruction / storage selection (was previously in tabPage5 "Хранилище")

**Layout (from screen 4.png):**
```
[centered bold label]  "Инструкция по поставке"

□  Внешнее хранилище

Номер    [txtStorageCode]  [...]  выбор хранилища

Наименование   [txtStorageName — full width]
```

**Controls for tabPage4:**
| Control | Name | X | Y | W | H | Notes |
|---------|------|---|---|---|---|-------|
| Label | lblDeliveryInstrTitle | 6 | 20 | 660 | 20 | Text="Инструкция по поставке", Bold, TextAlign=Center, Anchor=TLR |
| CheckBox | chkExternalStorage | 6 | 50 | 150 | 17 | Text="Внешнее хранилище" |
| Label | lblStorageNum | 6 | 80 | 50 | 15 | Text="Номер" |
| TextBox | txtStorageCode | 62 | 76 | 120 | 20 | MaxLength=20, ReadOnly=true, Enabled=false |
| Button | cmdStorage | 188 | 76 | 26 | 21 | Text="..." |
| Label | lblStorageSelect | 220 | 80 | 120 | 15 | Text="выбор хранилища" |
| Label | lblStorageName | 6 | 106 | 85 | 15 | Text="Наименование" |
| TextBox | txtStorageName | 98 | 102 | 580 | 20 | MaxLength=50, ReadOnly=true, Enabled=false, Anchor=TLR |

**Renamed/removed controls vs current designer:**
| Old control | Change |
|-------------|--------|
| `lblStorageTitle` (Text="Информация о хранилище") | → `lblDeliveryInstrTitle` (Text="Инструкция по поставке") |
| `lblStorageCode` (Text="Код хранилища") | → `lblStorageNum` (Text="Номер") |
| *(missing)* | **+ `lblStorageSelect`** (Text="выбор хранилища") |
| `lblStorageNote` (Text="Место хранения") | **Remove** (not in screen 4.png) |

---

### 2.5 Tab 5 — "Оплата" (screens: `5.png`, `6.png`)

**Sub-tab order (swapped):** "Покупатель" first, "Продавец" second
- `tabPageInstr1.Text = "Покупатель"` (index 0 controls: `*_0`)
- `tabPageInstr2.Text = "Продавец"` (index 1 controls: `*_1`)

**Layout per sub-tab (identical structure for both):**
```
[centered bold label]  "Инструкция по оплате"

□  Расчет через кассу

[...]  Выбор платежной инструкции по [покупателю/продавцу]

БИК  [txtBIK — short]          Корр. счет  [txtKS — long]

Наим. банка  [txtName — full width, read-only]

Расч. счет  [txtRS — formatted wide]  [...]

Клиент  [txtClient]

Примечание  [txtNote — multiline]

ИНН  [txtINN — short]

□  Безакцептное списание
```

**New/changed controls per sub-tab:**
| Control | Name (suffix _0 / _1) | X | Y | W | H | Notes |
|---------|----------------------|---|---|---|---|-------|
| Label | lblInstrTitle_0/_1 | 6 | 12 | 660 | 20 | **NEW** — "Инструкция по оплате", Bold, Center, Anchor=TLR |
| CheckBox | chkCash_0/_1 | 6 | 40 | 155 | 17 | **Visible=true** (was false) |
| Button | cmdListInstr_0/_1 | 6 | 64 | 26 | 21 | Text="...", TabIndex=47/53 |
| Label | lblCheckInstr_0/_1 | 38 | 67 | 295 | 15 | **move to RIGHT of button**; Text="Выбор платежной инструкции по покупателю/продавцу" |
| Label | lblBIK_0/_1 | 6 | 92 | 28 | 15 | Text="БИК" |
| TextBox | txtBIK_0/_1 | 36 | 88 | 80 | 20 | MaxLength=9 |
| Label | **lblKS_0/_1** | 130 | 92 | 75 | 15 | **NEW** — Text="Корр. счет" |
| TextBox | txtKS_0/_1 | 212 | 88 | 450 | 20 | **SAME ROW as txtBIK**, ReadOnly, Enabled=false, Anchor=TLR |
| Label | **lblNameBank_0/_1** | 6 | 116 | 80 | 15 | **NEW** — Text="Наим. банка" |
| TextBox | txtName_0/_1 | 92 | 112 | 573 | 20 | ReadOnly=true, Anchor=TLR |
| Label | lblRS_0/_1 | 6 | 140 | 70 | 15 | Text="Расч. счет" |
| TextBox | txtRS_0/_1 | 82 | 136 | 568 | 20 | Enabled=false, Anchor=TLR |
| Button | cmdAccount_0/_1 | 656 | 136 | 26 | 21 | **RIGHT of txtRS**, Enabled=false, Anchor=TR |
| Label | **lblClient_0/_1** | 6 | 165 | 45 | 15 | **NEW** — Text="Клиент" |
| TextBox | txtClient_0/_1 | 56 | 161 | 608 | 20 | Anchor=TLR |
| Label | **lblNote_0/_1** | 6 | 189 | 80 | 15 | **NEW** — Text="Примечание" |
| TextBox | txtNote_0/_1 | 92 | 185 | 572 | 60 | Multiline=true, Anchor=TLR |
| Label | **lblINN_0/_1** | 6 | 253 | 28 | 15 | **NEW** — Text="ИНН" |
| TextBox | txtINN_0/_1 | 36 | 249 | 150 | 20 | |
| CheckBox | chkNotAkcept_0/_1 | 6 | 277 | 200 | 17 | Text="Безакцептное списание" |

**New private field declarations needed:**
```csharp
// Seller (index 1)
private System.Windows.Forms.Label lblInstrTitle_1;
private System.Windows.Forms.Label lblKS_1;
private System.Windows.Forms.Label lblNameBank_1;
private System.Windows.Forms.Label lblClient_1;
private System.Windows.Forms.Label lblNote_1;
private System.Windows.Forms.Label lblINN_1;
// Buyer (index 0)
private System.Windows.Forms.Label lblInstrTitle_0;
private System.Windows.Forms.Label lblKS_0;
private System.Windows.Forms.Label lblNameBank_0;
private System.Windows.Forms.Label lblClient_0;
private System.Windows.Forms.Label lblNote_0;
private System.Windows.Forms.Label lblINN_0;
```

---

### 2.6 Tab 6 — "Дополнительные" (screen: `7.png`)

**Only change:** rename tab.  
`tabPage6.Text = "Дополнительные"` (was "Параметры")

`ucpParam` (`UbsCtrlFields`, Dock=Fill) unchanged.

---

## 3. BOTTOM STRIP CHANGES

**Simplify from 5 columns to 3 columns** (remove lblAccounts/cmdAccounts — they move to tabPage3):

```csharp
// OLD: 5 cols [info | lblAccounts | cmdAccounts | btnSave | btnExit]
// NEW: 3 cols [info | btnSave | btnExit]
this.tableLayoutPanel.ColumnCount = 3;
this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88F));
this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88F));
this.tableLayoutPanel.Controls.Add(this.ubsCtrlInfo, 0, 0);
this.tableLayoutPanel.Controls.Add(this.btnSave, 1, 0);
this.tableLayoutPanel.Controls.Add(this.btnExit, 2, 0);
```

**Remove declarations:** `lblAccounts`, `cmdAccounts` (as bottom-strip controls).

---

## 4. NEW CONTROLS REQUIRED (not in prior plan)

| Control | Name | Parent | Notes |
|---------|------|--------|-------|
| Label | `lblDeliveryInstrTitle` | tabPage4 | "Инструкция по поставке", Bold, centered |
| Label | `lblStorageSelect` | tabPage4 | "выбор хранилища" |
| Label | `lblAccountsOblig` | tabPage3 | "Счета по обязательству", Visible=false |
| Button | `cmdAccountsOblig` | tabPage3 | Text="...", Visible=false |
| Label | `lblInstrTitle_0` / `lblInstrTitle_1` | tabPageInstr1/2 | "Инструкция по оплате", Bold |
| Label | `lblKS_0` / `lblKS_1` | tabPageInstr1/2 | "Корр. счет" |
| Label | `lblNameBank_0` / `lblNameBank_1` | tabPageInstr1/2 | "Наим. банка" |
| Label | `lblClient_0` / `lblClient_1` | tabPageInstr1/2 | "Клиент" |
| Label | `lblNote_0` / `lblNote_1` | tabPageInstr1/2 | "Примечание" |
| Label | `lblINN_0` / `lblINN_1` | tabPageInstr1/2 | "ИНН" |

**Total new labels:** 14 new Label controls.

---

## 5. REMOVED / RENAMED CONTROLS

| Old control | Action | Reason |
|-------------|--------|--------|
| `lblStorageTitle` | **Rename** → `lblDeliveryInstrTitle`, Text="Инструкция по поставке" | Screen 4.png |
| `lblStorageCode` (Text="Код хранилища") | **Rename** → `lblStorageNum`, Text="Номер" | Screen 4.png |
| `lblStorageNote` | **Remove** | Not in screen 4.png |
| `lblAccounts` | **Move** from bottom strip → tabPage3 as `lblAccountsOblig` | Screens 3_1/3_2 |
| `cmdAccounts` | **Move** from bottom strip → tabPage3 as `cmdAccountsOblig` | Screens 3_1/3_2 |
| `lblRateCoefLabel` *(label for chkRate)* | **Add separate label** before the checkbox; chkRate becomes checkbox-only | Screen 3_1 |
| `lblCostCurOplLabel` *(label for chkSum)* | **Add separate label** before the checkbox; chkSumInCurValue becomes checkbox-only | Screen 3_1 |

> **Note on chkRate / chkSumInCurValue layout:** In the screen, these look like the label text is a separate Label control, and the CheckBox appears right before the Decimal field. The long text ("Коэф. пересчета...") is a Label, not the CheckBox's own Text.  
> Implementation choice: keep the CheckBox with Text="" and add a sibling Label with the descriptive text, OR keep the long text in CheckBox.Text (simpler). Choose: **Keep long text in CheckBox.Text** for simplicity — the AutoSize checkbox will show the text inline.

---

## 6. FORM SIZE

From screenshots, the form appears narrower than 700px. Estimated client size: **~470 × 530px** (matching VB6 ClientWidth 6765 twips ≈ 451px + chrome).

Recommend: `ClientSize = new Size(470, 530)` to match legacy proportions.  
Anchoring ensures the form remains fully functional at larger sizes.

---

## 7. IMPLEMENTATION STEPS (ordered)

### Step 1 — Fix tab names and tab swap (structural)
1. `tabPage1.Text = "Основные"`
2. `tabPage3.Text = "Данные"`
3. `tabPage4.Text = "Поставка"` + swap content (storage → tabPage4, payment → tabPage5)
4. `tabPage5.Text = "Оплата"` + sub-tab order: Покупатель first, Продавец second
5. `tabPage6.Text = "Дополнительные"`

### Step 2 — Bottom strip (simplify to 3 cols)
6. Remove `lblAccounts`, `cmdAccounts` from strip
7. Resize strip to 3 columns

### Step 3 — Tab 1 layout fix
8. Move `cmbTradeType` + `lblTradeType` to same row as `cmbKindSupplyTrade`
9. Set `cmbTradeType.Visible = true`
10. Restructure `grpContracts` with code/name on second row per contract
11. Set `lblCommission.Visible = true`, `cmbComission.Visible = true`

### Step 4 — Tab 3 sub-tab 1 layout fix
12. Reorder controls: Direction → CurOblig → Unit → CostUnit → chkRate row → chkSum row
13. Rename GroupBoxes: "Обязательство поставки" / "Обязательство оплаты"
14. Put `ucdMassa` and `ucdMassaGramm` on the same row inside "Обязательство поставки"
15. Move `lblObligInfo1/2` to `tabPageOblig2`

### Step 5 — Tab 3 bottom buttons fix
16. Add `lblAccountsOblig` + `cmdAccountsOblig` to `tabPage3`
17. Change `cmdExitOblig.Text` to "Отмена"
18. Adjust Y positions of cmdApplayOblig / cmdExitOblig

### Step 6 — Tab 4 "Поставка" layout
19. Rename labels; add `lblDeliveryInstrTitle`, `lblStorageSelect`
20. Reposition controls per section 2.4

### Step 7 — Tab 5 "Оплата" layout
21. Swap sub-tab order (Покупатель first)
22. Add 6 new label controls per sub-tab (12 total)
23. Reposition `txtKS` to same row as `txtBIK`
24. Move `cmdAccount` to right of `txtRS`
25. Set `chkCash.Visible = true` for both panels

### Step 8 — Verify build
26. Build project — expect 0 errors
27. Check ReadLints for 0 warnings

---

## 8. PLAN VERIFICATION CHECKLIST

```
✓ PLAN REVISION CHECKLIST — Screen-Based Correction
- All 7 screen files read and analyzed?              YES (1.png–7.png)
- Tab names corrected (all 6)?                       YES
- Tab 4/5 content swap documented?                   YES
- Tab 1 GroupBox layout corrected?                   YES
- Tab 3 control order corrected?                     YES
- Tab 3 GroupBox names corrected?                    YES
- Tab 3 Massa/MassaGramm same-row layout?            YES
- Tab 3 bottom controls (accounts/apply/cancel)?     YES
- Tab 4 storage layout corrected?                    YES
- Tab 5 sub-tab order corrected (Buyer first)?       YES
- Tab 5 instruction layout corrected (KS same row)?  YES
- Tab 5 new labels (title, KS, NameBank, Client...): YES (14 new)
- Tab 6 renamed?                                     YES
- Bottom strip simplified (3 cols)?                  YES
- Form size adjusted to ~470x530?                    YES
- New controls listed?                               YES (14 labels + 2 buttons)
- Removed controls listed?                           YES

→ Ready for BUILD (designer revision)
```

---

## 9. REFERENCE

- Screen 1.png → Tab 1 "Основные"  
- Screen 2.png → Tab 2 "Обязательства"  
- Screen 3_1.png → Tab 3 "Данные" / sub-tab "Обязательство"  
- Screen 3_2.png → Tab 3 "Данные" / sub-tab "Объекты"  
- Screen 4.png → Tab 4 "Поставка" (storage/delivery)  
- Screen 5.png → Tab 5 "Оплата" / sub-tab "Покупатель"  
- Screen 6.png → Tab 5 "Оплата" / sub-tab "Продавец"  
- Screen 7.png → Tab 6 "Дополнительные" (ucpParam grid)
