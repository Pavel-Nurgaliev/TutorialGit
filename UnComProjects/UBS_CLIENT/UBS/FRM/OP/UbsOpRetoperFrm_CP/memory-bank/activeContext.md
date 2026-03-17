# Memory Bank: Active Context

**Last updated:** 2026-03-17

## Current Focus

**Task:** Convert op_ret_oper (VB6) → UbsOpRetoperFrm (.NET WinForm)

**Phase:** BUILD complete. Phase 1–2 implemented. Ready for `/reflect` and manual build verification.

## Key Decisions

- **Decimal controls:** UbsControlMoney → UbsCtrlDecimal (8 fields)
- **Date controls:** Not applicable (no date fields in op_ret_oper)
- **Layout:** Option D (Hybrid) — GroupBox + content TableLayoutPanel + bottom strip
- **ComboBox:** KeyValuePair&lt;int, string&gt; + DataSource (align with UbsOpBlankFrm)
- **Data binding:** Explicit LoadFromParams / BuildSaveParams (no DDX)

## Reference Projects

- UbsOpCommissionFrm_CP — structure, memory-bank
- UbsOpBlankFrm_CP — structure, UbsCtrlDate, UbsCtrlInfo
- UbsBgContractFrm_CP — UbsCtrlDecimal usage
