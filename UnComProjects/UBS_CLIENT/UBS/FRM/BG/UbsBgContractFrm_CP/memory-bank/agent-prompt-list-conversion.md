# Agent Prompt: VB.NET to .NET Forms List Conversion

## Prompt for Converting VB.NET List Selection Handlers to C#

Use this prompt when converting VB.NET form list selection handlers to C# .NET forms.

---

## Conversion Task Prompt

```
Convert the VB.NET list selection handler [HANDLER_NAME] from [VB_FILE] to C# following the established conversion patterns.

Requirements:
1. Follow the patterns documented in memory-bank/vb-to-net-list-conversion-patterns.md
2. Convert btnX_Click → linkX_LinkClicked pattern
3. Replace window management with Ubs_ActionRun using action constants
4. Move grid filter configuration to Ubs_ActionRunBegin handler if needed
5. Convert data retrieval from ListKey handler to LinkClicked handler
6. Add action constant to constants region if new action is needed
7. Wire event handler in Designer.cs file

Steps:
1. Find btnX_Click handler in VB.NET file
2. Find corresponding Case NumChildWinX in ListKey handler
3. Identify filter file path and map to action name
4. Create/use action constant
5. Implement linkX_LinkClicked handler
6. Add grid configuration to Ubs_ActionRunBegin if needed
7. Update Designer.cs to wire LinkClicked event
8. Test conversion follows all patterns

Reference examples:
- linkFrameContract_LinkClicked (with grid filter)
- linkPrincipal_LinkClicked (simple client selection)
- linkAgent_LinkClicked (complex multi-step selection)
- linkBeneficiar_LinkClicked (client selection with focus management)
```

---

## Quick Reference Prompt

```
Convert btnX_Click to linkX_LinkClicked:

1. Add action constant: private const string ActionXxx = "UBS_XXX";
2. Implement handler:
   - EnabledCmdControl(false)
   - Ubs_ActionRun(ActionXxx, this, true)
   - Process ids array
   - Retrieve data via IUbsChannel
   - Set focus
   - EnabledCmdControl(true)
3. Add grid config to Ubs_ActionRunBegin if needed
4. Wire event in Designer.cs
```

---

## Pattern-Specific Prompts

### Simple List Selection (No Grid Filter)
```
Convert [HANDLER_NAME] - simple list selection pattern:
- Uses UBS_FLT\COMMON\client.flt or similar
- No grid filter configuration needed
- Single data retrieval call
- Follow linkPrincipal_LinkClicked pattern
```

### List Selection with Grid Filter
```
Convert [HANDLER_NAME] - list selection with grid filter:
- Has GridF.AddWhereItem calls in VB.NET
- Needs Ubs_ActionRunBegin handler
- Follow linkFrameContract_LinkClicked pattern
- Add grid configuration using UbsItemSet pattern
```

### Complex Multi-Step Selection
```
Convert [HANDLER_NAME] - complex multi-step selection:
- Multiple IUbsChannel.Run calls
- Multiple ParamIn/ParamOut operations
- Conditional logic based on command state
- Follow linkAgent_LinkClicked pattern
```

### Delete Button Conversion
```
Convert btnXDel_Click to btnXDel_Click:
- Clear ID variable (set to 0)
- Clear text fields (string.Empty)
- Clear decimal controls (DecimalValue = 0m)
- Clear date controls (Text = string.Empty)
- Update enabled states
- Follow btnAgentDel_Click pattern
```

---

## Checklist Prompt

```
When converting [HANDLER_NAME], verify:
✓ Action constant created/used
✓ Event handler signature correct (LinkClicked)
✓ Window management replaced with Ubs_ActionRun
✓ Grid configuration added to Ubs_ActionRunBegin (if needed)
✓ Data retrieval moved from ListKey to LinkClicked
✓ Control references updated (btn → link)
✓ Variable names updated (IdX → m_idX)
✓ Designer.cs event wiring added
✓ Focus management matches VB.NET behavior
✓ Error handling converted (On Error GoTo → try-catch)
✓ No DoEvents calls remaining
✓ No window tracking variables remaining
```

---

## Action Name Mapping Prompt

```
When identifying action name from filter file path:
- UBS_FLT\BG\LIST_XXX.flt → ActionUbsBgListXxx = "UBS_BG_LIST_XXX"
- UBS_FLT\COMMON\client.flt → ActionUbsCommonListClient = "UBS_COMMON_LIST_CLIENT"
- UBS_FLT\XXX\YYY.flt → ActionUbsXxxYyy = "UBS_XXX_YYY"

Follow naming convention: ActionUbs[Module][List][Entity]
Use constants from existing conversions as reference.
```

---

## Grid Filter Configuration Prompt

```
When converting grid filter configuration:
1. Find GridF.AddWhereItem calls in VB.NET
2. Identify field name, value, and visibility
3. Convert to UbsItemSet pattern:
   - наименование: field name
   - значение по умолчанию: default value
   - условие по умолчанию: "=" or "один из"
   - скрытый: false (visible) or true (hidden)
4. Add to Ubs_ActionRunBegin handler
5. Call UbsItemsRefresh after UbsItemSet
6. Use action constant for comparison
```

---

## Usage Examples

### Example 1: Basic Conversion Request
```
Convert btnGarant_Click to linkGarant_LinkClicked following the established patterns.
The handler uses UBS_FLT\COMMON\client.flt filter file.
```

### Example 2: Conversion with Grid Filter
```
Convert btnPreviousContract_Click to linkPreviousContract_LinkClicked.
This handler has grid filter configuration that needs to be moved to Ubs_ActionRunBegin.
```

### Example 3: Multiple Handlers
```
Convert the following handlers from BG_Contract.dob:
1. btnGarant_Click → linkGarant_LinkClicked
2. btnPreviousContract_Click → linkPreviousContract_LinkClicked
3. btnModel_Click → linkModel_LinkClicked (already exists, verify)

Follow the conversion patterns and ensure all action constants are defined.
```

---

## Error Prevention Checklist

Before completing conversion, verify:
- [ ] Action constant name follows convention
- [ ] Action constant value matches expected format
- [ ] All hardcoded action strings replaced with constants
- [ ] Grid configuration added if VB.NET had GridF calls
- [ ] Event handler wired in Designer.cs
- [ ] Control type changed (Button → LinkLabel) if needed
- [ ] Variable names follow C# conventions (m_ prefix)
- [ ] Focus management matches original behavior
- [ ] No compilation errors
- [ ] No linter warnings

---

*Use these prompts as templates when requesting list handler conversions*
