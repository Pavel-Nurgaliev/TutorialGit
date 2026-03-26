# CREATIVE: `SSTabs_BeforeTabClick` → main `tabControl` tab guard

**Source:** `legacy-form/Pm_Trade/Pm_Trade_ud.dob` (`Private Sub SSTabs_BeforeTabClick`, ~L2546–2659).

**Scope:** Before switching the **main** six-way tab strip (`SSTabs`), validate prerequisites, optionally cancel the switch, adjust obligation sub-UI (`SSTabs1` / `tabControlOblig`), and toggle **Save/Exit** visibility. Does **not** include **`SSTabs1_BeforeTabClick`** (nested «Обязательство» / «Объекты») — separate small handler (~L2662–2673).

---

## 1. Control mapping — main `SSTabs` → `tabControl`

Legacy **`NewTab.Index`** is **1-based**. **`tabControl.SelectedIndex`** is **0-based**.

| Legacy `NewTab.Index` | Tab caption (legacy intent) | .NET `tabControl` |
|----------------------|----------------------------|-------------------|
| 1 | (first — «Основные») | `tabPage1` «Основные» — `SelectedIndex == 0` |
| 2 | «Обязательства» | `tabPage2` — **`1`** |
| 3 | «Данные» | `tabPage3` — **`2`** |
| 4 | «Поставка» | `tabPage4` — `3` |
| 5 | «Оплата» | `tabPage5` — `4` |
| 6 | «Дополнительные» | `tabPage6` — `5` |

**Detection in handler:** compare **`e.TabPage`** to `tabPage2` / `tabPage3` (or map `e.TabPageIndex` / `tabControl.TabPages.IndexOf(e.TabPage)` to the indices above).

---

## 2. WinForms event (cancel before switch)

Use **`tabControl.Selecting`** with **`TabControlCancelEventArgs`**: set **`e.Cancel = true`** to block the tab change (replaces `Cancel = True` on `SSReturnBoolean`).

**Note:** `Selecting` fires when the user (or code) attempts to select a tab; keep logic idempotent and avoid recursion if the handler sets `SelectedIndex` programmatically (may need a **`m_suppressMainTabSelecting`** flag).

---

## 3. Behaviour blocks (legacy order)

### 3.1 Always first — matching contract types

If **`cmbContractType1`** and **`cmbContractType2`** selected **keys are equal** (legacy `ItemData`):

- Message: «Тип договора покупателя и продавца не может совпадать» (title «Проверка свойств», critical).
- Focus **`cmbContractType1`**.
- **Cancel** tab change.

**Implementation:** use **`UbsPmTradeComboUtil.TryGetSelectedKey`** for both; if either has no selection, skip this check or treat as invalid per product rules (legacy used `ListIndex` without guard — safer to require both selected).

---

### 3.2 Target is «Обязательства» (`NewTab.Index = 2` → `tabPage2`)

1. **Trade date:** if unset / invalid (legacy `txtTradeDate.DateValue = #1/1/2222#` or `Not IsValid`):
   - «Не указана дата сделки»
   - If current tab is «Основные» (`SSTabs.Tabs(1).Selected`), focus **`dateTrade`**.
   - **Cancel.**

2. **Metal (поставка):** if **`cmbCurrencyPost.SelectedIndex < 0`**:
   - «Не выбран драг.металл.»
   - Focus **`cmbCurrencyPost`**, **Cancel.**

3. **Buyer contract:** if **`DDX.MemberData("Покупатель")` empty** (buyer **contract id** not chosen):
   - «Не выбран покупатель»
   - Focus **`btnContract2`** if enabled, else **`cmbContractType2`** if enabled.
   - **Cancel.**

   **.NET:** introduce / use **`m_idContractBuyer`** (or equivalent) set when user picks buyer contract; treat **0 / unset** as empty. Do not confuse with **`m_idClient2`** (client id from `GetContractPM`).

4. **Kind of supply:** if **`cmbKindSupplyTrade`** has no selection (`ListIndex = -1`):
   - «Не выбран вид поставки по сделке»
   - Focus **`cmbKindSupplyTrade`**, **Cancel.**

5. **Side effects (no cancel):** adjust mass/unit enablement from kind text:
   - If display **«физическая»** (or key **2** per `FillCombos`): **`cmbUnit.SelectedIndex = 0`**, **`cmbUnit.Enabled = false`**, **`ucdMass.Enabled = false`** (legacy `txtMassa`).
   - If **«обезличенная»** (key **1**): **`ucdMass.Enabled = true`**, **`cmbUnit.Enabled = true`**.

---

### 3.3 Target is «Данные» (`NewTab.Index = 3` → `tabPage3`)

Only if **`lvwObligation.Items.Count != 0`**:

- **`btnSave` / `btnExit`**: **`Visible = false`**.
- If **not** add/edit obligation mode (`Not blnAddOblig And Not blnEditOblig`) — **view** mode:
  - **`cmdApplayObligation` / `cmdExitObligation`**: **`Visible = false`**.
  - Disable obligation editors: legacy **`EnableWindow(SSTabs1Panel1/4.hwnd, 0)`** → set **`tabPageOblig1.Enabled = false`** and **`tabPageOblig2.Enabled = false`** (or disable **`tabControlOblig`** / inner containers so children gray out).
  - **`cmdAddObject` / `cmdDelObject`**: **`Enabled = false`**.
  - Select nested **`tabControlOblig`** first page: **`tabPageOblig1`** (legacy `SSTabs1.Tabs(1).Selected = True`).
  - **`StrNumInPart`** from **`lvwObligation.SelectedItem.SubItems(1)`** (column «Номер в части»); **`sType = "Edit"`**; **`CallOblig`** — **separate port** (channel + fill subform).
- **Else** (add/edit obligation):
  - **`cmdApplayObligation` / `cmdExitObligation`**: **`Visible = true`**.
  - **`tabPageOblig1.Enabled = true`**, **`tabPageOblig2.Enabled = true`** (re-enable panels).
  - If kind **«физическая»**: **`cmdAddObject.Enabled = true`**, **`cmdDelObject.Enabled = true`**.

---

### 3.4 Any other main tab (not «Данные»)

- **`btnSave.Visible = true`**, **`btnExit.Visible = true`**.

---

### 3.5 Payment sub-tabs (`SSActiveTabs1`) — sync with contract types

Runs **after** the above for **every** successful (non-cancelled) navigation attempt — legacy still runs when not cancelled; if cancelled, exit before this block.

Mirror **`cmbContractType1/2_Click`** payment tab rules (see `creative-cmb-contract-type-click.md`):

| Condition | Action |
|-----------|--------|
| Type1 key **== 0** | Ensure **`tabPageInstr1`** visible, select it; hide **`tabPageInstr2`** (or `TabPages` remove pattern already used). |
| Else type2 key **== 0** | **`tabPageInstr2`** only, selected. |
| Else | Both instruction tabs; select **`tabPageInstr1`**. |

Prefer calling existing **`SetPaymentInstrTabsSellerOnly`** / **`SetPaymentInstrTabsBothBuyerSelected`** (or a shared helper) to avoid drift.

---

## 4. Channel contract

| Call | ParamIn | ParamOut |
|------|---------|----------|
| *(none inside `SSTabs_BeforeTabClick` itself)* | — | — |

**`CallOblig`** (invoked when entering «Данные» with list non-empty in view mode) is documented with the obligation subsystem / creative for `CallOblig`.

---

## 5. Error handling

Legacy: **`On Error` → `UbsErrMsg "SSTabs_BeforeTabClick", "ошибка выполнения"`**.

**.NET:** wrap handler body in **`try` / `catch` → `this.Ubs_ShowError(ex)`**; on failure, consider **`e.Cancel = true`** to avoid partial tab state.

---

## 6. User-facing strings (`UbsPmTradeFrm.Constants.cs`)

Add named constants for all **`MsgBox`** texts and titles used in §3 (explicit literals per style rule), including:

- «Тип договора покупателя и продавца не может совпадать»
- «Не указана дата сделки»
- «Не выбран драг.металл.»
- «Не выбран покупатель»
- «Не выбран вид поставки по сделке»

Titles: «Проверка свойств» / «Ошибка ввода» as in VB (`vbCritical` vs `vbExclamation` where different).

---

## 7. Related: `SSTabs1_BeforeTabClick` (optional same pass)

**Source:** ~L2662–2673. When switching nested tab to **«Объекты»** (`NewTab.Index = 4` in **nested** 1-based control — map to **`tabPageOblig2`**):

- Set **`lblObligationInfo1` / `lblObligationInfo2`** captions from obligation fields (legacy `txtDateOpl`/`txtDatePost` → **`datePayment`** / **`datePost`**; `txtCostUnit` → **`ucdCostUnit`**; `txtMassa` → **`ucdMass`**; `txtSumOpl` → **`ucdSumPayment`** or **`ucdSumObligation`** per legacy meaning — confirm against `Pm_Trade` field mapping).

---

## 8. Implementation checklist

- [ ] Wire **`tabControl.Selecting`**, map target page to legacy rules (especially **`tabPage2`** / **`tabPage3`**).
- [ ] Implement §3.1–3.4 with **`e.Cancel`**, focus, and **`MessageBox.Show`** (icons match VB).
- [ ] Add **`m_idContractBuyer`** (or bind from existing contract picker) for §3.2.3.
- [ ] Add **`m_blnAddOblig` / `m_blnEditOblig`** (or equivalent) for §3.3 branching.
- [ ] Port **`CallOblig`** or stub until obligation editor load exists.
- [ ] Reuse **`SetPaymentInstrTabs*`** for §3.5 after a **non-cancelled** select (or only when switch proceeds — match legacy: runs when handler does not `Exit Sub` early from cancel; if `Selecting` cancels, the rest of legacy sub is skipped — so only run §3.5 when **not** cancelling).
