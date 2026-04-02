# Memory Bank: Product Context

## Why This Project Exists

This project is part of a systematic migration of all UBS (UniSAB) VB6 client-side UserDocument forms to .NET Framework 2.0 WinForms. The PM (Portfolio Management) module handles trades in precious metals and financial instruments.

The `Pm_Trade` (Trade/Сделка) form is the primary data-entry screen for creating and editing PM trades. It is a complex, multi-tab form that manages:
- Both sides of a trade (buyer/seller contracts and payment instructions)
- Trade obligations (delivery schedule with amounts, dates, currencies)
- Precious metals characteristics per obligation
- Storage/warehouse information
- Additional parameters (AddFields)

## Business Domain

**Trade (Сделка):** A PM trade is a bilateral agreement between two parties (buyer and seller) for delivery of precious metals or other PM assets. The trade record tracks:
- **Trade identity:** Number, date, type (Вид сделки: 0=securities/ценные бумаги, 1=precious metals/драгметаллы)
- **Composite flag (chkIsComposit):** Whether the trade is a composite (составная) trade with a direction (Направление)
- **Contracts:** Buyer (Покупатель) and Seller (Продавец), each with contract type (cmbContractType1/2)
- **Currencies:** Delivery currency (ВалютаПоставки / cmbCurrencyPost), payment currency (ВалютаОплаты / cmbCurrencyOpl)
- **Kind of supply (Вид поставки):** Физическая (physical) or Переводная (transfer)
- **Commission:** Optional commission record (cmbComission)
- **NDS/Export flags:** VAT and export markings (chkNDS, chkExport)

**Obligations (Обязательства):** Each trade may have one or more delivery obligations, each defining:
- Direction (Направление): Купить/Продать
- Dates: delivery date (ДатаОплаты), posting date (ДатаПоставки)
- Price per unit in obligation currency, mass, total sum
- Obligation currency (ВалютаОбязательств)
- Rate flag (chkRate): whether to use a manual exchange rate
- For physical delivery: objects list (precious metals items)
- For transfer delivery: object list with assay marks (проба) and masses

**Payment Instructions (ИнструкцияОплаты):** Each side of a trade has payment instructions:
- BIK (bank code), RS (account), KS (correspondent account)
- Client name, note, INN
- NotAkcept flag (chkNotAkcept)
- Cash flag (chkCash): load cash instructions via GetInstructionOplataCash

## User Roles & Workflow

1. User opens the trade form from a list (grid) in EDIT mode (existing trade) or ADD mode (new trade).
2. In ADD mode: selects contracts, fills dates, currencies, and obligations.
3. In EDIT mode: views/modifies existing trade data (locked fields if operations exist for the trade).
4. Save sends data to `ModifyTrade` channel command.
5. On success: form closes and parent grid refreshes (blnNeedRefresh, SelectItem).

## Problem Being Solved

The legacy VB6 UserDocument runs in an IE-hosted ActiveX environment which is being phased out. The .NET WinForm version:
- Integrates with the same UBS channel backend (same server-side VBS contract)
- Uses modern .NET controls instead of ActiveX OCX controls
- Can be hosted in the new .NET-based UBS shell
