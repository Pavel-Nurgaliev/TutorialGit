# CREATIVE: Blank_ud Layout and Behavior (Design Decisions)

**Context:** Legacy `Blank_ud.dob` → target `UbsOpBlankFrm`. This doc records layout, control replacement, data binding, and UX decisions.

---

## 1. Layout Strategy

**Option A — Absolute positioning (VB6-like):** Quick visual parity; brittle resizing, DPI issues.  
**Option B — WinForms layout containers (recommended):** TabControl; TableLayoutPanel/FlowLayoutPanel inside tabs; bottom strip for Save/Exit/Info. Resilient and maintainable.

**Decision:** **Option B.**

---

## 2. Control Replacements

- **SSTabs** → WinForms `TabControl`.
- **UbsControlDate** → `DateTimePicker` or project date control.
- **UbsInfo** → existing info control (e.g. UbsCtrlInfo) — show "Данные сохранены!" after save.
- **UbsDDXControl** → explicit C# binding (LoadFromParams / BuildSaveParams).
- **ucpParam** → add-fields control; register with same key as second tab (e.g. "Набор параметров") in UbsCtrlFieldsSupportCollection if pattern matches Commission.

---

## 3. Data Binding (DDX Migration)

**Decision:** Explicit mapping — **LoadFromParams(objParamOut)** and **BuildSaveParams()**; no DDX abstraction in C#.

---

## 4. Channel Integration

Prefer existing app channel/service abstraction. Otherwise form owns channel with clear disposal. Calls: **Get_Data** (Идентификатор), **Blank_Save** (Идентификатор, Состояние; add-fields via stub). Param keys identical to VB6 (Russian names).

---

## 5. State Combo (FillCombo)

- If **Состояние = 17:** single item "Выплачно возмещение(возврат клиенту)", combo disabled.
- Else by **KindVal:**  
  - **KindVal = 3:** "Отказано в оплате" (15), "Оплачен" (13).  
  - **KindVal = 4:** "Признан неподлинным" (16), "Признан подлинным" (14).  
  - Then common: "Отправлен в банк-нерезидент" (12), "Отправлен в ГО" (11), "Принят от клиента" (10).
- Store state IDs in combo (ItemData equivalent: Tag or separate list); save uses selected state ID for param Состояние.

---

## 6. Implementation Guidelines

- Add **TabControl** to main panel; tab1 = main fields (date, series, number, name, kind, state); tab2 = add-fields.
- Form-level: **m_id**, **KindVal**, param objects.
- **InitDoc:** Get_Data → LoadFromParams; FillCombo; bind add-fields via stub.
- **btnSave_Click:** BuildSaveParams (Состояние from combo); run Blank_Save; show info.
- Keep all param/command **strings in Constants partial**; document in channel contract.
