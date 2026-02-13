VERSION 5.00
Begin VB.Form frmDetailsBenificiar 
   Caption         =   "Реквизиты бенефициара"
   ClientHeight    =   5115
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7335
   ControlBox      =   0   'False
   Icon            =   "frmDetailsBenificiar.frx":0000
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   ScaleHeight     =   5115
   ScaleWidth      =   7335
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdExit 
      Caption         =   "Выход"
      Height          =   375
      Left            =   5880
      TabIndex        =   32
      Top             =   4560
      Width           =   1335
   End
   Begin VB.CommandButton cmdSave 
      Caption         =   "Применить"
      Height          =   375
      Left            =   4080
      TabIndex        =   31
      Top             =   4560
      Width           =   1455
   End
   Begin VB.Frame frAddress 
      Caption         =   "Адрес"
      Height          =   3495
      Left            =   120
      TabIndex        =   4
      Top             =   960
      Width           =   7095
      Begin VB.TextBox txtStreet 
         Height          =   315
         Left            =   2760
         TabIndex        =   24
         Top             =   2520
         Width           =   4215
      End
      Begin VB.TextBox txtSettl 
         Height          =   315
         Left            =   2760
         TabIndex        =   21
         Top             =   2160
         Width           =   4215
      End
      Begin VB.TextBox txtCity 
         Height          =   315
         Left            =   2760
         TabIndex        =   18
         Top             =   1800
         Width           =   4215
      End
      Begin VB.TextBox txtArea 
         Height          =   315
         Left            =   2760
         TabIndex        =   15
         Top             =   1440
         Width           =   4215
      End
      Begin VB.TextBox txtRegion 
         Height          =   315
         Left            =   2760
         TabIndex        =   12
         Top             =   1080
         Width           =   4215
      End
      Begin VB.ComboBox cmbCountry 
         Height          =   315
         Left            =   2760
         Style           =   2  'Dropdown List
         TabIndex        =   9
         Top             =   720
         Width           =   4215
      End
      Begin VB.ComboBox cmbCodeCountry 
         Height          =   315
         Left            =   1200
         Sorted          =   -1  'True
         Style           =   2  'Dropdown List
         TabIndex        =   8
         Top             =   720
         Width           =   1455
      End
      Begin VB.TextBox txtFlat 
         Height          =   315
         Left            =   6000
         TabIndex        =   30
         Top             =   2880
         Width           =   975
      End
      Begin VB.ComboBox cmbTypeFlat 
         Height          =   315
         Left            =   5040
         TabIndex        =   29
         Top             =   2880
         Width           =   975
      End
      Begin VB.TextBox txtHousing 
         Height          =   315
         Left            =   3720
         TabIndex        =   28
         Top             =   2880
         Width           =   1095
      End
      Begin VB.ComboBox cmbTypeHousing 
         Height          =   315
         Left            =   2760
         TabIndex        =   27
         Top             =   2880
         Width           =   975
      End
      Begin VB.TextBox txtHome 
         Height          =   315
         Left            =   1200
         TabIndex        =   26
         Top             =   2880
         Width           =   1455
      End
      Begin VB.ComboBox cmbTypeHome 
         Height          =   315
         Left            =   120
         TabIndex        =   25
         Top             =   2880
         Width           =   1095
      End
      Begin VB.ComboBox cmbTypeStreet 
         Height          =   315
         Left            =   1200
         TabIndex        =   23
         Top             =   2520
         Width           =   1455
      End
      Begin VB.ComboBox cmbTypeSettl 
         Height          =   315
         Left            =   1200
         TabIndex        =   20
         Top             =   2160
         Width           =   1455
      End
      Begin VB.ComboBox cmbTypeCity 
         Height          =   315
         Left            =   1200
         TabIndex        =   17
         Top             =   1800
         Width           =   1455
      End
      Begin VB.ComboBox cmbTypeArea 
         Height          =   315
         Left            =   1200
         TabIndex        =   14
         Top             =   1440
         Width           =   1455
      End
      Begin VB.ComboBox cmbTypeRegion 
         Height          =   315
         Left            =   1200
         TabIndex        =   11
         Top             =   1080
         Width           =   1455
      End
      Begin VB.TextBox txtIndex 
         Height          =   285
         Left            =   5880
         TabIndex        =   6
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label lblCountry 
         Caption         =   "Страна"
         Height          =   210
         Left            =   120
         TabIndex        =   7
         Top             =   772
         Width           =   735
      End
      Begin VB.Label lblStreet 
         Caption         =   "Улица"
         Height          =   210
         Left            =   120
         TabIndex        =   22
         Top             =   2572
         Width           =   975
      End
      Begin VB.Label lblSettl 
         Caption         =   "Насел. пункт"
         Height          =   210
         Left            =   120
         TabIndex        =   19
         Top             =   2212
         Width           =   1095
      End
      Begin VB.Label lblCity 
         Caption         =   "Город"
         Height          =   210
         Left            =   120
         TabIndex        =   16
         Top             =   1852
         Width           =   615
      End
      Begin VB.Label lblArea 
         Caption         =   "Район"
         Height          =   210
         Left            =   120
         TabIndex        =   13
         Top             =   1492
         Width           =   735
      End
      Begin VB.Label lblRegion 
         Caption         =   "Регион"
         Height          =   210
         Left            =   120
         TabIndex        =   10
         Top             =   1132
         Width           =   735
      End
      Begin VB.Label lblIndex 
         Caption         =   "Индекс"
         Height          =   255
         Left            =   5040
         TabIndex        =   5
         Top             =   375
         Width           =   735
      End
   End
   Begin VB.TextBox txtINN 
      Height          =   285
      Left            =   1440
      MaxLength       =   12
      TabIndex        =   3
      Top             =   480
      Width           =   3135
   End
   Begin VB.TextBox txtName 
      Height          =   285
      Left            =   1440
      TabIndex        =   1
      Top             =   120
      Width           =   5775
   End
   Begin VB.Label lblINN 
      Alignment       =   1  'Right Justify
      Caption         =   "ИНН"
      Height          =   210
      Left            =   120
      TabIndex        =   2
      Top             =   517
      Width           =   1215
   End
   Begin VB.Label lblName 
      Alignment       =   1  'Right Justify
      Caption         =   "Наименование"
      Height          =   215
      Left            =   120
      TabIndex        =   0
      Top             =   155
      Width           =   1215
   End
End
Attribute VB_Name = "frmDetailsBenificiar"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim blnGetType As Boolean
Dim i As Integer
Dim objParamIn As Object
Dim objParamOut As Object
Dim objInterfaces As Object
Public blnApply As Boolean
Public arrTypeObject
Public arrCountry

Dim blnUseContry As Boolean

Private Sub cmbCodeCountry_Click()
      
  Dim i
  
  If blnUseContry Then Exit Sub
  
  blnUseContry = True
  
  If (Not IsArray(arrCountry)) Then Exit Sub
  
  For i = 0 To UBound(arrCountry, 2)
      If (Trim(CStr(arrCountry(0, i))) = Trim(CStr(cmbCodeCountry.Text))) Then
          cmbCountry.Text = CStr(arrCountry(1, i))
          Exit For
      End If
  Next
  
  Call ChangeTypeObject
    
  blnUseContry = False
  
End Sub

Private Sub cmbCountry_Click()

  Dim i
  
  If blnUseContry Then Exit Sub
  
  blnUseContry = True
  
  If (Not IsArray(arrCountry)) Then Exit Sub
  
  For i = 0 To UBound(arrCountry, 2)
      If (Trim(CStr(arrCountry(1, i))) = Trim(CStr(cmbCountry.Text))) Then
          cmbCodeCountry.Text = CStr(arrCountry(0, i))
          Exit For
       End If
  Next
  
  Call ChangeTypeObject
    
  blnUseContry = False

End Sub

Private Sub ChangeTypeObject()

   If IsEmpty(arrTypeObject) Then Exit Sub

   If cmbCodeCountry.Text <> "643" Then
      cmbTypeRegion.Clear
      cmbTypeArea.Clear
      cmbTypeCity.Clear
      cmbTypeSettl.Clear
      cmbTypeStreet.Clear
      cmbTypeHome.Clear
      cmbTypeHousing.Clear
      cmbTypeFlat.Clear
   Else
      If (cmbTypeRegion.ListCount > 0 Or cmbTypeArea.ListCount > 0 Or cmbTypeCity.ListCount > 0 Or cmbTypeSettl.ListCount > 0 Or _
          cmbTypeStreet.ListCount > 0 Or cmbTypeHome.ListCount > 0 Or cmbTypeHousing.ListCount > 0 Or cmbTypeFlat.ListCount > 0) Then
          Exit Sub
      End If
      
      For i = 0 To UBound(arrTypeObject, 2)
      
          Select Case CInt(arrTypeObject(0, i))
                 Case 1:
                    cmbTypeRegion.AddItem (arrTypeObject(1, i))
                 Case 2:
                    cmbTypeArea.AddItem (arrTypeObject(1, i))
                 Case 3:
                    cmbTypeCity.AddItem (arrTypeObject(1, i))
                 Case 4:
                    cmbTypeSettl.AddItem (arrTypeObject(1, i))
                 Case 5:
                    cmbTypeStreet.AddItem (arrTypeObject(1, i))
                 Case 6:
                    cmbTypeHome.AddItem (arrTypeObject(1, i))
                 Case 7:
                    cmbTypeHousing.AddItem (arrTypeObject(1, i))
                 Case 8:
                    cmbTypeFlat.AddItem (arrTypeObject(1, i))
          End Select

      Next
   End If

End Sub

Private Sub cmdExit_Click()
    blnApply = False
    Me.Hide
End Sub

Private Sub cmdSave_Click()
    blnApply = True
    Me.Hide
End Sub

Private Sub Form_Initialize()
    Set objInterfaces = CreateObject("ToolPubs.Interfaces")
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
    blnUseContry = False
End Sub
