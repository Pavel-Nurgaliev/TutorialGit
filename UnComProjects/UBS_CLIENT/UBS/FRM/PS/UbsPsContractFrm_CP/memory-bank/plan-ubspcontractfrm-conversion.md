# PLAN: UbsPsContractFrm — VB6 Contract → .NET conversion

**Date:** 2026-04-02  
**Legacy:** `legacy-form/Contract/Contract.dob` (VB6 `UserDocument`, `Implements UBSChild`)  
**Target:** `UbsPsContractFrm` (.NET Framework 2.0, `UbsFormBase`)

---

## 1. Executive summary

The legacy **Contract** form is a **large PS contract card** (~2250+ lines of VB): three-tab **SSActiveTabs** shell, **UbsDDX**-bound fields, **UbsControlProperty** (`AddProperties`) for additional parameters, modal child flows for client/account/kind pickers, and many **`UbsChannel.Run`** calls. The current `UbsPsContractFrm` is a **stub**; conversion is **multi-phase** and estimated **Level 4** (architectural + phased implementation per workspace isolation rules).

**Success:** Functional parity with `Contract.dob` for Init/load/save, grid returns, validation, commission and add-fields behavior, with **constants partial**, **channel contract** doc, and **no magic strings** for commands/param keys.

---

## 2. Legacy architecture snapshot

### 2.1 Shell integration (UBSChild → .NET)

| VB6 | Role |
|-----|------|
| `UBSChild_ParamInfo("InitParamForm", RHS)` | `RHS(0)` parent window, `RHS(1)` item array (contract id for EDIT), `RHS(2)` `StrCommand` (`ADD` / `EDIT`). Calls `Initialize`, then `InitDoc`. |
| `UBSChild_ParamInfo("RetFromGrid", RHS)` | Return from child list: client (`ReadClient`), account (`ReadAcc`), or kind (`Contract` + `CHANGEKIND`). |
| `UBSChild_ParamInfo("SetFocus", …)` | Restore focus |
| `UBSChild_Height` / `Width` | 6015 / 8175 twips — guide WinForms `ClientSize` |
| `UBSChild_TitleWindow` | Window title (legacy string in Cyrillic in source) |

**.NET mapping:** Map `InitParamForm` to the project’s **command/parameter contract** (often `CommandLine` + structured param or dedicated handler matching how other PS/PM forms receive `ADD`/`EDIT` and id). Map `RetFromGrid` to callbacks after modal/list selection (or host notifications). Document exact `IUbs` command names used by the shell for this form.

### 2.2 Tabs (SSActiveTabs → `TabControl`)

Legacy has **3 tabs** (`subContract`, `TabCount = 3`):

| Logical area | VB panels | Contents (high level) |
|--------------|-----------|------------------------|
| **Main** | `TabMain` | Code, number, OI combo, kind + picker, dates, status/close date, **Получатель** frame (client, BIK, bank, corr/recipient accounts, INN, address), comment |
| **Commission** | `TabCom` | Frames «Комиссия с плательщика» / «Комиссия с получателя»: `cboTypeSend`/`cboTypeRec`, `curPercentSend`/`curPercentRec`, `cb_comissType` (обратный метод) |
| **Add. fields** | `TabAdd` | `AddProperties` (`UbsControlProperty`) + keyboard routing to Save / previous tab |

**Designer:** Use `TabControl` per `.cursor/rules/designer-rules.mdc`; preserve `panelMain` and template height rules; match **`legacy-form/screens/`** when available.

### 2.3 DDX binding (UbsDDX → manual / `IUbsFieldCollection`)

`UbsDDX` members (field name → control):

| Member | Legacy (VB6) | .NET identifier (see `creative-ubspcontractfrm-conversion-architecture.md` §7) |
|--------|----------------|----------------------------------|
| `TXTACC` | `AccClient` | `ucaRecipientAccount` |
| `TXTACCCORR` | `AccKorr` | `ucaCorrespondentAccount` |
| `INTTYPEREC` | `cboTypeRec` | `cmbRecipientCommissionType` |
| `INTTYPESEND` | `cboTypeSend` | `cmbPayerCommissionType` |
| `CURPERCENTREC` | `curPercentRec` | `udcRecipientCommissionPercent` |
| `CURPERCENTSEND` | `curPercentSend` | `udcPayerCommissionPercent` |
| `DATECONTRACT` | `dateContract` | `ucdContract` |
| `TXTADRESS` | `txtAdress` | `txtRecipientAddress` |
| `INTIDCLIENT` | `txtClient` | `txtRecipientClient` |
| `TXTCODE` | `txtCode` | `txtContractCode` |
| `TXTCOMMENT` | `txtComment` | `txtComment` |
| `TXTKIND` | `txtKind` | `txtPaymentKindCode` |
| `TXTNAMEBANK` | `txtNameBank` | `txtBankName` |
| `TXTCOMMENTKIND` | `txtNoteKind` | `txtPaymentKindComment` |
| `TXTNUM` | `txtNum` | `txtContractNumber` |

**.NET:** Either replicate change-tracking similar to `UbsDDX.MembersValue` / `UpdateData` or port field-by-field into explicit DTO + `ParamIn` on save; **array indices** for any `object[,]` from channel: **row, column** (workspace rule).

### 2.4 Additional controls (not all in DDX list)

- `txtBic`, `txtINN` → **`udcRecipientBik`**, **`udcRecipientInn`** (`UbsCtrlDecimal`)  
- `txtDateClose` → **`ucdContractClose`** (`UbsCtrlDate`)  
- `cmbNameOI`, `cmbStatus` → **`cmbExecutor`**, **`cmbContractStatus`**  
- `btnClient`, `btnAcc`, `btnKind`, `btnDelClient` → **`btnRecipientClient`**, **`btnRecipientAccount`**, **`btnPaymentKind`**, **`btnRecipientClientClear`**  
- `Info` → **`uciContract`** (`UbsCtrlInfo`)  
- `AddProperties` + `objStub` → **`ucfAdditionalFields`** + same add-fields stub pattern as other UBS forms  

---

## 3. Channel contract (inventory from `Contract.dob`)

**LoadResource (legacy):** `"VBS:UBS_VBD\PS\Contract.vbs"` → **.NET:** ASM path TBD (document in `creative/creative-ubspcontractfrm-channel-contract.md`).

### 3.1 Commands (`UbsChannel.Run`)

| Command | Typical use |
|---------|-------------|
| `InitFormContract` | After `LoadResource`; **ParamOut:** e.g. `BIKBANK`, `nIdOI`, `varExecutors`, `VARSTATE` (commission types matrix) |
| `Contract` | `STRCOMMAND`: `READ`, `READF`, `CHANGEKIND`, and save with `STRCOMMAND` = `ADD`/`EDIT`; params include `IDCONTRACT`, DDX field names, `DATECLOSE`, `STATE`, `nIdOI`, `Метод расчета комиссии с получателя`, `TXTBIC`, `TXTINN`, etc. |
| `ReadClient` | `IDCLIENT` → `NAME`, `BIC`, `ADRESS`, `INN`, `StrError`, optional add-field `КППУ` |
| `ReadAcc` | `IDACC` → account text |
| `ReadKind` | `IDKINDPAYMENT` / READ kind fields |
| `GetNameFilterKindPaym` | Kind picker filter |
| `CheckKey` | Validation |
| `CheckClientAcc` | Validation |
| `PSCheckAccounts` | Validation |
| `ReadBankBIK` | BIK lookup |
| `SearchAccClient` | Account search |
| `CheckExistAddFieldContract` | Add-fields existence |

**Deliverable:** One **creative** doc listing every `Run`, **ParamIn** / **ParamOut** keys, and error parameters (`strError`, `StrError`).

---

## 4. Phased conversion roadmap

### Phase A — Foundation (prep)

- [ ] **CREATIVE:** `memory-bank/creative/creative-ubspcontractfrm-channel-contract.md` (full command/param matrix).
- [ ] **Constants:** `UbsPsContractFrm.Constants.cs` — `LoadResource`, command names, param keys, user messages (Russian strings from legacy).
- [ ] **IUbs mapping:** Map `InitParamForm` / `RetFromGrid` / `SetFocus` to concrete `Ubs_AddName` handlers and host contract (align with `UbsPmTradeFrm` / OP forms).
- [ ] **Designer shell:** `tabContract` (3 `TabPage`s), `panelMain`, bottom **`tblActions`** + **`btnSave`** / **`btnExit`** / **`uciContract`**; approximate **ClientSize** ~8175×6015 twips converted to pixels.

### Phase B — Main tab (data entry + recipient)

**Detailed plan:** `memory-bank/plan-phase-b-main-tab.md` (work packages B.1–B.7, success criteria, risks).  
**CREATIVE:** `memory-bank/creative/creative-phase-b-main-tab.md`.

- [ ] Place all **`tabPageMain`** controls and labels; wire **read-only** states (`txtRecipientClient`, `txtBankName`, …) per legacy.
- [ ] Implement **InitDoc** equivalent: `LoadResource`, `InitFormContract`, fill commission combos from `VARSTATE`, OI list, status combo, **EDIT** branch: `Contract` READ + parameter loop + `ReadKind` + `ReadClient` + arbitrary-contract flags + **`ucfAdditionalFields`** refresh.
- [ ] Implement **ADD** branch: `ClearBankFields`, executor OI default, etc.
- [ ] Wire **RecipientClientBrowseButton** / **RecipientAccountBrowseButton** / **PaymentKindBrowseButton** / **RecipientClientClearButton** to host child windows or .NET dialogs; on return simulate `RetFromGrid` (`ReadClient`, `ReadAcc`, `Contract` `CHANGEKIND`).
- [ ] BIK / bank name / `GetBankNameACC`, `SearchOneAccClient`, `EnableFieldsCl`, keyboard **Enter** / **Esc** tab order (large; optional sub-phase).

### Phase C — Commission tab

- [x] `cmbPayerCommissionType` / `cmbRecipientCommissionType` + percent editors + **`chkRecipientCommissionReverse`**; **`EnableSumCommissionControls`** (legacy `EnableSum`) + combo **`SelectedIndexChanged`**.
- [x] **Load** `Метод расчета комиссии с получателя` in **`InitDoc`** (existing).
- [ ] **Save** `Метод расчета комиссии с получателя` on **`btnSave_Click`** — **Phase E**.

### Phase D — Add-properties tab

- [ ] **`ucfAdditionalFields`** (`UbsCtrlFields`) + stub channel for add-fields; keyboard (Enter → **`btnSave`**, Esc → previous tab).
- [ ] `CheckExistAddFieldContract` and refresh rules from legacy.

### Phase E — Save / validation

- [ ] Port **`Check`** and related checks (`GetBankNameACC`, arbitrary contract, closed-contract date rule, uniqueness `READF` + second `Contract` call).
- [ ] **`btnSave_Click`:** `UbsDDX` replacement — build `ParamIn` from changed members; **`uciContract`** messages «Договор сохранен в БД» / errors.

### Phase F — Polish

- [x] **`legacy-form/screens/`** alignment; tabindex and focus (main/commission/footer; labels **`TabStop = false`**; **`UbsPsContractFrm.Keys.cs`** Esc between tabs).
- [x] Split partials where warranted: **`Keys.cs`** added; **`Initialization.cs`** split deferred until save/validation partial exists.
- [x] **REFLECT** — `memory-bank/reflection/reflection-phase-f-ubspcontractfrm.md`; **`progress.md`** updated.

---

## 5. Risk and complexity notes

- **Parent/child windowing:** VB uses `Parent`, `NumWin`, `CloseWindow`, `SetFocusToWindow`. .NET host may differ; requires agreement with shell team or parity with an existing converted form that uses the same pattern.
- **UbsDDX:** No direct OCX in .NET; **largest** porting cost — plan explicit change sets or a small internal helper class.
- **Encoding:** Legacy Cyrillic strings; keep literals consistent with existing UBS .NET forms.

---

## 6. Related documents

| File | Purpose |
|------|---------|
| `plan-conversion-goals-revised.md` | High-level goal and pointers |
| `plan-legacy-source-conversion.md` | Path roles |
| `plan-form-appearance-legacy-screens.md` | Screenshots |
| `projectbrief.md` | Success criteria |

---

## 7. Complexity

**Level 4** — multiple tabs, DDX replacement, many channel calls, child form returns, add-fields integration. Use phased implementation and creative docs before large code drops.
