' Дообработка файлов с зарплатой/пополнением/списанием

'#include UBS_VBS\PLCARD\Lib\PCLib.vbs
'#include UBS_VBS\PLCARD\Lib\PCLibFile.vbs
'#include UBS_VBS\PLCARD\LIB\PCLibGetDescription.vbs
'#include UBS_VBS\PLCARD\Processing\PCPRCFile.vbs
'#include UBS_VBS\PLCARD\Lib\PCLibLock.vbs

Sub Pc_GetFileFolder(objParamIn, objParamOut)
    Dim FileFolder
    Dim RegKey
    Dim objWshShell : Set objWshShell = CreateObject("WScript.Shell")
    
    objParamOut.ClearParameter("FileFolder")
    
    RegKey = "HKLM\SOFTWARE\UniSAB\UBS\FOLDERS\UbsPLCARDFileRefill\" & _
                Replace(GlobalUser.CommonSetup("DatabaseName"), "\", "#")
    
    If objParamIn.Parameter("StrCommand") = "POPOLNENIE" Then
        On Error Resume Next
        FileFolder = objWshShell.RegRead(RegKey)
        On Error GoTo 0
    End If
    
    objParamOut.Parameter("FileFolder") = FileFolder
    objParamOut.Parameter("RegKey") = RegKey
End Sub

Sub PCSalaryImport_GetTypePaymentOrder(objParamIn, objParamOut)
    Dim var
    
    If IsBusinessActivated("RC") Then       AddRowToArray var, 2, Array(0, "RC", "Платежное поручение Расчетного центра")
    If IsBusinessActivated("CLBANK") Then   AddRowToArray var, 2, Array(1, "CLBANK", "Документ Клиент-банка")
    AddRowToArray var, 2, Array(2, "OD_WAIT", "Отложенные платежные документы раздела А")
    AddRowToArray var, 2, Array(3, "OD_DEL", "Удаленные платежные документы раздела А")

    objParamOut.Parameter("Типы платежных поручений") = var
End Sub

Sub PCSalaryImport_ReadClient(objParamIn, objParamOut)
    Dim var, n  : n = CLng(objParamIn.Parameter("Клиент.Идентификатор"))
    
    If ReadBase(" select ID_CLIENT, LONG_NAME from CLIENTS where ID_CLIENT = " & n, var) > 0 Then
        objParamOut.Parameter("Клиент.Идентификатор") = CLng(var(0, 0))
        objParamOut.Parameter("Клиент.Полное наименование") = Trim(var(1, 0))
        objParamOut.Parameter("bResult") = True
    Else
        objParamOut.Parameter("bResult") = False
        objParamOut.Parameter("StrReport") = "Клиент ид. "  & n & " не найден."
    End If
End Sub

Sub PCSalaryImport_ReadPaymOrder(objParamIn, objParamOut)
    Dim PaymentOrder
    Dim PaymentOrderId      : PaymentOrderId    = CLng(objParamIn.Parameter("Сводный платеж.Идентификатор"))
    Dim PaymentOrderType    : PaymentOrderType  = objParamIn.Parameter("Сводный платеж.Бизнес")
    Dim strNote
    Dim var, i
    
    Select Case PaymentOrderType
        Case "CLBANK"
            If ReadBase(" select D.NDOC, D.DATE_DOC, D.STRACCOUNT_DB, D.OBOROT_DB, D.MEANING, D.STRACCOUNT_CR, D.STATE_DOC from CLBANK_DOCUMENT D " & _
                        " where ID_DOC = " & PaymentOrderId, var) > 0 Then
                
                If CLng(var(6, 0)) <> 4 And CLng(var(6, 0)) <> 7 Then
                    objParamOut.Parameter("StrReport") = "Недопустимое состояние сводного платежа."
                    Exit Sub
                End If
                
                strNote = PaymentOrderType & ": №" & Trim(var(0, 0)) & " от " & DToS4(var(1, 0)) & _
                    " на " & MToS2(var(3, 0)) & " " & PCCurrencyDictionary.CodeISO(Mid(var(2, 0), 6, 3))
                
                If Trim(var(4, 0)) <> "" Then _
                    strNote = strNote & ". " & Trim(var(4, 0))
                
                objParamOut.Parameter("Сводный платеж.Описание") = strNote
                objParamOut.Parameter("Сводный платеж.Счет плательщика") = Trim(var(2, 0))
                objParamOut.Parameter("Сводный платеж.Счет получателя") = Trim(var(5, 0))
                objParamOut.Parameter("Сводный платеж.Сумма") = CCur(var(3, 0))
                
                objParamOut.Parameter("bResult") = True
            Else
                objParamOut.Parameter("StrReport") = "Документ клиент-банка ид. " & PaymentOrderId & " не найден."
            End If
        Case "RC"
            Set PaymentOrder = UbsCreateObject2("UbsPayment.1", "UbsPayment", Scripter)
            PaymentOrder.Read PaymentOrderId

            strNote = PaymentOrderType & ": №" & PaymentOrder.Param("Ndoc") & " от " & DToS4(PaymentOrder.DPP) & _
                " на " & MToS2(PaymentOrder.SummaPayment) & " " & PCCurrencyDictionary.CodeISO(Mid(PaymentOrder.Param("AccPayer"), 6, 3))
            objParamOut.Parameter("Сводный платеж.Описание") = strNote
            objParamOut.Parameter("Сводный платеж.Счет плательщика") = PaymentOrder.Param("AccPayer")
            objParamOut.Parameter("Сводный платеж.Счет получателя") = PaymentOrder.Param("AccBen")
            objParamOut.Parameter("Сводный платеж.Сумма") = PaymentOrder.SummaPayment
            
            objParamOut.Parameter("bResult") = True
        Case "OD_DEL", "OD_WAIT"
            If ReadBase("select NUM_DOC, DATE_DOC, STRACCOUNT_P, STRACCOUNT_R, OBOROT_DB, ID_CURRENCY_DB, NOTE_TRN, NAME_CLI_P " & _
                "from OD_DOC_0 where ID_DOC = " & PaymentOrderId, var) > 0 Then

                strNote = "OD: №" & Trim(var(0, 0)) & " от " & DToS4(var(1, 0)) & _
                    " на " & MToS2(var(4, 0)) & " " & PCCurrencyDictionary.CodeISO(CLng(var(5, 0)))
                
                If Trim(var(6, 0)) <> "" Then _
                    strNote = strNote & ". " & Trim(var(6, 0))
                
                objParamOut.Parameter("Сводный платеж.Описание") = strNote
                objParamOut.Parameter("Сводный платеж.Счет плательщика") = Trim(var(2, 0))
                objParamOut.Parameter("Сводный платеж.Счет получателя") = Trim(var(3, 0))
                objParamOut.Parameter("Сводный платеж.Сумма") = CCur(var(4, 0))
                
                strNote = "Зачисление выполнено на основании п/п: " & _
                    Trim(var(7, 0)) & ", номер " & Trim(var(0, 0)) & ", от " & DToS4(var(1, 0)) & ", " & _
                    MToS2(var(4, 0)) & " " & PCCurrencyDictionary.CodeISO(CLng(var(5, 0))) & ", " & Trim(var(6, 0))
                objParamOut.Parameter("Сводный платеж.Описание п/п") = strNote
                
                objParamOut.Parameter("bResult") = True
            Else
                objParamOut.Parameter("StrReport") = "Документ ид. " & PaymentOrderId & " не найден."
            End If
        Case Else
            Err.Raise vbObjectError + 1, "PCSalaryImport_ReadPaymOrder", _
                "Недопустимый тип платежного поручения '" & PaymentOrderType & "'."
    End Select

    If objParamOut.Parameter("Сводный платеж.Счет получателя") <> "00000000000000000000" And _
       objParamOut.Parameter("Сводный платеж.Счет получателя") <> "" Then
            objParamOut.Parameter("bResult") = False
            objParamOut.Parameter("StrReport") = "Счет получателя должен быть пустым или равняться '00000000000000000000'."
    End If
End Sub

Function PCSalaryImport_Undo(objParamIn, objParamOut)
    Dim objScript   : Set objScript = UbsCreateObject2("UbsRunScript.1", "URunScr.IUbsRunScript", Scripter)
    objScript.LoadFiles "UBS_VBS\PLCARD\PCUndoFileZp.vbs;UBS_VBS\PLCARD\PCUndoOperation.vbs"
    objScript.Parameter("UndoOnImportError") = objParamIn.Parameter("UndoOnImportError")
    objScript.Run "PCUndo", Array(CLng(objParamIn.Parameter("Идентификатор файла")))
End Function

Sub PCSalaryImport_CheckMoveFunds(objParamIn, objParamOut)
    Dim objAccountConfirm : Set objAccountConfirm = UbsCreateObject2("UbsOdAccountConfirm.1", "UbsBusiness.UbsOdAccountConfirm", Scripter)
    Dim var

    objParamOut.Parameter("bResult") = False
    
    ' varAccountId, varOperationDate, varSummaDB, varStraccountRecipient, varKindDoc, varPriorityPay, varKbk, varCashSymbols, varDocumentId
    var = Trim(objAccountConfirm.CheckSaldo(CLng(objParamIn.Parameter("Salary.Payment.IdAccDB")), _
                                              CDate(objParamIn.Parameter("DateTrn")), _
                                              CCur(objParamIn.Parameter("Salary.Payment.curDB")) + CCur(objParamIn.Parameter("Salary.Fee.curDB")), _
                                              Empty, _
                                              Empty, _
                                              Empty, _
                                              Empty, _
                                              Empty, _
                                              Empty))
    
    objParamOut.Parameter("bResult") = (var = "")
    objParamOut.Parameter("StrReport") = var
End Sub

Sub PCSalaryImport_RegisterFileSourceParam(objParamIn, objParamOut)
    Dim strReport
    
    BeginTransact
    Call PCSalaryImport_RegisterFileSource(CLng(objParamIn.Parameter("Идентификатор файла")), _
                                           objParamIn.Parameter("Сводный платеж.Бизнес"), _
                                           CLng(objParamIn.Parameter("Сводный платеж.Идентификатор")), _
                                           strReport)
    objParamOut.Parameter("strReport") = strReport
    CompleteTransact
End Sub

Function PCSalaryImport_RegisterFileSource(ByVal FileId, ByVal PaymOrderBusiness, ByVal PaymOrderID, varReport)
    Dim PaymOrder

    PCSalaryImport_RegisterFileSource = False
    
    Select Case PaymOrderBusiness
        Case "CLBANK"
            Set PaymOrder = UbsCreateObject2("UbsCliBankDoc.1", "UbsCliBankDoc", Scripter)
            PaymOrder.Read CLng(PaymOrderId)
            PaymOrder.IdStateDoc = 55
            PaymOrder.Modify
            PCWriteReportLine varReport, "Сводный платеж передан системе расчетов."
        Case "OD_DEL", "OD_WAIT"
            Set PaymOrder = UbsCreateObject2("UbsPayDoc0.1", "UbsPayDoc0", Scripter)
            PaymOrder.Read CLng(PaymOrderId)
            PaymOrder.PutAddField "Идентификатор файла пополнения (PLCARD)", FileId
            PaymOrder.Modify
            
            PaymOrderBusiness = "OD"
    End Select

    PCSalaryImport_RegisterFileSource = PCLibFile_RegisterFileSource(FileId, PaymOrderBusiness, PaymOrderID, varReport)
End Function

Function PCSalaryImport_CreateSvodAvizo(objParamIn, objParamOut)
    BeginTransact
    
    PCSalaryImport_CreateSvodAvizo = PCSalaryImport_CreateSvodAvizo_Inner(objParamIn, objParamOut)
    
    If PCSalaryImport_CreateSvodAvizo Then
        CompleteTransact
    Else
        AbortTransact
    End If
End Function

Function PCSalaryImport_CreateSvodAvizo_Inner(objParamIn, objParamOut)
    Dim IdOper
    Dim objScript   : Set objScript = UbsCreateObject2("UbsRunScript.1", "URunScr.IUbsRunScript", Scripter) : objScript.NotAbort = False
    Dim strReport
    
    PCSalaryImport_CreateSvodAvizo_Inner = False
    
    If Not InTransact Then err.Raise 1, "PCSalaryImport_CreateSvodAvizo", "Вызов ф-ии не в транзакции"

    objScript.Clear
    objScript.NotAbort = True
    objScript.LoadFiles "UBS_VBS\RC\RCApprovalSvodAvizo.vbs"

    objScript.SuppresError = True

    objParamOut.Parameter("bResult") = False
    strReport = "Создание сводного авизо по платежу 'Расчетного центра'." & vbNewLine

    IdOper = objScript.Run("RCApprovalSvodAvizo", _
                            objParamIn.Parameter("Сводный платеж.Идентификатор"), _
                            objParamIn.Parameter("Сводный платеж.Массив документов"), _
                            strReport)
    objParamOut.Parameter("bResult") = CBool(IdOper > 0)
    
    If objScript.LastErrorNumber <> 0 Then _
        PCWriteError strReport, objScript.LastErrorDescription
        
    objScript.SuppresError = False

    If IdOper > 0 Then
        Dim objLock
        Set objLock = UbsCreateObject2("UbsObjectLock.1", "UbsObjectLock", Scripter)
        objLock.LockTbl = "RC_OPERATION"
        objLock.SetLock "UBS_PLCARD_FILE_IMPORT", IdOper, 0
    End If

    objParamOut.Parameter("strReport") = strReport
    PCSalaryImport_CreateSvodAvizo_Inner = CBool(IdOper > 0)
End Function

Function PCSalaryImport_CanProcess(pIn, pOut)
    Dim IdFile : IdFile = CLng(pIn.Parameter("Ид.файла"))

    If ReadBase("select " & IIf(IsSqlClient, "top 1", "") & " 0 from CARD_FILE_ACTIONS " & _
                " where ID_FILE = " & IdFile & IIf(IsSqlClient, "", " and ROWNUM = 1"), Empty) > 0 Then
        pOut.Parameter("strError") = "Файл загружен по скоростной технологии." & vbNewLine & _
                                         "Довыполнение файла может быть выполнено из списка 'Файлы обмена.'"
    End If

End Function

Sub PCSalaryImport_GetDataByParam(objParamIn, objParamOut)
    Dim objFilter
	Dim lngFileID, arrData
	
	lngFileID = objParamIn.Parameter("IdFile")
	
	PCSalaryImport_GetData lngFileID, arrData
	
	objParamOut.Parameter("arrData") = arrData
End Sub

Sub PCSalaryImport_GetData(ByVal lngFileID, ByRef arrData)
    Dim objFilter
    
    Set objFilter = UbsCreateObject4("UbsFilter")
    objFilter.UbsUser = GlobalUser
    objFilter.DataAccess = GlobalDataAccess
    objFilter.Read "UBS_LIST_PLCARD_SALARY" 'файл UBS_FLT\PLCARD\salary.flt
    objFilter.ClearAllLists
    
    objFilter.CheckSelectItem "Счет карты", 1
    objFilter.CheckSelectItem "Сумма", 1
    objFilter.CheckSelectItem "Идентификатор ответственного клиента", 1
    objFilter.CheckSelectItem "Корреспондирующий счет", 1
    objFilter.CheckSelectItem "От доверенного лица", 1

    objFilter.AddWhereItem "Идентификатор файла", 4, CLng(lngFileID)
    objFilter.AddWhereItem "Идентификатор операции", 4, 0 '"Тип объекта", 4, "CLIENTS" 'enEQ, "CLIENTS"
    objFilter.CheckOrderItem "Идентификатор записи", 1, 1
    objFilter.CheckSelectItem "ФИО клиента", 1
    objFilter.CheckSelectItem "Номер карты", 1
      
    objFilter.GetRecords arrData
End Sub

Sub PCSalaryImport_ProcessData(objParamIn, objParamOut)
    Dim bDoOper
    Dim CC                  : Set CC = UbsCreateObject2("UbsPlCardCore.1", "PlCardCore.IUbsPlCardCore", Scripter)
    Dim arrData             : arrData               = objParamIn.Parameter("arrData")
    Dim blnControlRun       : blnControlRun         = CBool(objParamIn.Parameter("blnControlRun")) Or CBool(objParamIn.Parameter("TestMode"))
    Dim bResult
    Dim curOpers            : curOpers              = CCur(0)
    Dim datOper             : datOper               = objParamIn.Parameter("datOper")
    Dim IdAccCorr
    Dim IdFile              : IdFile                = CLng(objParamIn.Parameter("Ид.файла"))
    Dim objReportBuffer     : Set objReportBuffer   = CreateObject("Lib2.IUbsBuffer")
    Dim objScript           : Set objScript         = UbsCreateObject2("UbsRunScript.1", "URunScr.IUbsRunScript", Scripter) : objScript.ClearParameters
    Dim nDel                : nDel = 1
    Dim nOK                 : nOK = 0

    Dim lngUpIndex, TypeFile
    Dim sNotify
    Dim strReport
    Dim i, var

    dim err_desc
    dim err_line
    dim err_source
    dim err_number
getvarvalue objParamIn.Parameters
    If Not IsArray(arrData) Then _
        Exit Sub
        
    Dim file_name : file_name = UbsFile.GetTempFile(UbsOdbc.GetUbsFolder("Temp"))
    objReportBuffer.MaxBuffer = 10000 '000
    objReportBuffer.FileName = file_name
    objParamOut.Parameter("Имя файла") = file_name
    objReportBuffer.Buffer = vbNewLine
    
    If Ubound(arrData, 2) > 10 Then nDel = 2
    If Ubound(arrData, 2) > 100 Then nDel = 5
    If Ubound(arrData, 2) > 1000 Then nDel = 10

    ' Определяем, что обрабатываем
    Call ReadBase("SELECT TYPE_FILE, NAME_FILE FROM CARD_FILE WHERE ID_FILE = " & IdFile, var) : TypeFile = CLng(var(0, 0))
    Select Case TypeFile
        Case 2, 17  ' Файл с зарплатой, файл с пополнениями
            objScript.LoadFiles "UBS_VBS\PLCARD\Operations\PCOperContractorCredit.vbs"
        Case 9  ' файл со списаниями
            objScript.LoadFiles "UBS_VBS\PLCARD\Operations\PCOperContractorDebet.vbs"
        Case Else
            Err.Raise vbObjectError + 1, "PCSalaryImport_ProcessData", "Недопустимый тип файла - " & TypeFile & "."
    End Select

    ' Установка флага тестового режима в область исполнения скрипта
    Call objScript.Run("PCSetTestMode", blnControlRun)

    objScript.ClearParameter("Сводный платеж.Идентификатор")
    objScript.ClearParameter("Сводный платеж.Бизнес")
    objScript.ClearParameter("Сводный платеж.Дата")
    objScript.ClearParameter("Сводный платеж.Номер")
    objScript.Parameter("Имя файла") = var(1, 0)

'    Dim PaymOrderId  : PaymOrderId = CLng(objParamIn.Parameter("Сводный платеж.Идентификатор"))
'    If PaymOrderId > 0 Then
'        objScript.Parameter("Сводный платеж.Идентификатор") = PaymOrderId
'        objScript.Parameter("Сводный платеж.Бизнес") = objParamIn.Parameter("Сводный платеж.Бизнес")
'    End If

    ' Чтение параметров сводного платежа, если он есть
    If ReadBase(" select " & IIf(IsSqlClient, "top 1", "") & " FS.ID_OBJECT, FS.COD_BUSINESS, P.DPP, PB.NDOC " & _
                " from CARD_FILE_SOURCE FS, RC_PAYMENT P, RC_PAYMENT_BASE PB " & _
                " where	  FS.ID_OBJECT = P.ID_PAYMENT " & _
                "     and P.ID_PAYMENT = PB.ID_PAYMENT " & _
                "     and FS.COD_BUSINESS = 'RC' " & _
                "     and FS.ID_FILE = " & IdFile  & _
                IIf(IsSqlClient, "", " and ROWNUM = 1 ") & _
                " union all " & _
                " select " & IIf(IsSqlClient, "top 1", "") & " FS.ID_OBJECT, FS.COD_BUSINESS, D.DATE_DOC, D.NDOC " & _
                " from CARD_FILE_SOURCE FS, CLBANK_DOCUMENT D " & _
                " where	FS.ID_OBJECT = D.ID_DOC " & _
                "     and FS.COD_BUSINESS = 'CLBANK' " & _
                "     and FS.ID_FILE = " & IdFile & _
                IIf(IsSqlClient, "", " and ROWNUM = 1 "), var) > 0 Then
        objScript.Parameter("Сводный платеж.Идентификатор") = CLng(var(0, i))
        objScript.Parameter("Сводный платеж.Бизнес") = var(1, i)
        objScript.Parameter("Сводный платеж.Дата") = var(2, i)
        objScript.Parameter("Сводный платеж.Номер") = var(3, i)
    End If

    var = Empty
    For i = 0 To Ubound(arrData, 2)
        ' Если на форме нажали выход или отменили расчет, то выхожу из ф-ии
        if GlobalNotice.InterruptFlag <> 0 then
            Exit For
        end if

        If CCur(arrData(2, i)) > 0 Then
            objScript.SuppresError = True
                bResult = False : strReport = ""

                objScript.ClearParameter("Вид дохода")
                ' (17090) читается в операции. objScript.Parameter("Вид дохода") = objParamIn.Parameter("Вид дохода")
                If TypeFile = 9 Then    ' Списание
                    bResult = objScript.Run("PCActionContractorDebet", CLng(arrData(0, i)), datOper, IdFile, TypeFile, _
                        objParamIn.Parameter("Назначение платежа"), strReport)
                    If bResult Then curOpers = curOpers - arrData(2, i)
                Else
                    bResult = objScript.Run("PCActionContractorCredit", CLng(arrData(0, i)), datOper, IdFile, TypeFile, _
                        objParamIn.Parameter("Назначение платежа"), strReport)
                    If bResult Then curOpers = curOpers + CCur(arrData(2, i))
                End if
                objScript.ClearParameter("Вид дохода")

                If objScript.LastErrorNumber = 0 Then
                    If Not bResult Or strReport <> "" Then
                        PCWriteReportSeparator objReportBuffer, "="
                        PCWriteReportText objReportBuffer, strReport
                    End If
                Else
                    lngUpIndex = InStr(1, objScript.LastErrorDescription,"Ошибка выполнения сценария.", vbBinaryCompare)

                    PCWriteReportSeparator objReportBuffer, "="
                    PCWriteError objReportBuffer, Left(objScript.LastErrorDescription, lngUpIndex - 5)
                End If
                
                If Not bResult Then _
                    If InTransact Then _
                        AbortTransact
                
            objScript.SuppresError = False

            If bResult And blnControlRun Then
                If TypeFile = 2 Then
                    If CLng(IdAccCorr) <> CLng(objScript.Parameter("Salary.Payment.IdAccDB")) Then 
                        Call AddRowToArray(var, 4, Array(objScript.Parameter("Salary.Payment.IdAccDB"), _
                                                         objScript.Parameter("Salary.Payment.AccDB"), _
                                                         CCur(objScript.Parameter("Salary.Payment.SaldoAccDB")), _
                                                         CCur(objScript.Parameter("Salary.Payment.curDB")), _
                                                         CCur(objScript.Parameter("Salary.Fee.curDB"))))
                    Else
                        var(3, UBound(var, 2)) = CCur(var(3, UBound(var, 2))) + CCur(objScript.Parameter("Salary.Payment.curDB"))
                        var(4, UBound(var, 2)) = CCur(var(4, UBound(var, 2))) + CCur(objScript.Parameter("Salary.Fee.curDB"))
                    End If
                ElseIf TypeFile = 17 Or TypeFile = 9 Then
                    Call AddRowToArray(var, 5, Array(objScript.Parameter("Salary.Payment.IdAccDB"), _
                                                     objScript.Parameter("Salary.Payment.AccDB"), _
                                                     objScript.Parameter("Salary.Payment.AccCR"), _
                                                     CCur(objScript.Parameter("Salary.Payment.curDB")), _
                                                     CCur(objScript.Parameter("Salary.Payment.curCR")),_
                                                     CCur(objScript.Parameter("Salary.Payment.SaldoAccDB"))))
                End If
            End If
        End If

        If (i + 1) mod nDel = 0 Then
            sNotify = "OK: " & i + 1 - nOK
            
            on error resume next
            GlobalNotice.SendNotice sNotify
            if err.number <> 0 then
                err_desc = err.Description
                err_source = err.Source
                err_number = err.number
                err_line = Erl
                on error goto 0
                PCWriteReportSeparator objReportBuffer, "="
                PCWriteReportLine objReportBuffer, "!!! ОШИБКА GlobalNotice.SendNotice !!!"
                PCWriteReportLine objReportBuffer, "Индекс массива: " & i & " из " & Ubound(arrData, 2)
                PCWriteReportLine objReportBuffer, "Код           :" & err_number
                PCWriteReportLine objReportBuffer, "Истосник      :" & err_source
                PCWriteReportLine objReportBuffer, "Описание      :" & err_desc
                PCWriteReportLine objReportBuffer, "Строка        :" & err_line
                PCWriteReportSeparator objReportBuffer, "="
            else
                on error goto 0
                nOK = i + 1
            end if
        End If
    Next

    sNotify = "OK: " & i - nOK
    GlobalNotice.SendNotice sNotify

    If objScript.ExistParameter("Сводный платеж.Массив документов") Then _
        objParamOut.Parameter("Сводный платеж.Массив документов") = objScript.Parameter("Сводный платеж.Массив документов")

    objParamOut.Parameter("Общая сумма операций") = curOpers
    objParamOut.Parameter("Имя файла") = objReportBuffer.Buffer
    If IsArray(var) Then objParamOut.Parameter("varZPData") = var
End Sub

Sub PCSalaryImport__AsyncError(objParamIn, objParamOut)
    objParamOut.Parameter("ReportError") = _
        "Ошибка канала <" & objParamIn.Parameter("NC") & ">" & vbNewLine & objParamIn.Parameter("Error")
End Sub

Sub PCSalaryImport_PostProcess(objParamIn, objParamOut)
    On Error Resume Next
    PCSalaryImport_PostProcess_Inner objParamIn, objParamOut
    If Err.number <> 0 Then _
        objParamOut.Parameter("strReport") = vbNewLine & Err.Description
    On Error Goto 0
End Sub

Sub PCSalaryImport_PostProcess_Inner(objParamIn, objParamOut)
    Dim AccCorr         : AccCorr = Trim(objParamIn.Parameter("AccCorr"))
    Dim IdClientMain    : IdClientMain = CLng(objParamIn.Parameter("IdClientMain"))
    Dim IdFile          : IdFile = CLng(objParamIn.Parameter("IdFile"))
    Dim SumOper
    Dim TypeFile
    Dim IsComission
    Dim strSql
    Dim var, i

    If IdFile > 0 Then
        UbsWriteRead.Clear
        UbsWriteRead.ClearFileRecord
	    UbsWriteRead.TypeTable = 1
        UbsWriteRead.FileParam("Id_File") = IdFile
        UbsWriteRead.ReadFileRecord

        'IdClientMain    = CLng(UbsWriteRead.FileParam("IdClientMain"))
        IsComission     = CBool(UbsWriteRead.FileParam("IsComission"))
        SumOper         = CLng(UbsWriteRead.FileParam("SumOper"))
        TypeFile        = CLng(UbsWriteRead.FileParam("TypeFile"))
        
        If TypeFile = 17 And IdClientMain > 0 And Not IsComission And SumOper > 0 Then
            ' Если все сообщения файла обработаны......
            ReadBase " select count(ID_FILE), " & _
                              "(select count(ID_FILE) from CARD_SALARY_PLAN where ID_FILE = " & IdFile & _
                                                                                " and ID_OPERATION = 0) " & _ 
                     " from CARD_SALARY_PLAN where ID_FILE = " & IdFile, var
            If CLng(var(0, 0)) > 0 And CLng(var(1, 0)) = 0 Then
                If IdClientMain > 0 And AccCorr <> "" Then
                    strSql = " select C.ID_CONTRACT from SP_CONTRACT C, SP_CONTRACT_ADDFL_STRING AF, SP_CONTRACT_ADDFL_DIC D " & _
                             " where   C.ID_CONTRACT = AF.ID_OBJECT and AF.ID_FIELD = D.ID_FIELD " & _
                             "    and C.STATE = 0 " & _
                             "    and D.NAME_FIELD = 'Счет для распределения зарплаты' " & _
                             "    and C.ID_CLIENT = " & IdClientMain & _
                             "    and AF.FIELD = '" & AccCorr & "'"
                ElseIf IdClientMain > 0 And AccCorr = "" Then
                    strSql = " select C.ID_CONTRACT from SP_CONTRACT C " & _
                             " where   C.STATE = 0 " & _
                             "    and C.ID_CLIENT = " & IdClientMain
                ElseIf IdClientMain = 0 And AccCorr <> "" Then
                    strSql = " select C.ID_CONTRACT from SP_CONTRACT C, SP_CONTRACT_ADDFL_STRING AF, SP_CONTRACT_ADDFL_DIC D " & _
                             " where   C.ID_CONTRACT = AF.ID_OBJECT and AF.ID_FIELD = D.ID_FIELD " & _
                             "    and C.STATE = 0 " & _
                             "    and D.NAME_FIELD = 'Счет для распределения зарплаты' " & _
                             "    and AF.FIELD = '" & AccCorr & "'"
                End If
                If ReadBase(strSql, var) = 1 Then
                    Dim objCVA   : Set objCVA   = UbsCreateObject2("UbsSPComissPayZPCVA.1", "UbsBusiness.UbsSPComissPayZPCVA", Scripter)
                    Dim objParam : Set objParam = UbsCreateLib("UbsSPComissPayZPCVA.UbsParam","Lib2.IUbsParam", Scripter) : objParam.ClearParameters
                    
                    objParam.Parameter("Дата операции") = CDate(UbsWriteRead.FileParam("DateFile"))
                    objParam.Parameter("Идентификатор договора") = CLng(var(0, 0))
                    objParam.Parameter("Идентификатор файла") = IdFile
                    objParam.Parameter("Режим") = IIf(objParamIn.Parameter("TestMode") = 1, 0, 1)
                    objParam.Parameter("NotLockPlactic") = True
                    
                    BeginTransact
                    
                    Call objCVA.DoOperationCOM(objParam)
                    
                    objParamOut.Parameter("strReport") = "Режим: " & IIf(objParamIn.Parameter("TestMode") = 1, 0, 1) & "." & vbNewLine & _
                        objParam.Parameter("Протокол")
                    'objParamOut.Parameter("strReport") = objParam.Parameter("Протокол")
                    
                    Dim IdLog : IdLog = CLng(objParam.Parameter("Идентификатор лога"))
                    If IdLog > 0 Then
                        Call WriteTransact("insert into CARD_FILE_ACTIONS (ID_FILE, SID_OBJECT, ID_OBJECT, TYPE_ACT) " & _
                                           "values (" & IdFile & ",'SpOperation'," & IdLog & ",'создана')", 0)
                        Call PCSetLock("SP_OPERLOG", "UBS_CARD_LOCK_DEL", IdLog, IdFile, Empty)
                        CompleteTransact
                    Else
                        AbortTransact
                    End If
                End If
            End If
        End If
    End If
End Sub
