---
name: ""
overview: ""
todos: []
isProject: false
---

# VB.NET to C# Array Conversion Plan

## Overview

VB.NET uses column-major array indexing `(col, row)`, while C# uses row-major indexing `[row, col]`. This document provides conversion patterns and rules for transforming VB.NET arrays to C# arrays.

## Last Updated

2026-02-13

---

## Core Conversion Rule

### Index Order Transformation

**VB.NET Pattern:**

```vb
' Array declaration: (columns, rows)
ReDim arrCountry(1, 9)  ' 2 columns (0-1), 10 rows (0-9)
arrCountry(0, i)  ' Column 0, Row i
arrCountry(1, i)  ' Column 1, Row i

' Access pattern: (col, row)
arrCountry(0, i)  ' First column, i-th row
arrTypeObject(0, i)  ' Column 0 (ID), Row i
arrTypeObject(1, i)  ' Column 1 (Name), Row i
```

**C# Pattern:**

```csharp
// Array declaration: [rows, columns]
object[,] arrCountry = new object[10, 2];  // 10 rows, 2 columns
arrCountry[i, 0]  // Row i, Column 0
arrCountry[i, 1]  // Row i, Column 1

// Access pattern: [row, col]
arrCountry[i, 0]  // i-th row, first column
arrTypeObject[i, 0]  // Row i, Column 0 (ID)
arrTypeObject[i, 1]  // Row i, Column 1 (Name)
```

### Key Transformation Rule

```
VB.NET: arr(col, row)  →  C#: arr[row, col]
VB.NET: (cols, rows)  →  C#: [rows, cols]
```

---

## Dimension Conversion

### VB.NET Dimensions

```vb
ReDim arrCountry(1, 9)     ' (columns, rows) = (2 cols, 10 rows)
ReDim arrDetails(2, 0)     ' (columns, rows) = (3 cols, 1 row)
ReDim arrAddress(17, 0)   ' (columns, rows) = (18 cols, 1 row)
```

### C# Dimensions

```csharp
object[,] arrCountry = new object[10, 2];      // [rows, cols] = [10 rows, 2 cols]
object[,] arrDetails = new object[1, 3];       // [rows, cols] = [1 row, 3 cols]
object[,] arrAddress = new object[1, 18];      // [rows, cols] = [1 row, 18 cols]
```

### Conversion Formula

```
VB.NET: ReDim arr(cols, rows)
C#:     new object[rows, cols]

Example:
VB.NET: ReDim arr(1, 9)    → C#: new object[10, 2]
VB.NET: ReDim arr(2, 0)    → C#: new object[1, 3]
VB.NET: ReDim arr(17, 0)   → C#: new object[1, 18]
```

---

## UBound Conversion

### VB.NET UBound Usage

```vb
' Get upper bound of dimension 2 (rows)
For i = 0 To UBound(arrCountry, 2)
    arrCountry(0, i)  ' Column 0, Row i
    arrCountry(1, i)  ' Column 1, Row i
Next

' Get upper bound of dimension 1 (columns)
UBound(arrCountry, 1)  ' Returns 1 (last column index)
UBound(arrCountry, 2)  ' Returns 9 (last row index)
```

### C# GetLength Usage

```csharp
// GetLength(0) = rows (dimension 0)
// GetLength(1) = columns (dimension 1)

for (int i = 0; i < arrCountry.GetLength(0); i++)  // Iterate rows
{
    arrCountry[i, 0]  // Row i, Column 0
    arrCountry[i, 1]  // Row i, Column 1
}

arrCountry.GetLength(0)  // Returns 10 (number of rows)
arrCountry.GetLength(1)  // Returns 2 (number of columns)
```

### UBound to GetLength Mapping


| VB.NET               | C#                     | Meaning           |
| -------------------- | ---------------------- | ----------------- |
| `UBound(arr, 1)`     | `arr.GetLength(1) - 1` | Last column index |
| `UBound(arr, 2)`     | `arr.GetLength(0) - 1` | Last row index    |
| `UBound(arr, 2) + 1` | `arr.GetLength(0)`     | Number of rows    |
| `UBound(arr, 1) + 1` | `arr.GetLength(1)`     | Number of columns |


**Note:** `GetLength()` returns the count, while `UBound()` returns the last index. Use `GetLength() - 1` for index comparison.

---

## Common Conversion Patterns

### Pattern 1: Iterating Over Rows

**VB.NET:**

```vb
For i = 0 To UBound(arrCountry, 2)
    frmDetailsBenificiar.cmbCodeCountry.AddItem (arrCountry(0, i))
    frmDetailsBenificiar.cmbCountry.AddItem (arrCountry(1, i))
Next
```

**C#:**

```csharp
for (int i = 0; i < arrCountry.GetLength(0); i++)
{
    form.CbCodeCountry.Items.Add(arrCountry[i, 0]);
    form.CbCountry.Items.Add(arrCountry[i, 1]);
}
```

### Pattern 2: Accessing Specific Elements

**VB.NET:**

```vb
arrDetailsBeneficiar(0, 0)  ' Column 0, Row 0
arrDetailsBeneficiar(1, 0)  ' Column 1, Row 0
arrDetailsBeneficiar(2, 0)  ' Column 2, Row 0
```

**C#:**

```csharp
arrDetailsBeneficiar[0, 0]  // Row 0, Column 0
arrDetailsBeneficiar[0, 1]  // Row 0, Column 1
arrDetailsBeneficiar[0, 2]  // Row 0, Column 2
```

### Pattern 3: Array Declaration and Initialization

**VB.NET:**

```vb
ReDim arrDetailsBeneficiar(2, 0)
arrDetailsBeneficiar(0, 0) = frmDetailsBenificiar.txtName.Text
arrDetailsBeneficiar(1, 0) = frmDetailsBenificiar.txtINN.Text

ReDim arrAddress(17, 0)
arrAddress(0, 0) = frmDetailsBenificiar.txtIndex.Text
arrAddress(1, 0) = frmDetailsBenificiar.cmbCodeCountry.Text
' ... more assignments ...
arrDetailsBeneficiar(2, 0) = arrAddress
```

**C#:**

```csharp
var arrDetails = new object[1, 3];  // 1 row, 3 columns
arrDetails[0, 0] = form.NameValue;
arrDetails[0, 1] = form.INNValue;

var arrAddress = new object[1, 18];  // 1 row, 18 columns
arrAddress[0, 0] = form.IndexValue;
arrAddress[0, 1] = form.CodeCountryValue;
// ... more assignments ...
arrDetails[0, 2] = arrAddress;
```

### Pattern 4: Nested Array Access

**VB.NET:**

```vb
arrAddress = arrDetailsBeneficiar(2, 0)
frmDetailsBenificiar.txtIndex.Text = arrAddress(0, 0)
frmDetailsBenificiar.cmbCodeCountry.Text = arrAddress(1, 0)
```

**C#:**

```csharp
object[,] arrAddress = m_arrDetailsBeneficiar[0, 2] as object[,];
form.IndexValue = Convert.ToString(arrAddress[0, 0]);
form.CodeCountryValue = Convert.ToString(arrAddress[0, 1]);
```

### Pattern 5: Conditional Array Access

**VB.NET:**

```vb
If IsArray(arrDetailsBeneficiar) Then
    frmDetailsBenificiar.txtName.Text = arrDetailsBeneficiar(0, 0)
    frmDetailsBenificiar.txtINN.Text = arrDetailsBeneficiar(1, 0)
    
    arrAddress = arrDetailsBeneficiar(2, 0)
    If IsArray(arrAddress) Then
        frmDetailsBenificiar.txtIndex.Text = arrAddress(0, 0)
    End If
End If
```

**C#:**

```csharp
if (m_arrDetailsBeneficiar != null && m_arrDetailsBeneficiar.GetLength(0) > 0)
{
    form.NameValue = Convert.ToString(m_arrDetailsBeneficiar[0, 0]);
    form.INNValue = Convert.ToString(m_arrDetailsBeneficiar[0, 1]);
    
    object[,] arrAddress = m_arrDetailsBeneficiar[0, 2] as object[,];
    if (arrAddress != null && arrAddress.GetLength(0) > 0)
    {
        form.IndexValue = Convert.ToString(arrAddress[0, 0]);
    }
}
```

---

## Real-World Examples

### Example 1: Country Array

**VB.NET:**

```vb
' Array structure: arrCountry(column, row)
' Column 0: Country code
' Column 1: Country name
' 10 countries

For i = 0 To UBound(arrCountry, 2)
    frmDetailsBenificiar.cmbCodeCountry.AddItem (arrCountry(0, i))
    frmDetailsBenificiar.cmbCountry.AddItem (arrCountry(1, i))
Next
```

**C#:**

```csharp
// Array structure: arrCountry[row, column]
// Column 0: Country code
// Column 1: Country name
// 10 countries

for (int i = 0; i < arrCountry.GetLength(0); i++)
{
    form.CbCodeCountry.Items.Add(arrCountry[i, 0]);
    form.CbCountry.Items.Add(arrCountry[i, 1]);
}
```

### Example 2: Type Object Array

**VB.NET:**

```vb
' Array structure: arrTypeObject(column, row)
' Column 0: Type ID
' Column 1: Type Name

For i = 0 To UBound(arrTypeObject, 2)
    Select Case CInt(arrTypeObject(0, i))
        Case 1: frmDetailsBenificiar.cmbTypeRegion.AddItem (arrTypeObject(1, i))
        Case 2: frmDetailsBenificiar.cmbTypeArea.AddItem (arrTypeObject(1, i))
        ' ... more cases ...
    End Select
Next
```

**C#:**

```csharp
// Array structure: arrTypeObject[row, column]
// Column 0: Type ID
// Column 1: Type Name

for (int i = 0; i < arrTypeObject.GetLength(0); i++)
{
    int typeId = Convert.ToInt32(arrTypeObject[i, 0]);
    string typeName = arrTypeObject[i, 1].ToString();
    
    switch (typeId)
    {
        case 1:
            cbTypeRegion.Items.Add(typeName);
            break;
        case 2:
            cbTypeArea.Items.Add(typeName);
            break;
        // ... more cases ...
    }
}
```

### Example 3: Details Beneficiar Array

**VB.NET:**

```vb
' Array structure: arrDetailsBeneficiar(column, row)
' Column 0: Name
' Column 1: INN
' Column 2: Address (nested array)

ReDim arrDetailsBeneficiar(2, 0)
arrDetailsBeneficiar(0, 0) = frmDetailsBenificiar.txtName.Text
arrDetailsBeneficiar(1, 0) = frmDetailsBenificiar.txtINN.Text

ReDim arrAddress(17, 0)
arrAddress(0, 0) = frmDetailsBenificiar.txtIndex.Text
arrAddress(1, 0) = frmDetailsBenificiar.cmbCodeCountry.Text
' ... 16 more address fields ...
arrDetailsBeneficiar(2, 0) = arrAddress
```

**C#:**

```csharp
// Array structure: arrDetailsBeneficiar[row, column]
// Column 0: Name
// Column 1: INN
// Column 2: Address (nested array)

var arrDetails = new object[1, 3];  // 1 row, 3 columns
arrDetails[0, 0] = form.NameValue;
arrDetails[0, 1] = form.INNValue;

var arrAddress = new object[1, 18];  // 1 row, 18 columns
arrAddress[0, 0] = form.IndexValue;
arrAddress[0, 1] = form.CodeCountryValue;
// ... 16 more address fields ...
arrDetails[0, 2] = arrAddress;
```

---

## Conversion Checklist

When converting VB.NET arrays to C#:

- **Identify array dimensions:**
  - Find `ReDim` statements to understand (cols, rows) structure
  - Note which dimension is columns and which is rows
- **Convert array declarations:**
  - `ReDim arr(cols, rows)` → `new object[rows, cols]`
  - Swap dimension order in declaration
- **Convert array access:**
  - `arr(col, row)` → `arr[row, col]`
  - Swap indices in all access operations
- **Convert loop bounds:**
  - `UBound(arr, 2)` → `arr.GetLength(0)` (for row iteration)
  - `UBound(arr, 1)` → `arr.GetLength(1)` (for column count)
  - `UBound(arr, 2) + 1` → `arr.GetLength(0)` (for row count)
- **Convert array checks:**
  - `IsArray(arr)` → `arr != null`
  - `UBound(arr, 2)` check → `arr.GetLength(0) > 0`
- **Update nested array access:**
  - `arrDetails(2, 0)` → `arrDetails[0, 2]`
  - Ensure nested arrays also follow [row, col] pattern
- **Verify array assignments:**
  - All assignments use [row, col] order
  - Nested array assignments maintain consistency

---

## Common Pitfalls and Solutions

### Pitfall 1: Forgetting to Swap Dimensions

**Wrong:**

```csharp
// VB.NET: ReDim arr(1, 9) → C#: new object[2, 10]  ❌ WRONG!
var arr = new object[2, 10];  // This is wrong!
```

**Correct:**

```csharp
// VB.NET: ReDim arr(1, 9) → C#: new object[10, 2]  ✅ CORRECT!
var arr = new object[10, 2];  // 10 rows, 2 columns
```

### Pitfall 2: Using Wrong GetLength Dimension

**Wrong:**

```csharp
// Iterating rows but using GetLength(1) ❌
for (int i = 0; i < arr.GetLength(1); i++)  // Wrong dimension!
```

**Correct:**

```csharp
// Iterating rows, use GetLength(0) ✅
for (int i = 0; i < arr.GetLength(0); i++)  // Correct!
```

### Pitfall 3: Not Swapping Indices in Access

**Wrong:**

```csharp
// VB.NET: arr(0, i) → C#: arr[0, i]  ❌ WRONG!
arrCountry[0, i]  // Wrong - indices not swapped!
```

**Correct:**

```csharp
// VB.NET: arr(0, i) → C#: arr[i, 0]  ✅ CORRECT!
arrCountry[i, 0]  // Correct - indices swapped!
```

### Pitfall 4: Nested Array Dimension Confusion

**VB.NET:**

```vb
arrAddress = arrDetailsBeneficiar(2, 0)  ' Get nested array
arrAddress(0, 0)  ' Access nested array element
```

**C#:**

```csharp
// Correct conversion
object[,] arrAddress = arrDetailsBeneficiar[0, 2] as object[,];
arrAddress[0, 0]  // Nested array also uses [row, col]
```

---

## Quick Reference Table


| VB.NET                        | C#                                           | Notes                 |
| ----------------------------- | -------------------------------------------- | --------------------- |
| `ReDim arr(cols, rows)`       | `new object[rows, cols]`                     | Swap dimensions       |
| `arr(col, row)`               | `arr[row, col]`                              | Swap indices          |
| `UBound(arr, 1)`              | `arr.GetLength(1) - 1`                       | Last column index     |
| `UBound(arr, 2)`              | `arr.GetLength(0) - 1`                       | Last row index        |
| `UBound(arr, 2) + 1`          | `arr.GetLength(0)`                           | Row count             |
| `UBound(arr, 1) + 1`          | `arr.GetLength(1)`                           | Column count          |
| `IsArray(arr)`                | `arr != null`                                | Array existence check |
| `For i = 0 To UBound(arr, 2)` | `for (int i = 0; i < arr.GetLength(0); i++)` | Row iteration         |


---

## Testing Checklist

After conversion, verify:

- Array dimensions are correct (rows × columns)
- All array accesses use [row, col] order
- Loop bounds use correct GetLength dimension
- Nested arrays maintain [row, col] pattern
- Array assignments preserve data structure
- No IndexOutOfRangeException errors
- Data is accessed from correct positions

---

## Related Patterns

- **Array Initialization:** See `vb-to-net-list-conversion-patterns.md` for array handling in list conversions
- **Channel Parameters:** Arrays passed to/from IUbsChannel maintain VB.NET (col, row) format from channel
- **Data Binding:** ComboBox data binding may require array transformation

---

## Notes

1. **Channel Arrays:** Arrays received from `IUbsChannel.ParamOut()` may still be in VB.NET (col, row) format. Verify and convert if needed.
2. **Performance:** Array dimension swapping doesn't affect performance, but be consistent throughout the codebase.
3. **Documentation:** Always document array structure when converting:
  ```csharp
   // Array structure: arrCountry[row, column]
   // Column 0: Country code
   // Column 1: Country name
  ```
4. **Type Safety:** Use `as object[,]` for type casting when receiving arrays from channel or other sources.

---

*This document is maintained based on successful conversions from BG_Contract.dob to UbsBgContractFrm.cs*