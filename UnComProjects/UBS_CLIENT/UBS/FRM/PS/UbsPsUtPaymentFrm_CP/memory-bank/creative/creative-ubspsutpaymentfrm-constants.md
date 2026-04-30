# CREATIVE: Constants file � UbsPsUtPaymentFrm

**Scope:** Define what belongs in `UbsPsUtPaymentFrm.Constants.cs`: resource names, local mode strings, fixed captions, reusable messages, and selected UI text.  
**Sources:** `memory-bank/tasks.md`, `legacy-form/UtPayment/UtPayment.dob`, sibling PS constants docs, workspace `style-rule.mdc`.  
**Critical local rule:** This workspace explicitly prefers **string literals for channel `Run` names and param keys**, so this constants strategy must not fight that rule.

---

## ENTERING CREATIVE PHASE: Architecture (constants organization)

### Requirements

1. `UbsPsUtPaymentFrm.Constants.cs` should remove high-risk repeated literals from business code.
2. It must preserve legacy strings exactly where they are user-facing or resource-related.
3. It must respect the workspace rule that channel `Run("...")` names and `ParamIn` / `ParamOut` keys stay as explicit string literals in code.
4. It should remain maintainable in .NET 2.0 WinForms and avoid turning into an unstructured dump of hundreds of constants.
5. It should support both the main payment resource and the alternate print-form-check resource seen in VB6.

### Options

#### Option A � Put everything into `Constants.cs`

- **Description:** store `LoadResource`, channel `Run` names, param keys, messages, captions, and UI text together.
- **Pros:** single inventory file, easy to audit in one place.
- **Cons:** conflicts with the workspace rule for explicit channel command/key literals; creates a very large constants file; duplicates the future channel-contract document.

**Verdict:** reject.

#### Option B � Hybrid constants file: resources, local mode strings, captions, messages, selected UI text; keep channel commands/keys inline (**recommended**)

- **Description:** use `Constants.cs` for form-owned fixed strings, but keep channel `Run` names and parameter keys as explicit literals in code while documenting them in the separate channel-contract creative doc.
- **Pros:** matches workspace rules, keeps `Constants.cs` focused, still removes the most error-prone repeated non-channel literals, aligns well with partial-class organization.
- **Cons:** channel strings are split between implementation and documentation rather than centralized in one code file.

**Verdict:** **selected.**

#### Option C � Minimal constants file with only `LoadResource` and a few messages

- **Description:** keep almost everything else inline.
- **Pros:** smallest constants file.
- **Cons:** leaves repeated mode strings, captions, and reusable UI text scattered across files; weak payoff for adding a dedicated partial.

**Verdict:** too small to justify the partial.

---

## EXITING CREATIVE PHASE � Decision summary

**Chosen approach:** Option B.

### What `UbsPsUtPaymentFrm.Constants.cs` should contain

Use one partial with compact `#region` blocks:

- `Resource`
- `StrCommand`
- `Caption`
- `Message`
- `UiText`
- `Support`

### What must stay out of `Constants.cs`

Do **not** move these into constants for this project:

- `IUbsChannel.Run("...")` command names
- `ParamIn["..."]` keys
- `ParamOut["..."]` keys
- one-off Designer label text that is never referenced outside `Designer.cs`

Those values should instead be handled like this:

- keep command names and param keys as explicit literals in code
- document them comprehensively in the dedicated channel-contract creative document

### Region layout

#### `#region Resource`

These belong in constants because they are reused and not business-data keys:

- `LoadResource` -> main payment script resource
- `LoadResourcePrintForm` -> print-form validation/check script resource

Planned values:

| Const | Value |
|------|-------|
| `LoadResource` | `@"VBS:UBS_VBD\PS\UT\UtPaymentF.vbs"` |
| `LoadResourcePrintForm` | `@"VBS:UBS_VBS\PS\PsCheckPrintForm.vbs"` |

Note:

- The template placeholder `ASM:UBS_ASM\Business\DllName.dll->UbsBusiness.NameClass` must be removed during BUILD.
- The commented `PsLibTools.vbs` line from VB6 is not part of the initial constants inventory.

#### `#region StrCommand`

These are local mode strings compared repeatedly across init/save flows. They are not channel param keys themselves, so they should be constants.

Expected constants:

- `StrCommandAdd` -> `ADD`
- `StrCommandRead` -> `READ`
- `StrCommandClear` -> `CLEAR`
- `StrCommandView` -> `VIEW`
- `StrCommandCopy` -> `COPY`
- `StrCommandAddFromClient` -> `ADD_FROM_CLIENT`
- `StrCommandAddParam` -> `ADD_PARAM`
- `StrCommandGroupAdd` -> `GROUP_ADD`
- `StrCommandGroupProceed` -> `GROUP_PROCEED`
- `StrCommandGroupView` -> `GROUP_VIEW`
- `StrCommandGroupChange` -> `GROUP_CHANGE`
- `StrCommandChangePart` -> `CHANGE_PART`
- `StrCommandChangePartIncoming` -> `CHANGE_PART_INCOMING`
- `StrCommandChangeContract` -> `CHANGECONTRACT`

Rule:

- Use these for local mode comparisons like `if (m_strCommand == StrCommandAdd)`.
- Do not use them as a substitute for explicit param-key literals such as `"StrCommand"`.

#### `#region Caption`

Keep fixed dialog and title strings here when they are reused from code:

- `CaptionForm`
- `CaptionError`
- `CaptionValidation`
- `CaptionCheck`
- `CaptionCashSymbolControl`
- `CaptionClientCheck`

Planned guidance:

- `CaptionForm` should follow the legacy payment-form title family and sibling payment-group convention: `������`.
- Keep separate caption constants when legacy uses a distinct wording or spelling.
- Dynamic captions such as group-payment captions with appended ids should use a constant prefix plus runtime concatenation.

Examples:

- `CaptionForm = "������"`
- `CaptionError = "������"`
- `CaptionGroupInputPrefix = "������ � ������ �������� ID = "`

#### `#region Message`

Store reusable, fixed user-facing message bodies here.

Expected categories:

- save success
- recipient-attribute save success/failure
- missing recipient contract
- invalid account/key/sum validation messages
- group-payment continuation prompt
- contract closed warnings
- IPDL / terrorism blocking messages
- print-form related informational messages

Examples inferred from the legacy payment family and observed VB6 messages:

- `MsgPaymentSavedDb`
- `MsgRecipientAttributesSaved`
- `MsgRecipientContractRequired`
- `MsgInvalidSettlementAccount`
- `MsgInvalidAccountKey`
- `MsgGroupContinue`
- `MsgIpdlSaveForbidden`

Rule:

- Only fixed reusable message bodies become constants.
- Dynamic channel error text (`strError`, `StrError`, `Error`) remains dynamic and should not be copied into constants.

#### `#region UiText`

This region is allowed, but should stay selective.

Good candidates:

- tab captions if assigned outside Designer or reused in code
- group captions if reused during runtime
- add-fields support captions/keys

Planned constants:

- `TabTextGeneral = "��������"`
- `TabTextThirdPerson = "�������� � ������� ����"`
- `TabTextTariff = "�����"`
- `TabTextTelephone = "���������� ����"`
- `TabTextTax = "�����"`
- `TabTextAddFields = "�������������� ��������"`
- `GroupTextPayer = "����������"`
- `GroupTextRecipient = "����������"`

Rule:

- If a caption only appears once in `Designer.cs` and never in code, keeping it inline in the Designer is acceptable.
- If BUILD chooses to centralize tab/group captions for consistency, this region is the correct home.

#### `#region Support`

Keep a small group for non-channel fixed support strings such as:

- `AddFieldsSupportKey`
- optional fixed browse-script or helper identifiers that are not `Run` names or param keys

Planned guidance:

- `AddFieldsSupportKey` should be a constant because it represents local registration/configuration rather than a channel key.
- Confirm the exact value during BUILD when wiring `UbsCtrlFields`.

### Naming policy

- Use `private const string`.
- Use PascalCase constant names.
- Use prefixes by role:
  - `LoadResource*`
  - `StrCommand*`
  - `Caption*`
  - `Msg*`
  - `TabText*`
  - `GroupText*`

### File-boundary policy

- `UbsPsUtPaymentFrm.Constants.cs` should serve the **main form**.
- Child-form-specific captions/messages should stay local to each child form unless the main form and child form genuinely share the same text.
- Do not use the main-form constants file as a dumping ground for `FrmCalc`, `FrmCashOrd`, and `FrmCashSymb` internals.

### BUILD guidance

1. Create `Constants.cs` before implementing large init/save logic.
2. Add `LoadResource` and `LoadResourcePrintForm` first.
3. Add the `StrCommand*` constants next, because they affect init/save branching early.
4. Add captions and messages incrementally while porting `MsgBox`, info-line, and title logic.
5. Keep channel command names and param keys explicit in code even while the channel-contract doc exists.

### Verification

- [x] Multiple options considered.
- [x] Workspace rule conflict resolved explicitly.
- [x] Resource constants identified.
- [x] Local mode-string strategy defined.
- [x] Caption/message scope defined.
- [x] Channel command/key literals intentionally excluded from `Constants.cs`.

---

**Recommended next step:** `/creative Channel contract: all Run calls, ParamIn, ParamOut`
