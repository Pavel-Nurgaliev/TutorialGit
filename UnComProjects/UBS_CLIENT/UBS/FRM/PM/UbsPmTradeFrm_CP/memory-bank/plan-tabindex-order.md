# Plan: TabIndex Correction — UbsPmTradeFrm.Designer.cs

**Rule used:** TabIndex values must be *per-container* (restart from 0 inside each GroupBox /
TabPage / nested TabControl). Non-interactive display-only controls must have `TabStop = false`
so the keyboard focus skips them.

---

## Problems Found

| Problem | Detail |
|---------|--------|
| Duplicate TabIndex=15 | `lstViewOblig` AND `cmdAddOblig` both have TabIndex=15 |
| Form-wide numbering inside GroupBoxes | `txtContractCode1`=128, `txtClientName1`=129, `btnContract1`=132 etc. — should be local 1..N |
| Display-only fields have TabStop=true | `txtContractCode1/2`, `txtClientName1/2`, `txtKS_0/1`, `txtName_0/1`, `txtRS_0/1`, `txtStorageCode`, `txtStorageName` are ReadOnly/Disabled but still accept focus |
| `chkCash_0` / `chkCash_1` = 0 | Collides with all the labels also set to 0 in the same container |
| `linkSeller` / `linkBuyer` = 0 | They are clickable LinkLabels and need non-zero position after labels |
| Bottom buttons 101/102 | Not wrong functionally, but inconsistent style; keep 100 for tableLayoutPanel, 0/1 inside it |
| `grpMetalChar`, `grpMetalCharPost` = 0 | Duplicate 0 with labels in parent; they are containers and should have unique sequential index in parent |

---

## Correct TabIndex Table

### panelMain direct children
| Control | Old | New |
|---------|-----|-----|
| tabControl | 0 | 0 ✓ |
| tableLayoutPanel | 100 | 100 ✓ (keep high so tabs come first) |

### tableLayoutPanel children
| Control | Old | New |
|---------|-----|-----|
| btnSave | 101 | 0 |
| btnExit | 102 | 1 |

### tabPage1 direct children
| Control | Old | New |
|---------|-----|-----|
| grpTrade | 0 | 0 ✓ |
| cmbCurrencyPost | 6 | 1 |
| cmbCurrencyOpl | 7 | 2 |
| grpContracts | 0 | 3 |
| cmbComission | 14 | 4 |
| labels (all) | 0 | 0 ✓ |

### grpTrade children
| Control | Old | New |
|---------|-----|-----|
| dateTrade | 1 | 1 ✓ |
| txtTradeNum | 2 | 2 ✓ |
| chkIsComposit | 3 | 3 ✓ |
| cmbKindSupplyTrade | 4 | 4 ✓ |
| cmbTradeType | 5 | 5 ✓ |

### grpContracts children
| Control | Old | New | TabStop |
|---------|-----|-----|---------|
| linkSeller | 0 | 1 | true |
| cmbContractType1 | 8 | 2 | true |
| btnContract1 | 132 | 3 | true |
| txtContractCode1 | 128 | 4 | **false** |
| txtClientName1 | 129 | 5 | **false** |
| linkBuyer | 0 | 6 | true |
| cmbContractType2 | 10 | 7 | true |
| btnContract2 | 133 | 8 | true |
| txtContractCode2 | 130 | 9 | **false** |
| txtClientName2 | 131 | 10 | **false** |
| chkNDS | 12 | 11 | true (hidden) |
| chkExport | 13 | 12 | true (hidden) |

### tabPage2 direct children
| Control | Old | New |
|---------|-----|-----|
| lstViewOblig | 15 → **DUPLICATE** | 0 |
| cmdAddOblig | 15 | 1 |
| cmdEditOblig | 16 | 2 |
| cmdDelOblig | 17 | 3 |

### tabPage3 direct children
| Control | Old | New |
|---------|-----|-----|
| tabControlOblig | 0 | 0 ✓ |
| linkAccountsOblig | 42 | 1 |
| cmdApplayOblig | 40 | 2 |
| cmdExitOblig | 41 | 3 |

### tabPageOblig1 children
| Control | Old | New |
|---------|-----|-----|
| cmbNaprTrade | 18 | 1 |
| cmbCurOblig | 19 | 2 |
| cmbUnit | 20 | 3 |
| ucdCostUnit | 21 | 4 |
| chkRate | 22 | 5 |
| ucdRateCurOblig | 23 | 6 |
| chkSumInCurValue | 24 | 7 |
| ucdCostCurOpl | 25 | 8 |
| grpMetalChar | 0 | 9 |
| grpMetalCharPost | 0 | 10 |

### grpMetalChar children
| Control | Old | New |
|---------|-----|-----|
| datePost | 26 | 1 |
| ucdMassa | 27 | 2 |
| ucdMassaGramm | 28 | 3 |

### grpMetalCharPost children
| Control | Old | New |
|---------|-----|-----|
| dateOpl | 29 | 1 |
| ucdSumOblig | 30 | 2 |
| ucdSumOpl | 31 | 3 |

### tabPageOblig2 children
| Control | Old | New |
|---------|-----|-----|
| lstViewObject | 32 | 1 |
| cmdAddObject | 33 | 2 |
| cmdDelObject | 34 | 3 |

### tabPage4 direct children
| Control | Old | New | TabStop |
|---------|-----|-----|---------|
| chkExternalStorage | 37 | 1 | true |
| linkStorage | 41 | 2 | true |
| txtStorageCode | 39 | 3 | **false** |
| txtStorageName | 40 | 4 | **false** |

### tabPageInstr1 children ("Покупатель")
| Control | Old | New | TabStop |
|---------|-----|-----|---------|
| chkCash_0 | 0 | 1 | true |
| linkListInstr | 56 | 2 | true |
| txtBIK_0 | 48 | 3 | true |
| txtKS_0 | 0 | 4 | **false** |
| txtName_0 | 50 | 5 | **false** (ReadOnly display) |
| txtRS_0 | 49 | 6 | **false** (Disabled, set via link) |
| linkAccountPay | 57 | 7 | true |
| txtClient_0 | 52 | 8 | true |
| txtNote_0 | 53 | 9 | true |
| txtINN_0 | 54 | 10 | true |
| chkNotAkcept_0 | 55 | 11 | true |

### tabPageInstr2 children ("Продавец")
| Control | Old | New | TabStop |
|---------|-----|-----|---------|
| chkCash_1 | 0 | 1 | true |
| linkListInstrSeller | 62 | 2 | true |
| txtBIK_1 | 54 | 3 | true |
| txtKS_1 | 0 | 4 | **false** |
| txtName_1 | 56 | 5 | **false** (ReadOnly) |
| txtRS_1 | 55 | 6 | **false** (Disabled) |
| linkAccountSeller | 63 | 7 | true |
| txtClient_1 | 58 | 8 | true |
| txtNote_1 | 59 | 9 | true |
| txtINN_1 | 60 | 10 | true |
| chkNotAkcept_1 | 61 | 11 | true |

---

## 11. CREATIVE / BUILD: `UbsCtrlAccount` replaces `txtKS` / `txtRS` (Tab 5)

**VB6:** `UBSCTRLLibCtl.UbsControlAccount`. **.NET:** `UbsCtrlAccount`.

When migrating the designer:

| Replace | TabIndex | TabStop |
|---------|----------|---------|
| `txtKS0` / `txtKS1` → `ucaKS0` / `ucaKS1` | **4** (unchanged slot) | **false** while read-only/disabled |
| `txtRS0` / `txtRS1` → `ucaRS0` / `ucaRS1` | **6** (unchanged slot) | **false** while display-only |

- Keep **per-container** ordering: the sequence **1→11** above is unchanged; only the **control type** at slots 4 and 6 changes.
- Set `TabIndex` on the **hosted** `UbsCtrlAccount` in the `TabPage`, not on legacy form-wide numbers.
- If `cmdAccount` is added beside RS, insert it into the sequence and **renumber** so no duplicate `TabIndex` among siblings (see `memory-bank/creative/creative-trade-account-control-and-indexes.md`).
- Update **TabStop=false additions** list: remove `txtKS_*`, `txtRS_*` after migration; apply the same policy to `ucaKS_*` / `ucaRS_*`.

### tabPage6 child
| Control | Old | New |
|---------|-----|-----|
| ubsCtrlField | 62 | 0 |

---

## TabStop=false additions (display-only fields)
- `txtContractCode1`, `txtContractCode2` — ReadOnly
- `txtClientName1`, `txtClientName2` — ReadOnly  
- `txtKS_0`, `txtKS_1` — ReadOnly + Disabled (**→ `ucaKS_*` after `UbsCtrlAccount` migration**)
- `txtName_0`, `txtName_1` — ReadOnly (bank name, auto-filled)
- `txtRS_0`, `txtRS_1` — Disabled (set via linkAccountPay/Seller) (**→ `ucaRS_*` after migration**)
- `txtStorageCode`, `txtStorageName` — ReadOnly + Disabled

---

## Build steps
1. Apply all TabIndex changes via StrReplace in Designer.cs
2. Add TabStop=false to the 10 display-only TextBox controls
3. Build project — expect 0 errors
