VERSION 5.00
Object = "{C3A4B116-8248-4301-BB9C-E2DF040AAE6B}#1.0#0"; "UbsCtrl.dll"
Object = "{A9609DF2-7123-4FAA-8347-2AEEE0D8C251}#1.3#0"; "UbsInfo.ocx"
Begin VB.Form frmBGBonusPayInterval 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Период"
   ClientHeight    =   3555
   ClientLeft      =   2760
   ClientTop       =   3750
   ClientWidth     =   5145
   ControlBox      =   0   'False
   Icon            =   "BGBonusPayInterval.frx":0000
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3555
   ScaleWidth      =   5145
   Begin VB.CommandButton cmdExit 
      Caption         =   "Выход"
      Height          =   375
      Left            =   3900
      TabIndex        =   13
      Top             =   3120
      Width           =   1155
   End
   Begin VB.CommandButton cmdApply 
      Caption         =   "Применить"
      Height          =   375
      Left            =   2640
      TabIndex        =   12
      Top             =   3120
      Width           =   1155
   End
   Begin VB.Frame Frame2 
      Caption         =   "Дата гашений"
      Height          =   1635
      Left            =   0
      TabIndex        =   16
      Top             =   1320
      Width           =   5115
      Begin VB.ComboBox cmbTypeDate 
         Height          =   315
         Left            =   1680
         Style           =   2  'Dropdown List
         TabIndex        =   3
         Top             =   240
         Width           =   3255
      End
      Begin VB.CheckBox chkArray 
         Caption         =   "Ср"
         Enabled         =   0   'False
         Height          =   315
         Index           =   2
         Left            =   1560
         TabIndex        =   7
         Top             =   1140
         Width           =   600
      End
      Begin VB.CheckBox chkArray 
         Caption         =   "Чт"
         Enabled         =   0   'False
         Height          =   315
         Index           =   3
         Left            =   2280
         TabIndex        =   8
         Top             =   1140
         Width           =   600
      End
      Begin VB.CheckBox chkArray 
         Caption         =   "Пт"
         Enabled         =   0   'False
         Height          =   315
         Index           =   4
         Left            =   3000
         TabIndex        =   9
         Top             =   1140
         Width           =   600
      End
      Begin VB.CheckBox chkArray 
         Caption         =   "Сб"
         Enabled         =   0   'False
         Height          =   315
         Index           =   5
         Left            =   3720
         TabIndex        =   10
         Top             =   1140
         Width           =   660
      End
      Begin VB.CheckBox chkArray 
         Caption         =   "Вс"
         Enabled         =   0   'False
         Height          =   315
         Index           =   6
         Left            =   4440
         TabIndex        =   11
         Top             =   1140
         Width           =   600
      End
      Begin VB.CheckBox chkArray 
         Caption         =   "Вт"
         Enabled         =   0   'False
         Height          =   315
         Index           =   1
         Left            =   840
         TabIndex        =   6
         Top             =   1140
         Width           =   600
      End
      Begin VB.CheckBox chkArray 
         Caption         =   "Пн"
         Enabled         =   0   'False
         Height          =   315
         Index           =   0
         Left            =   120
         TabIndex        =   5
         Top             =   1140
         Width           =   600
      End
      Begin UBSCTRLLibCtl.UbsControlMoney ucmNumDay 
         Height          =   315
         Left            =   1620
         TabIndex        =   4
         Top             =   720
         Width           =   555
         _cx             =   268436435
         _cy             =   268436012
         Appearance      =   1
         Enabled         =   -1  'True
         Object.TabStop         =   -1  'True
         Text            =   "0"
         Valid           =   -1  'True
         Range           =   3
         Precision       =   0
         SignEnable      =   0
         SeparatorsEnable=   0
         ReadOnly        =   0
      End
      Begin VB.Label Label2 
         Alignment       =   1  'Right Justify
         Caption         =   "Номер дня периода"
         Height          =   195
         Left            =   60
         TabIndex        =   18
         Top             =   780
         Width           =   1515
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         Caption         =   "Тип"
         Height          =   195
         Left            =   1200
         TabIndex        =   17
         Top             =   300
         Width           =   375
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Период"
      Height          =   1215
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   5115
      Begin VB.ComboBox cmbTypePeriod 
         Height          =   315
         ItemData        =   "BGBonusPayInterval.frx":000C
         Left            =   840
         List            =   "BGBonusPayInterval.frx":000E
         Style           =   2  'Dropdown List
         TabIndex        =   1
         Top             =   240
         Width           =   4035
      End
      Begin UBSCTRLLibCtl.UbsControlMoney ucmPeriod 
         Height          =   315
         Left            =   840
         TabIndex        =   2
         Top             =   720
         Width           =   555
         _cx             =   268436435
         _cy             =   268436012
         Appearance      =   1
         Enabled         =   -1  'True
         Object.TabStop         =   -1  'True
         Text            =   "0"
         Valid           =   -1  'True
         Range           =   3
         Precision       =   0
         SignEnable      =   0
         SeparatorsEnable=   0
         ReadOnly        =   0
      End
      Begin VB.Label Label4 
         Alignment       =   1  'Right Justify
         Caption         =   "Период"
         Height          =   195
         Left            =   180
         TabIndex        =   15
         Top             =   780
         Width           =   615
      End
      Begin VB.Label Label3 
         Alignment       =   1  'Right Justify
         Caption         =   "Тип"
         Height          =   195
         Left            =   360
         TabIndex        =   14
         Top             =   300
         Width           =   435
      End
   End
   Begin UbsInfo32.Info Info 
      Height          =   315
      Left            =   60
      Top             =   3060
      Width           =   2175
      _ExtentX        =   3836
      _ExtentY        =   556
      Caption         =   "Данные сохранены!"
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   204
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
End
Attribute VB_Name = "frmBGBonusPayInterval"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Public blnApply As Boolean
Dim objErr As Object
Dim blnEditRegime As Boolean
Dim objInterfaces As Object


Private Sub cmbTypeDate_Click()
Dim i As Integer
On Error GoTo ErrorCode

    blnEditRegime = True
    If cmbTypeDate.ListIndex > -1 Then
        If cmbTypeDate.ItemData(cmbTypeDate.ListIndex) = 0 Or cmbTypeDate.ItemData(cmbTypeDate.ListIndex) = 1 Then
           '"Последний день периода", "Последний рабочий день периода" - дизейблим все
           ucmNumDay.Enabled = False
           For i = 0 To chkArray.UBound
              chkArray(i).Enabled = False
           Next i
        ElseIf cmbTypeDate.ItemData(cmbTypeDate.ListIndex) = 2 Then
           '"Указанный день периода"
           If cmbTypePeriod.ItemData(cmbTypePeriod.ListIndex) = 1 Then
              '"Неделя" - раздизейбливаем чекбоксы
              ucmNumDay.Enabled = False
              For i = 0 To chkArray.UBound
                 chkArray(i).Enabled = True
              Next i
           Else
              'Не "Неделя" - раздизейбливаем контрол для ввода "Номера дня периода"
              ucmNumDay.Enabled = True
              For i = 0 To chkArray.UBound
                 chkArray(i).Enabled = False
              Next i
           End If
        ElseIf cmbTypeDate.ItemData(cmbTypeDate.ListIndex) = 2 Then
           '"В конкретную дату" - раздизейбливаем контрол для ввода конкретной даты
           ucmNumDay.Enabled = False
           For i = 0 To chkArray.UBound
              chkArray(i).Enabled = False
           Next i
        End If
    End If
    Exit Sub
ErrorCode:
    objErr.UbsErrMsg "cmbTypeDate_Click", "ошибка выполнения"
End Sub

Private Sub cmbTypePeriod_Click()
    'cmbTypeDate_Click
    If cmbTypePeriod.ListIndex = 0 Then
        cmbTypeDate.ListIndex = 0
        cmbTypeDate.Enabled = False
    Else
        cmbTypeDate.Enabled = True
    End If
End Sub

Private Sub cmdExit_Click()
    blnApply = False
    Me.Hide
End Sub

Private Sub Form_Initialize()
    Set objErr = CreateObject("ToolPubs.IUbsError")
    Set objInterfaces = CreateObject("ToolPubs.Interfaces")
    
    If cmbTypePeriod.ListIndex = 0 Then
        cmbTypeDate.ListIndex = 0
        cmbTypeDate.Enabled = False
    Else
        cmbTypeDate.Enabled = True
    End If
End Sub

Private Sub cmdApply_Click()
On Error GoTo ErrorCode

    If Not Check() Then
       Exit Sub
    End If

    blnApply = True
    Me.Hide

    Exit Sub
ErrorCode:
    objErr.UbsErrMsg "cmdApply_Click", "ошибка выполнения"
    If blnEditRegime Then cmdApply.Enabled = True
    cmdExit.Enabled = True
End Sub


Private Function Check() As Boolean
Dim i As Integer, blnFlagChecked As Boolean

    Check = False

    If cmbTypePeriod.ListIndex < 0 Then
       MsgBox "Необходимо указать тип периода", vbCritical, "Ошибка"
       If blnEditRegime Then cmdApply.Enabled = True
       cmdExit.Enabled = True
       cmbTypePeriod.SetFocus
       Exit Function
    End If

    If ucmPeriod.Text = "" Or ucmPeriod.CurrencyValue = 0 Then
       MsgBox "Необходимо указать корректный период", vbCritical, "Ошибка"
       If blnEditRegime Then cmdApply.Enabled = True
       cmdExit.Enabled = True
       ucmPeriod.SetFocus
       Exit Function
    End If
    
    If ucmNumDay.Enabled And cmbTypePeriod.ListIndex <> 0 And _
          (ucmNumDay.Text = "" Or ucmNumDay.CurrencyValue = 0) Then
       MsgBox "Необходимо указать номер дня периода", vbCritical, "Ошибка"
       If blnEditRegime Then cmdApply.Enabled = True
       cmdExit.Enabled = True
       ucmNumDay.SetFocus
       Exit Function
    End If

    If cmbTypeDate.ListIndex < 0 Then
       MsgBox "Необходимо указать тип даты гашений", vbCritical, "Ошибка"
       If blnEditRegime Then cmdApply.Enabled = True
       cmdExit.Enabled = True
       cmbTypeDate.SetFocus
       Exit Function
    End If
    
    If chkArray(0).Enabled Then
       blnFlagChecked = False
       For i = 0 To chkArray.UBound
          If chkArray(i).Value = Checked Then
             blnFlagChecked = True
             Exit For
          End If
       Next i
       
       If Not blnFlagChecked Then
          MsgBox "Необходимо указать дни недели для исполнения поручения", vbCritical, "Ошибка"
          If blnEditRegime Then cmdApply.Enabled = True
          cmdExit.Enabled = True
          chkArray(0).SetFocus
          Exit Function
       End If
    End If

    Check = True
  
End Function

Private Sub Form_KeyPress(KeyAscii As Integer)

Dim blnMoveFlag As Boolean
On Error GoTo TryToCorrectError_
'Call CheckForFocus(cmbPortfolio)

    Select Case (KeyAscii)
        Case 13                              'Enter
            blnMoveFlag = True
            objInterfaces.NextCtrl
            KeyAscii = 0
        
        Case 27                              'Esc
            blnMoveFlag = False
            objInterfaces.PrevCtrl
            KeyAscii = 0
        
    End Select
        
On Error GoTo 0
    Exit Sub
    
TryToCorrectError_:
    Err.Clear
    If blnMoveFlag Then
        objInterfaces.NextCtrl
    Else
        objInterfaces.PrevCtrl
    End If
    Resume Next

End Sub
