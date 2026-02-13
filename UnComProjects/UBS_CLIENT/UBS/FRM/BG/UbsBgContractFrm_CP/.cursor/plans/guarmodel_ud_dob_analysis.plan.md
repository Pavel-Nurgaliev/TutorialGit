---
name: GuarModel_ud.dob Structure Analysis
overview: Complete analysis of the VB6 GuarModel_ud.dob UserDocument — controls, events, business logic, and channel communication — for conversion reference and verification.
todos:
  - id: read-source
    content: Read GuarModel_ud.dob, .dox, .vbp, and existing ANALYSIS.md
    status: completed
  - id: document-controls
    content: Document control hierarchy, types, and properties
    status: completed
  - id: document-events
    content: Document event handlers and keyboard navigation
    status: completed
  - id: document-business-logic
    content: Document initialization, validation, and channel commands
    status: completed
  - id: cross-reference-ubs
    content: Cross-reference with UbsGuarModelFrm channel contract and converted form
    status: completed
isProject: false
---

# GuarModel_ud.dob VB6 Structure Analysis Plan

## Executive Summary

**Source:** `source/GuarModel/GuarModel_ud.dob`  
**Type:** VB6 UserDocument (ActiveX Document) — equivalent to a form in an ActiveX DLL  
**Project:** GuarModel (OleDll), Version 1.0.23  
**Related .NET Form:** UbsGuarModelFrm.cs (already converted)

This plan documents the complete structure of the VB6 GuarModel_ud UserDocument for conversion verification, gap analysis, and future maintenance.

---

## 1. Document Structure

### 1.1 File Layout
- **GuarModel_ud.dob** — Single file containing both form definition (lines 1–186) and VB6 code (lines 187–530)
- **GuarModel_ud.dox** — Binary blob storage for tab content, combo list data (ItemData/List), and OleObjectBlob
- **GuarModel.vbp** — Project file; references UBSChilds, UBSParrents, sstabs2.ocx, UbsInfo.ocx, UbsProp.dll, UbsChlCtrl.ocx

### 1.2 UserDocument Properties
| Property | Value |
|----------|-------|
| ClientWidth | 7350 twips (~490 px) |
| ClientHeight | 4410 twips (~294 px) |
| KeyPreview | True |
| Scroll | H/V SmallChange = 225 |
| ScaleMode | Twips (default) |

---

## 2. Control Hierarchy

```
GuarModel_ud (VB.UserDocument)
├── SSActiveTabs1 (ActiveTabs.SSActiveTabs)
│   ├── SSActiveTabPanel1 [Main tab]
│   │   ├── txtMeaning (TextBox) — Наименование
│   │   ├── cmbPattern (ComboBox, Dropdown) — Шаблон
│   │   ├── cmbExecut (ComboBox, Dropdown) — ОИ
│   │   ├── cmbState (ComboBox, Dropdown) — Состояние
│   │   ├── cmbClientType (ComboBox, Dropdown) — Тип клиента
│   │   └── Label1–5
│   └── SSActiveTabPanel2 [Properties tab]
│       └── ucpParam (UbsControlProperty) — Additional fields grid
├── cmdSave (CommandButton) — Сохранить
├── cmdExit (CommandButton) — Выход
├── Info (UbsInfo32.Info) — Status message
└── UbsChannel (UbsChlCtrl.UbsChannel) — Invisible channel
```

### 2.1 Control Details (Main Tab)

| Control | Type | MaxLength | Purpose |
|---------|------|-----------|---------|
| txtMeaning | TextBox | 255 | Name/title of guarantee model |
| cmbPattern | ComboBox (Style=2) | — | Template selector; locked after save |
| cmbExecut | ComboBox (Style=2) | — | Responsible executor |
| cmbState | ComboBox (Style=2) | — | State/status |
| cmbClientType | ComboBox (Style=2) | — | Client type (0 = none) |

### 2.2 Labels (Cyrillic)
- Label1: Наименование (Name)
- Label2: Шаблон (Template)
- Label3: Ответственный исполнитель (Executor)
- Label4: Состояние (State)
- Label5: Тип клиента (Client Type)

---

## 3. Events

### 3.1 Form Lifecycle
| Event | Purpose |
|-------|---------|
| UserDocument_Initialize | Create objStub, objFormParam; call ArrayParam |
| UserDocument_Hide | Release channel, set objects to Nothing, notify parent NeedRefreshGrid |

### 3.2 Button Events
| Event | Action |
|-------|--------|
| cmdSave_Click | Validate, call GuarModelEdit, show success, disable cmbPattern |
| cmdExit_Click | Parent.CloseWindow NumWin |

### 3.3 ComboBox Events
| Event | Action |
|-------|--------|
| cmbPattern_Click | If not blnFlag: set Шаблон, Run GuarModelInitUcp, ucpParam.Refresh |

### 3.4 Property Grid Events
| Event | Action |
|-------|--------|
| ucpParam_KeyPress | Enter → next; Esc → back to main tab, focus cmbState |
| ucpParam_TextChange | blnAddFlChanged = True |

### 3.5 Keyboard Events
| Event | Keys | Action |
|-------|------|--------|
| UserDocument_KeyPress | Enter | cmbState → tab 2 or cmdSave; else NextCtrl |
| UserDocument_KeyPress | Esc | cmdExit/cmdSave/txtMeaning → specific targets; else PrevCtrl |
| UserDocument_KeyDown | Shift+Tab | Store focus, Parent.LoaderParamInfo("NextWindow") = NumWin |

---

## 4. Business Logic

### 4.1 Initialization Flow
1. **Initialize** — Create objErr, objInterfaces, objArray
2. **InitChannel** — Configure UbsChannel: HostAddress, UserGUID, ParentHwnd, LoadResource = `VBS:UBS_VBD\GUAR\GuarModel.vbs`
3. **InitDoc** — Run GuarModelInit; populate cmbPattern, cmbExecut, cmbState, cmbClientType; if Edit → GuarModelRead; cmbPattern_Click; ucpParam.Refresh

### 4.2 UBSChild_ParamInfo (InitParamForm)
- **RHS(0)** = NumParentWindow  
- **RHS(1)** = ItemArray (idObject = ItemArray(0))  
- **RHS(2)** = StrCommand ("Edit" or empty)  
- Edit with idObject=0 → MsgBox "Не выбран типовой договор!", optionally close  
- txtMeaning.Enabled = False in Edit mode  
- Focus: cmbExecut (Edit) or txtMeaning (New)

### 4.3 Validation (Check)
- **Rule:** Len(txtMeaning.Text) >= 1  
- **Error:** "Задайте наименование."; focus txtMeaning; return False

### 4.4 Save Flow (cmdSave_Click)
1. Disable buttons  
2. Check()  
3. objParamIn: StrCommand, Наименование, Шаблон, ОИ, Состояние, Тип клиента  
4. UbsChannel.Run "GuarModelEdit"  
5. If Error in objParamOut → MsgBox, re-enable, exit  
6. StrCommand = "Edit" if new  
7. Info.Caption = "Данные сохранены!"; cmbPattern.Enabled = False  

---

## 5. Channel Commands

| Command | Purpose | Input | Output |
|---------|---------|-------|--------|
| GuarModelInit | Load master data | — | Шаблоны, Типы клиентов, ОИ, Состояния |
| GuarModelInitUcp | Init UCP for template | Шаблон | (used by ucpParam) |
| GuarModelRead | Load record | Id | Наименование, ОИ, Состояние, Шаблон, Тип клиента |
| GuarModelEdit | Save record | StrCommand, Наименование, Шаблон, ОИ, Состояние, Тип клиента | Error (optional) |

**Resource:** `VBS:UBS_VBD\GUAR\GuarModel.vbs`

---

## 6. Module-Level Variables

| Variable | Type | Purpose |
|----------|------|---------|
| NumWin, NumParentWindow | Integer | Window IDs |
| Parent | UBSParrent | Parent object |
| StrCommand | String | "Edit" or "" |
| idObject | String | Record ID |
| objErr, objInterfaces, objFormParam, objParamIn, objParamOut | Object | COM helpers |
| objStub | Object | UbsAddFiledsStub |
| objArray | Object | Lib1.IUbsArray |
| arrState, arrPattern, arrExecut, arrIdPattern, arrTypeClient | Variant | 2D arrays [ID, Name] |
| blnFlag | Boolean | Prevents recursive cmbPattern_Click |
| blnAddFlChanged, bNeedRefreshGrid, bParentWindowClose | Boolean | State flags |

---

## 7. Cross-Reference with UbsGuarModelFrm

| VB6 | UbsGuarModelFrm | Notes |
|-----|-----------------|-------|
| GuarModelInit | Same | Channel contract matches |
| GuarModelEdit | Same | Channel contract matches |
| GuarModelRead | Same | Channel contract matches |
| GuarModelInitUcp | Same | Channel contract matches |
| — | GuarModelCheckRights | .NET adds rights checking (not in VB6) |
| — | DeleteInstances | .NET adds delete via ListKey (not in VB6) |
| UBSChild interface | IUbs + UbsFormBase | Different integration pattern |
| SSActiveTabs | TabControl | Direct .NET equivalent |
| ucpParam | ubsCtrlAddFields | Same concept |

---

## 8. Key Conversion Considerations

1. **UBSChild → IUbs:** Map ParamInfo(InitParamForm/SetFocus/LostFocus) to CommandLine/ListKey and focus handling.  
2. **Arrays:** VB6 2D Variant → C# `object[,]` or `List<KeyValuePair<short, string>>` for combos.  
3. **blnFlag:** Prevent recursive template-load during InitDoc; replicate in .NET.  
4. **Keyboard nav:** Implement ProcessDialogKey or form-level KeyDown/KeyPress.  
5. **Dynamic sizing:** UBSChild_HeightBrowser/WidthBrowser → Form.Resize with min 7350×4410 twips.

---

## 9. Existing Documentation

- **ANALYSIS.md** — `source/GuarModel/ANALYSIS.md` (full 16-section analysis)
- **Channel Contract** — `memory-bank/creative/ubsguarmodelfrm-channel-contract.md`
- **Creative Phase** — `memory-bank/creative/creative-ubsguarmodelfrm.md`

---

## 10. Next Steps (Optional)

- [ ] Verify UbsGuarModelFrm implements all VB6 behaviors (especially InitCmdClientType empty-option logic)
- [ ] Document GuarModel.vbs script if available for server-side contract validation
- [ ] Phase 2: Extract validation layer per creative phase decision
