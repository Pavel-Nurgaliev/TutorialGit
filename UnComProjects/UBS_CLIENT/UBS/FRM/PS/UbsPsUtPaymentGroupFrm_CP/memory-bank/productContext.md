# Product Context: UbsPsUtPaymentGroupFrm

## Form Purpose
The **UtPaymentGroup_F** form is a Group Payment entry form in the UBS banking system's PS (Payment Services) module. It allows operators to create and edit grouped payment transactions.

## Form Layout (from screenshots)

### Tab 1: "Основные" (Main)
Two framed sections plus monetary fields:

#### Frame "Плательщик" (Payer)
| Label | Control | VB6 Name | Type | Notes |
|-------|---------|----------|------|-------|
| ФИО | TextBox | txtFIOPay | MaxLen 70 | Payer full name |
| ... | Button | btnClient | | Opens client selection list |
| ИНН | TextBox | txtINNPay | MaxLen 12 | Payer INN (tax ID) |
| Адрес | TextBox | txtAdressPay | MaxLen 255 | Payer address |
| Реквизиты | TextBox | txtInfoClient | Disabled | Client requisites info |
| Номер карты | TextBox | txtNomerCardPay | | Card number (Enter triggers lookup) |

#### Frame "Получатель" (Recipient)
| Label | Control | VB6 Name | Type | Notes |
|-------|---------|----------|------|-------|
| Код договора | ComboBox | cmbCode | DropdownList | Contract code selector |
| ... | Button | btnListAttributeRecip | | Opens recipient attribute list |
| C | Button | btnSaveAttribute | | Saves recipient attributes |
| БИК | TextBox | txtBic | | Bank BIC code |
| Корр. счет | UbsControlAccount | AccKorr | Disabled | Correspondent account |
| Наименование банка | TextBox | txtNameBank | Disabled | Bank name (auto-filled) |
| Р/с | UbsControlAccount | AccClient | Enabled | Client account |
| ИНН | TextBox | txtINN | MaxLen 12 | Recipient INN |
| Назначение | UbsComboEditControl | cmbPurpose | | Payment purpose |
| Наименование получателя | TextBox | txtRecip | MaxLen 160 | Recipient name |

#### Monetary Fields
| Label | Control | VB6 Name | Type |
|-------|---------|----------|------|
| Сумма | UbsControlMoney | curSumma | Enabled, triggers commission calc |
| Сумма пени | UbsControlMoney | curPeny | Enabled |
| Комиссия с плательщика | UbsControlMoney | curSummaRateSend | Disabled (calculated) |
| Итого к оплате | UbsControlMoney | curSummaTotal | Disabled (calculated) |

#### Status & Buttons
- Info (UbsInfo) — status message bar
- btnSave — "Сохранить" (Save)
- btnExit — "Выход" (Exit)

### Tab 2: "Дополнительные свойства" (Additional Properties)
- AddProperties (UbsControlProperty) — dynamic additional fields

## Business Workflow
1. **Form opens** → `InitFormGroup` channel call
2. **Payer entry**: Manual FIO/INN/Address or via Client List (`btnClient`) or Card Number lookup
3. **Contract selection**: via `cmbCode` dropdown → `UtReadContract` fills recipient fields
4. **Recipient attributes**: can browse/save via dedicated list (`btnListAttributeRecip`)
5. **Amount entry**: triggers commission calculation (`CalcSumCommiss`)
6. **Validation on Save**: CheckPayment → CheckLockPassport → CheckIPDL → CheckTerror → Ut_CheckUserBeforeSave
7. **Save**: `Payment_Save` channel call
8. **Group cycle**: after save, prompts "Add another payment to group?" → if Yes, runs `PsPaymentIncomingReception.vbs` script

## Commands (StrCommand values)
- `ADD` — new payment in group
- `GROUP_EDIT` — editing existing group payment
- `GROUP_ADD` — adding to existing group
- `CHANGE_PART` — partial change
- `READ` — reading payment data
- `CLEAR` — clearing form for new entry
- `CHANGECONTRACT` — contract changed notification
