VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.2#0"; "MSCOMCTL.OCX"
Begin VB.Form frmInstr 
   Caption         =   "Инструкции по оплате"
   ClientHeight    =   2895
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   5595
   LinkTopic       =   "Form1"
   ScaleHeight     =   2895
   ScaleWidth      =   5595
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdChoice 
      Caption         =   "Выбор"
      Height          =   375
      Left            =   3120
      TabIndex        =   2
      Top             =   2400
      Width           =   1095
   End
   Begin VB.CommandButton cmdCancel 
      Caption         =   "Отмена"
      Height          =   375
      Left            =   4320
      TabIndex        =   1
      Top             =   2400
      Width           =   1095
   End
   Begin MSComctlLib.ListView lvInstrOplata 
      Height          =   2175
      Left            =   240
      TabIndex        =   0
      Top             =   120
      Width           =   5175
      _ExtentX        =   9128
      _ExtentY        =   3836
      View            =   3
      LabelEdit       =   1
      LabelWrap       =   -1  'True
      HideSelection   =   0   'False
      FullRowSelect   =   -1  'True
      _Version        =   393217
      ForeColor       =   -2147483640
      BackColor       =   -2147483643
      BorderStyle     =   1
      Appearance      =   1
      NumItems        =   8
      BeginProperty ColumnHeader(1) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         Text            =   "БИК банка"
         Object.Width           =   2540
      EndProperty
      BeginProperty ColumnHeader(2) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   1
         Text            =   "Наименование банка"
         Object.Width           =   2540
      EndProperty
      BeginProperty ColumnHeader(3) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   2
         Text            =   "К/с"
         Object.Width           =   2540
      EndProperty
      BeginProperty ColumnHeader(4) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   3
         Text            =   "Р/с"
         Object.Width           =   2540
      EndProperty
      BeginProperty ColumnHeader(5) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   4
         Text            =   "Наименование клиента"
         Object.Width           =   2540
      EndProperty
      BeginProperty ColumnHeader(6) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   5
         Text            =   "Примечание"
         Object.Width           =   2540
      EndProperty
      BeginProperty ColumnHeader(7) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   6
         Text            =   "ИНН"
         Object.Width           =   2540
      EndProperty
      BeginProperty ColumnHeader(8) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   7
         Text            =   "Безакцептное списание"
         Object.Width           =   776
      EndProperty
   End
End
Attribute VB_Name = "frmInstr"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim blnChoice As Boolean
Dim RSOut As Variant

Public Sub StartForm(ptoolerr, ArrInstr)
On Error GoTo ErrorCode

    Set toolerr = ptoolerr
        If Not IsEmpty(ArrInstr) Then
        For n = LBound(ArrInstr, 2) To UBound(ArrInstr, 2)
            Set objList = lvInstrOplata.ListItems.Add(, , ArrInstr(0, n)) 'БИК
            objList.SubItems(1) = ArrInstr(1, n) 'Наименование банка
            objList.SubItems(2) = ArrInstr(2, n) 'К/с
            objList.SubItems(3) = ArrInstr(3, n) 'Р/с
            objList.SubItems(4) = ArrInstr(4, n) 'Наименование клиента
            objList.SubItems(5) = ArrInstr(5, n) 'Примечание
            objList.SubItems(6) = ArrInstr(6, n) 'ИНН
            objList.SubItems(7) = ArrInstr(7, n) 'Безакцептное списание
        Next
    End If

    Exit Sub
    
ErrorCode:
    toolerr.UbsMsgRaise "StartForm", "ошибка выполнения"
End Sub

Private Sub cmdCancel_Click()
    blnChoice = False
    Me.Hide
End Sub

Private Sub cmdChoice_Click()
    ReDim RSOut(7)
    
    If lvInstrOplata.ListItems.Count = 0 Then Exit Sub
    
    RSOut(0) = lvInstrOplata.SelectedItem.Text
    RSOut(1) = lvInstrOplata.SelectedItem.SubItems(1)
    RSOut(2) = lvInstrOplata.SelectedItem.SubItems(2)
    RSOut(3) = lvInstrOplata.SelectedItem.SubItems(3)
    RSOut(4) = lvInstrOplata.SelectedItem.SubItems(4)
    RSOut(5) = lvInstrOplata.SelectedItem.SubItems(5)
    RSOut(6) = lvInstrOplata.SelectedItem.SubItems(6)
    RSOut(7) = lvInstrOplata.SelectedItem.SubItems(7)
    
    blnChoice = True
    Me.Hide
End Sub

Public Function IsChoice() As Boolean
    IsChoice = blnChoice
End Function

Public Function GetInstr() As Variant
    GetInstr = RSOut
End Function

Private Sub lvInstrOplata_DblClick()
    Call cmdChoice_Click
End Sub
