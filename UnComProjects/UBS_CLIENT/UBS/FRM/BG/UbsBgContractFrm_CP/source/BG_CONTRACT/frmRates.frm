VERSION 5.00
Object = "{C3A4B116-8248-4301-BB9C-E2DF040AAE6B}#1.0#0"; "UbsCtrl.dll"
Begin VB.Form frmRates 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Ставка"
   ClientHeight    =   2160
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   4290
   Icon            =   "frmRates.frx":0000
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2160
   ScaleWidth      =   4290
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.ComboBox cmbRateTypes 
      Enabled         =   0   'False
      Height          =   315
      Left            =   1440
      Style           =   2  'Dropdown List
      TabIndex        =   0
      Top             =   120
      Width           =   2775
   End
   Begin VB.CommandButton cmdApply 
      Caption         =   "Применить"
      Height          =   375
      Left            =   1560
      TabIndex        =   3
      Top             =   1680
      Width           =   1155
   End
   Begin VB.CommandButton cmdExit 
      Caption         =   "Отмена"
      Height          =   375
      Left            =   3000
      TabIndex        =   4
      Top             =   1680
      Width           =   1155
   End
   Begin UBSCTRLLibCtl.UbsControlMoney ucmRate 
      Height          =   315
      Left            =   1440
      TabIndex        =   2
      Top             =   1200
      Width           =   1575
      _cx             =   268438234
      _cy             =   268436012
      Appearance      =   1
      Enabled         =   -1  'True
      Object.TabStop         =   -1  'True
      Text            =   "0,00"
      Valid           =   -1  'True
      Range           =   14
      Precision       =   2
      SignEnable      =   0
      SeparatorsEnable=   0
      ReadOnly        =   0
   End
   Begin UBSCTRLLibCtl.UbsControlDate ucdDateRate 
      Height          =   315
      Left            =   1440
      TabIndex        =   1
      Top             =   660
      Width           =   1215
      _cx             =   268437599
      _cy             =   268436012
      Appearance      =   1
      Enabled         =   -1  'True
      Object.TabStop         =   -1  'True
      Text            =   "  .  .    "
      Valid           =   -1  'True
      Format          =   1
      ReadOnly        =   0
   End
   Begin VB.Label Label1 
      Alignment       =   1  'Right Justify
      Caption         =   "Тип ставки"
      Height          =   195
      Left            =   360
      TabIndex        =   7
      Top             =   180
      Width           =   915
   End
   Begin VB.Label Label2 
      Alignment       =   1  'Right Justify
      Caption         =   "Дата установки"
      Height          =   195
      Left            =   0
      TabIndex        =   6
      Top             =   720
      Width           =   1275
   End
   Begin VB.Label Label3 
      Alignment       =   1  'Right Justify
      Caption         =   "Ставка"
      Height          =   195
      Left            =   360
      TabIndex        =   5
      Top             =   1260
      Width           =   915
   End
End
Attribute VB_Name = "frmRates"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Public blnApply As Boolean
Dim objInterfaces As Object
Dim dDate2222 As Date
Dim dDate1990 As Date

Private Sub cmdApply_Click()

    If ucdDateRate.DateValue >= dDate2222 Or ucdDateRate.DateValue <= dDate1990 Or Not IsDate(ucdDateRate.DateValue) Then
       MsgBox "Необходимо указать дату", vbExclamation + vbOKOnly, "Ошибка"
       ucdDateRate.SetFocus
       Exit Sub
    End If

    blnApply = True
    Me.Hide
    
End Sub

Private Sub cmdExit_Click()
    Me.Hide
End Sub

Private Sub Form_Initialize()
    Set objInterfaces = CreateObject("ToolPubs.Interfaces")
    dDate2222 = DateSerial(2222, 1, 1)
End Sub

Private Sub Form_KeyPress(KeyAscii As Integer)
    Select Case (KeyAscii)
       Case 13                              'Enter
          KeyAscii = 0
          objInterfaces.NextCtrl
       Case 27                              'Esc
          KeyAscii = 0
          objInterfaces.PrevCtrl
    End Select
End Sub

Private Sub Form_Load()
    blnApply = False
End Sub

Private Sub Form_Unload(Cancel As Integer)
    Set objInterfaces = Nothing
    Set frmRates = Nothing
End Sub
