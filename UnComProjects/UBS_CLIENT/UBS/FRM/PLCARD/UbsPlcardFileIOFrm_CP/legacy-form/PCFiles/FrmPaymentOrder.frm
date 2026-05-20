VERSION 5.00
Begin VB.Form frmPaymentOrder 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Платежные поручения"
   ClientHeight    =   3720
   ClientLeft      =   45
   ClientTop       =   345
   ClientWidth     =   5640
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3720
   ScaleWidth      =   5640
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdOK 
      Caption         =   "Выбор"
      Default         =   -1  'True
      Height          =   375
      Left            =   3180
      TabIndex        =   2
      Top             =   3240
      Width           =   1092
   End
   Begin VB.CommandButton cmdCancel 
      Cancel          =   -1  'True
      Caption         =   "Отмена"
      Height          =   375
      Left            =   4320
      TabIndex        =   1
      Top             =   3240
      Width           =   1092
   End
   Begin VB.ListBox ListPaymentOrder 
      Height          =   2985
      Left            =   180
      TabIndex        =   0
      Top             =   120
      Width           =   5235
   End
End
Attribute VB_Name = "frmPaymentOrder"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Public Relult As Long

Private Sub cmdCancel_Click()
    Relult = vbCancel
    Me.Hide
End Sub

Private Sub cmdOK_Click()
    Relult = vbOK
    Me.Hide
End Sub

Private Sub ListPaymentOrder_DblClick()
    If (ListPaymentOrder.ListIndex >= 0) Then _
        Call cmdOK_Click
End Sub
