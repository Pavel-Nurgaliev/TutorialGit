VERSION 5.00
Object = "{A9609DF2-7123-4FAA-8347-2AEEE0D8C251}#1.3#0"; "UbsInfo.ocx"
Object = "{5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0"; "MSFLXGRD.OCX"
Begin VB.Form frmCashSymb 
   Caption         =   "Кассовые символа"
   ClientHeight    =   3990
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   6585
   LinkTopic       =   "Form1"
   ScaleHeight     =   3990
   ScaleWidth      =   6585
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdExit 
      Caption         =   "Выход"
      Height          =   375
      Left            =   5040
      TabIndex        =   1
      Top             =   3480
      Width           =   1395
   End
   Begin VB.CommandButton cmdSave 
      Caption         =   "Сохранить"
      Height          =   375
      Left            =   3480
      TabIndex        =   0
      Top             =   3480
      Width           =   1395
   End
   Begin MSFlexGridLib.MSFlexGrid FlxGrdDemo 
      Height          =   2835
      Left            =   0
      TabIndex        =   2
      Top             =   0
      Width           =   6675
      _ExtentX        =   11774
      _ExtentY        =   5001
      _Version        =   393216
      FixedCols       =   0
      AllowUserResizing=   1
   End
   Begin UbsInfo32.Info lblHelpPay 
      Height          =   435
      Left            =   120
      Top             =   2880
      Width           =   6495
      _ExtentX        =   11456
      _ExtentY        =   767
      Alignment       =   0
      Caption         =   "Enter - вставить строку; Esc - удалить строку; BackSpace - стереть цифры; Delete -удалить запись"
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   204
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ShowMode        =   1
   End
End
Attribute VB_Name = "frmCashSymb"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

'Public ClearSymbol As Boolean    'Требуется ли очищать кассовые символа
Public arrCashSymb As Variant    'Массив, после ввода (на форму)
Public ArrayData As Variant      'Массив для заполнения Grida (с формы)
Public curSummaTotal As Currency 'Общая сумма с формы, с которой будет сравниваться значение
Public UbsChannel As Object      'Канал
Public arrTypeCashSymbol As Variant 'Допустимые кассовые символа
Public TypeExit As Integer       'Тип выхода с формы (с сохранением 1, без сохранения 0)
Dim ToolPubs As Object
Dim ToolErr As Object
Dim objParamIn As Object
Dim objParamOut As Object
Dim arrCashSymbTemp As Variant

Private Sub cmdExit_Click()
On Error GoTo ErrorCode
  Hide
  TypeExit = 0
  Exit Sub
ErrorCode:
  ToolErr.UbsErrMsg "cmdExit_Click", "ошибка выполнения"
End Sub

'Private Sub cmdExit_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
'On Error GoTo ErrorCode
'  Hide
'  Exit Sub
'ErrorCode:
'  ToolErr.UbsErrMsg "ExitForm_Click", "ошибка выполнения"
'End Sub

Private Sub cmdSave_Click()

Dim lngIndex As Long, lngItem As Long
Dim curSumma As Currency

    With FlxGrdDemo

        'For lngItem = 1 To .Rows - 1
        '    curSumma = curSumma + CCur(.TextMatrix(lngItem, 1))
        'Next

        'If CBool(curSumma <> curSummaTotal) Then
        '    MsgBox "Общая сумма, должна совпадать с введенной на форме"
        '    Exit Sub
        'End If

        For lngItem = 1 To .Rows - 1

If Not (.TextMatrix(lngItem, 0) = "" And .TextMatrix(lngItem, 1) = "") Then
            If .TextMatrix(lngItem, 0) = "" Then
                'Call CreateError("Дата не задана!")
                MsgBox "Кассовый символ не задан!", 16, "Проверка кассовых символов"
                .Row = lngItem
                .Col = 0
                .SetFocus
                Exit Sub
            End If
            If .TextMatrix(lngItem, 1) = "0" Or .TextMatrix(lngItem, 1) = "" Then
                'Call CreateError("Затраченное время не задано!")
                MsgBox "Сумма не задана!", 16, "Проверка кассовых символов"
                .Row = lngItem
                .Col = 1
                .SetFocus
                Exit Sub
            End If

            curSumma = curSumma + CCur(.TextMatrix(lngItem, 1))
End If
        Next
        
        If CBool(curSumma <> curSummaTotal) Then
            MsgBox "Общая сумма, должна совпадать с введенной на форме", 16, "Проверка кассовых символов"
            Exit Sub
        End If
        
        arrCashSymbTemp = Empty
        'Сохраняем данные в массив
        For lngItem = 1 To .Rows - 1
If Not (.TextMatrix(lngItem, 0) = "" And .TextMatrix(lngItem, 1) = "") Then
            If IsEmpty(arrCashSymbTemp) Then
                ReDim arrCashSymbTemp(2, 0)
            Else
                ReDim Preserve arrCashSymbTemp(2, UBound(arrCashSymbTemp, 2) + 1)
            End If
            
            arrCashSymbTemp(1, UBound(arrCashSymbTemp, 2)) = CStr(.TextMatrix(lngItem, 0))
            arrCashSymbTemp(2, UBound(arrCashSymbTemp, 2)) = CCur(.TextMatrix(lngItem, 1))
End If
        Next
       
        objParamIn.ClearParameters
        objParamOut.ClearParameters
        
        objParamIn.Parameter("arrCashSymb") = arrCashSymbTemp
        objParamIn.Parameter("arrTypeCashSymbol") = arrTypeCashSymbol
        
        UbsChannel.Run "UtCheckArrayCashSymbol", objParamIn, objParamOut
        If Not objParamOut.Parameter("bRetValCheck") And objParamOut.Parameter("strError") <> "" Then
           MsgBox objParamOut.Parameter("strError"), 16, "Проверка кассовых символов"
           Exit Sub
        End If

        frmCashSymb.arrCashSymb = objParamOut.Parameter("arrCashSymb")
                    
        'Скрываем форму
        Hide
        
    End With
    TypeExit = 1
End Sub

Private Sub Form_Load()
On Error GoTo ErrorCode

    Dim lngItem As Integer

    Set ToolPubs = CreateObject("ToolPubs.Interfaces")
    Set ToolErr = CreateObject("ToolPubs.IUbsError")
    
    Set objParamIn = CreateObject("ToolPubs.IUbsParam")
    Set objParamOut = CreateObject("ToolPubs.IUbsParam")

    FlxGrdDemo.TextMatrix(0, 0) = "Кассовый символ"
    FlxGrdDemo.ColWidth(0) = 1700
    FlxGrdDemo.ColAlignment(0) = flexAlignCenterCenter
    FlxGrdDemo.TextMatrix(0, 1) = "Сумма"
    FlxGrdDemo.ColWidth(1) = 2100
    FlxGrdDemo.ColAlignment(1) = flexAlignCenterCenter
    
    'If ClearSymbol = False Then
    '    FlxGrdDemo.TextMatrix(1, 0) = " "
    '    FlxGrdDemo.TextMatrix(1, 1) = " "
    'End If
    
    If Not IsEmpty(ArrayData) Then
        With FlxGrdDemo
            'For lngItem = 0 To UBound(ArrayData, 2)
            '    .AddItem "", lngItem + 1
            '    .Row = lngItem + 1
            '    .Col = 0
            '    .Text = CLng(ArrayData(0, lngItem))
            '    .Col = 1
            '    .Text = CCur(ArrayData(1, lngItem))
            'Next
            
            'Заполняем Grid так, чтобы небыло пустой строки
            For lngItem = 0 To UBound(ArrayData, 2)
                'If ClearSymbol Then
                '    ArrayData(1, lngItem) = ""
                'End If
                
                If lngItem <> 0 Then
                    .AddItem "", lngItem + 1
                End If
                .Row = lngItem + 1
                .Col = 0
                .Text = CStr(ArrayData(1, lngItem))
                .Col = 1
                .Text = CCur(ArrayData(2, lngItem))
            Next
        End With
    End If

    Exit Sub
ErrorCode:
    ToolErr.UbsErrMsg "Загрузка формы", "ошибка выполнения"
End Sub

Private Sub Form_Resize()

    FlxGrdDemo.Height = frmCashSymb.Height - 1400 '1200
    lblHelpPay.Top = FlxGrdDemo.Height - FlxGrdDemo.Top + 100
    cmdSave.Top = FlxGrdDemo.Height - FlxGrdDemo.Top + 400 '200
    cmdExit.Top = FlxGrdDemo.Height - FlxGrdDemo.Top + 400 '200

    FlxGrdDemo.Width = frmCashSymb.Width - 250
    cmdSave.Left = frmCashSymb.Width - 3300
    cmdExit.Left = frmCashSymb.Width - 1800
    
End Sub

Private Sub Form_Unload(Cancel As Integer)
On Error GoTo ErrorCode
    
    Set ToolPubs = Nothing
    Set ToolErr = Nothing
    Set UbsChannel = Nothing
    Set objParamIn = Nothing
    Set objParamOut = Nothing

    Exit Sub
ErrorCode:
    ToolErr.UbsErrMsg "Form_Unload", "ошибка выполнения"
End Sub

Private Sub FlxGrdDemo_KeyPress(KeyAscii As Integer)
    Select Case KeyAscii
        Case vbKeyReturn
            ' Когда пользователь нажимает Enter
            ' то курсор перепрыгивает на следующую
            ' ячейку или строку.
            With FlxGrdDemo
                If .Col = 0 Then
                    .Col = 1
                ElseIf .Col = 1 Then
                    .Col = 0
                    If (.TextMatrix(.Row, 0) = "") And (.TextMatrix(.Row, 1) = "0" Or .TextMatrix(.Row, 1) = "") Then
                        'Если строка пустая, то не добавляем новой строки по энтеру
                    ElseIf (.Rows - 1) = .Row Then
                        'Если строка последняя, добавляем новую строку и переводим фокус
                        .Rows = .Rows + 1
                        .Row = .Row + 1
                    Else
                        'В иных случаях просто перемещаем фокус
                        .Row = .Row + 1
                    End If
                End If
            End With
        Case vbKeyEscape
            With FlxGrdDemo
                If .Col = 1 Then
                    .Col = 0
'                ElseIf .Col = 0 And .Row >= 1 Then
                ElseIf .Col = 0 And .Row > 1 Then
                    .Col = 1
                    .Row = .Row - 1
                    .Rows = .Rows - 1
                End If
            End With
        Case vbKeyBack
        ' Удаляем предыдущий символ при
        ' нажатии клавиши backspace.
            With FlxGrdDemo
                If Trim(.Text) <> "" Then .Text = Mid(.Text, 1, Len(.Text) - 1)
            End With
        Case Is < 44, Is > 57, Is = 45, Is = 46, Is = 47
        ' Допускаем ввод только цифр и запятой

        Case Else 'Иначе печатаем всё остальное
            With FlxGrdDemo
            'MsgBox InStr(.Text, ",") & "__" & Len(.Text)
              If .Col = 0 Then
                  If Len(.Text) >= 2 Then
                      'Кассовые символа не могут быть больше 2 символов
                  ElseIf Chr(KeyAscii) <> "," Then
                      .Text = .Text & Chr(KeyAscii)
                  End If
              ElseIf .Col = 1 Then
                  If Len(.Text) >= 14 Then
                  ElseIf Len(.Text) = 0 And Chr(KeyAscii) = "," Then
                      ' Если первый символ запятая, ввод запрещен
                  ElseIf InStr(.Text, ",") <> 0 And Chr(KeyAscii) = "," Then
                      ' Если Запятая уже есть, то больше вводить нельзя
                  ElseIf InStr(.Text, ",") <> 0 And (Len(.Text) - InStr(.Text, ",") > 1) Then
                      ' Ввод третьего символа после запятой запрещен
                  Else
                      .Text = .Text & Chr(KeyAscii)
                  End If
              End If

            End With
    End Select
End Sub
Private Sub FlxGrdDemo_KeyUp(KeyCode As Integer, Shift As Integer)
    Select Case KeyCode
        ' Copy
        Case vbKeyC And Shift = 2 ' Control + C
            Clipboard.Clear
            Clipboard.SetText FlxGrdDemo.Text
            KeyCode = 0
        ' Paste
        Case vbKeyV And Shift = 2 'Control + V
            FlxGrdDemo.Text = Clipboard.GetText
            KeyCode = 0
        ' Cut
        Case vbKeyX And Shift = 2 'Control + X
            Clipboard.Clear
            Clipboard.SetText FlxGrdDemo.Text
            FlxGrdDemo.Text = ""
            KeyCode = 0
        ' Delete
        Case vbKeyDelete
            FlxGrdDemo.Text = ""
    End Select
End Sub

