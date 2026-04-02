# CREATIVE: `cmbContractType1_Click`, `cmbContractType2_Click`

**Source:** `legacy-form/Pm_Trade/Pm_Trade_ud.dob` (~L1674–1844).

**Scope:** Changing **Продавец** / **Покупатель** contract type clears one side’s payment fields, toggles **broker vs bank** UX (commission, kind of supply, cash checkbox, nested payment tabs), clears or reloads contract pickers, and (type **2** only) refreshes **НДС / экспорт** visibility.

---

## 1. Control & symbol mapping (VB6 → .NET)

| VB6 | .NET |
|-----|------|
| `cmbContractType1` | `cmbContractType1` (seller — `lblSeller`) |
| `cmbContractType2` | `cmbContractType2` (buyer — `lblBuyer`) |
| `ItemData(ListIndex)` | `KeyValuePair<int,string>.Key` from bound items (`UbsPmTradeComboUtil.FillComboFrom2DArray`) |
| `cmdContract1` / `cmdContract2` | `btnContract1` / `btnContract2` |
| `txtContractCode1/2`, `txtClientName1/2` | same |
| `Label8` | `lblCommission` (commission label next to `cmbComission`) |
| `cmbComission` | `cmbComission` |
| `cmbKindSupplyTrade` | `cmbKindSupplyTrade` |
| `chkCash(0)` / `chkCash(1)` | `chkCash0` / `chkCash1` (buyer / seller on «Оплата») |
| `cmdListInstr(0/1)` | `linkListInstr0` / `linkListInstr1` |
| `lblCheckInstr(0/1)` | **Not present** in current `Designer.cs`; legacy forces `.Visible = True`. Either add labels later or treat as no-op if UX merged into links. |
| `SSActiveTabs1.Tabs(1)` / `Tabs(2)` | `tabControlInstr`: **Tab 1** = `tabPageInstr1` (**Покупатель**), **Tab 2** = `tabPageInstr2` (**Продавец**) — verified against legacy first panel using `Index = 0` controls vs second panel `Index = 1`. |
| `strRunParam` | `m_command` (compare to `CmdEdit`) |
| `intVidTrade` | `m_kindTrade` |
| `ClearOpl(g)` | Reuse payment clear: align with `ClearPayment(int index)` (already clears BIK/name/KS/RS/client/note/INN/not accept for index **0/1**). Legacy `ClearOpl` used `txtKS`/`txtRS`; .NET uses `ucaKS`/`ucaRS` — **same method**. |

**Side index reminder** (see `creative-trade-account-control-and-indexes.md`): `0` = покупатель, `1` = продавец.

---

## 2. Contract type keys (legacy semantics)

Combo rows come from channel **`Типы договоров`** (id + text). Legacy logic uses:

| `ItemData` | Role in these handlers |
|------------|-------------------------|
| **0** | «Банк» — сторона без выбора договора с клиентом: `FillControlContract` с member id **0**, кнопка договора **выкл.**, особая логика **одной** вкладки оплаты (см. §5). |
| **1** | «Брокерский договор с клиентом» — взаимные ограничения с другой стороной, касса, комиссия/вид поставки (см. §4). |
| **≠ 0** | «Нужен выбор договора»: очистка полей (кроме первого изменения в **EDIT** — см. §6), `cmdContract` **вкл.** |

Any id **≠ 0** participates in «оба таба оплаты видны»; только **0** включает режим одной вкладки.

---

## 3. `cmbContractType1_Click` → `cmbContractType1_SelectedIndexChanged` (or `SelectionChangeCommitted`)

**Legacy steps:**

1. **`ClearOpl(1)`** — очистить **продавца** (индекс **1**): `ClearPayment(1)`.
2. **`chkCash(1).Visible = False`** — `chkCash1.Visible = false` (и при показе кассы ниже: `Value = 0`).
3. **Брокер / банк / комиссия / вид поставки** (зависит от `ItemData` типа **1** и **2**):
   - Если **тип продавца = 1** и выбран тип покупателя (`ListIndex <> -1`):
     - `Label8` / `cmbComission` видимы, если **тип покупателя ≠ 0** (у другой стороны не «чистый банк»).
     - `cmbKindSupplyTrade`: текст **«обезличенная»**, `Enabled = False`.
   - Иначе если **тип продавца ≠ 1** и покупатель выбран:
     - Если **тип покупателя = 1**: комиссия видна, если **тип продавца ≠ 0**; вид поставки — «обезличенная», disabled.
     - Иначе: скрыть комиссию, включить выбор вида поставки.
   - Если **`m_kindTrade = 0`** и **тип продавца = 1**: показать **`chkCash1`**, снять галку.
4. **Инструкции:** если `lblCheckInstr(1)` / `cmdListInstr(1)` не видны — сделать видимыми → в .NET: обеспечить видимость **`linkListInstr1`** (и `lblCheckInstr` при появлении контрола).
5. **Вкладки оплаты `SSActiveTabs1`** (см. §5) от **`ItemData` типа продавца**.
6. **Поля договора продавца:**
   - Если **тип продавца ≠ 0**:
     - Не **EDIT** или режим очистки — обнулить код/имя; **`btnContract1.Enabled = true`**.
     - **EDIT** + `blnClearContr1`: то же; иначе первый раз только **`blnClearContr1 = true`** без очистки.
   - Если **тип продавца = 0**:
     - `DDX.MemberData("Продавец") = 0`
     - **`FillControlContract(..., txtContractCode1, txtClientName1, IdClient1)`**
     - **`btnContract1.Enabled = false`**
     - Сброс комиссии `ListIndex = -1`; если `blnCheckType2` и **тип покупателя = 1**, выбрать в комбо комиссию с `ItemData = IdComission2`.
     - **`blnClearContr1 = True`**

**Channel:** нет прямых `Run` в этом обработчике.

**Не вызывает** в VB: `UpdateDisplayNDS` / `UpdateDisplayExport` — **не добавлять** в type1 для паритета (изменение клиента 2 идёт через type2 и договор).

---

## 4. `cmbContractType2_Click` → `cmbContractType2_SelectedIndexChanged`

**Legacy steps:**

1. **`ClearOpl(0)`** — `ClearPayment(0)`.
2. **`chkCash(0).Visible = False`**.
3. Симметричная ветвилка **брокер / комиссия / вид поставки**, но опорная сторона — **тип покупателя**:
   - **Тип покупателя = 1**: комиссия видна, если **тип продавца ≠ 0**; вид поставки «обезличенная» + disabled; при **`m_kindTrade = 0`** показать **`chkCash0`**, снять.
   - **Иначе**: если **тип продавца = 1** — зеркально (комиссия от **типа покупателя ≠ 0**); иначе скрыть комиссию и включить вид поставки.
4. Видимость **`linkListInstr0`** (и legacy `lblCheckInstr(0)`).
5. **Вкладки оплаты** от **`ItemData` типа покупателя** (§5).
6. **Поля договора покупателя** — зеркально type1: `Покупатель`, `IdClient2`, **`FillControlContract`**, **`btnContract2`**, комиссия по **`IdComission1`** если **тип продавца = 1**, иначе `-1`; **`blnClearContr2 = True`**.
7. **`UpdateDisplayNDS`** и **`UpdateDisplayExport`** — в .NET вызвать существующие методы (после порта `UpdateDisplayNDS`, если ещё заглушка — задокументировать порядок вызова).

**Channel (косвенно):** через `UpdateDisplayExport` → `IsClientNotResident` при уже заданном `m_idClient2`.

---

## 5. Логика `tabControlInstr` (замена `SSActiveTabs1`)

Оба обработчика задают видимость **двух** вкладок и выбранную.

| Условие | Действие |
|---------|----------|
| **Тип продавца (type1) `ItemData = 0`** | Видна только **`tabPageInstr2`**, выбрана она; **`tabPageInstr1`** скрыта. |
| **Тип продавца ≠ 0** | Обе вкладки видны, выбрана **`tabPageInstr1`**. |
| **Тип покупателя (type2) `ItemData = 0`** | Видна только **`tabPageInstr2`**, выбрана она; **`tabPageInstr1`** скрыта. |
| **Тип покупателя ≠ 0** | Обе видны, выбрана **`tabPageInstr1`**. |

**WinForms:** у `TabPage` нет штатного `Visible = false` в старых версиях так же, как у VB — использовать **`TabPage.Hide()` / показ через `tabControl.TabPages.Add/Remove`** или **`Enabled` + переключение**, как принято в проекте; зафиксировать один способ (часто **временно убирать страницу из `TabPages`**), чтобы не оставлять пустой заголовок.

---

## 6. Состояние EDIT: `blnClearContr1` / `blnClearContr2`

При **`m_command == CmdEdit`** первое изменение типа **не** очищает код/клиента; со второго — очистка. Нужны флаги **`m_clearContr1Pending`**, **`m_clearContr2Pending`** (или имена как в VB).

---

## 7. Нереализованные зависимости (сделать до/вместе)

| Элемент | Назначение |
|---------|------------|
| **`FillControlContract`** | Загрузка кода/названия договора и id клиента для стороны «банк». |
| **`m_idClient1`**, **`m_idClient2`** | Уже частично: `m_idClient2` для экспорта; дополнить **`m_idClient1`** при заполнении продавца. |
| **`IdComission1`**, **`IdComission2`**, **`blnCheckType2`** | Восстановление выбранной комиссии после смены типа. |
| **`UpdateDisplayNDS`** | Видимость `chkNDS` — отдельный порт; вызывать из **type2** после изменения типов/контракта. |
| **`Text «обезличенная»`** | Согласовать с `FillCombos`: ключ **1** для `cmbKindSupplyTrade`. |

---

## 8. Событие WinForms

- Для `DropDownList` надёжнее **`SelectedIndexChanged`** или **`SelectionChangeCommitted`**, не `Click` (избежать лишних срабатываний при инициализации — при необходимости флаг **`m_suppressContractTypeEvents`** на время `FillCombos` / загрузки сделки).

---

## 9. Ошибки

Оба обработчика: **`try` / `catch` → `this.Ubs_ShowError(ex)`**.

---

## 10. Чеклист реализации

- [ ] Вспомогательные методы: ключ выбранного типа (с проверкой `SelectedIndex >= 0`), установка вида поставки «обезличенная», видимость комиссии, `ClearPayment(0/1)`, переключение `tabControlInstr`.
- [ ] Реализовать/вызвать **`FillControlContract`** и обновление **`m_idClient1` / `m_idClient2`** для веток `ItemData = 0`.
- [ ] Флаги **`blnClearContr*`** и логика **EDIT**.
- [ ] Подключить **`UpdateDisplayNDS`** + **`UpdateDisplayExport`** только в обработчике **типа 2**.
- [ ] Провести **SelectedIndex = -1** (ничего не выбрано): не обращаться к `ItemData` без проверки.
