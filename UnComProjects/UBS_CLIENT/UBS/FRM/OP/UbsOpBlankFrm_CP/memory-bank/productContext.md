# Memory Bank: Product Context

## Product

- UBS client — OP (operations) module, "Принятая ценность" (accepted value) card/form.

## Users / Stakeholders

- OP users managing accepted values (дата принятия, серия, номер, наименование, вид, состояние).
- UBS system developers; UniSAB administrators.

## Constraints

- Must match legacy Blank_ud behavior and channel contract (Get_Data, Blank_Save, param names).
- Form must work within existing UBS host (ListKey/InitParamForm pattern).
