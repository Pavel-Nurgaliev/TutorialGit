# UbsOpRetoperFrm — Channel Contract

**Purpose:** Single reference for channel resource, commands, and param in/out. Align with legacy op_ret_oper.dob.

**Implementation rule:** Use explicit string literals in code for `.Run` commands and param keys. Do not use constants. Example: `UbsChannel_Run("Save")`, `UbsChannel_ParamIn("Id операции", m_idOper)`.

---

## Resource

- **LoadResource:** `VBS:UBS_VBS\OP\opers.vbs` (or ASM equivalent; set in Constants partial as `LoadResource`).
- Used by form for `"CheckCash"`, `"InitForm"`, `"GetOperParam"`, `"Save"`.

---

## Commands

| Command     | Direction | Purpose |
|------------|-----------|---------|
| **CheckCash**   | Form → channel | Pre-init validation; called before InitDoc. |
| **InitForm**    | Form → channel | Load operation data by Id операции. |
| **GetOperParam**| Form → channel | Get rate, commission params when valMinusCB changes. |
| **Save**        | Form → channel | Save operation (Retoper_Save). |

---

## InitForm

- **ParamIn:** `Тип операции` ("ret"), `initedDoc` (bool), `Id операции` (long).
- **ParamOut:**  
  `Error`, `Список операций`, `SID операции`, `Id операции`, `Id ценности`, `Id клиента`, `Список документов`, `Список валют`, plus DDX params (ФИО, Документ, Комиссия, Номер квитанции, Номинал, Курс за, Номер платежного документа, Номер документа, Платежный документ, Подоходный налог, Курс, Резидент, Серия платежного документа, Серия документа, Сумма выданная, Валюта номинала).

- **sidOper:** INCASH, INCASH_PAYDOC, EXPERT, EXPERT_PAYDOC — determines payMoney enable/disable.
- **payMoney:** If sidOper = EXPERT_PAYDOC → payMoney enabled, valMinusCB disabled; else payMoney disabled, valMinusCB enabled.

---

## GetOperParam

- **ParamIn:** `idPov`, `SID Операции` ("BUY"), `Валюта принятая` (valNom), `Валюта выданная` (valMinusCB ItemData), `Платежный документ` (idPayDoc), `SID операции`, `Операция` (from operArr).
- **ParamOut:** `Комиссия` (sumKom), `Комиссия в валюте` (isKomCur), `Комиссия в процентах` (isKomPer), `Курс`, `Курс за(единиц)`.

---

## Save

- **ParamIn:** DDX members (MembersValue) + `Валюта выданная`, `Валюта комиссии`, `Валюта принятая` (0), `Сумма принятая` (0), `Платежный документ` (0 or idPayDoc), `Номер квитанции(справки)` (idFOper), `SID операции`, `Операция`, `idClient`.
- If payMoney unchecked: override `Валюта выданная` = valNom, `Сумма выданная` = nomPDoc, `Платежный документ` = idPayDoc.
- **ParamOut:** `Error` (empty on success).

---

## Init Flow

- Form opened with **ListKey(param_in)**; param_in contains idOper (from list selection). Empty list → form closes with error "Список ценностей пуст.".
- **CheckCash** first; if Error → close.
- **InitDoc:** InitForm with Id операции; LoadFromParams; fill valMinusCB, valComis, docCB; set payMoney/valMinusCB enable/disable; focus valMinusCB or payMoney.

---

*Update this doc when contract changes. Code uses explicit literals for commands and params.*
