VERSION 5.00
Object = "{C3A4B116-8248-4301-BB9C-E2DF040AAE6B}#1.0#0"; "UbsCtrl.dll"
Begin VB.Form frmCalc 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Расчет с клиентом"
   ClientHeight    =   1905
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   4680
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1905
   ScaleWidth      =   4680
   StartUpPosition =   1  'CenterOwner
   Begin VB.CommandButton cmdExit 
      Caption         =   "Выход"
      Height          =   375
      Left            =   2520
      TabIndex        =   5
      Top             =   1440
      Width           =   1215
   End
   Begin VB.CommandButton cmdCalcSum 
      Caption         =   "Расчет"
      Height          =   375
      Left            =   960
      TabIndex        =   4
      Top             =   1440
      Width           =   1215
   End
   Begin UBSCTRLLibCtl.UbsControlMoney curSummaNal 
      Height          =   315
      Left            =   2460
      TabIndex        =   3
      Top             =   540
      Width           =   1815
      _cx             =   92474497
      _cy             =   92471852
      Appearance      =   1
      Enabled         =   -1  'True
      Object.TabStop         =   -1  'True
      Text            =   "0,00"
      Valid           =   -1  'True
      Range           =   14
      Precision       =   2
      SignEnable      =   1
      SeparatorsEnable=   1
      ReadOnly        =   0
   End
   Begin VB.Label curSummaChange 
      Alignment       =   1  'Right Justify
      Caption         =   "#"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   2520
      TabIndex        =   7
      Top             =   960
      Width           =   1755
   End
   Begin VB.Label curSummaPaym 
      Alignment       =   1  'Right Justify
      Caption         =   "#"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   2580
      TabIndex        =   6
      Top             =   180
      Width           =   1695
   End
   Begin VB.Label Label4 
      Caption         =   "Сдача"
      Height          =   255
      Left            =   240
      TabIndex        =   2
      Top             =   900
      Width           =   2175
   End
   Begin VB.Label Label3 
      Caption         =   "Сумма наличных"
      Height          =   255
      Left            =   240
      TabIndex        =   1
      Top             =   540
      Width           =   2175
   End
   Begin VB.Label Label2 
      Caption         =   "Сумма принятых платежей"
      Height          =   255
      Left            =   240
      TabIndex        =   0
      Top             =   180
      Width           =   2175
   End
End
Attribute VB_Name = "frmCalc"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Public curSumPaym As Currency
'-------------------------- Новопашин В.М. 30.05.2006 ----------------
Private curSumChange As Currency
Dim objFormat As Object
'---------------------------------------------------------------------
Public TypeExit As Integer
Dim ToolPubs As Object
Dim ToolErr As Object

Private Sub cmdCalcSum_Click()

    curSummaChange.Caption = "0,00"
    
    'curSumChange = curSummaNal.CurrencyValue - curSummaPaym.CurrencyValue
    curSumChange = curSummaNal.CurrencyValue - curSumPaym
    curSummaChange.Caption = objFormat.MToPrintC(curSumChange, False, ",")
    
    'begin - Данилова, 12.09.2005
    'If curSummaPaym.CurrencyValue = 0 Then
    If curSumPaym = 0 Then
        MsgBox "Сумма платежа нулевая! Нажмите кнопку Выход и еще раз кнопку Расчет", vbCritical, "Платеж"
        Exit Sub
    End If
    'end - Данилова, 12.09.2005
    
    If curSumChange < 0 Then
        MsgBox "Недостаток суммы наличных!", vbCritical, "Платеж"
    Else
        curSummaChange.Caption = objFormat.MToPrintC(curSumChange, False, ",")
        TypeExit = 1
        curSummaPaym.Caption = "0,00"  'Данилова, 12.09.2005
        Hide
    End If

End Sub

Private Sub cmdCalcSum_KeyPress(KeyAscii As Integer)

    If KeyAscii = 27 Then     'Esc
        KeyAscii = 0
        curSummaNal.SetFocus
    End If
    
End Sub

Private Sub cmdExit_Click()

    TypeExit = 0
    curSummaPaym.Caption = "0,00" 'Данилова, 12.09.2005
    Hide
    
End Sub

Private Sub curSummaNal_KeyPress(ByVal nKeyCode As Integer)
    
    Dim curSumChange As Currency

    If nKeyCode = 13 Then
    
        curSummaChange.Caption = "0,00"
        curSumChange = curSummaNal.CurrencyValue - curSumPaym
        
        If curSumChange < 0 Then
            MsgBox "Недостаток суммы наличных!", vbCritical, "Платеж"
        Else
            curSummaChange.Caption = objFormat.MToPrintC(curSumChange, False, ",")
            cmdCalcSum.SetFocus
        End If
    
    End If
    
End Sub

Private Sub Form_Load()
On Error GoTo ErrorCode

    Set ToolPubs = CreateObject("ToolPubs.Interfaces")
    Set ToolErr = CreateObject("ToolPubs.IUbsError")
    Set objFormat = CreateObject("ToolPubs.IUbsFormat")

    curSummaPaym.Caption = objFormat.MToPrintC(curSumPaym, False, ",")
    curSummaChange.Caption = "0,00"
    'curSummaPaym.Enabled = False
    'curSummaChange.Enabled = False
    
    Exit Sub
ErrorCode:
    ToolErr.UbsErrMsg "Загрузка формы", "ошибка выполнения"

End Sub

Private Sub Form_Unload(Cancel As Integer)
On Error GoTo ErrorCode
    
    Set ToolPubs = Nothing
    Set ToolErr = Nothing
    Set objFormat = Nothing

    Exit Sub
ErrorCode:
    ToolErr.UbsErrMsg "Form_Unload", "ошибка выполнения"
End Sub

