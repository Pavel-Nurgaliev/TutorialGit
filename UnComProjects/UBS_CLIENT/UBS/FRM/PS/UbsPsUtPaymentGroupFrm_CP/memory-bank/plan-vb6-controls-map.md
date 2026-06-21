# PLAN: VB6 → .NET Control Map — UtPaymentGroup_F

Source: `legacy-form\UtPaymentGroup\UtPaymentGroup_F.dob`  
Target class: `UbsPsUtPaymentGroupFrm` (`namespace UbsBusiness`)  
Naming: workspace style-guide (Hungarian prefixes: `btn`, `txt`, `lbl`, `cmb`, `grp`, `tab`/`tabPage`, `tbl`, `uci`, `uca`, `udc`, `ucf`).

## 1. Non-visual / host (no WinForms counterpart on surface)

| VB6 | .NET / host | Notes |
|-----|-------------|--------|
| `UbsChannel` (UbsChlCtrl) | `base.IUbsChannel` / channel API on `UbsFormBase` | VB6 had a hidden control; .NET uses base channel. `LoadResource`, `Run`, `ParamIn`/`ParamOut` as in other PS forms. |

## 2. Root-level (outside tab strip in VB6)

| VB6 name | VB6 type | .NET type | C# field name | Notes |
|-----------|----------|-----------|---------------|--------|
| `btnSave` | CommandButton | `Button` | `btnSave` | Caption: Сохранить |
| `btnExit` | CommandButton | `Button` | `btnExit` | Caption: Выход |
| `subPayment` | SSActiveTabs | `TabControl` | `tabPayment` | Two tabs (see below) |

## 3. Tab structure (SSActiveTabs → TabControl)

| VB6 | Role | .NET | C# field name | Tab text (legacy) |
|-----|------|------|---------------|-------------------|
| `SSActiveTabPanel1` | Main data | `TabPage` | `tabPageMain` | Основные |
| `SSActiveTabPanel2` | Additional properties | `TabPage` | `tabPageAddProperties` | Дополнительные свойства |

Designer order: add `tabPageMain` first, then `tabPageAddProperties`, so **index 0 = Main**, **index 1 = Additional** (verify against VB6 `Tabs(1)`/`Tabs(2)` semantics when porting).

## 4. Tab “Дополнительные свойства”

| VB6 name | VB6 type | .NET type | C# field name | Notes |
|-----------|----------|-----------|---------------|--------|
| `AddProperties` | UbsControlProperty | `UbsCtrlFields` | `ucfAddProperties` | Register with `UbsCtrlFieldsSupportCollection.Add` (tab title string in Constants). Stub wiring: `IUbsAddFiledsStub` / base pattern from VB6 `InitChannel`. |

## 5. Tab “Основные” — Group “Плательщик” (`frmSend`)

| VB6 name | VB6 type | .NET type | C# field name | Notes |
|-----------|----------|-----------|---------------|--------|
| `frmSend` | Frame | `GroupBox` | `grpPayer` | Caption: Плательщик |
| `txtFIOPay` | TextBox | `TextBox` | `txtFIOPay` | MaxLength 70 |
| `btnClient` | CommandButton | `Button` | `btnClient` | Caption: ... |
| `txtINNPay` | TextBox | `TextBox` | `txtINNPay` | MaxLength 12 |
| `txtAdressPay` | TextBox | `TextBox` | `txtAdressPay` | MaxLength 255 |
| `txtInfoClient` | TextBox | `TextBox` | `txtInfoClient` | ReadOnly / Enabled false |
| `txtNomerCardPay` | TextBox | `TextBox` | `txtNomerCardPay` | Enter → card read |
| `Label5` | Label | `Label` | `lblFIO` | Caption: ФИО |
| `Label1` | Label | `Label` | `lblINNPayer` | Caption: ИНН (payer column) |
| `Label15` | Label | `Label` | `lblAddressPay` | Caption: Адрес |
| `Label4` | Label | `Label` | `lblRequisites` | Caption: Реквизиты |
| `lblNomerCardPay` | Label | `Label` | `lblNomerCardPay` | Caption: Номер карты |

## 6. Tab “Основные” — Group “Получатель” (`frmRec`)

| VB6 name | VB6 type | .NET type | C# field name | Notes |
|-----------|----------|-----------|---------------|--------|
| `frmRec` | Frame | `GroupBox` | `grpRecipient` | Caption: Получатель |
| `cmbCode` | ComboBox (Dropdown List) | `ComboBox` | `cmbCode` | `DropDownStyle = DropDownList`; display + `Tag`/item data for contract id |
| `btnListAttributeRecip` | CommandButton | `Button` | `btnListAttributeRecip` | Caption: ... |
| `btnSaveAttribute` | CommandButton | `Button` | `btnSaveAttribute` | Caption: C |
| `txtComment` | TextBox | `TextBox` | `txtComment` | ReadOnly (contract description) |
| `txtBic` | TextBox | `TextBox` | `txtBic` | |
| `AccKorr` | UbsControlAccount | `UbsCtrlAccount` | `ucaAccKorr` | Often read-only |
| `txtNameBank` | TextBox | `TextBox` | `txtNameBank` | ReadOnly |
| `AccClient` | UbsControlAccount | `UbsCtrlAccount` | `ucaAccClient` | |
| `txtINN` | TextBox | `TextBox` | `txtINN` | MaxLength 12 |
| `cmbPurpose` | UbsComboEditControl | `ComboBox` | `cmbPurpose` | `DropDownStyle` DropDown or DropDownList per CREATIVE (editable legacy) |
| `txtRecip` | TextBox | `TextBox` | `txtRecip` | MaxLength 160 |
| `Label2` | Label | `Label` | `lblContractCode` | Caption: Код договора |
| `Label11` | Label | `Label` | `lblBic` | Caption: БИК |
| `Label12` | Label | `Label` | `lblCorrAccount` | Caption: Корр. счет |
| `Label13` | Label | `Label` | `lblBankName` | Caption: Наименование банка |
| `Label16` | Label | `Label` | `lblSettleAccount` | Caption: Р/с |
| `Label14` | Label | `Label` | `lblINNRecipient` | Caption: ИНН |
| `Label3` | Label | `Label` | `lblPurpose` | Caption: Назначение |
| `lblRecip` | Label | `Label` | `lblRecip` | Caption: Наименование получателя |

## 7. Tab “Основные” — Amounts and status row (still inside tab in VB6)

| VB6 name | VB6 type | .NET type | C# field name | Notes |
|-----------|----------|-----------|---------------|--------|
| `curSumma` | UbsControlMoney | `UbsCtrlDecimal` | `udcSumma` | Editable; `TextChange` → commission |
| `curPeny` | UbsControlMoney | `UbsCtrlDecimal` | `udcPeny` | Legacy often enabled/disabled by mode |
| `curSummaRateSend` | UbsControlMoney | `UbsCtrlDecimal` | `udcSummaRateSend` | Read-only calculated |
| `curSummaTotal` | UbsControlMoney | `UbsCtrlDecimal` | `udcSummaTotal` | Read-only calculated |
| `Label6` | Label | `Label` | `lblSumma` | Caption: Сумма |
| `Label9` | Label | `Label` | `lblPeny` | Caption: Сумма пени |
| `Label8` | Label | `Label` | `lblCommission` | Caption: Комиссия с плательщика |
| `Label7` | Label | `Label` | `lblTotal` | Caption: Итого к оплате |
| `Info` | UbsInfo32.Info | `UbsCtrlInfo` | `uciInfo` | **Layout:** VB6 placed on main tab; UbsFormTemplate often uses bottom `tblActions` + one `UbsCtrlInfo`. CREATIVE: single `uciInfo` in bottom bar vs duplicate messaging — prefer one control to match template. |

## 8. Bottom command bar (template)

| Purpose | .NET type | C# field name | Notes |
|---------|-----------|---------------|--------|
| Bottom strip | `TableLayoutPanel` | `tblActions` | Dock Bottom; same row as Save/Exit + info |
| (from template) | `Button` | `btnSave`, `btnExit` | Already at root mapping; must not duplicate if only one pair |
| Status line | `UbsCtrlInfo` | `uciInfo` or reuse name | Align with section 7 note |

**VB6 layout:** `btnSave`/`btnExit` are **siblings** of `subPayment`, not inside the tab. **Match:** `panelMain` contains `tabPayment` (Fill) and `tblActions` (Bottom) with `btnSave`, `btnExit`, and `uciInfo` — same as `UbsPsContractFrm`-style template.

## 9. Property mapping quick reference (VB6 → C#)

| VB6 pattern | .NET (UbsCtrlDecimal / account) |
|-------------|----------------------------------|
| `curSumma.CurrencyValue` | `udcSumma` value property per `UbsCtrlDecimal` API (e.g. `Value` / `DecimalValue` — confirm from reference DLL usage in `UbsPsContractFrm`) |
| `AccClient.Text` | `ucaAccClient.Text` (or documented property name) |
| `cmbCode.ItemData(i)` | `ComboBox` + `Items[i]` as wrapper with id, or separate `m_contractIds[]` |

## 10. Summary checklist (control count)

- **TabControl:** 1 (`tabPayment`)  
- **TabPage:** 2  
- **GroupBox:** 2 (`grpPayer`, `grpRecipient`)  
- **Button:** 5 (`btnSave`, `btnExit`, `btnClient`, `btnListAttributeRecip`, `btnSaveAttribute`)  
- **TextBox:** 10 (`txtFIOPay`, `txtINNPay`, `txtAdressPay`, `txtInfoClient`, `txtNomerCardPay`, `txtComment`, `txtBic`, `txtNameBank`, `txtINN`, `txtRecip`)  
- **ComboBox:** 2 (`cmbCode`, `cmbPurpose`)  
- **Label:** 17 (renamed from `Label*` + `lblRecip` + `lblNomerCardPay`)  
- **UbsCtrlAccount:** 2 (`ucaAccKorr`, `ucaAccClient`)  
- **UbsCtrlDecimal:** 4 (`udcSumma`, `udcPeny`, `udcSummaRateSend`, `udcSummaTotal`)  
- **UbsCtrlFields:** 1 (`ucfAddProperties`)  
- **UbsCtrlInfo:** 1 (`uciInfo`)  
- **TableLayoutPanel:** 1 (`tblActions`)  

## 11. Code migration rename table (when VB6 used generic label names)

Use this when porting line-by-line from VB6:

| VB6 | C# (preferred) |
|-----|----------------|
| `Label1` | `lblINNPayer` |
| `Label5` | `lblFIO` |
| `Label15` | `lblAddressPay` |
| `Label4` | `lblRequisites` |
| `Label2` | `lblContractCode` |
| `Label11` | `lblBic` |
| `Label12` | `lblCorrAccount` |
| `Label13` | `lblBankName` |
| `Label16` | `lblSettleAccount` |
| `Label14` | `lblINNRecipient` |
| `Label3` | `lblPurpose` |
| `Label6` | `lblSumma` |
| `Label9` | `lblPeny` |
| `Label8` | `lblCommission` |
| `Label7` | `lblTotal` |
| `frmSend` | `grpPayer` |
| `frmRec` | `grpRecipient` |
| `subPayment` | `tabPayment` |
| `curSumma` | `udcSumma` |
| `curPeny` | `udcPeny` |
| `curSummaRateSend` | `udcSummaRateSend` |
| `curSummaTotal` | `udcSummaTotal` |
| `AccKorr` | `ucaAccKorr` |
| `AccClient` | `ucaAccClient` |
| `AddProperties` | `ucfAddProperties` |
| `Info` | `uciInfo` |

---

**Next:** CREATIVE fixes exact tab order, `uciInfo` placement, and `cmbPurpose` drop-down style; BUILD declares fields in `Designer.cs` using the C# names above.
