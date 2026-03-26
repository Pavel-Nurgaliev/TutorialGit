# PLAN: UbsPmTradeFrm — Form Designer Conversion

**Date:** 2026-03-24  
**Project:** UbsPmTradeFrm_CP  
**Scope:** Designer file only — `UbsPmTradeFrm.Designer.cs` (and the form class rename)  
**Legacy source:** `legacy-form/Pm_Trade/Pm_Trade_ud.dob`

---

## 1. GOAL

Convert the VB6 `Pm_Trade_ud` UI layout to a .NET `UbsPmTradeFrm.Designer.cs` that:
- Declares and configures all controls in `InitializeComponent()`.
- Uses .NET equivalents for all VB6/OCX controls (see mapping in `techContext.md`).
- Replicates the 6-tab layout with nested panels/tab controls where required.
- Anchors controls so the form resizes correctly (matching VB6 `UBSChild_HeightBrowser`/`WidthBrowser` resize logic).

---

## 2. PRE-REQUISITES (Phase 1 Prep — do first)

| Step | Action | File Affected |
|------|--------|---------------|
| P1 | Rename class `UbsForm1` → `UbsPmTradeFrm` in all 3 files | `UbsForm1.cs`, `UbsForm1.Designer.cs`, `UbsForm1.resx` |
| P2 | Rename files: `UbsForm1.*` → `UbsPmTradeFrm.*` | — |
| P3 | Update `UbsPmTradeFrm.csproj`: `<Compile>`, `<EmbeddedResource>` entries | `.csproj` |
| P4 | Add `UbsCtrlDecimal` reference to `.csproj` | `.csproj` |
| P5 | Add `UbsCtrlDate` reference to `.csproj` (for `txtTradeDate`, `txtDateOpl`, `txtDatePost`) | `.csproj` |
| P6 | Add `UbsCtrlAddFields` reference to `.csproj` (for `ucpParam`) | `.csproj` |

---

## 3. FORM STRUCTURE OVERVIEW

```
UbsPmTradeFrm (UbsFormBase)
├── panelMain (inherited, DockStyle.Fill)
│   ├── tabControl (DockStyle.Fill, TabCount=6)
│   │   ├── tabPage1  "Основные данные"     [SSTabsPanel1]
│   │   ├── tabPage2  "Обязательства"       [SSTabsPanel2]
│   │   ├── tabPage3  "Детали"              [SSTabsPanel3]
│   │   ├── tabPage4  "Инструкция оплаты"   [SSActiveTabPanel1]
│   │   ├── tabPage5  "Хранилище"           [SSTabsPanel4]
│   │   └── tabPage6  "Параметры"           [SSActiveTabPanel2]
│   └── tableLayoutPanel  (DockStyle.Bottom, height=32)
│       ├── ubsCtrlInfo   (col 0, Dock=Fill)
│       ├── btnSave       (col 1, 88px)
│       └── btnExit       (col 2, 88px)
```

**Form size:** `ClientSize = new Size(700, 520)` (approximate, matches VB6 ClientWidth=6765 twips / 15 ≈ 451px, ClientHeight=7365 twips / 15 ≈ 491px + room for resize).  
Use `AutoScaleDimensions = new SizeF(6F, 13F)` and `AutoScaleMode = AutoScaleMode.Font`.

---

## 4. BOTTOM STRIP (applies to all tabs)

Identical pattern to `UbsOpRetoperFrm.Designer.cs`:

```csharp
this.tableLayoutPanel = new TableLayoutPanel();    // DockStyle.Bottom, Height=32, 3 cols
this.btnSave = new Button();                       // Text="Сохранить", TabIndex=101
this.btnExit = new Button();                       // Text="Выход", CausesValidation=false
this.ubsCtrlInfo = new UbsControl.UbsCtrlInfo();   // Visible=false
this.lblAccounts = new Label();                    // Visible=false, right area
this.cmdAccounts = new Button();                   // Visible=false, Text="..."
```

> `lblAccounts` and `cmdAccounts` are visible only when `intVidTrade != 0` (precious metals trade). Place them in the bottom strip or within `tabPage2`.

---

## 5. TAB 1 — "Основные данные" (SSTabsPanel1)

**Source panel:** `SSTabsPanel1` (TabGuid 02F3) in the VB6 DOB.

### 5.1 GroupBox "Сделка" (frmTrade)

| Control | VB6 Name | .NET Name | .NET Type | Notes |
|---------|----------|-----------|-----------|-------|
| Label | Label1 | lblDate | Label | Text="Дата" |
| Date field | txtTradeDate | txtTradeDate | UbsCtrlDate | TabIndex=1 |
| Label | Label2 | lblNum | Label | Text="Номер" |
| TextBox | txtTradeNum | txtTradeNum | TextBox | MaxLength=20, TabIndex=2 |
| Label | Label31 | lblDelivery | Label | Text="Поставка" |
| ComboBox | cmbKindSupplyTrade | cmbKindSupplyTrade | ComboBox | DropDownList, TabIndex=4 |
| CheckBox | chkIsComposit | chkIsComposit | CheckBox | Text="Составная?", TabIndex=3 |
| Label | Label3 | lblTradeType | Label | Text="Тип", Visible=false |
| ComboBox | cmbTradeType | cmbTradeType | ComboBox | DropDownList, TabIndex=5, Visible=false |

### 5.2 Currency rows (below frmTrade)

| Control | VB6 Name | .NET Name | .NET Type | Notes |
|---------|----------|-----------|-----------|-------|
| Label | Label14 | lblCurrencyPost | Label | Text="Драг.металл" |
| ComboBox | cmbCurrencyPost | cmbCurrencyPost | ComboBox | DropDownList, TabIndex=6 |
| Label | Label15 | lblCurrencyOpl | Label | Text="Валюта оплаты" |
| ComboBox | cmbCurrencyOpl | cmbCurrencyOpl | ComboBox | DropDownList, TabIndex=7 |

### 5.3 GroupBox "Стороны сделки" (frmContracts)

Two rows — Продавец (seller = contract 1) and Покупатель (buyer = contract 2):

| Control | VB6 Name | .NET Name | .NET Type | Notes |
|---------|----------|-----------|-----------|-------|
| Label | Label5 | lblSeller | Label | Text="Продавец" |
| ComboBox | cmbContractType1 | cmbContractType1 | ComboBox | DropDownList, TabIndex=8 |
| Button | cmdContract1 | cmdContract1 | Button | Text="...", Width=26, TabIndex=9 |
| TextBox | txtContractCode1 | txtContractCode1 | TextBox | MaxLength=20, ReadOnly=true, TabIndex=128 |
| TextBox | txtClientName1 | txtClientName1 | TextBox | MaxLength=50, ReadOnly=true, TabIndex=129 |
| Label | Label6 | lblBuyer | Label | Text="Покупатель" |
| ComboBox | cmbContractType2 | cmbContractType2 | ComboBox | DropDownList, TabIndex=10 |
| Button | cmdContract2 | cmdContract2 | Button | Text="...", Width=26, TabIndex=11 |
| TextBox | txtContractCode2 | txtContractCode2 | TextBox | MaxLength=20, ReadOnly=true, TabIndex=130 |
| TextBox | txtClientName2 | txtClientName2 | TextBox | MaxLength=50, ReadOnly=true, TabIndex=131 |
| CheckBox | chkNDS | chkNDS | CheckBox | Text="НДС", Visible=false, TabIndex=12 |
| CheckBox | chkExport | chkExport | CheckBox | Text="Экспортная", Visible=false, TabIndex=13 |

### 5.4 Commission row (below frmContracts)

| Control | VB6 Name | .NET Name | .NET Type | Notes |
|---------|----------|-----------|-----------|-------|
| Label | Label8 | lblCommission | Label | Text="Комиссия", Visible=false |
| ComboBox | cmbComission | cmbComission | ComboBox | DropDownList, TabIndex=14, Visible=false |

### 5.5 Direction row (conditional)

| Control | VB6 Name | .NET Name | .NET Type | Notes |
|---------|----------|-----------|-----------|-------|
| ComboBox | cmbNaprTrade | cmbNaprTrade | ComboBox | DropDownList — WAIT: cmbNaprTrade is actually in Tab 3 (obligation detail), not Tab 1 |

> **Note:** `cmbNaprTrade` is on Tab 3 (SSTabs1Panel1), not Tab 1. In Tab 1, the composit checkbox controls it via enabling/disabling logic.

---

## 6. TAB 2 — "Обязательства" (SSTabsPanel2)

**Source panel:** `SSTabsPanel2` (TabGuid 02CB).

### Controls

| Control | VB6 Name | .NET Name | .NET Type | Notes |
|---------|----------|-----------|-----------|-------|
| ListView | lstViewOblig | lstViewOblig | ListView | View=Details, FullRowSelect=true, GridLines=true |
| Button | cmdAddOblig | cmdAddOblig | Button | Text="Добавить", TabIndex=15 |
| Button | cmdEditOblig | cmdEditOblig | Button | Text="Изменить", TabIndex=16 |
| Button | cmdDelOblig | cmdDelOblig | Button | Text="Удалить", TabIndex=17 |

### lstViewOblig — 11 columns (SubItem 0–10)

| # | Header text | SubItem Index | Width | Notes |
|---|-------------|---------------|-------|-------|
| 1 | Направление | 0 (item text) | 100 | visible |
| 2 | Номер в части | 1 | 90 | visible |
| 3 | Дата оплаты | 2 | 100 | visible |
| 4 | Дата поставки | 3 | 100 | visible |
| 5 | Цена за ед. в валюте обязательства | 4 | 130 | visible |
| 6 | Масса | 5 | 80 | visible |
| 7 | Сумма в валюте обязательства | 6 | 130 | visible |
| 8 | Валюта обязательства | 7 | 0 | hidden (id) |
| 9 | Курс пересчета | 8 | 0 | hidden |
| 10 | Единица измерения веса | 9 | 0 | hidden |
| 11 | Фиксированный курс | 10 | 0 | hidden (0 width in VB6) |

### Layout
```
[lstViewOblig — Anchor=All]    [cmdAddOblig ]
                               [cmdEditOblig]
                               [cmdDelOblig ]
```

---

## 7. TAB 3 — "Детали" (SSTabsPanel3 with nested SSTabs1)

**Source panel:** `SSTabsPanel3` (TabGuid 01D1) containing nested `SSTabs1` (TabCount=2).

### Outer panel controls (below nested TabControl)

| Control | VB6 Name | .NET Name | .NET Type | Notes |
|---------|----------|-----------|-----------|-------|
| Button | cmdApplayOblig | cmdApplayOblig | Button | Text="Применить", Visible=false initially |
| Button | cmdExitOblig | cmdExitOblig | Button | Text="Выход", Visible=false initially |

### Nested TabControl `tabControlOblig` (SSTabs1, 2 sub-tabs)

#### Sub-tab 1 — "Обязательство" (SSTabs1Panel1)

| Control | VB6 Name | .NET Name | .NET Type | Enabled? |
|---------|----------|-----------|-----------|----------|
| Label | lblNaprTrade | lblNaprTrade | Label | Text="Направление сделки" |
| ComboBox | cmbNaprTrade | cmbNaprTrade | ComboBox | DropDownList, TabIndex=18 |
| Label | Label4 | lblCurOblig | Label | Text="Валюта обязательства" |
| ComboBox | cmbCurOblig | cmbCurOblig | ComboBox | DropDownList, TabIndex=19 |
| CheckBox | chkRate | chkRate | CheckBox | Text="Фикс. валютный курс ОБязательств.", TabIndex=22 |
| UbsControlMoney | txtRateCurOblig | ucdRateCurOblig | UbsCtrlDecimal | Precision=10, SignEnable=0, Enabled=false, TabIndex=23 |
| CheckBox | chkSumInCurValue | chkSumInCurValue | CheckBox | Text="Цена по ед. в разрезе ОПлаты", TabIndex=24 |
| UbsControlMoney | txtCostCurOpl | ucdCostCurOpl | UbsCtrlDecimal | Precision=4, Enabled=false, TabIndex=25 |
| Label | Label32 | lblUnit | Label | Text="Единица измерения веса" |
| ComboBox | cmbUnit | cmbUnit | ComboBox | DropDownList, TabIndex=20 |
| Label | Label18 | lblCostUnit | Label | Text="Цена за ед. в валюте обязательства" |
| UbsControlMoney | txtCostUnit | ucdCostUnit | UbsCtrlDecimal | Precision=4, TabIndex=21 |

**GroupBox "Характеристики металла" (Frame1)** — inside SSTabs1Panel1:

| Control | VB6 Name | .NET Name | .NET Type | Notes |
|---------|----------|-----------|-----------|-------|
| Label | Label16 | lblDatePost | Label | Text="Дата поставки" |
| UbsControlDate | txtDatePost | txtDatePost | UbsCtrlDate | TabIndex=26 |
| Label | Label26 | lblMassa | Label | Text="Масса металла в ед. измерения" |
| UbsControlMoney | txtMassa | ucdMassa | UbsCtrlDecimal | Precision=2, TabIndex=27 |
| Label | Label33 | lblMassaGramm | Label | Text="Масса металла в граммах" |
| UbsControlMoney | txtMassaGramm | ucdMassaGramm | UbsCtrlDecimal | Precision=1, Enabled=false, TabIndex=28 |

**GroupBox "Характеристики металла (поставка)" (Frame2)** — inside SSTabs1Panel1:

| Control | VB6 Name | .NET Name | .NET Type | Notes |
|---------|----------|-----------|-----------|-------|
| Label | Label7 | lblDateOpl | Label | Text="Дата оплаты" |
| UbsControlDate | txtDateOpl | txtDateOpl | UbsCtrlDate | TabIndex=29 |
| Label | Label24 | lblSumOblig | Label | Text="Сумма в валюте обязательства" |
| UbsControlMoney | txtSumOblig | ucdSumOblig | UbsCtrlDecimal | Precision=2, Enabled=false, TabIndex=30 |
| Label | Label30 | lblSumOpl | Label | Text="Сумма в валюте оплаты" |
| UbsControlMoney | txtSumOpl | ucdSumOpl | UbsCtrlDecimal | Precision=2, Enabled=false, TabIndex=31 |

**Summary label row (lblObligInfo1/2):**

| Control | VB6 Name | .NET Name | .NET Type | Notes |
|---------|----------|-----------|-----------|-------|
| Label | lblObligInfo1 | lblObligInfo1 | Label | Alignment=Center, Visible=false |
| Label | lblObligInfo2 | lblObligInfo2 | Label | Alignment=Center |

#### Sub-tab 2 — "Объекты" (SSTabs1Panel4)

| Control | VB6 Name | .NET Name | .NET Type | Notes |
|---------|----------|-----------|-----------|-------|
| ListView | lstViewObject | lstViewObject | ListView | View=Details, 7 cols |
| Button | cmdAddObject | cmdAddObject | Button | Text="Добавить", TabIndex=32 |
| Button | cmdDelObject | cmdDelObject | Button | Text="Удалить", TabIndex=33 |

**lstViewObject — 7 columns (from VB6 ColumnHeader definitions):**

| # | Header text | SubItem Index | Width |
|---|-------------|---------------|-------|
| 1 | Инстр.объект | 0 | 100 |
| 2 | Код объекта | 1 | 100 |
| 3 | Лигатурная масса | 2 | 100 |
| 4 | Проба | 3 | 100 |
| 5 | Масса | 4 | 100 |
| 6 | Масса | 5 | 100 |
| 7 | Идентификатор объекта | 6 | 0 (hidden) |

---

## 8. TAB 4 — "Инструкция оплаты" (SSActiveTabPanel1 with nested SSActiveTabs1)

**Source panel:** `SSActiveTabPanel1` (TabGuid 0323) containing nested `SSActiveTabs1` (TabCount=2).

### Nested TabControl `tabControlInstr` — 2 sub-tabs

**Screen-correct order:** **Sub-tab 1 = Покупатель** (`InstrSideBuyer`, `_0`), **Sub-tab 2 = Продавец** (`InstrSideSeller`, `_1`).

Both sub-tabs share the same control layout — one set for Index=1, one for Index=0:

| Control | VB6 Name | .NET Name (pattern) | .NET Type | Notes |
|---------|----------|---------------------|-----------|-------|
| Label | lblCheckInstr | lblCheckInstr[k] | Label | Text="Инструкция по оплате" |
| Button | cmdListInstr | cmdListInstr[k] | Button | Text="...", TabIndex=53/47 |
| CheckBox | chkCash | chkCash[k] | CheckBox | Text="Расчет через кассу", Visible=false |
| Label | (lblBIK) | lblBIK[k] | Label | Text="БИК" |
| TextBox | txtBIK | txtBIK[k] | TextBox | MaxLength=9, TabIndex=54/48 |
| TextBox | txtName | txtName[k] | TextBox | ReadOnly=true (Locked), TabIndex=56/50 |
| Label | (lblRS) | lblRS[k] | Label | Text="Р/С" |
| TextBox | txtRS | txtRS[k] | TextBox | TabIndex=55/49, Enabled=false |
| Button | cmdAccount | cmdAccount[k] | Button | Text="...", Enabled=false, TabIndex=57/51 |
| TextBox | txtKS | txtKS[k] | TextBox | ReadOnly, Enabled=false, TabIndex=... |
| TextBox | txtClient | txtClient[k] | TextBox | TabIndex=58/52 |
| TextBox | txtNote | txtNote[k] | TextBox | Multiline=true, TabIndex=59/53 |
| TextBox | txtINN | txtINN[k] | TextBox | TabIndex=60/54 |
| CheckBox | chkNotAkcept | chkNotAkcept[k] | CheckBox | Text="Безакцептное списание", TabIndex=61/55 |

> **Implementation choice:** Use two sets of named controls (e.g., `txtBIK_Seller`, `txtBIK_Buyer`) instead of VB6 control arrays. The [k] indexing in code should use a helper method or index-based access via an array/list of Panel references.

---

## 9. TAB 5 — "Хранилище" (SSTabsPanel4)

**Source panel:** `SSTabsPanel4` (TabGuid 01A9).

| Control | VB6 Name | .NET Name | .NET Type | Notes |
|---------|----------|-----------|-----------|-------|
| Label | Label13 | lblStorageTitle | Label | Text="Информация о хранилище", Bold |
| CheckBox | chkExternalStorage | chkExternalStorage | CheckBox | Text="Внешнее хранилище", TabIndex=37 |
| Label | Label9 | lblStorageCode | Label | Text="Код хранилища" |
| TextBox | txtStorageCode | txtStorageCode | TextBox | MaxLength=20, ReadOnly, Enabled=false, TabIndex=39 |
| Button | cmdStorage | cmdStorage | Button | Text="...", TabIndex=38 |
| Label | Label10 | lblStorageName | Label | Text="Наименование" |
| TextBox | txtStorageName | txtStorageName | TextBox | MaxLength=50, ReadOnly, Enabled=false, TabIndex=40 |
| Label | Label12 | lblStorageNote | Label | Text="Место хранения" |

---

## 10. TAB 6 — "Параметры" (SSActiveTabPanel2)

**Source panel:** `SSActiveTabPanel2` (TabGuid 0151).

| Control | VB6 Name | .NET Name | .NET Type | Notes |
|---------|----------|-----------|-----------|-------|
| UbsControlProperty | ucpParam | ucpParam | UbsCtrlAddFields (TBD) | Dock=Fill, TabIndex=62 |

> `ucpParam` is initialized with `ucpParam.UbsAddFields = objStub` and `.Refresh()` in code. The .NET equivalent is to set the `IUbsAddFields` source on `UbsCtrlFieldsSupport` or similar. Confirm exact .NET class during Phase 1.

---

## 11. ANCHOR / RESIZE STRATEGY

The VB6 form implements custom resize via `UBSChild_HeightBrowser` and `UBSChild_WidthBrowser` property setters. In .NET, use Anchor/Dock:

| Element | VB6 resize behavior | .NET equivalent |
|---------|--------------------|-----------------| 
| `tabControl` (outer) | Width and Height resize to fill | `Dock=Fill` |
| `lstViewOblig` | Width/Height resize with form | `Anchor=Top,Left,Right,Bottom` |
| `lstViewObject` | Width/Height resize | `Anchor=Top,Left,Right,Bottom` |
| `txtClientName1/2` | Width stretches | `Anchor=Top,Left,Right` |
| `ucpParam` | Width/Height resize | `Dock=Fill` |
| `tableLayoutPanel` (bottom strip) | Width stretches | `Dock=Bottom` |
| `cmdSave`, `cmdExit` | Fixed width, move right | Fixed width in TableLayoutPanel col |
| `cmdApplayOblig`, `cmdExitOblig` | Move to bottom-right of SSTabs1 | `Anchor=Bottom,Right` |

---

## 12. CONTROL ARRAY REPLACEMENT (.NET)

VB6 uses indexed control arrays (`txtBIK(0)`, `txtBIK(1)`, `chkCash(0)`, `chkCash(1)`, etc.). .NET does not support control arrays. Replace with:

**Option A (Recommended): Named pairs + helper arrays**
```csharp
// Declare paired controls with suffixes:
private TextBox txtBIK_0, txtBIK_1;
private TextBox txtRS_0, txtRS_1;
// ...

// Helper array initialized in constructor:
private TextBox[] _txtBIK;
private void InitControlArrays()
{
    _txtBIK = new[] { txtBIK_0, txtBIK_1 };
    _txtRS = new[] { txtRS_0, txtRS_1 };
    // ...
}
// Usage: _txtBIK[k]
```

**Control arrays in legacy form:**
- `txtBIK(0/1)`, `txtRS(0/1)`, `txtKS(0/1)`, `txtName(0/1)`, `txtClient(0/1)`, `txtNote(0/1)`, `txtINN(0/1)`
- `chkNotAkcept(0/1)`, `chkCash(0/1)`
- `cmdListInstr(0/1)`, `cmdAccount(0/1)`
- `lblCheckInstr(0/1)` (Label)

---

## 13. SPECIAL CONTROLS

### UbsCtrlDecimal (replacing UbsControlMoney)
```csharp
private UbsControl.UbsCtrlDecimal ucdCostUnit;
// Key properties: .Text, .Value (decimal), Enabled, TabIndex
// No direct "Precision" property — check UbsCtrlDecimal API; use .DecimalPlaces if available
// VB6 VariantValue equivalent: use .Value (double) not .Text for high-precision rates
```

### UbsCtrlDate (replacing UbsControlDate)
```csharp
private UbsControl.UbsCtrlDate txtTradeDate;
// Key properties: .Value (DateTime), .IsValid, Enabled
// VB6: txtTradeDate.DateValue = Date → .NET: txtTradeDate.Value = DateTime.Today
```

### UbsCtrlAddFields (replacing UbsControlProperty ucpParam)
```csharp
// Exact .NET class name TBD — check project references or similar UBS forms
// VB6: ucpParam.UbsAddFields = objStub; ucpParam.Refresh()
// Event: ucpParam.TextChange → .NET equivalent event name TBD
```

---

## 14. IMPLEMENTATION STEPS (ordered)

### Step 1 — Phase 1 Prep
1. [ ] Add `UbsCtrlDecimal` HintPath reference to `.csproj`
2. [ ] Add `UbsCtrlDate` HintPath reference to `.csproj`
3. [ ] Add `UbsCtrlAddFields`/`UbsCtrlFieldsSupport` reference
4. [ ] Rename `UbsForm1` → `UbsPmTradeFrm` (class + files + `.csproj` entries)
5. [ ] Verify project builds (empty skeleton) after rename

### Step 2 — Bottom Strip
6. [ ] Add `tableLayoutPanel` (DockStyle.Bottom, 3-column: info | save | exit)
7. [ ] Add `btnSave`, `btnExit`, `ubsCtrlInfo`
8. [ ] Wire click handlers in `.cs` file (stub only)

### Step 3 — Outer TabControl
9. [ ] Add `tabControl` (DockStyle.Fill, 6 tabs with correct Text labels)
10. [ ] Confirm build compiles

### Step 4 — Tab 1 Controls
11. [ ] Declare and configure all Tab 1 controls (GroupBox Сделка + currencies + GroupBox Стороны + commission row)
12. [ ] Set Anchor/initial Enabled state per VB6

### Step 5 — Tab 2 Controls
13. [ ] Declare and configure `lstViewOblig` (11 columns, 7 visible)
14. [ ] Declare Add/Edit/Delete buttons (Anchor=Top,Right)

### Step 6 — Tab 3 Controls (nested tabControlOblig)
15. [ ] Declare inner `tabControlOblig` (2 sub-tabs)
16. [ ] Sub-tab 1: all obligation detail controls including GroupBoxes
17. [ ] Sub-tab 2: `lstViewObject` (7 cols) + Add/Delete buttons
18. [ ] Declare `cmdApplayOblig`, `cmdExitOblig` (Anchor=Bottom,Right, Visible=false)

### Step 7 — Tab 4 Controls (nested tabControlInstr)
19. [ ] Declare inner `tabControlInstr` (2 sub-tabs: Seller / Buyer)
20. [ ] Declare all instruction controls as named pairs (`*_0`/`*_1`)
21. [ ] Set initial Enabled states

### Step 8 — Tab 5 Controls
22. [ ] Declare storage controls (CheckBox, 2 TextBox, 1 Button)

### Step 9 — Tab 6 Controls
23. [ ] Declare `ucpParam` control (Dock=Fill)

### Step 10 — Verify Build
24. [ ] Build project — expect 0 errors
25. [ ] Check ReadLints for 0 warnings

---

## 15. CREATIVE DECISIONS REQUIRED (before building)

| Decision | Options | Impact |
|----------|---------|--------|
| **Control array replacement** | Named pairs (`txtBIK_0/1`) + helper array | Designer + code-behind |
| **Tab names** | Confirm exact Russian tab captions (from screens or VB6 DOX) | Designer |
| **UbsCtrlAddFields class name** | Check existing references or ask | `.csproj` + Designer |
| **UbsCtrlDate class name/properties** | Check existing references | Designer |
| **Sub-form strategy** | Modal dialog vs inline for contract/object pickers | Out of scope for designer alone — stub button handler |

---

## 16. PLAN VERIFICATION CHECKLIST

```
✓ PLAN VERIFICATION CHECKLIST — Designer Conversion
- Requirements clearly documented?              YES
- Technology stack validated (controls mapped)?  YES
- Affected components identified (all 6 tabs)?  YES
- Implementation steps detailed (14 steps)?     YES
- Control arrays replacement strategy defined?  YES
- Anchor/resize strategy defined?               YES
- Missing references identified (UbsCtrlDecimal, Date, AddFields)? YES
- Creative decisions flagged?                   YES

→ Ready for BUILD (designer phase)
  after completing Phase 1 Prep (rename + add references)
```

---

## 17. REFERENCES

- Legacy source: `legacy-form/Pm_Trade/Pm_Trade_ud.dob` (lines 718–978 = Tab 1, 608–717 = Tab 2, 134–596 = Tab 3, 995–1498 = Tab 4 instr)
- Reference designer: `../OP/UbsOpRetoperFrm_CP/UbsOpRetoperFrm/UbsOpRetoperFrm.Designer.cs`
- `memory-bank/techContext.md` — control mapping table, channel contract
- `memory-bank/systemPatterns.md` — tab structure, state machine
