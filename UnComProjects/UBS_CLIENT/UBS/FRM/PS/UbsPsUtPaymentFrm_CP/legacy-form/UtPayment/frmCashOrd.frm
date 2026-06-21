VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.1#0"; "MSCOMCTL.OCX"
Begin VB.Form frmCashOrd 
   Caption         =   "Подготовка кассовых ордеров"
   ClientHeight    =   3450
   ClientLeft      =   60
   ClientTop       =   405
   ClientWidth     =   8010
   LinkTopic       =   "Form1"
   ScaleHeight     =   3450
   ScaleWidth      =   8010
   StartUpPosition =   1  'CenterOwner
   Begin VB.CommandButton btnMove 
      Caption         =   "Выполнить "
      Height          =   375
      Left            =   2220
      TabIndex        =   1
      Top             =   2880
      Width           =   1575
   End
   Begin VB.CommandButton btnExit 
      Caption         =   "Выход"
      Height          =   375
      Left            =   4140
      TabIndex        =   2
      Top             =   2880
      Width           =   1575
   End
   Begin MSComctlLib.ListView lstDoc 
      Height          =   2535
      Left            =   120
      TabIndex        =   0
      TabStop         =   0   'False
      Top             =   180
      Width           =   7815
      _ExtentX        =   13785
      _ExtentY        =   4471
      View            =   3
      LabelEdit       =   1
      LabelWrap       =   -1  'True
      HideSelection   =   -1  'True
      FullRowSelect   =   -1  'True
      GridLines       =   -1  'True
      _Version        =   393217
      SmallIcons      =   "ImageList1"
      ForeColor       =   0
      BackColor       =   16777215
      BorderStyle     =   1
      Appearance      =   1
      NumItems        =   4
      BeginProperty ColumnHeader(1) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         Text            =   "Счет плательщика"
         Object.Width           =   3881
      EndProperty
      BeginProperty ColumnHeader(2) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   1
         Text            =   "Счет получателя"
         Object.Width           =   3881
      EndProperty
      BeginProperty ColumnHeader(3) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         Alignment       =   1
         SubItemIndex    =   2
         Text            =   "Сумма"
         Object.Width           =   2540
      EndProperty
      BeginProperty ColumnHeader(4) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   3
         Text            =   "Примечание"
         Object.Width           =   35278
      EndProperty
   End
End
Attribute VB_Name = "frmCashOrd"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim varDoc As Variant
Dim varPayments As Variant
Dim varDocuments As Variant
Dim strAccDb As String
Dim intNumDiv As Integer
Dim intIdUser As Integer
Dim datDate As Date
Public TypeExit As Integer
Public UbsChannel As Object
Public varPaymIn As Variant
Public varContractIn As Variant
Public m_nIdPayment As Long
Dim ToolPubs As Object
Dim ToolErr As Object

'--------------------------------Трубаков В.Ю  11.07.2007--------------------
'Для групповых платежей сделаем массив, вместо m_nIdPayment
Public m_nIdPaymentArray As Variant
'----------------------------------------------------------------------------

'---------------------------------- Новопашин В.М. 21.08.2006 г. ------------
Public m_bIsLoadOK As Boolean
'----------------------------------------------------------------------------

Public blnCreate As Boolean
Dim bIsPushApply As Boolean


Sub FillDoc()
Dim i As Integer
Dim objList As Object

On Error GoTo ErrorCode

If Not IsArray(varDoc) Then Exit Sub

lstDoc.ListItems.Clear
For i = 0 To UBound(varDoc, 2)
Set objList = lstDoc.ListItems.Add(, , varDoc(0, i))
         objList.SubItems(1) = Trim$(varDoc(1, i))
         objList.SubItems(2) = Format$(Trim$(varDoc(2, i)), "##############0.00")
         objList.SubItems(3) = Trim$(varDoc(3, i))
'MsgBox lstDoc.ListItems.Count
Next
Exit Sub
ErrorCode:
    ToolErr.UbsErrMsg "Заполнение документов", "ошибка выполнения"
End Sub

' найти счет Дб кассы
'
Sub FindAccDb(bRetVal As Boolean)
    bRetVal = False
    
    Dim objParamIn As Object
    Dim objParamOut As Object
    On Error GoTo ErrorCode
    
    Set objParamIn = CreateObject("ToolPubs.IUbsParam")
    Set objParamOut = CreateObject("ToolPubs.IUbsParam")
    UbsChannel.Run "UtGetGlobalUserData", objParamIn, objParamOut
    datDate = CDate(objParamOut.Parameter("DATEDOC"))
    
    UbsChannel.Run "Ps_FindAccCash1", objParamIn, objParamOut
    
    If Not CBool(objParamOut.Parameter("bRetVal")) Then
        btnMove.Enabled = False
        MsgBox objParamOut.Parameter("StrError"), vbOKOnly, "Подготовка кассовых ордеров"
        Exit Sub
    End If
    strAccDb = objParamOut.Parameter("StrAccCash")
    intNumDiv = objParamOut.Parameter("NumDiv")
    
    Set objParamIn = Nothing
    Set objParamOut = Nothing
  
    bRetVal = True
    Exit Sub
ErrorCode:
    ToolErr.UbsErrMsg "FindAccDb", "ошибка выполнения"
 End Sub

' получить данные по документам
'
Sub GetPayms(bRetVal As Boolean)

    bRetVal = False
 
    Dim objParamIn As Object, objParamOut As Object
 
    Dim i As Integer
    On Error GoTo ErrorCode
    Set objParamIn = CreateObject("ToolPubs.IUbsParam")
    Set objParamOut = CreateObject("ToolPubs.IUbsParam")
    
    objParamIn.ClearParameters
    objParamOut.ClearParameters
    
    '------------------------------------ Новопашин В.М. 07.09.2006 г. -----
    'Режим вызова - при приеме платежа
    objParamIn.Parameter("REGIM") = 1
    '-----------------------------------------------------------------------

    objParamIn.Parameter("NUMDIV") = intNumDiv
    objParamIn.Parameter("DATEDOC") = datDate
    objParamIn.Parameter("DATETRN") = datDate
    objParamIn.Parameter("ACCDB") = strAccDb
    objParamIn.Parameter("VARPAYMENTS") = varPaymIn
    objParamIn.Parameter("VARCONTRACT") = varContractIn
    
    UbsChannel.Run "UtGetDataCashOrder", objParamIn, objParamOut
    If Not objParamOut.Parameter("BRETVAL") Then
        btnMove.Enabled = False
        MsgBox objParamOut.Parameter("StrError"), 16, "Подготовка кассовых ордеров"
        lstDoc.ListItems.Clear
        Exit Sub
    End If
    varDoc = objParamOut.Parameter("VARDOC")
    varDocuments = objParamOut.Parameter("Документы")
     
   Set objParamIn = Nothing
   Set objParamOut = Nothing
  
   bRetVal = True
   
   Exit Sub
   
ErrorCode:
    ToolErr.UbsErrMsg "GetPayms", "ошибка выполнения"
    
 End Sub
 
Private Sub btnExit_Click()
On Error GoTo ErrorCode
  TypeExit = 0
  Hide
  Exit Sub
ErrorCode:
  ToolErr.UbsErrMsg "ExitForm_Click", "ошибка выполнения"
End Sub

 Sub btnMove_Click()
    
    ' если не нажата
    If Not bIsPushApply Then
         ' нажата
         bIsPushApply = True
    
         Dim objParamIn As Object, objParamOut As Object
         TypeExit = 1
         Dim i As Integer
         Dim varSinglContract As Variant
         
         blnCreate = False
         
         On Error GoTo ErrorCode
         Set objParamIn = CreateObject("ToolPubs.IUbsParam")
         Set objParamOut = CreateObject("ToolPubs.IUbsParam")

         objParamIn.Parameter("VARPAYMENTS") = varPaymIn
         objParamIn.Parameter("ACCDB") = strAccDb
         objParamIn.Parameter("DATEDOC") = datDate
         objParamIn.Parameter("NUMDIV") = intNumDiv
         ' для установления доп. поля Подг.касс.орд. в 1
         objParamIn.Parameter("IsBySeparPaym") = True  'касс.орд. по конкр. платежу
         ' Для групповых платежей сделаем массив, вместо m_nIdPayment
         If IsEmpty(m_nIdPaymentArray) Then
             objParamIn.Parameter("IdPayment") = m_nIdPayment
         Else
             objParamIn.Parameter("IdPayment") = m_nIdPaymentArray
         End If

        If Len(Trim(strAccDb)) > 0 Then
            objParamIn.Parameter("Документы") = varDocuments
            UbsChannel.Run "UtMainCashOrder", objParamIn, objParamOut
          
            If objParamOut.Parameter("BRETVAL") = False Then
                Call MsgBox(objParamOut.Parameter("StrError"), 16, "Создание кассовых ордеров")
            Else
                blnCreate = True
               lstDoc.ListItems.Clear
            End If
        Else
            Call MsgBox("Не найден кассовый счет для выбранного оператора", 16, "Создание кассовых ордеров")
        End If
        
        Call btnExit_Click
         
        Set objParamIn = Nothing
        Set objParamOut = Nothing
       
        ' не нажата
        bIsPushApply = False
    End If
    
  Exit Sub

ErrorCode:
    ToolErr.UbsErrMsg "Подготовка документов", "ошибка выполнения"
End Sub

Private Sub Form_Activate()
   If m_bIsLoadOK Then btnMove.SetFocus
End Sub

 Sub Form_Load()
 
On Error GoTo ErrorCode
  Dim bRet As Boolean

  Set ToolPubs = CreateObject("ToolPubs.Interfaces")
  Set ToolErr = CreateObject("ToolPubs.IUbsError")

 ' найти счет Дб кассы
 Call FindAccDb(bRet)
 If bRet Then
    ' получить данные по документам
    Call GetPayms(bRet)
    If bRet Then
       ' заполнить документы
       Call FillDoc
    End If
 End If
 If bRet Then
    m_bIsLoadOK = True
 Else
    m_bIsLoadOK = False
 End If
 
 Exit Sub
ErrorCode:
    ToolErr.UbsErrMsg "Загрузка формы", "ошибка выполнения"
End Sub

Private Sub Form_Unload(Cancel As Integer)
On Error GoTo ErrorCode
  Set ToolPubs = Nothing
  Set ToolErr = Nothing
  Set UbsChannel = Nothing
  Exit Sub
ErrorCode:
  ToolErr.UbsErrMsg "Form_Unload", "ошибка выполнения"
End Sub
