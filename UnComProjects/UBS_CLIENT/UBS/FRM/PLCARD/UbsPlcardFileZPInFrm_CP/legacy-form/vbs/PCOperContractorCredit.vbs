' Действие обработки записи пополнения по файлу контрагента.

'#include UBS_VBS\PLCARD\Lib\PCLibOper.vbs
'#include UBS_VBS\PLCARD\Lib\PCLibSchemeAccount.vbs
'#include UBS_VBS\PLCARD\Operations\PCOperInnerTransferToCard.vbs
'#include UBS_VBS\PLCARD\PCGPAutoRepay.vbs

Function PCActionContractorCredit(IdRecord, datOper, IdFile, TypeFile, strDescription, varReport)
    Dim bCheckClient
    Dim CC              : Set CC            = UbsCreateObject2("UbsPlCardCore.1", "PlCardCore.IUbsPlCardCore", Scripter)
	Dim WR              : Set WR            = UbsCreateObject2("UbsPlcardWriteRead.2", "UbsPlcardWriteRead", Scripter)
    Dim objAccCorr      : Set objAccCorr    = UbsCreateObject2("UbsAcc0.1", "UBSACC0", Scripter) : objAccCorr.Clear
	Dim objBuffer       : Set objBuffer     = UbsCreateLib("ContractorCredit.UbsBuffer", "Lib2.IUbsBuffer", Scripter) : objBuffer.Clear
	Dim objLog
	Dim ErrPrefix       : ErrPrefix         = "Запись ид. " & IdRecord & ". "
	Dim var, i
        

    PCActionContractorCredit = False
    
    If InTransact Then _
        Err.Raise vbObjectError + 1, "PCActionContractorCredit", "Запуск действия в транзакции недопустим."

    If TypeFile <> 2 And TypeFile <> 17 Then _
        Err.Raise vbObjectError + 1, "PCActionContractorCredit", "Недопустимый тип файла - " & TypeFile & "."

    ' Из за многопоточной обработки перечитываем запись сообщения файла, 
    ' вдруг операция по записи уже выполнена.
    WR.Clear : WR.TypeTable = 2
    WR.ClearTableRecord
    WR.Param("Id_Record") = IdRecord
    WR.ReadTableRecord
    If CLng(WR.Param("Id_Operation")) <> 0 Then _
        PCWriteError varReport, ErrPrefix & "Операция по записи уже выполнена. Ид. операции - " & _
            WR.Param("Id_Operation") & "." : Exit Function
    
    Dim curDB           : curDB         = CCur(WR.Param("SummaPaym"))
    Dim curCR           : curCR         = CCur(0)
    Dim AccCorr         : AccCorr       = Trim(WR.Param("StrAccountCorr"))
    Dim IdMainClient    : IdMainClient  = CLng(WR.Param("Id_ClientMain"))
    Dim FIO             : FIO           = Trim(WR.Param("InitialsClient"))
    Dim RetainedAmount  : RetainedAmount = CCur(WR.Param("RetainedAmount"))
    Scripter.Parameter("TrustFace")     = Trim(WR.Param("TrustFace"))
    Scripter.Parameter("Вид дохода")    = Trim(WR.Param("IncomeTypeCode"))
    
    If RetainedAmount > 0 Then
        var = UbsFormat.MtoPrintC(Round(RetainedAmount, 2), False, "-")
        var = "//ВЗС//" & var & "// "
        strDescription = var & strDescription
    End If

    If curDB = 0 Then _
        PCWriteError varReport, ErrPrefix & "Сумма операции равна 0." : Exit Function
            
    BeginTransact
    
    ' Читаем карту с блокировкой
    CC.Clear : CC.ReadEx Trim(WR.Param("NumCard")), Trim(WR.Param("StrAccount")), True
    ErrPrefix = ErrPrefix & CC.CardToString & "."

    ' Работаем только по главной карте
    If CC.Card.IdMainCard <> 0 Then _
        PCWriteError varReport, ErrPrefix & "Карта не является главной." _
            : AbortTransact : Exit Function

    If CC.AccSKS.StateAcc <> 0 And CC.AccSKS.StateAcc <> 3 Then _
        PCWriteError varReport, ErrPrefix & "Счет СКС " & CC.AccSKS.StrAccount & " " & CC.AccSKS.NamedFields("Состояние счета") & "." _
            : AbortTransact : Exit Function

    ' Стандартные проверки: Проверяются только даты опердня PLCARD и незавершенных периодов
    If Not PCCheckCard(CC, datOper, 0, varReport) Then _
        AbortTransact : Exit Function

    ' Дата окончания действия карты
    var = PCReadSetting("Параметры загрузки пополнений и зарплат")
    i = UbsArray.AScan(var, "Проверка даты окончания действия договора", 2, 0, 0)
    If i > -1 Then
        If var(1, i) = "1" Then
            If CC.Card.CardExpireDate < datOper Then
                PCWriteError varReport, ErrPrefix & "Срок действия карты истек."
                AbortTransact : Exit Function
            End If
        End If
    End If

    ' Проверяю, если клиент из файла = клиенту счета, то оставляю все как есть, а иначе, ошибка!
    If CC.AccColl.IsCorporate Then
        If Trim(AccCorr) = "" Then
            PCWriteError varReport, ErrPrefix & "Не задан счет корреспондента."
            AbortTransact : Exit Function
        End If
    Else
        If CC.ReadSetting("Идентификация счета по клиенту") = 1 Then
            If Replace(UCase(Trim(CC.AccSKS.NameClient)), "Ё", "Е") <> Replace(UCase(FIO), "Ё", "Е") Then
                PCWriteError varReport, ErrPrefix & "Клиент " & FIO & " не является клиентом счета " & CC.AccSKS.StrAccount & _
                    " (клиент счета " & CStr(CC.AccSKS.NameClient) & ")."
                AbortTransact : Exit Function
            End If
        End If
    End If

    ' Проверять наличие у главной карты ответственного клиента если:
    'а) коррсчёт не задан
    'б) в диалоге задан ответственный клиент
    'в) в диалоге ответственный клиент не задан, но значение установки 'Проверка отв.клиента в файле пополн/списан' = 1
    bCheckClient = Trim(AccCorr) = "" And (IdMainClient <> 0  Or  _
        (IdMainClient = 0 and CC.ReadSetting("Проверка отв.клиента в файле пополн/списан") = 1))

    If CC.Card.IdMainClient = 0 And bCheckClient Then
        PCWriteError varReport, ErrPrefix & "Карте не назначен ответственный клиент."
        AbortTransact : Exit Function
    Else
        If IdMainClient <> 0 Then ' ответственный клиент из диалога
            If CC.Card.IdMainClient <> IdMainClient And bCheckClient Then
                PCWriteError varReport, ErrPrefix & "Ответственный клиент карты не совпадает с выбранным в форме ввода."
                AbortTransact : Exit Function
            End if
        End if

        If Trim(AccCorr) = "" Then
            If CLng(CC.Card.IdMainClient) = 0 Then
                PCWriteError varReport, ErrPrefix & "Карте " & CC.Card.NumCard & " не назначен ответственный клиент."
                AbortTransact : Exit Function
            End If

             if not CC.Client.IsResident  and  CC.MainClient.IsFiledAddField("Счет для распределения зарплаты (нерез)") then
                   AccCorr = Trim(CC.MainClient.GetAddField("Счет для распределения зарплаты (нерез)"))
             else  
                   AccCorr = Trim(CC.MainClient.GetAddField("Счет для распределения зарплаты"))
             End if 

            If AccCorr = "" Then
                PCWriteError varReport, ErrPrefix & "Ответственному клиенту '" & CC.MainClient.Name & " не назначен счет для распределения зарплаты."
                AbortTransact : Exit Function
            End If
            If objAccCorr.ReadF(AccCorr) = 0 Then
                PCWriteError varReport, ErrPrefix & "Счет для распределения зарплаты " & AccCorr & _
                                    ", назначенный клиенту [" & CC.MainClient.Name & "], не найден." & vbNewLine
                AbortTransact : Exit Function
            End If
        Else
            If objAccCorr.ReadF(AccCorr) = 0 Then
                PCWriteError varReport, ErrPrefix & "Счет корреспондента " & AccCorr & " не найден." & vbNewLine
                AbortTransact : Exit Function
            End If
        End If
    End If
    
    ' Конвертация
    ' -----------------------------------------------------------------------------------------
    Dim IdCurBuy        : IdCurSale = CC.Card.IdCurrency
    Dim IdCurSale       : IdCurBuy  = PCCurrencyDictionary.ID(Mid(AccCorr, 6, 3))
    Dim curRate         : curRate   = CCur(1)
    Dim curRateBuy
    Dim curRateSale
    
    If IdCurBuy <> IdCurSale Then
        If Not PCCurrencyConverter.GetRates(datOper, CC.Card.IdProcessing, _
            curDB, IdCurBuy, IdCurSale, curRateBuy, curRateSale, objBuffer) Then _
                PCWriteError varReport, objBuffer.Buffer : AbortTransact : Exit Function

        curRate = curRateSale / curRateBuy
        curCR   = UbsFormat.Round(curDB / curRate, 2)
    Else
        curCR   = curDB
    End If
    ' -----------------------------------------------------------------------------------------

    If TypeFile = 2 Then _
        If Not PCTakeFeeSalary(CC, datOper, curCR, curDB, AccCorr, objBuffer) Then _
            PCWriteError varReport, objBuffer.Buffer : AbortTransact : Exit Function

    If Not g_bTest Then
        Scripter.ClearParameter("Очередность платежа")
        If TypeFile = 17 Then
            ' Чтобы PCOperInnerTransferToCardEx взяла комиссию за пополнение
            Scripter.Parameter("GetCommiss") = True
            If Scripter.Parameter("Вид дохода") = "1" Then _
                Scripter.Parameter("Очередность платежа") = CLng(GlobalUser.ReadSetting("Пластиковые карты", "Очередность платежа при зачислении ЗП"))
        ElseIf TypeFile = 2 Then 
            Scripter.Parameter("Очередность платежа") = CLng(GlobalUser.ReadSetting("Пластиковые карты", "Очередность платежа при зачислении ЗП"))
        End If
        
        If Not PCOperInnerTransferToCardEx(CC, 0, datOper, AccCorr, curDB, curCR, curRate, _
            IIf(TypeFile = 2, "UBS_SALARYPAYMENT", "UBS_MANUALPAYMENT"), strDescription, Empty, Empty, True, True, Empty, objBuffer) Then _
                PCWriteError varReport, objBuffer.Buffer : AbortTransact : Exit Function
            
        ' Из за многопоточной обработки перечитываем запись сообщения файла, 
        ' вдруг операция по записи уже выполнена.
        WR.ClearTableRecord
        WR.Param("Id_Record") = IdRecord
        WR.ReadTableRecord
        If CLng(WR.Param("Id_Operation")) <> 0 Then _
            PCWriteError varReport, "Списание с карты. Операция по записи ид. " & IdRecord & _
                " уже выполнена. Ид. операции - " & WR.Param("Id_Operation") & "." : AbortTransact : Exit Function

        ' Достаем лог операции из
        Set objLog = Scripter.Parameter("LOG_UBS_MANUALPAYMENT")
        ' Модифицируем запись сообщения
	    WR.Param("Id_Operation") = objLog.Id
	    WR.Param("DateTransaction") = datOper
	    WR.SaveTableRecord
	
	    '---- Отчёт -------------------------------
	    If CC.ReadSetting("Протокол обработки файла пополн/списан") = 1 Then
'	        If TypeFile = 2 Then
	            PCWriteReportText varReport, objBuffer.Buffer
'	        Else
'	            PCWriteReportText varReport, CC.CardToString & ". Выполнено пополнение на сумму " & MToS2(curCR)
'	        End If
        End If
    Else
        If TypeFile = 2 Or TypeFile = 17 Then
            Scripter.Parameter("Salary.Payment.IdAccDB") = objAccCorr.IdAccount
            Scripter.Parameter("Salary.Payment.AccDB") = objAccCorr.StrAccount
            Scripter.Parameter("Salary.Payment.curDB") = curDB
            Scripter.Parameter("Salary.Payment.SaldoAccDB") = _
                objAccCorr.GetSaldo(GlobalUser.CommonDate("Server")) * _
                    IIf(objAccCorr.ActivAcc = 0 Or objAccCorr.ActivAcc = 2, -1, 1)
                    
            If TypeFile = 17 Then
                Scripter.Parameter("Salary.Payment.AccCR") = CC.AccColl.NumAccountMain
                Scripter.Parameter("Salary.Payment.curCR") = curCR
            End If
        End If
    End If
    
	If g_bTest Then
	    AbortTransact
	Else
	    CompleteTransact
	    
        If CBool(CC.Card.GetAddField("Автогашение зад-тей по кр. картам")) Then
            BeginTransact
            Dim RB  : Set RB = UbsCreateObject("PCGPAutoRepay_DoPayments.UbsBuffer", "Lib2.IUbsBuffer", Scripter) : RB.Clear
            If PCGPAutoRepay_DoPaymentsEx(CC, objLog.DateOperation, objLog, Empty, RB) Then
                PCWriteReport varReport, RB.Buffer
                CompleteTransact
            Else
                PCWriteReport varReport, "!!! Ошибка автоматического гашения обязательного минимального платежа" & vbNewLine & _
                                         "!!! по картам с Grace Period этого клиента. (см. протокол ниже)" & vbNewLine & _
                                         "!!! Автоматическое гашение не выполнено."
                PCWriteReport varReport, RB.Buffer
                AbortTransact
            End If
        End If
	End If

    PCActionContractorCredit = True
End Function

Function PCTakeFeeSalary(CC, datOper, curCard, curCorr, AccCorr, varReport)
    Dim FeeMode             : FeeMode       = Trim(CC.Product.GetAddField("Комиссия за зачисление ЗП"))
    Dim bFromClient         : bFromClient   = (FeeMode = "с клиента")
    Dim curOper             : curOper       = IIf(bFromClient, curCard, curCorr)
    Dim IdCurrFee           : IdCurrFee     = IIf(bFromClient, CC.Card.IdCurrency, PCCurrencyDictionary.ID(Mid(AccCorr, 6, 3)))
    Dim AccCR
    Dim curFeeDB, curFeeCR
    Dim var, strTmp
    
    Dim curTMP

    PCTakeFeeSalary = False
    Scripter.ClearParameter("Salary.Fee.AccCR")
    Scripter.ClearParameter("Salary.Fee.AccDB")
    Scripter.ClearParameter("Salary.Fee.curCR")
    Scripter.ClearParameter("Salary.Fee.curDB")

    If FeeMode = "" Then _
        PCTakeFeeSalary = True : Exit Function

    If Not g_bTest Or Not bFromClient Then
        ' Чтение тарифа
        Call PlC_getTariff("Зачисление зарплаты", CC.Card, CC.Product, PCCurrencyDictionary.CodeCB(IdCurrFee), Empty, 1, var)
        If Not IsArray(var) Then _
            PCWriteError varReport, "Не найдены тарифы для расчета комиссии!" : Exit Function
        ' Расчет комиссии
        If Not PlC_CalcCommissionLib(1, IdCurrFee, curOper, datOper, var, curFeeDB, curTMP, strTmp) Then _
            PCWriteError varReport, strTmp : Exit Function
        Scripter.Parameter("Salary.Fee.curDB") = IIf(Not g_bTest Or Not bFromClient, curFeeDB, 0)

        ' Для тестового режима нужно только посчитать комиссию, если она берется с клиента
        If curFeeDB > 0 And Not g_bTest Then
'            If Not PCCurrencyConverter.ConvertCB(datOper, 2, curFeeDB, IdCurrFee, Global_BaseCurrencyId, curFeeCR, varReport) Then _
'                Exit Function

            ' Так как счет дохода всегда рублевый                
'            Scripter.Parameter("Salary.Fee.curCR") = curFeeCR
            Scripter.Parameter("Salary.Fee.curCR") = curTMP
          
            If Not PCLibSchemeAccount_GetAccount(CC, "Счет дохода за зачисление зарплаты", datOper, AccCR, varReport) Then _
                Exit Function

            Scripter.Parameter("Salary.Fee.AccCR") = AccCR
            Scripter.Parameter("Salary.Fee.AccDB") = IIf(bFromClient, CC.Card.IdAccountMain, AccCorr)
        End If            
    End If
    
    PCTakeFeeSalary = True
End Function
