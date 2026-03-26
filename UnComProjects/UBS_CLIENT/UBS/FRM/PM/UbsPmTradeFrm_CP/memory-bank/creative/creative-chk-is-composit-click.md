# CREATIVE: `chkIsComposit_Click` → `chkComposit_CheckedChanged`

**Source:** `legacy-form/Pm_Trade/Pm_Trade_ud.dob` (`Private Sub chkIsComposit_Click`, ~L1598–1648).

**Scope:** Toggling the «Составная?» flag controls trade direction (`cmbNaprTrade` / `cmbTradeDirection`) and, when turning composite **off**, optionally removes «обратная» obligations and related in-memory obligation parameters.

---

## 1. Channel contract

| Call | ParamIn | ParamOut |
|------|---------|----------|
| *(none in legacy handler)* | — | — |

This handler does **not** call the channel. It only updates WinForms controls, `objParamOblig`-equivalent state, list rows, and the legacy `bNeedSendOblig` flag.

---

## 2. Control / data mapping (VB6 → .NET)

| VB6 | .NET (current project) |
|-----|-------------------------|
| `chkIsComposit` | `chkComposit` |
| `cmbNaprTrade` | `cmbTradeDirection` (`DropDownList`, keys **1** = «прямая», **2** = «обратная» — see `FillCombos`) |
| `lstViewOblig` (first column = direction text) | `lvwObligation` / `colObligDir` |
| `DDX.MemberData("Макс.номер в части 2")` | Pending: max index counter for **part 2** obligations (must be > 0 to mean «есть обязательства обратной сделки») |
| `objParamOblig` parameters `Object` + `StrNumInPart` where part digit = 2 | Pending: same naming when obligation param bag is ported |
| `bNeedSendOblig` | Pending: dirty flag for saving obligations to server |

**Visibility (already aligned in `InitDoc` ADD path):** legacy `chkIsComposit.Visible = (intVidTrade <> 0)` → `chkComposit.Visible = (m_kindTrade != 0)`. Full EDIT/load path should apply the same rule when trade kind is known.

---

## 3. Behaviour (verbatim logic)

### 3.1 Composite **checked** (`chkIsComposit` true)

1. Enable direction combo.
2. Clear selection: `ListIndex = -1` (no default direction until user chooses).

**.NET:** `cmbTradeDirection.Enabled = true`; `SelectedIndex = -1` (or equivalent so validation `CheckDataOblig` still requires an explicit choice).

### 3.2 Composite **unchecked**

1. Set direction display to **«прямая»** and **disable** the combo.
2. If **max number in part 2** > 0 (there exist obligations for the reverse leg):
   - Show **Yes/No** message: «Удалить обязательства обратной сделки ?» (title in VB: «Предупреждение»).
   - **Yes:**
     - Set obligation-send dirty flag (`bNeedSendOblig = True`).
     - If obligation param collection has entries: scan parameter names; for each name starting with `"Object"` and whose part index is **2**, call `ClearParameter` for that key (legacy parses `ArrRez(0,n)` like `"Object2..."`).
     - Remove every **ListView** row whose **first column** equals **«обратная»** (VB uses a `GoTo` loop to handle index shift after `Remove`).
   - **No:** revert checkbox to checked (`chkIsComposit.Value = 1`) so the user keeps composite mode and reverse obligations.

### 3.3 Error handling

Legacy: `On Error` → `objError.UbsErrMsg "chkIsComposit_Click", "ошибка выполнения"`.

**.NET:** wrap handler body in `try` / `catch` → `this.Ubs_ShowError(ex)` per project rules.

---

## 4. Related legacy rules (implement together or stub consistently)

These are **not** inside `chkIsComposit_Click` but define how the direction combo interacts with add/edit obligation flows:

| Location | Rule |
|----------|------|
| `cmdAddOblig_Click` | After `CallOblig`, if `chkIsComposit` **and** `strRunParam = "EDIT"`, **enable** `cmbNaprTrade`. |
| `cmdEditOblig_Click` | After `CallOblig`, if `chkIsComposit` **and** `strRunParam = "EDIT"`, **disable** `cmbNaprTrade` (direction locked while editing). |
| `CallOblig` | If `strNapr` empty and composite: `ListIndex = -1`; else set combo text from `strNapr`. |
| `CheckDataOblig` | If `cmbNaprTrade.ListIndex = -1` → error «Не указано направление.» |

When wiring `chkComposit_CheckedChanged`, ensure **add/edit obligation** handlers apply the same enable/disable rules for `cmbTradeDirection` using `CmdEdit` / run mode constants.

---

## 5. WinForms event choice

VB6 `Click` on the checkbox runs on toggle. Use **`CheckedChanged`** (or `Click` if you explicitly need parity). Watch for **re-entrancy** when programmatically reverting the checkbox after **No** on the message box — use a short `m_suppressCompositEvent` flag or set state in a way that avoids infinite loops.

---

## 6. User-facing strings (`UbsPmTradeFrm.Constants.cs`)

Add named constants (explicit literals per style rule), for example:

- Prompt: «Удалить обязательства обратной сделки ?»
- Message box title: «Предупреждение»
- Use `MessageBox.Show` with `MessageBoxButtons.YesNo` and `MessageBoxIcon.Information` to mirror `vbYesNo + vbInformation`.

---

## 7. Implementation checklist

- [ ] Implement max-counters for part 1 / part 2 and obligation param bag (`Object` + part.number keys) before this handler can fully match legacy deletion behaviour.
- [ ] Wire `chkComposit_CheckedChanged`: enable/disable `cmbTradeDirection`, set «прямая» + key **1** when unchecked, `SelectedIndex = -1` when checked.
- [ ] When unchecking with part-2 obligations: confirm dialog, then remove `lvwObligation` rows whose direction column is «обратная», clear matching `Object2*` params, set dirty flag.
- [ ] On **No**: restore `chkComposit.Checked = true` without leaving the combo in an inconsistent state.
- [ ] Align `cmdAddObligation` / `cmdEditObligation` (when implemented) with §4 for `cmbTradeDirection.Enabled` in EDIT mode.
