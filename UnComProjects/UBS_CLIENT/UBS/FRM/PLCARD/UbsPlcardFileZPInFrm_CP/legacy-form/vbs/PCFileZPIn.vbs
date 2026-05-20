'Загрузка файла заработной платы - Пластиковые карты
'#include UBS_VBS\PLCARD\Lib\PCLib.vbs
'#include UBS_VBS\PLCARD\Lib\PCLibReadSet.vbs
'#include UBS_VBS\PLCARD\Lib\PCLibFile.vbs
'#include UBS_VBS\PLCARD\OPENWAY\PCSalaryImport.vbs
'#include UBS_VBS\PLCARD\Processing\PCPRCFile.vbs

' ScriptRunner.Parameter("DateIn")
' ScriptRunner.Parameter("FileIn")
' ScriptRunner.Run "Handle_FileIn"

' ScriptRunner.Parameter("DateIn")
' ScriptRunner.Parameter("IdFile")
' ScriptRunner.Run "Salary_Oper_Call"
Sub Handle_FileIn()
    Dim IdFile
    Dim DateIn
Scripter.Parameter("lblProgress").Text = "Загружено " & 1 & " строк..."
    DateIn = Scripter.Parameter("DateIn")
    IdFile = 0
    Call FileIn_ReadAndInput (IdFile, DateIn)

    If IdFile <> 0 Then
        ' 02.06.09 SVF
        If CBool(Scripter.Parameter("MultiThread")) Then
'            Scripter.Parameter("IdFile") = IdFile
            Scripter.Parameter("blnResult") = True
        Else
            Call Salary_Oper (IdFile, DateIn)
        End If
    Else
'        if GlobalDataAccess.InTransact Then GlobalDataAccess.AbortTransact
    End If
End Sub
'----------------------------------------------------------
'Загрузка файла с пополнением
Sub Handle_FileIn_Payment()
    Dim bTestMode   : bTestMode = CBool(Scripter.Parameter("TestMode"))
    Dim IdFile      : IdFile      = 0
    Dim DateIn      : DateIn      = Scripter.Parameter("DateIn")
    Dim DateDir     : DateDir     = Year(DateIn) & Right("0" & Month(DateIn), 2) & Right("0" & Day(DateIn), 2)
    Dim FileIn      : FileIn      = Trim(Scripter.Parameter("FileIn"))
    Dim objFile     : Set objFile = CreateObject("Lib1.IUbsFile")
    Dim FileName    : FileName    = objFile.GetFileName(FileIn)
    Dim LogPath
    Dim FilePath    : FilePath    = Trim(Scripter.Parameter("FileFolder"))
    Dim Wait        : Set Wait = CreateObject("UbsWait.UbsWaitBox")
    Dim var, i

    Dim FilePathWORK, FilePathARC, FilePathERROR
    
    If FilePath <> "" Then
        If objFile.Path(FilePath) <> objFile.Path(FileIn) Then
            Wait.WriteLine = "Каталог указанного файла отличается от заданного - " & FilePath & "."
            Wait.ViewFile
            Wait.MaximizeView
            Exit Sub
        End If
        LogPath = objFile.BuildPath(FilePath, "LoadFileRefillLog.txt")
        If Not bTestMode Then
            Call WriteLog(LogPath, String(80, "-") & vbNewLine)
            Call WriteLog(LogPath, "START: " & Now & vbNewLine & _
                                   "LOGIN: " & GlobalUser.Login & vbNewLine & _
                                   "FILE: " & FileName & vbNewLine)
        End If
        ' Рабочие подкаталоги
        FilePathWORK    = objFile.BuildPath(FilePath, "WORK")
        FilePathARC     = objFile.BuildPath(FilePath, "ARC")
        FilePathERROR   = objFile.BuildPath(FilePath, "ERROR")
        ' Проверка рабочих подкаталогов
        If Not objFile.FolderExists(FilePathWORK)  Then objFile.CreateFolder(FilePathWORK)
        If Not objFile.FolderExists(FilePathARC)   Then objFile.CreateFolder(FilePathARC)
        If Not objFile.FolderExists(FilePathERROR) Then objFile.CreateFolder(FilePathERROR)
        ' Перенос файла в рабочую папку
        If Not bTestMode Then
            var = objFile.BuildPath(FilePathWORK, FileName)
            objFile.MoveFile FileIn, var, True
            FileIn = var
            Scripter.Parameter("FileIn") = var
        End If
    End If

    Dim strReport : strReport = ""
    Call FileIn_ReadAndInput_Payment (IdFile, DateIn, strReport)

    If IdFile <> 0 Then
        If Not bTestMode Then
            If FilePathARC <> "" Then
                var = objFile.BuildPath(FilePathARC, DateDir)
                If Not objFile.FolderExists(var) Then objFile.CreateFolder(var)
                var = objFile.BuildPath(var, FileName)
                objFile.MoveFile FileIn, var, True
                Call WriteLog(LogPath, "RESULT: OK" & vbNewLine)
                Call WriteLog(LogPath, "END: " & Now & vbNewLine)
            End If
        End If
        Scripter.Parameter("blnResult") = True
    Else
        If Not bTestMode Then
            If FilePathERROR <> "" Then
                var = objFile.BuildPath(FilePathERROR, DateDir)
                If Not objFile.FolderExists(var) Then objFile.CreateFolder(var)
                var = objFile.BuildPath(var, FileName)
                objFile.MoveFile FileIn, var, True
                Call WriteLog(LogPath, "RESULT: " & strReport & vbNewLine)
                Call WriteLog(LogPath, "END: " & Now & vbNewLine)
            End If
        End If

        Wait.WriteLine = vbNewLine & strReport
        Wait.ViewFile
        Wait.MaximizeView        
    End If
End Sub

Sub FileIn_ReadAndInput(IdFile, DateIn)
    Dim FlNoError  
    Dim Fmt
    Dim UFile
    Dim CurrLine
    Dim ArrStr
    Dim N
    Dim WriteReadZP
    Dim NumStr
    Dim FlFileError
    Dim FileIn
    Dim Wait
    FlNoError = True
    FlFileError = True
    dim strReport 
    Dim strError
    Dim i, arrStroka, SearchByAccount, arrStraccount, arrData, blnNotErr
    Dim arrNewStr, objParamIn, objParamOut, CardError
    Dim Str
    Dim arrError, arrData1  
    Dim AccCorr
    Dim curTotal, curTmp

    Dim var
    
    Set objParamIn = CreateObject("Lib2.IUbsParam")
    Set objParamOut = CreateObject("Lib2.IUbsParam")  
    CardError = True

    FileIn = Trim(Scripter.Parameter("FileIn"))
    Set Wait = CreateObject("UbsWait.UbsWaitBox")
    Set Fmt = CreateObject("Lib3.IUbsFormat")
    Set UFile  = CreateObject("Lib1.IUbsFile")

    If GlobalDataAccess.InTransact Then _
        Err.Raise vbObjectError + 1, "FileIn_ReadAndInput", "Запуск ф-ии во внешней транзакции"
    If Not PCLibFile_CheckFileNameForMask("Маски файлов контрагентов", "Заработная плата", FileIn, 0, strError) Then
            Wait.WriteLine = strError
            Wait.ViewFile
            Wait.MaximizeView        
            Exit Sub
    End If

    Wait.ClearFile
    ' --- Проверка на повторную загрузку файла
    strReport = CheckLoadFile(0,2,FileIn, DateIn)
    if strReport <> "" then
'        if GlobalDataAccess.InTransact Then GlobalDataAccess.AbortTransact
        Wait.WriteLine = strReport
        Wait.ViewFile
        Wait.MaximizeView        
        exit sub
    end if    

    SearchByAccount = CBool(Scripter.Parameter("SearchByAccount"))

    '============04.05.2007=============
    Dim blnTatFond, arrValue, FileFormat ,blnBorov
    blnTatFond = False 
	blnBorov = false
    'В случаи наличия в установках параметра SMARTVISTA, выставляем флаг "нового режима":
    'Разделительным символом является "^"

    arrValue = GlobalUser.ReadSetting("Пластиковые карты","Форматы файлов")
    If IsArray(arrValue) Then
        For i = 0 To UBound(arrValue, 2)
            If arrValue(0,i) = "Файл с заработной платой" Then
               FileFormat = arrValue(1,i)
               Exit For
            End If
        Next
    End If 

    If UCase(FileFormat) = "ТАТФОНД" Then
        blnTatFond = True
    End If
    If UCase(FileFormat) = "БОГОРОДСКИЙ" Then
        blnBorov = True
    End If

    '==================================
    UFile.OpenTextFile FileIn, True
    NumStr = 0
    CurrLine = ""
    i = 0
   
    Dim blnOEM, nAsc, nCh
    Do While UFile.EndOfFile <> True    
        FlFileError = True
        CurrLine = UFile.ReadLine
        ' =============================================
        ' Проверка на кодировку
		blnOEM = empty
        If IsEmpty(blnOEM) Then
            blnOEM = True
            For nCh = 1 To Len(CurrLine)
                nAsc = Asc(Mid(CurrLine, nCh, 1))
                If (nAsc >= 192 And nAsc <= 223) Or (nAsc >= 242 And nAsc <= 255) Then _
                    blnOEM = False : Exit For
            Next
        End If
        If blnOEM Then 
            CurrLine = Fmt.OemToAnsi(CurrLine)
		End if	
        ' =============================================
        NumStr = NumStr + 1              
        'Режим SMARTVISTA           
        If blnTatFond Then           
            If Not IsArray(arrStroka) Then 
                ReDim arrStroka(1, 0)
            Else
                ReDim Preserve arrStroka(1, i)
            End If 
            arrStroka(0, i) = Trim(CurrLine)
            If UBound(arrStroka, 2) = 0 Then
                If UCase(arrStroka(0, 0)) = "FH" Then
                    Wait.WriteLine = "Загрузка файла с заголовком не поддерживается действием."
                    Wait.ViewFile
                    Wait.MaximizeView
                    Exit Sub
                End If
            End If 

            i = i + 1
		ElseIf blnBorov then
   		    N = Fmt.TextLineSeparate(CurrLine,";",ArrStr)      
            If N <> 5  and  N <> 4 Then
                FlFileError = False
            Else
				if N = 5 then
					ArrStr(0) = Trim(ArrStr(0))
					ArrStr(1) = Trim(ArrStr(1))
					ArrStr(2) = Trim(ArrStr(2))
					ArrStr(3) = Trim(ArrStr(3))				
					ArrStr(4) = Trim(ArrStr(4))				
					' ====== Majorow [21.12.2004] ======================================
					if not IsCorrectSumm(ArrStr(4)) then FlFileError = False
					' ==================================================================      
					If Len(ArrStr(3)) > 20 Then
						FlFileError = False
					End If
					If IsEmpty(Fmt.CtoM(Trim(ArrStr(4)))) Then
					   FlFileError = False
					End If
					If Len(ArrStr(2)) > 100 Then
					   FlFileError = False
					End If
					if left(ArrStr(4),1) = "-" then
						strError = strError & "Строка " & NumStr & ". Cумма не может быть отрицательной" & vbNewLine
						FlFileError = False
					end if
				else
				    'NumStr = NumStr - 1              
				End if	
            End If  
        ElseIf FileFormat = "NCC" Then
            N = Fmt.TextLineSeparate(CurrLine, "^", ArrStr)
            If NumStr = 1 Then
                If N <> 3 Then
                    strError = strError & "Строка " & NumStr & ". Недопустимое количество полей заголовка файла." & vbNewLine
                    FlFileError = False
                Else
                    If UCase(ArrStr(1)) <> "ЗАРПЛАТА" Then
                        strError = strError & "Строка " & NumStr & ". Тип файла в заголовке (" & ArrStr(1) & ") не соответствует файлу с зарплатой." & vbNewLine
                        FlFileError = False
                    End If
                End If
            Else
                If N = 2 Then ' Итог
                    If CLng(ArrStr(0)) <> (NumStr - 2) Then
                        strError = strError & "Строка " & NumStr & ". Количество записей в файле (" & (NumStr - 2) & _
                            ") не совпадает с итоговым значением (" & ArrStr(0) & ")." & vbNewLine
                        FlFileError = False
                    End If
                    curTmp = UbsFormat.CtoM(ArrStr(1))
                    If curTmp <> curTotal Then
                        strError = strError & "Строка " & NumStr & ". Сумма записей в файле (" & curTotal & _
                            ") не совпадает с итоговым значением (" & ArrStr(0) & ")." & vbNewLine
                        FlFileError = False
                    End If
                Else
                    If N = 6 Then
                        If Not IsCorrectSumm(ArrStr(4)) Then
                            strError = strError & "Строка " & NumStr & ". Некорректный формат суммы." & vbNewLine
                            FlFileError = False
                        End If
                        If Len(ArrStr(0)) > 20 Then
                            strError = strError & "Строка " & NumStr & ". Длина поля 1 превышает 20 символов." & vbNewLine
                            FlFileError = False
                        End If
                        If Len(Trim(Trim(ArrStr(1) & " " & ArrStr(2)) & " " & ArrStr(3))) > 100 Then
                            strError = strError & "Строка " & NumStr & ". Сумма длин полей 1-3 превышает 100 символов." & vbNewLine
                            FlFileError = False
                        End If

                        curTmp = UbsFormat.CtoM(ArrStr(4))
                        If curTmp < 0 Then
                            strError = strError & "Строка " & NumStr & ". Cумма не может быть отрицательной." & vbNewLine
                            FlFileError = False
                        End If
                        curTotal = CCur(curTotal) + CCur(Abs(curTmp))
                    Else
                        strError = strError & "Строка " & NumStr & ". Недопустимое коичество полей." & vbNewLine
                        FlFileError = False
                    End If
                End If
            End If
        ElseIf FileFormat = "РТС" Then
            If Not CBool(SearchByAccount) Then
                Wait.WriteLine = "Режим поиска карты по номеру не поддерживается." & vbNewLine
                Wait.ViewFile
                Wait.MaximizeView
                Exit Sub
            End If
            If Len(CurrLine) <> 0 Then
                If Len(CurrLine) < 91 Then
                    strError = strError & "Строка " & NumStr & ". Недопустимое количество символов в сообщении." & vbNewLine
                    FlFileError = False
                End If
            End If
        Else  
		    N = Fmt.TextLineSeparate(CurrLine,",",ArrStr)      
            If N <> 3 Then
                FlFileError = False
			  
            Else
                ArrStr(0) = Trim(ArrStr(0))
                ArrStr(1) = Trim(ArrStr(1))
                ArrStr(2) = Trim(ArrStr(2))
                ' ====== Majorow [21.12.2004] ======================================
                if not IsCorrectSumm(ArrStr(1)) then FlFileError = False
                ' ==================================================================      
                If Len(ArrStr(0)) > 20 Then
                    FlFileError = False
                End If
                If IsEmpty(Fmt.CtoM(Trim(ArrStr(1)))) Then
                   FlFileError = False
                End If
                If Len(ArrStr(2)) > 100 Then
                   FlFileError = False
                End If
                if left(ArrStr(1),1) = "-" then
                    strError = strError & "Строка " & NumStr & ". Cумма не может быть отрицательной" & vbNewLine
                    FlFileError = False
                end if
            End If  
        End If  
    Loop

    UFile.CloseFile  
    If Not FlFileError Then
        Wait.WriteLine = "Загрузка файла з/п: Неверный формат файла"
        Wait.WriteLine = strError
        Wait.ViewFile
        Wait.MaximizeView
        Exit Sub
    End If 

    Dim PaymOrderId         : PaymOrderId           = CLng(Scripter.Parameter("Сводный платеж.Идентификатор"))
    Dim PaymOrderBusiness   : PaymOrderBusiness     = Scripter.Parameter("Сводный платеж.Бизнес")
    Dim PaymOrderDate
    Dim PaymOrderSum
    Dim PaymOrderCheckMode  : PaymOrderCheckMode    = CLng(0)
    
    ' Проверка и чтение параметров сводного платежа
    If Not PCSalaryImport_CheckPaymentOrder(DateIn, PaymOrderId, PaymOrderBusiness, PaymOrderDate, PaymOrderSum, PaymOrderCheckMode, AccCorr, strError) Then
        Wait.WriteLine = strError
        Wait.ViewFile
        Wait.MaximizeView
        Exit Sub
    End If 

    'Режим SMARTVISTA            
    If blnTatFond Then  
        SmartVista arrStroka, arrStraccount, arrData, Wait, Fmt,SearchByAccount,blnNotErr 
        If not blnNotErr Then  Exit Sub        
    End If    

    ' 02.06.09 SVF 
    If Not CBool(Scripter.Parameter("MultiThread")) Then
        Wait.ShowBox
        Wait.TextVisible = True
        Wait.Label = "Загрузка операций по зараб.плате"
    End If
    NumStr = 0

'    GlobalDataAccess.BeginTransact
    Set WriteReadZP = UbsCreateObject4("UbsWriteRead")
    WriteReadZP.UbsUser = GlobalUser
    WriteReadZP.DataAccess = GlobalDataAccess
    Dim strFileSender
  
    If Not GetFileSender(0,"Код банка в процессинге","",strFileSender,strError) then
        Wait.WriteLine = strError
        Wait.ViewFile
        Wait.MaximizeView
'        GlobalDataAccess.AbortTransact
        Exit Sub
    End if   

    ' Заполнение таблицы CardFile
    Scripter.Parameter("FileState") = 254
    FillCardFile WriteReadZP, 2, UFile.GetFileName(FileIn), DateIn, 0, 2, strFileSender, 0  
    IdFile = WriteReadZP.FileParam("Id_File")
    Scripter.ClearParameter("FileState")
    Scripter.Parameter("IdFile") = IdFile
    
    Dim strTemp,ScriptRunner,blnCheck

    if Not SearchByAccount  Then
        Set ScriptRunner = UbsCreateObject4("URunScr.IUbsRunScript")
        ScriptRunner.UbsUser = GlobalUser
        ScriptRunner.DataAccess = GlobalDataAccess
        ScriptRunner.LoadFiles "UBS_VBS\PLCARD\PcFileZpIn_Srv.vbs"
        ScriptRunner.ClearParameters
    end if

    'Режим SMARTVISTA
    If blnTatFond Then     
        For i = 0 to (UBound(arrData,2) - 1) 
            NumStr = NumStr + 1
            WriteReadZP.ClearTableRecord
            WriteReadZP.Param("Id_File") = IdFile
            WriteReadZP.Param("Id_Operation") = 0  

            if Not CBool(SearchByAccount) Then
                    strTemp = CStr(arrData(0, i) & "")
                    ScriptRunner.Run "NUM_CARD" ,strTemp, blnCheck
                if blnCheck then
                    WriteReadZP.Param("NumCard") = arrData(0, i)
                    arrData(0, i) = strTemp
                else
'                    GlobalDataAccess.AbortTransact
                    strError = strError & "Строка " & i+1 & ". Не найден номер счета для карты " & arrData(0, i) & ". Загрузка файла невозможна"                    
                    Wait.WriteLine = strError
                    Wait.ViewFile
                    Wait.MaximizeView
                    IdFile = 0
                    Exit Sub
                end if
            end if
        
            WriteReadZP.Param("StrAccount") = CStr(arrData(0, i)) 'Добавить поиск счета         
            WriteReadZP.Param("DateTransaction") = DateIn
            WriteReadZP.Param("SummaPaym") = Fmt.CtoM(arrData(1, i))
            curTotal = CCur(curTotal) + WriteReadZP.Param("SummaPaym")

            If PaymOrderId > 0 And PaymOrderBusiness = "RC" Then
                ' Сохранение счета платежа РЦ
                If Trim(AccCorr) <> "" Then WriteReadZP.Param("StrAccountCorr") = AccCorr
            End If
            
            WriteReadZP.Param("InitialsClient") = arrData(2, i)
            WriteReadZP.SaveTableRecord

            ' 02.06.09 SVF 
            If Not CBool(Scripter.Parameter("MultiThread")) Then
                Wait.Text = "Строка " + CStr(NumStr)
            Else
                Scripter.Parameter("lblProgress").Text = "Загружено " & NumStr & " строк..."
            End If

        Next
	ElseIf blnBorov Then
	        UFile.OpenTextFile FileIn, True
            Do While UFile.EndOfFile <> True
            NumStr = NumStr + 1
            CurrLine = UFile.ReadLine
			blnOEM = Empty
			If IsEmpty(blnOEM) Then
				blnOEM = True
				For nCh = 1 To Len(CurrLine)
					nAsc = Asc(Mid(CurrLine, nCh, 1))
					If (nAsc >= 192 And nAsc <= 223) Or (nAsc >= 242 And nAsc <= 255) Then _
						blnOEM = False : Exit For
				Next
			End If
        If blnOEM Then 
            CurrLine = Fmt.OemToAnsi(CurrLine)
		End if	

            'CurrLine = Fmt.OemToAnsi(CurrLine)
            N = Fmt.TextLineSeparate(CurrLine,";",ArrStr)
            if N = 5 then  
				ArrStr(0) = Trim(ArrStr(0))
				ArrStr(1) = Trim(ArrStr(1))
				ArrStr(2) = Trim(ArrStr(2))
				ArrStr(3) = Trim(ArrStr(3))
				ArrStr(4) = Trim(ArrStr(4))
				WriteReadZP.ClearTableRecord
				WriteReadZP.Param("Id_File") = IdFile
				WriteReadZP.Param("Id_Operation") = 0

				if Not CBool(SearchByAccount) Then
						strTemp = CStr(ArrStr(3) & "")
						ScriptRunner.Run "NUM_CARD" ,strTemp, blnCheck
					if blnCheck then
						WriteReadZP.Param("NumCard") = ArrStr(3)
						ArrStr(3) = strTemp
					else
'						GlobalDataAccess.AbortTransact
						strError = strError & "Строка " & i+1 & ". Не найден номер счета для карты " & ArrStr(3) & ". Загрузка файла невозможна" & vbNewLine
						Wait.WriteLine = strError
						Wait.ViewFile
						Wait.MaximizeView
						IdFile = 0
						Exit Sub
					end if
				end if
				
				WriteReadZP.Param("StrAccount") = ArrStr(3) 'Добавить поиск счета
				WriteReadZP.Param("DateTransaction") = DateIn
				WriteReadZP.Param("SummaPaym") = Fmt.CtoM(ArrStr(4))
				curTotal = CCur(curTotal) + WriteReadZP.Param("SummaPaym")
				WriteReadZP.Param("InitialsClient") = ArrStr(2)

				If PaymOrderId > 0 And PaymOrderBusiness = "RC" Then
					' Сохранение счета платежа РЦ
					If Trim(AccCorr) <> "" Then WriteReadZP.Param("StrAccountCorr") = AccCorr
				End If

				WriteReadZP.SaveTableRecord

				' 02.06.09 SVF 
				If Not CBool(Scripter.Parameter("MultiThread")) Then
					Wait.Text = "Строка " + CStr(NumStr)
				Else
					Scripter.Parameter("lblProgress").Text = "Загружено " & NumStr & " строк..."
				End If
			else
			    'NumStr = NumStr - 1
		    End if 	
        Loop
        UFile.CloseFile
    ElseIf FileFormat = "NCC" Then
	    UFile.OpenTextFile FileIn, True
        Do While UFile.EndOfFile <> True
            NumStr = NumStr + 1
            CurrLine = UFile.ReadLine
            UbsFormat.TextLineSeparate CurrLine, "^" ,ArrStr

            If UBound(ArrStr) = 5 Then
				ArrStr(0) = Trim(ArrStr(0))
				ArrStr(1) = Trim(ArrStr(1))
				ArrStr(2) = Trim(ArrStr(2))
				ArrStr(3) = Trim(ArrStr(3))
				ArrStr(4) = Trim(ArrStr(4))
				ArrStr(5) = Trim(ArrStr(5))

				WriteReadZP.ClearTableRecord
				WriteReadZP.Param("Id_File") = IdFile
				WriteReadZP.Param("Id_Operation") = 0

				If Not CBool(SearchByAccount) Then
						strTemp = CStr(ArrStr(0) & "")
						ScriptRunner.Run "NUM_CARD" ,strTemp, blnCheck
					If blnCheck Then
						WriteReadZP.Param("NumCard") = ArrStr(0)
						ArrStr(0) = strTemp
					Else
'						GlobalDataAccess.AbortTransact
						strError = strError & "Строка " & NumStr & ". Не найден номер счета для карты " & ArrStr(0) & ". Загрузка файла невозможна" & vbNewLine
						Wait.WriteLine = strError
						Wait.ViewFile
						Wait.MaximizeView
						IdFile = 0
						Exit Sub
					End If
				End If
				
				WriteReadZP.Param("StrAccount") = ArrStr(0) 'Добавить поиск счета
				WriteReadZP.Param("DateTransaction") = DateIn
				WriteReadZP.Param("SummaPaym") = Fmt.CtoM(ArrStr(4))
				WriteReadZP.Param("CurrencyCod") = ArrStr(5)
				curTotal = CCur(curTotal) + WriteReadZP.Param("SummaPaym")
				WriteReadZP.Param("InitialsClient") = Trim(Trim(ArrStr(1) & " " & ArrStr(2)) & " " & ArrStr(3))

				If PaymOrderId > 0 And PaymOrderBusiness = "RC" Then
					' Сохранение счета платежа РЦ
					If Trim(AccCorr) <> "" Then WriteReadZP.Param("StrAccountCorr") = AccCorr
				End If

				WriteReadZP.SaveTableRecord

				If Not CBool(Scripter.Parameter("MultiThread")) Then
					Wait.Text = "Строка " + CStr(NumStr)
				Else
					Scripter.Parameter("lblProgress").Text = "Загружено " & NumStr & " строк..."
				End If
		    End if 	
        Loop
        UFile.CloseFile
    ElseIf FileFormat = "РТС" Then
	    UFile.OpenTextFile FileIn, True
        Do While UFile.EndOfFile <> True
            NumStr = NumStr + 1
            CurrLine = UFile.ReadLine

            If Len(CurrLine) > 0 Then
				WriteReadZP.ClearTableRecord
				WriteReadZP.Param("Id_File") = IdFile
				WriteReadZP.Param("Id_Operation") = 0
				WriteReadZP.Param("StrAccount") = Mid(CurrLine, 22, 20)
				WriteReadZP.Param("DateTransaction") = DateIn
				WriteReadZP.Param("SummaPaym") = CCur(Fmt.CtoM(Trim(Mid(CurrLine, 82, 10)))/100)
				curTotal = CCur(curTotal) + WriteReadZP.Param("SummaPaym")

				WriteReadZP.SaveTableRecord

				If Not CBool(Scripter.Parameter("MultiThread")) Then
					Wait.Text = "Строка " + CStr(NumStr)
				Else
					Scripter.Parameter("lblProgress").Text = "Загружено " & NumStr & " строк..."
				End If
            End If
        Loop
        UFile.CloseFile
    Else
        UFile.OpenTextFile FileIn, True
        Do While UFile.EndOfFile <> True
            NumStr = NumStr + 1
            CurrLine = UFile.ReadLine
			blnOEM = Empty
			If IsEmpty(blnOEM) Then
				blnOEM = True
				For nCh = 1 To Len(CurrLine)
					nAsc = Asc(Mid(CurrLine, nCh, 1))
					If (nAsc >= 192 And nAsc <= 223) Or (nAsc >= 242 And nAsc <= 255) Then _
						blnOEM = False : Exit For
				Next
			End If
			If blnOEM Then 
				CurrLine = Fmt.OemToAnsi(CurrLine)
			End if	
			
            'CurrLine = Fmt.OemToAnsi(CurrLine)
            N = Fmt.TextLineSeparate(CurrLine,",",ArrStr)

            ArrStr(0) = Trim(ArrStr(0))
            ArrStr(1) = Trim(ArrStr(1))
            ArrStr(2) = Trim(ArrStr(2))
            WriteReadZP.ClearTableRecord
            WriteReadZP.Param("Id_File") = IdFile
            WriteReadZP.Param("Id_Operation") = 0

            if Not CBool(SearchByAccount) Then
                    strTemp = CStr(ArrStr(0) & "")
                    ScriptRunner.Run "NUM_CARD" ,strTemp, blnCheck
                if blnCheck then
                    WriteReadZP.Param("NumCard") = ArrStr(0)
                    ArrStr(0) = strTemp
                else
 '                   GlobalDataAccess.AbortTransact
                    strError = strError & "Строка " & i+1 & ". Не найден номер счета для карты " & ArrStr(0) & ". Загрузка файла невозможна" & vbNewLine
                    Wait.WriteLine = strError
                    Wait.ViewFile
                    Wait.MaximizeView
                    IdFile = 0
                    Exit Sub
                end if
            end if
            
            WriteReadZP.Param("StrAccount") = ArrStr(0) 'Добавить поиск счета
            WriteReadZP.Param("DateTransaction") = DateIn
            WriteReadZP.Param("SummaPaym") = Fmt.CtoM(ArrStr(1))
            curTotal = CCur(curTotal) + WriteReadZP.Param("SummaPaym")
            WriteReadZP.Param("InitialsClient") = ArrStr(2)

            If PaymOrderId > 0 And PaymOrderBusiness = "RC" Then
                ' Сохранение счета платежа РЦ
                If Trim(AccCorr) <> "" Then WriteReadZP.Param("StrAccountCorr") = AccCorr
            End If

            WriteReadZP.SaveTableRecord

            ' 02.06.09 SVF 
            If Not CBool(Scripter.Parameter("MultiThread")) Then
                Wait.Text = "Строка " + CStr(NumStr)
            Else
                Scripter.Parameter("lblProgress").Text = "Загружено " & NumStr & " строк..."
            End If
        Loop
        UFile.CloseFile
    End If    

    If curTotal <> 0 Then
        WriteReadZP.Clear
        WriteReadZP.ClearFileRecord
	    WriteReadZP.TypeTable = 1
        WriteReadZP.FileParam("Id_File") = IdFile
        WriteReadZP.ReadFileRecord
        WriteReadZP.FileParam("SumOper") = curTotal
        WriteReadZP.SaveFileRecord
    End If

    If PaymOrderId > 0 Then
        GlobalDataAccess.BeginTransact
        If PaymOrderSum <> curTotal Then
            IdFile = 0
            AbortTransact

            Wait.WriteLine = "Сумма сводного платежа (" & MToS2(PaymOrderSum) & _
                             ") не совпадает с суммой сообщений в файле (" & MToS2(curTotal) & ")." & vbNewLine

            ' Отбраковка
            If PaymOrderCheckMode = 1 Then
                strReport = ""
                Call PCSalaryImport_RejectPaymentOrder(PaymOrderId, PaymOrderBusiness, "Несоответствие суммы документа итоговой сумме реестра", strReport)
                Wait.WriteLine = strReport
            End If
            
            Wait.ViewFile
            Wait.MaximizeView
            Exit Sub
        End If
        
        GlobalDataAccess.CompleteTransact
    End If

    Wait.HideBox
End Sub

Sub SmartVista(arrStroka, arrStraccount, arrData, Wait, Fmt,SearchByAccount,blnNotErr)

    Dim i,FlNoError,CurrLine,N,FlFileError,Sum,ArrStr,blnCorrect,objCardContr,CardError,arrError
    Dim objParamIn,objParamOut,j,FileError1,FileError2

    Set objParamIn = CreateObject("Lib2.IUbsParam")
    Set objParamOut = CreateObject("Lib2.IUbsParam")     
    Set objCardContr = UbsCreateObject2("FileIn_ReadAndInput.IUbsPlContract.0","UbsPlContract",Scripter) 
  
    CardError = True
    blnCorrect = True
    blnNotErr = False
    FlFileError = True
    FileError1 = True
    FileError2 = True
    FlNoError = True

    Sum = 0

    If isArray(arrStroka) Then
        'Смотрим, если есть пустая строка в конце, то сокращаем массив и так два раза
        If arrStroka(0, UBound(arrStroka,2)) = "" Then
            ReDim Preserve arrStroka(1, UBound(arrStroka,2)-1)
        End If
        If arrStroka(0, UBound(arrStroka,2)) = "" Then
            ReDim Preserve arrStroka(1, UBound(arrStroka,2)-1)
        End If
    Else
        Wait.WriteLine = "Файл пуст!"
        Wait.ViewFile  
        Wait.MaximizeView    
        Exit Sub
    End If 
    If UBound(arrStroka,2) = 0 Then   
        Wait.WriteLine = "В файле нет итоговой строки!"
        Wait.ViewFile  
        Wait.MaximizeView    
        Exit Sub
    End If 

    For i = 0 To UBound(arrStroka,2)-1
        CurrLine = Trim(arrStroka(0, i))
        if Right(CurrLine,1) <> "^" then CurrLine = CurrLine & "^"
        ArrStr = Split(CurrLine,"^")
        N = UBound(ArrStr)
     
        'Знака "^" не обнаружено или недопустимое их количество
        If (N > 3) or (N < 2) Then
           FlNoError = False
        'Знак "^" в начале строки или нет номера карты   
        ElseIf Trim(ArrStr(0)) = "" Then
            FlNoError = False   
        'Если строки с суммой нет 
        ElseIf IsEmpty(Fmt.CtoM(Trim(ArrStr(1)))) Then
            FlNoError = False
        'Если после точки в сумме больше двух знаков   
        ElseIf not IsCorrectSumm(Trim(ArrStr(1))) then 
            FlNoError = False                   
        'Если значений третьей колонки нет, добавляем пустую строку               
        ElseIf UBound(ArrStr) < 2 Then  
            ReDim Preserve ArrStr(2)
            ArrStr(2) = ""         
        'Проверяем на допустимую длинну поля    
        ElseIf Len(Trim(ArrStr(0)))>20 or Len(Trim(ArrStr(1)))>38 or Len(Trim(ArrStr(2)))>100 Then
            FlNoError = False  
        ElseIf N = 3 Then
            If Not( ArrStr(3) = "" ) Then
                FlNoError = False    
            End If 
        End If  
           
        If FlNoError = False Then
           Wait.WriteLine = "Строка " + CStr(i+1) + ". Неверный формат: " + CurrLine
           FlFileError = False
        End If  
       
        if cstr(Left(ArrStr(1),1)) = "-" then
            Wait.WriteLine = "Строка " + CStr(i+1) + ". Cумма не может быть отрицательной."
            FlFileError = False
        end if
       
        If FlNoError Then           
            ArrStr(1) = Replace(ArrStr(1),".",",")
            'Записываем считанные значения в массив 
            If Not IsArray(arrData) Then 
                ReDim arrData(2, 0)
            Else
                ReDim Preserve arrData(2, j)
            End If 
            arrData(0, j) = Trim(ArrStr(0))              
            arrData(1, j) = Fmt.CtoM(Trim(ArrStr(1)))  
            arrData(2, j) = Trim(ArrStr(2))  
            j = j + 1                
        End If  
    
        'Проверяем отдельно итоговую строку, вдруг формат поменяют, чтоб не переделывать
        If i = UBound(arrStroka,2)-1 Then     
            CurrLine = arrStroka(0, i+1)
            ArrStr = Split(CurrLine,"^")
            N = UBound(ArrStr)
            If UBound(ArrStr) < 2 Then  
                ReDim Preserve ArrStr(2)
                ArrStr(2) = ""  
            End If
            If (N > 2) or (N < 1) or _
                Trim(ArrStr(2)) <> "" or _                     
                Trim(ArrStr(0)) = "" Then
                'Если ранне были ошибки формата, то отделяем сообщение об ошибке в
                'итоговой строке пустой строкой
                If FlFileError = False Then
                    Wait.WriteLine = " "
                End If              
                FileError1 = False
            End If
            If IsEmpty(Fmt.CtoM(Trim(ArrStr(1)))) Then
                FileError2 = False
            End If   
               
            If FileError1 = False or FileError2 = False Then     
                Wait.WriteLine = "Итоговая строка "+ CStr(i+2) + " неверный формат: " + CurrLine                
            End If   
            If FileError1 = False Then 
                Wait.WriteLine = "Должно быть два значения: Итоговое количество и итоговая сумма." 
            End If
               
            If FileError2 = False and FileError1 = True Then
                Wait.WriteLine = "Значение " + CStr(Trim(ArrStr(1))) + " не является суммой."  
            End If
               
            if cstr(Left(ArrStr(1),1)) = "-" then
                Wait.WriteLine = "Итоговая сумма не может быть отрицательной."
            end if
                       
            If FileError1 and FileError2 Then
                If Not isArray(arrData) Then
                    ReDim arrData(2, 0)
                Else  
                    ReDim Preserve arrData(2, j)
                End If  
                ArrStr(1) = Replace(ArrStr(1),".",",")
                arrData(0, j) = Trim(ArrStr(0))              
                arrData(1, j) = Fmt.CtoM(Trim(ArrStr(1)))
                arrData(2, j) = Trim(ArrStr(2)) 
            End If    
        End If
    Next

    If FlFileError = False or FileError1 = False or FileError2 = False Then
        Wait.ViewFile
        Wait.MaximizeView
        Exit Sub
    End If

    'Производим проверку контрольных значений и полученных

    'Получаем подсчитанное программой значение суммы
    For i = 0 to (UBound(arrData,2)-1)     
        Sum = Sum + Fmt.CtoM(arrData(1, i))      
    Next
    'Проверяем, чтобы подсчитанная сумма совпадала с контрольной
    
    If Fmt.CtoM(arrData(1,UBound(arrData,2))) <> Fmt.CtoM(Sum) Then    
        blnCorrect = False      
        Wait.WriteLine = "Итоговая сумма " + CStr(Replace(arrData(1,UBound(arrData,2)),",",".")) + _
        " не совпадает с расчитанной " + CStr(Replace(Sum,",","."))
    End If       
    'Проверяем, чтобы количество элементов (проверочную строку не считаем) 
    'совпадало с контрольным значением
    If CStr(arrData(0,UBound(arrData,2))) <> CStr(UBound(arrData,2)) Then
        blnCorrect = False
        Wait.WriteLine = "Итоговое количество " + CStr(arrData(0,UBound(arrData,2))) + " не совпадает с расчитанным " + CStr(UBound(arrData,2))
    End If 
    'Если контрольные значения не совпадают, ругаемся 
    If blnCorrect = False Then
        Wait.ViewFile
        Wait.MaximizeView 
        Exit Sub
    End If   
   
    blnNotErr = True
End Sub

Sub FileIn_ReadAndInput_Payment(IdFile, DateIn, strReport)
    Call FileIn_ReadAndInput_Payment_Inner(IdFile, DateIn, strReport)
End Sub

Sub FileIn_ReadAndInput_Payment_Inner(IdFile, DateIn, varReport)
    Dim Wait : Set Wait = CreateObject("UbsWait.UbsWaitBox")
    Dim curTotal
    Dim FlNoError
    Dim Fmt
    Dim UFile
    Dim CurrLine
    Dim ArrStr
    Dim N
    Dim WriteReadZP
    Dim NumStr
    Dim FlFileError
    Dim IdClientOrganisation
    Dim NameClientLetter
    Dim Accounts
    Dim strReport
    Dim strError
    Dim blnTatFond, arrValue, FileFormat ,blnBorov 
    Dim objParamIndex
    Dim var

    Dim FileIn      : FileIn = Trim(Scripter.Parameter("FileIn"))
    Dim strCommand  : strCommand = Scripter.Parameter("StrCommand")
    DIm bTestMode   : bTestMode = CBool(Scripter.Parameter("TestMode"))
    Dim AccCorr

    Dim objFile     : Set objFile = CreateObject("Lib1.IUbsFile")
    Dim FileName    : FileName    = objFile.GetFileName(FileIn)
    Dim bXML        : bXML = (UCase(Right(FileName, 4)) = ".XML")

    Dim SearchByAccount : SearchByAccount = CBool(Scripter.Parameter("SearchByAccount"))
    
    If bXML Then
        If Not SearchByAccount Then
            varReport = "Для XML файлов поиск карт по номеру карты не поддерживается"
            Exit Sub
        End If
        If strCommand = "SPISANIE" Then
            varReport = "Для XML режим 'Списание' не поддерживается."
            Exit Sub
        End If
    End If

    blnTatFond = False 
	blnBorov = false
    'В случаи наличия в установках параметра SMARTVISTA, выставляем флаг "нового режима":
    'Разделительным символом является "^"

    If GlobalDataAccess.InTransact Then _
        Err.Raise vbObjectError + 1, "FileIn_ReadAndInput_Payment", "Запуск ф-ии во внешней транзакции."

    If Not PCLibFile_CheckFileNameForMask("Маски файлов контрагентов", _
        IIf(strCommand = "SPISANIE", "Списание", "Пополнение"), FileIn, 0, strError) Then
            varReport = strError
            Exit Sub
    End If

    If Not bXML Then
        If Scripter.Parameter("StrCommand") <> "SPISANIE" Then
            arrValue = GlobalUser.ReadSetting("Пластиковые карты","Форматы файлов")
            dim i
            If IsArray(arrValue) Then
                For i = 0 To UBound(arrValue, 2)
                    If arrValue(0,i) = "Файл с заработной платой" Then
                        FileFormat = UCase(arrValue(1,i))
                        Exit For
                    End If
                Next
            End If 

            If UCase(FileFormat) = "ТАТФОНД" Then
                blnTatFond = True
            End If
            
            If UCase(FileFormat) = "БОГОРОДСКИЙ" Then
                blnBorov = True
            End If

        End if
    End If

    FlNoError = True
    FlFileError = True
    IdClientOrganisation = Scripter.Parameter("IdClientOrganisation")
    NameClientLetter = Scripter.Parameter("NameClientLetter")
    Accounts=Scripter.Parameter("Accounts")
    Set Fmt = CreateObject("Lib3.IUbsFormat")
    Set UFile = CreateObject("Lib1.IUbsFile")
    ' --- Проверка на повторную загрузку файла
    strReport = CheckLoadFile(0, IIf(strCommand = "SPISANIE", 9, 17),FileIn, DateIn)
    if strReport <> "" then
        varReport = strReport
        exit sub
    end if

    Dim PaymOrderId         : PaymOrderId           = CLng(Scripter.Parameter("Сводный платеж.Идентификатор"))
    Dim PaymOrderBusiness   : PaymOrderBusiness     = Scripter.Parameter("Сводный платеж.Бизнес")
    Dim PaymOrderDate
    Dim PaymOrderSum
    Dim PaymOrderCheckMode  : PaymOrderCheckMode    = CLng(0)

    ' Проверка и чтение параметров сводного платежа
    If Not PCSalaryImport_CheckPaymentOrder(DateIn, PaymOrderId, PaymOrderBusiness, PaymOrderDate, PaymOrderSum, PaymOrderCheckMode, AccCorr, strError) Then
        varReport = strError
        Exit Sub
    End If 

    ' -----------------------------------------  
    ' Чтение файла
    Dim varData, varRow
    If Not bXML Then
        UFile.OpenTextFile FileIn, True
        NumStr = 0
  
        Dim arrStroka
        i= 0

        Dim blnOEM, nAsc, nCh
        Do While UFile.EndOfFile <> True
            FlNoError = True
            CurrLine = UFile.ReadLine
            ' =============================================
            ' Проверка на кодировку
            If IsEmpty(blnOEM) Then
                blnOEM = True
                For nCh = 1 To Len(CurrLine)
                    nAsc = Asc(Mid(CurrLine, nCh, 1))
                    If (nAsc >= 192 And nAsc <= 223) Or (nAsc >= 242 And nAsc <= 255) Then _
                        blnOEM = False : Exit For
                Next
            End If
            If blnOEM Then _
                CurrLine = Fmt.OemToAnsi(CurrLine)
            ' =============================================

            NumStr = NumStr + 1
            'N = Fmt.TextLineSeparate(CurrLine,",",ArrStr)
            If blnTatFond Then
                If Not IsArray(arrStroka) Then 
                    ReDim arrStroka(1, 0)
                Else
                    ReDim Preserve arrStroka(1, i)
                End If 
                arrStroka(0, i) = Trim(CurrLine)
                i = i + 1
            ElseIf 	blnBorov Then           
                N = Fmt.TextLineSeparate(CurrLine,";",ArrStr)      
                If N <> 5  and  N <> 4 Then
                    FlFileError = False
                Else
                    if N = 5 then
                        ArrStr(0) = Trim(ArrStr(0))
                        ArrStr(1) = Trim(ArrStr(1))
                        ArrStr(2) = Trim(ArrStr(2))
                        ArrStr(3) = Trim(ArrStr(3))				
                        ArrStr(4) = Trim(ArrStr(4))				
                        ' ====== Majorow [21.12.2004] ======================================
                        if not IsCorrectSumm(ArrStr(4)) then FlFileError = False
                        ' ==================================================================      
                        If Len(ArrStr(3)) > 20 Then FlFileError = False
                        If IsEmpty(Fmt.CtoM(Trim(ArrStr(4)))) Then FlFileError = False
                        If Len(ArrStr(2)) > 100 Then FlFileError = False
                        if left(ArrStr(4),1) = "-" then
                            strError = strError & "Строка " & NumStr & ". Cумма не может быть отрицательной" & vbNewLine
                            FlFileError = False
                        end if

                        Redim varRow(5)
                        If Not SearchByAccount Then
                            varRow(IOP("NumCard")) = ArrStr(3)
                        Else
                            varRow(IOP("StrAccount")) = ArrStr(3)
                        End If
                        varRow(IOP("SummaPaym")) = Fmt.CtoM(ArrStr(4))
                        varRow(IOP("InitialsClient")) = ArrStr(2)
                        AddRowToArray varData, 5, varRow
                    else
                        'NumStr = NumStr - 1              
                    End if	
                End If  
            ElseIf FileFormat = "РТС" Then
                If Not CBool(SearchByAccount) Then
                    varReport = "Режим поиска карты по номеру не поддерживается." & vbNewLine
                    Exit Sub
                End If
                If Len(CurrLine) <> 0 Then
                    If Len(CurrLine) < 91 Then
                        strError = strError & "Строка " & NumStr & ". Недопустимое количество символов в сообщении." & vbNewLine
                        FlFileError = False
                    Else
                        Redim varRow(5)
                        varRow(IOP("StrAccount")) = Mid(CurrLine, 22, 20)
                        varRow(IOP("SummaPaym")) = CCur(Fmt.CtoM(Trim(Mid(CurrLine, 82, 10)))/100)
                        AddRowToArray varData, 5, varRow
                    End If
                End If
            Else  
                N = Fmt.TextLineSeparate(CurrLine,",",ArrStr)      
                If N <> 3 Then
                    FlFileError = False
                Else
                    ArrStr(0) = Trim(ArrStr(0))
                    ArrStr(1) = Trim(ArrStr(1))
                    ArrStr(2) = Trim(ArrStr(2))
                    ' ====== Majorow [21.12.2004] ======================================
                    'Если кол-во знаков после . >2, тогда ошибка!
                    if not IsCorrectSumm(ArrStr(1)) then FlFileError = False
                    ' ==================================================================           
                    If Len(ArrStr(0)) > 20 Then FlFileError = False
                    If IsEmpty(Fmt.CtoM(Trim(ArrStr(1)))) Then FlFileError = False
                    If Len(ArrStr(2)) > 100 Then FlFileError = False
                    If left(ArrStr(1),1) = "-" Then
                        FlFileError = False
                        strError = strError & "Строка " + CStr(NumStr)& ". Сумма не может быть отрицательной" & vbNewLine
                    End if
                    
                    Redim varRow(5)
                    If Not SearchByAccount Then
                        varRow(IOP("NumCard")) = ArrStr(0)
                    Else
                        varRow(IOP("StrAccount")) = ArrStr(0)
                    End If
                    varRow(IOP("SummaPaym")) = Fmt.CtoM(ArrStr(1))
                    varRow(IOP("InitialsClient")) = ArrStr(2)
                    AddRowToArray varData, 5, varRow
                End If
            end if
        Loop
        UFile.CloseFile

        If Not FlFileError Then
            If Scripter.Parameter("StrCommand") <> "SPISANIE" Then
                varReport = "Загрузка файла з/п: Неверный формат файла"
            Else
                varReport = "Загрузка файла списания: Неверный формат файла"
            End if
            varReport = varReport & strError
            strError = ""
            Exit Sub
        End If

        Dim blnNotErr
        If blnTatFond Then  
            dim arrStraccount,arrData
            
            Call SmartVista (arrStroka, arrStraccount, arrData, Wait, Fmt,SearchByAccount,blnNotErr)
            If Not blnNotErr Then
                Exit Sub
            End If

            For i = 0 to (UBound(arrData,2) - 1)
                Redim varRow(5)
                If Not SearchByAccount Then
                    varRow(IOP("NumCard")) = CStr(arrData(0, i))
                Else
                    varRow(IOP("StrAccount")) = CStr(arrData(0, i))
                End If
                varRow(IOP("SummaPaym")) = Fmt.CtoM(arrData(1, i))
                varRow(IOP("InitialsClient")) = arrData(2, i)
                AddRowToArray varData, 5, varRow
            Next
            arrData = Empty : arrStroka = Empty : arrStraccount = Empty
        End If
    Else
        ' XML
        If Not Read1C(FileIn, varData, strError) Then
            varReport = strError
            Exit Sub
        End If
    End If

    strError = ""
    If IsArray(varData) Then
        if Not SearchByAccount  Then
            Set ScriptRunner = UbsCreateObject4("URunScr.IUbsRunScript")
            ScriptRunner.UbsUser = GlobalUser
            ScriptRunner.DataAccess = GlobalDataAccess
            ScriptRunner.LoadFiles "UBS_VBS\PLCARD\PcFileZpIn_Srv.vbs"
            ScriptRunner.ClearParameters
        End If

        For i = 0 To UBound(varData, 2)
            If Not SearchByAccount  Then
                var = varData(IOP("NumCard"), i)
                ScriptRunner.Run "NUM_CARD", var, blnCheck
                If blnCheck Then
                    varData(IOP("StrAccount"), i) = var
                Else
                    strError = strError & "Строка " & (i+1) & ". Не найден номер счета для карты " & ArrStr(0) & ". Загрузка файла невозможна"
                End If
            End If
            curTotal = CCur(curTotal) + varData(IOP("SummaPaym"), i)
        Next
        If strError <> "" Then
            If Scripter.Parameter("StrCommand") <> "SPISANIE" Then
                varReport = "Загрузка файла з/п: Ошибки файла" & vbNewLine
            Else
                varReport = "Загрузка файла списания: Ошибки файла" & vbNewLine
            End if
            varReport = varReport & strError
            strError = ""
            Exit Sub
        End If
    End If

    If Scripter.Parameter("StrCommand") <> "SPISANIE" Then
        varReport = "Загрузка операций по зараб.плате c пополнением карт" & vbNewLine
    Else
        varReport = "Загрузка списаний" & vbNewLine
    End if
    NumStr = 0

    ' Контроль файла
    If PaymOrderId > 0 Then
        If PaymOrderSum <> curTotal Then
            IdFile = 0

            varReport = varReport & "Сумма сводного платежа (" & MToS2(PaymOrderSum) & _
                             ") не совпадает с суммой сообщений в файле (" & MToS2(curTotal) & ")." & vbNewLine

            ' Отбраковка
            If PaymOrderCheckMode = 1 Then
                If bTestMode Then
                    varReport = varReport & "Документ сводного платежа будет отбракован."
                Else
                    strReport = ""
                    Call PCSalaryImport_RejectPaymentOrder(PaymOrderId, PaymOrderBusiness, "Несоответствие суммы документа итоговой сумме реестра", strReport)
                    varReport = varReport & strReport
                End If
            End If
            
            Exit Sub
        End If
    Else
        var = CCur(Scripter.Parameter("SumReestr"))
        If var > 0 And var <> curTotal Then
            IdFile = 0
            varReport = varReport & "Общая сумма реестра (" & MToS2(var) & _
                             ") не совпадает с суммой сообщений в файле (" & MToS2(curTotal) & ")." & vbNewLine
            Exit Sub
        End If
    End If

    Set WriteReadZP = UbsCreateObject4("UbsWriteRead")
    WriteReadZP.UbsUser = GlobalUser
    WriteReadZP.DataAccess = GlobalDataAccess
    Dim strFileSender
    Scripter.Parameter("TypeFile") = ""    
  
    if Not GetFileSender(0,"Код банка в процессинге","",strFileSender, strError) then        
        varReport = strError
        Exit Sub
    end if

    ' Полный тест
    Dim strTemp,ScriptRunner,blnCheck
    Set ScriptRunner = UbsCreateObject4("URunScr.IUbsRunScript")
    ScriptRunner.UbsUser = GlobalUser
    ScriptRunner.DataAccess = GlobalDataAccess
    ScriptRunner.LoadFiles "UBS_VBS\PLCARD\PcFileZpIn_FullTest.vbs"
    ScriptRunner.ClearParameters
    ScriptRunner.Parameter("DateIn") = Scripter.Parameter("DateIn")
    ScriptRunner.Parameter("FileIn") = FileIn
    ScriptRunner.Parameter("StrCommand") = Scripter.Parameter("StrCommand") 
    ScriptRunner.Parameter("IdClientOrganisation") = Scripter.Parameter("IdClientOrganisation") 
    ScriptRunner.Parameter("NameClientLetter") = Scripter.Parameter("NameClientLetter")
    ScriptRunner.Parameter("SearchByAccount") = Scripter.Parameter("SearchByAccount")
    ScriptRunner.Parameter("Назначение платежа") = Scripter.Parameter("Назначение платежа")
    ScriptRunner.Parameter("TestMode") = bTestMode
    ScriptRunner.Parameter("TypeFile") = Scripter.Parameter("TypeFile")
    ScriptRunner.Parameter("varData") = varData
    ScriptRunner.Parameter("PaymOrderId") = CLng(PaymOrderId)
    ScriptRunner.Parameter("PaymOrderSum") = CCur(PaymOrderSum)
    ScriptRunner.Parameter("AccCorr") = Trim(AccCorr)
    
    If Trim(AccCorr) <> "" Then
        ' Сохранение счета платежа РЦ
        ScriptRunner.Parameter("Accounts") = AccCorr
    Else
        ScriptRunner.Parameter("Accounts") = Accounts
    End If
    
    Dim bResult
    bResult = ScriptRunner.Run("PcFileZpIn_FullTest_New")
    If bTestMode Then
        varReport = varReport & ScriptRunner.Parameter("StrReport")
        Exit Sub
    Else
        If Not bResult Then
            varReport = varReport & ScriptRunner.Parameter("StrReport")
            Exit Sub
        End If
    End If
    
    ' Регистрация файла
    Scripter.Parameter("FileState") = 254
    If Scripter.Parameter("StrCommand") <> "SPISANIE" Then
        Call FillCardFile (WriteReadZP, 2, UFile.GetFileName(FileIn), DateIn, 0, 17, strFileSender, 0)
    Else
        Call FillCardFile (WriteReadZP, 2, UFile.GetFileName(FileIn), DateIn, 0, 9, strFileSender, 0)
    End if
    Scripter.ClearParameter("FileState")
    IdFile = WriteReadZP.FileParam("Id_File")
    Scripter.Parameter("IdFile") = IdFile

    ' Закачка файла
    If IsArray(varData) Then
        For i = 0 To UBound(varData, 2)
            WriteReadZP.ClearTableRecord
            WriteReadZP.Param("Id_File") = IdFile
            WriteReadZP.Param("Id_Operation") = 0  
            WriteReadZP.Param("DateTransaction") = DateIn
            WriteReadZP.Param("Id_ClientMain") = IdClientOrganisation
            WriteReadZP.Param("TrustFace") = NameClientLetter
            WriteReadZP.Param("IncomeTypeCode") = Trim(Scripter.Parameter("Вид дохода"))
            If PaymOrderId > 0 And PaymOrderBusiness = "RC" Then
                ' Сохранение счета платежа РЦ
                WriteReadZP.Param("StrAccountCorr") = AccCorr
            Else
                WriteReadZP.Param("StrAccountCorr") = Accounts
            End If

            WriteReadZP.Param("NumCard") = CStr(varData(IOP("NumCard"), i))
            WriteReadZP.Param("StrAccount") = CStr(varData(IOP("StrAccount"), i))
            WriteReadZP.Param("SummaPaym") = CCur(varData(IOP("SummaPaym"), i))
            WriteReadZP.Param("InitialsClient") = CStr(varData(IOP("InitialsClient"), i))
            WriteReadZP.Param("CurrencyCod") = CStr(varData(IOP("CurrencyCod"), i))
            WriteReadZP.Param("RetainedAmount") = CCur(varData(IOP("RetainedAmount"), i))
            WriteReadZP.SaveTableRecord
        Next
    End If

    If IdClientOrganisation > 0 Or curTotal <> 0 Then
        WriteReadZP.Clear
        WriteReadZP.ClearFileRecord
	    WriteReadZP.TypeTable = 1
        WriteReadZP.FileParam("Id_File") = IdFile
        WriteReadZP.ReadFileRecord
        WriteReadZP.FileParam("IdClientMain") = IdClientOrganisation
        WriteReadZP.FileParam("SumOper") = curTotal
        WriteReadZP.SaveFileRecord
    End If
End Sub
'------------------------------------------------------------

Sub Salary_Oper_Call()
    Dim IdFile
    Dim DateIn

    IdFile = Scripter.Parameter("IdFile")
    DateIn = Scripter.Parameter("DateIn")
    Salary_Oper IdFile, DateIn
End Sub
'------------------------------------------------------------
Sub Salary_Oper(IdFile, DateIn)
    Dim ArrOper
    Dim nCount, N
    Dim ScriptRunner, ParamIn, ParamOut, OFilter, Wait, FlWait
    Dim IdRecord, lngUpIndex
    Dim DateTrn
    Dim AccCard
    Dim SummaZP
    Dim IdClientOrganisation
    Dim NameClientLetter
    Dim Accounts
    Dim SearchByAccount
    Dim FIO
    Dim blnReturn

    SearchByAccount = Scripter.Parameter("SearchByAccount")

    Set Wait = CreateObject("UbsWait.UbsWaitBox")
    Set ScriptRunner = UbsCreateObject4("URunScr.IUbsRunScript")

    ScriptRunner.UbsUser = GlobalUser
    ScriptRunner.DataAccess = GlobalDataAccess
    If Scripter.Parameter("StrCommand") = "SPISANIE" Then
        ScriptRunner.LoadFiles "UBS_VBS\PLCARD\PcPerevodEx.vbs"
    Else
        ScriptRunner.ReadAction "UBS_PLCARD_SALARY_FILE_EXEC"
    End if
    ScriptRunner.ClearParameters

    FlWait = False
    Wait.ClearFile

    Set OFilter = UbsCreateObject4("UbsFilter")
    OFilter.UbsUser = GlobalUser
    OFilter.DataAccess = GlobalDataAccess
    OFilter.Read "UBS_LIST_PLCARD_SALARY"
    OFilter.ClearAllLists
    ' "Идентификатор записи" - 0 by default
    OFilter.CheckSelectItem "Счет карты", 1            '1
    OFilter.CheckSelectItem "Сумма", 1      '2  
    OFilter.CheckSelectItem "Идентификатор ответственного клиента", 1      '2
    OFilter.CheckSelectItem "Корреспондирующий счет", 1      '2
    OFilter.CheckSelectItem "От доверенного лица", 1      '2 

    OFilter.AddWhereItem "Идентификатор файла", 4, IdFile
    OFilter.AddWhereItem "Идентификатор операции", 4, 0 '"Тип объекта", 4, "CLIENTS" 'enEQ, "CLIENTS"
    OFilter.CheckOrderItem "Идентификатор записи", 1, 1
    ' Добавил: Дорофеев Ю.[29.11.05]
    OFilter.CheckSelectItem "ФИО клиента", 1           '3
    OFilter.CheckSelectItem "Номер карты", 1      '2 
      
    nCount = OFilter.GetRecords(ArrOper)

    Wait.ShowBox
    Wait.TextVisible = True
    If Scripter.Parameter("StrCommand") <> "SPISANIE" Then
           ScriptRunner.Parameter("DescriptionForm") = Scripter.Parameter("Назначение платежа")
           Wait.Label = "Выполнение операций по зараб.плате"
    Else
           ScriptRunner.Parameter("strDocDescription") = Scripter.Parameter("Назначение платежа")
           Wait.Label = "Выполнение операций по файлу"
    End if

    For N = 0 To nCount - 1
        IdRecord = ArrOper(0, N)
        DateTrn  = DateIn
        AccCard  = ArrOper(1, N)
        SummaZP  = CCur(ArrOper(2, N))
        IdClientOrganisation = ArrOper(3, N)
        NameClientLetter     = ArrOper(5, N)
        Accounts             = ArrOper(4, N)
        FIO                  = ArrOper(6, N)
        Wait.Text = "Счет " & AccCard
        ScriptRunner.Parameter("IdRecord") = IdRecord
        ScriptRunner.Parameter("DateTrn") = DateTrn
        ScriptRunner.Parameter("AccCard") = AccCard
        ScriptRunner.Parameter("FIO")     = FIO
        ScriptRunner.Parameter("SummaZP") = SummaZP
        ScriptRunner.Parameter("Id_ClientMain") = IdClientOrganisation
        ScriptRunner.Parameter("StrAccountCorr") = Accounts
        ScriptRunner.Parameter("TrustFace") = NameClientLetter
        ScriptRunner.Parameter("NumCard") = ArrOper(7, N)
        if Scripter.ExistParameter("SearchByAccount") then ScriptRunner.Parameter("SearchByAccount") = Scripter.Parameter("SearchByAccount")
        ScriptRunner.ClearParameter("Report")
        ScriptRunner.ClearParameter("LastErrorDescription")

        If SummaZP > 0 Then
            ScriptRunner.SuppresError = True
            If Scripter.Parameter("StrCommand") = "SPISANIE" Then
               Call ScriptRunner.Run ("PCPerevodEx")
            Else
               Call ScriptRunner.Run ("ZPExecution")
            End if
            
            If ScriptRunner.LastErrorNumber <> 0 Then
                If GlobalDataAccess.InTransact Then GlobalDataAccess.AbortTransact
                lngUpIndex = InStr(1, ScriptRunner.LastErrorDescription,"Ошибка выполнения сценария.",vbBinaryCompare)
                Wait.WriteLine = "==================================================="
                Wait.WriteLine = "Ошибка: " & left(ScriptRunner.LastErrorDescription,lngUpIndex-3)
                FlWait = True
            Else
                If ScriptRunner.Parameter("Report") <> "" Then
                    Wait.WriteLine = "==================================================="
                    Wait.WriteLine = ScriptRunner.Parameter("Report")
                    ScriptRunner.ClearParameter("Report")
                    FlWait = True
                End If
            End If
            
            ScriptRunner.SuppresError = False
        End If
    Next
  
    If not FlWait then
        MsgBox "Обработка завершена!",vbOKOnly,"Загрузка файла с пополнением/зарплатой"
    else
        If FlWait Then
            Wait.MaximizeView
            Wait.ViewFile
        End if
    end if
End Sub

function CheckLoadFile(InputOutput,TypeFile,FileIn, datFile)
    ' --- Проверка на повторную загрузку файла
    ' Вызов серверного сценария через заглушку скриптера.
    Dim objScripter
    Set objScripter = UbsCreateObject2("URunScrStub.IUbsRunScriptStub", "URunScrStub.IUbsRunScriptStub", Scripter)
    objScripter.LoadFiles "UBS_VBS\PlCARD\PlCardService.vbs"
    objScripter.Parameter("Дата загрузки файла") = datFile
    ' Передаю в кач-ве идентификатора процессинга 0 (временно чтоб не возникало ошибок)
    CheckLoadFile = objScripter.Run("PCCheckLoadFile",InputOutput,TypeFile,FileIn,0)
    Set objScripter = Nothing
    ' -----------------------------------------  
end function

Function IsCorrectSumm(StrSum)
' ====== Majorow [21.12.2004] ======================================
    'Если кол-во знаков после . >2, тогда ошибка!
    Dim intPointPos
    IsCorrectSumm = CBool(-1)
    intPointPos = InStr(1,StrSum,".",vbBinaryCompare) 
    if (intPointPos > 0) and (len(StrSum) - intPointPos) > 2 then
        IsCorrectSumm = CBool(0)
    end if
' ==================================================================          
End Function 


'удаляем данные о файле из базы
Sub DeleteFileData(lngIdFile)
    Dim varKeyArray
    
    ' Вызов серверного сценария через заглушку скриптера.
    Dim objScripter
    Set objScripter = UbsCreateObject4("URunScr.IUbsRunScript")
    objScripter.UbsUser = GlobalUser
    objScripter.DataAccess = GlobalDataAccess
        
    ReDim varKeyArray(0)
    varKeyArray(0) = lngIdFile
    objScripter.LoadFiles "UBS_VBS\PlCARD\PCUndoFileZp.vbs"
    objScripter.ClearParameters    
    
    Call objScripter.Run ("PCUndo", varKeyArray)
            
    Set objScripter = Nothing 
    
End Sub


Function PCSalaryImport_CheckPaymentOrderParam(pIn, pOut)
    Dim datOper             : datOper           = CDate(pIn.Parameter("datOper"))
    Dim PaymOrderId         : PaymOrderId       = CLng(pIn.Parameter("PaymOrder.Id"))
    Dim PaymOrderBusiness   : PaymOrderBusiness = Trim(pIn.Parameter("PaymOrder.Business"))
    Dim PaymOrderDate
    Dim PaymOrderSum
    Dim PaymOrderCheckMode
    Dim AccCorr
    Dim strReport
    
    PCSalaryImport_CheckPaymentOrderParam = PCSalaryImport_CheckPaymentOrder(datOper, PaymOrderId, PaymOrderBusiness, PaymOrderDate, _
                                                                             PaymOrderSum, PaymOrderCheckMode, AccCorr, strReport)
    If PCSalaryImport_CheckPaymentOrderParam Then
        pOut.Parameter("PaymOrder.Id")          = PaymOrderId
        pOut.Parameter("PaymOrder.Business")    = PaymOrderBusiness
        pOut.Parameter("PaymOrder.Date")        = PaymOrderDate
        pOut.Parameter("PaymOrder.Sum")         = PaymOrderSum
        pOut.Parameter("PaymOrder.CheckMode")   = PaymOrderCheckMode
        pOut.Parameter("PaymOrder.AccCorr")     = AccCorr
    End If
    
    pOut.Parameter("strReport")         = strReport
End Function

' Проверка сводного платежа, если неудачно - отбраковка
' [IN]
'   datOper             - дата операции
'   PaymOrderId         - ид. сводного платежа
'   PaymOrderBusiness   - код бизнеса сводного платежа
' [OUT]
'   PaymOrderDate       - дата сводного платежа
'   PaymOrderSum        - сумма сводного платежа
'   PaymOrderCheckMode  - режим отбраковки сводного платежа
'   AccCorr             - счет пополнения/списания сводного платежа
' varReport             - отчет
Function PCSalaryImport_CheckPaymentOrder(ByVal datOper, ByVal PaymOrderId, ByVal PaymOrderBusiness, _
                                          ByRef PaymOrderDate, ByRef PaymOrderSum, ByRef PaymOrderCheckMode, _
                                          ByRef AccCorr, ByRef varReport)
    Dim PaymOrder
    Dim var, i

    PCSalaryImport_CheckPaymentOrder = False
    
    If CLng(PaymOrderId) = 0 Then _
        PCSalaryImport_CheckPaymentOrder = True : Exit Function

    i = -1
    var =  GlobalUser.ReadSetting("Пластиковые карты", "Параметры загрузки пополнений и зарплат")
    i = UbsArray.AScan(var, "Режим проверки сводного платежа", 2, 0, 0)
    If i >= 0 Then PaymOrderCheckMode = CLng(var(1, i))

    Select Case PaymOrderBusiness
        Case "CLBANK"
            Set PaymOrder = UbsCreateObject2("UbsCliBankDoc.1", "UbsCliBankDoc", Scripter)
            PaymOrder.Read CLng(PaymOrderId)
            
            If PaymOrder.IdStateDoc <> 4 And PaymOrder.IdStateDoc <> 7 Then _
                PCWriteError varReport, "Состояние документа некорректно." : Exit Function
                
            var = PaymOrder.NamedFields("Рублевый платеж")
                
            If var(16, 0) <> "00000000000000000000" And var(16, 0) <> "" Then
                PCWriteError varReport, "Счет получателя сводного платежа должен быть не заполнен или равен '00000000000000000000'."
                Exit Function
            End If

            PaymOrderDate = UbsFormat.DateTimePart(PaymOrder.TimeCreate, "d")
            PaymOrderSum = CCur(var(6, 0))
        Case "RC"
            Set PaymOrder = UbsCreateObject2("UbsPayment.1", "UbsPayment", Scripter)
            PaymOrder.Read PaymOrderId
                
            PaymOrderDate   = PaymOrder.DPP
            PaymOrderSum    = PaymOrder.SummaPayment

            If datOper = PaymOrderDate Then
                '1 - входящий
                If PaymOrder.Direction <> 1 Then
                    PCWriteError varReport, "Сводный платеж не является входящим."
                    Exit Function
                End If

                ' Решено обрабатывать только платежи неопределенных сумм.
                If PaymOrder.StrIdStage <> "UBS_UNKNOWN" And PaymOrder.StrIdStage <> "UBS_REC_ERROR" Then
                    PCWriteError varReport, "Сводный платеж находится в недопустимой стадии: " & PaymOrder.StrIdStage
                    Exit Function
                End If

                If PaymOrder.Archive <> 0 Then
                    PCWriteError varReport, "Сводный платеж находится в архиве."
                    Exit Function
                End If

                Dim datRC : datRC = GlobalUser.CommonDate("Текущая дата расчетного центра")
                If PaymOrder.DPP > datRC Then
                    PCWriteError varReport, "ДПП платежа (" & DToS4(PaymOrder.DPP) & ") не соответствует текущей дате расчетного центра (" & DToS4(datRC) & ")."
                    Exit Function
                End If
                
                ' Если сумма была отнесена на неопределенные, надо найти счет на который ушли деньги
                Dim objLog : Set objLog = UbsCreateObject2("UbsRcOperLog.99", "UbsRcOperLog", Scripter)
                If PaymOrder.StrIdStage = "UBS_UNKNOWN" Then
                    Dim objDoc : Set objDoc = UbsCreateObject2("UbsPayDoc0.99", "UbsPayDoc0", Scripter)

                    If objLog.ReadFLast(PaymOrder.IdPayment) <> 0 Then
                        If objLog.StrIdOperation <> "UBS_MOVE_TO_UNKNOWN" Then
                            PCWriteError varReport, "Ошибка определения счета невыясненных сумм: последняя операция по платежу не равна постановке на невыясненные суммы."
                            Exit Function
                        End If
                        ' Поиск документа отнесения на невыясненные    
                        var = objLog.ListPayDoc
                        If Not IsArray(var) Then
                            PCWriteError varReport, "Ошибка определения счета невыясненных сумм: не найден документ постановки на невыясненные суммы."
                            Exit Function
                        End If
                        
                        objDoc.Read CLng(var(1, 0))
                        AccCorr = objDoc.Account_CR
                    Else
                        PCWriteError varReport, "Ошибка определения счета невыясненных сумм: отсутствует последняя операция по платежу PC ид. " & IdPayment & "."
                        Exit Function
                    End If
                ElseIf PaymOrder.StrIdStage = "UBS_REC_ERROR" Then
                    AccCorr = PaymOrder.StrAccountDB
                End If
            End If
        Case "OD_DEL", "OD_WAIT"
            'дата создания документа меньше либо равна дате загрузки файла;
            'дата документа меньше либо равна дате загрузки файла;
            'счет получателя равен "00000000000000000000" или пустой;
            'сумма платежа равна общей сумме файла;
            'доп.поле документа «Идентификатор файла пополнения (PLCARD)» не заполнено.
            Set PaymOrder = UbsCreateObject2("UbsPayDoc0.1", "UbsPayDoc0", Scripter)
            PaymOrder.Read CLng(PaymOrderId)

            If UbsFormat.DateTimePart(PaymOrder.TimeCreate, "d") > datOper Then _
                PCWriteError varReport, "Дата создания документа больше, чем дата загрузки файла." : Exit Function
            If PaymOrder.DateDoc > datOper Then _
                PCWriteError varReport, "Дата документа больше, чем дата загрузки файла." : Exit Function
            If PaymOrder.Account_RD <> "" And PaymOrder.Account_RD <> "00000000000000000000" Then _
                PCWriteError varReport, "Счет получателя должен быть '' или '00000000000000000000'." : Exit Function
            If CLng(PaymOrder.GetAddField("Идентификатор файла пополнения (PLCARD)")) > 0 Then _
                PCWriteError varReport, "По указанному документу ранее уже был загружен файл с з/платой." : Exit Function

            PaymOrderDate = PaymOrder.DateDoc
            PaymOrderSum = PaymOrder.SummaDB
            AccCorr = PaymOrder.Account_DB
        Case Else
            PCWriteError varReport, "Недопустимый тип сводного платежа '" & PaymOrderBusiness & "'."
            Exit Function
    End Select

    If PaymOrderBusiness <> "OD_DEL" And PaymOrderBusiness <> "OD_WAIT" Then
        If datOper <> PaymOrderDate Then
            PCWriteReportLine varReport, "Дата сводного платежа " & DToS4(PaymOrderDate) & " не совпадает с датой загрузки файла " & DToS4(datOper) & "."

            ' Отбраковка                    
            If PaymOrderCheckMode = 1 Then
                Call PCSalaryImport_RejectPaymentOrderEx(PaymOrder, PaymOrderBusiness, "Несоответствие даты поступления в банк плательщика дате реестра", varReport)
            End If

            Exit Function            
        End If
    End If

    PCSalaryImport_CheckPaymentOrder = True
End Function

' --------------------------------------------------------------------------------------------------
' Отбраковка сводного платежа
' Проверка сводного платежа, если неудачно - отбраковка
' PaymOrderId           - ид. сводного платежа
' PaymOrderBusiness     - код бизнеса сводного платежа
' RejectNote            - причина отбраковки
' varReport             - отчет
Function PCSalaryImport_RejectPaymentOrder(ByVal PaymOrderId, ByVal PaymOrderBusiness, ByVal RejectNote, ByRef varReport)
    Dim PaymOrder
    
    PCSalaryImport_RejectPaymentOrder = False
    
    If InTransact Then _
        Err.Raise vbObjectError + 1, PCSalaryImport_RejectPaymentOrder, "Запуск ф-ии в транзакции недопустим."
    
    Select Case PaymOrderBusiness
        Case "CLBANK"
            Set PaymOrder = UbsCreateObject2("UbsCliBankDoc.1", "UbsCliBankDoc", Scripter)
            PaymOrder.Read CLng(PaymOrderId)
        Case "RC"
            Set PaymOrder = UbsCreateObject2("UbsPayment.1", "UbsPayment", Scripter)
            PaymOrder.Read PaymOrderId
        Case "OD_DEL", "OD_WAIT"
            ' Ничего не надо. Документ и так отложен или удален.
        Case Else
            PCWriteReportLine varReport, "Недопустимый тип сводного платежа '" & PaymOrderBusiness & "'."
            Exit Function
    End Select
    
    PCSalaryImport_RejectPaymentOrder = PCSalaryImport_RejectPaymentOrderEx(PaymOrder, PaymOrderBusiness, RejectNote, varReport)
End Function

Function PCSalaryImport_RejectPaymentOrderEx(ByVal PaymOrder, ByVal PaymOrderBusiness, ByVal RejectNote, ByRef varReport)
    Dim objScript
    Dim strReport
    
    PCSalaryImport_RejectPaymentOrderEx = False

    If InTransact Then _
        Err.Raise vbObjectError + 1, PCSalaryImport_RejectPaymentOrder, "Запуск ф-ии в транзакции недопустим."
    
    BeginTransact
    
    Select Case PaymOrderBusiness
        Case "CLBANK"
            PaymOrder.IdStateDoc = 6
            PaymOrder.NamedFields("Причина отбраковки") = RejectNote
            PaymOrder.Modify
            PCWriteReportLine varReport, "Документ сводного платежа отбракован."
        Case "RC"
            Set objScript   = UbsCreateObject2("RejectPaymentOrder.UbsRunScript.1", "URunScr.IUbsRunScript", Scripter)
            objScript.LoadFiles "UBS_VBS\PLCARD\AnyBusiness\RCMovePaymToUnknownEx.vbs"
        
            If Not objScript.Run("RCMovePaymToUnknownEx", PaymOrder, RejectNote, strReport) Then
                PCWriteError varReport, strReport
                AbortTransact
                Exit Function
            Else
                PCWriteReportLine varReport, strReport
                'PCWriteReportLine varReport, "Документ сводного платежа отбракован."
            End If
        Case "OD_DEL", "OD_WAIT"
            ' Ничего не надо. Документ и так отложен или удален.
        Case Else
            PCWriteReportLine varReport, "Недопустимый тип сводного платежа '" & PaymOrderBusiness & "'."
            AbortTransact
            Exit Function
    End Select
    
    CompleteTransact
    
    PCSalaryImport_RejectPaymentOrderEx = True
End Function
' --------------------------------------------------------------------------------------------------

Function Read1C(ByVal path, varData, strError)
    Dim objDOM
    Dim nodeChild
    Dim nodeX
    Dim varRow
    Dim var, i  : i = 0

    Read1C = False

    'Set objDOM = CreateObject("MSXML2.DOMDocument.4.0")
    Set objDOM = CreateObject("MSXML2.DOMDocument.6.0")

    'objDOM.Async = False
    objDOM.Load path
    
    If objDOM.parseError.errorCode Then
        strError =  "Ошибка чтения XML файла:" & vbNewLine & _
                    "  errorCode: " & objDOM.parseError.errorCode & vbNewLine & _
                    "  filepos: " & objDOM.parseError.filepos & vbNewLine & _
                    "  line: " & objDOM.parseError.line & vbNewLine & _
                    "  linepos: " & objDOM.parseError.linepos & vbNewLine & _
                    "  reason: " & objDOM.parseError.reason & vbNewLine & _
                    "  srcText: " & objDOM.parseError.srcText & vbNewLine & _
                    "  url: " & objDOM.parseError.url
        Exit Function
    End If

    If objDOM.documentElement.nodeName <> "СчетаПК" Then _
        strError = "Файл не является файлом 1С." : Exit Function

    'Set objParamIndex = CreateObject("Lib2.IUbsParamIndex")

    Dim TotalSumm, TotalCount
    For Each nodeChild in objDOM.documentElement.childNodes
        If nodeChild.nodeName = "ЗачислениеЗарплаты"  Then
            Dim nodeEmployee, nodeEmployeeChild
            Dim FIO, Surname, Name, Patronymic, NumAcc, Summ, CodeCur
            Dim RetainedAmount
            For Each nodeEmployee in nodeChild.childNodes
                If nodeEmployee.nodeName = "Сотрудник" Then
                    i = i + 1
                    var = CLng(nodeEmployee.getAttribute("Нпп"))
                    If i <> CLng(nodeEmployee.getAttribute("Нпп")) Then _
                        strError = "Пропущена запись сотрудника 'Нпп'=" & i : Exit Function
                    
                    RetainedAmount = CCur(0)
                    For Each nodeX in nodeEmployee.childNodes
                        Select Case nodeX.nodeName
                            Case "Фамилия"      : Surname       = Trim(nodeX.text)
                            Case "Имя"          : Name          = Trim(nodeX.text)
                            Case "Отчество"     : Patronymic    = Trim(nodeX.text)
                            Case "ЛицевойСчет"  : NumAcc        = Trim(nodeX.text)
                            Case "Сумма"        : Summ          = SToM(Trim(nodeX.text))
                            Case "КодВалюты"    : CodeCur       = Trim(nodeX.text)
                            Case "ОбщаяСуммаУдержаний" : RetainedAmount = SToM(Trim(nodeX.text))
                        End Select
                    Next
                    
                    If CodeCur <> 643 Then _
                        strError = "Сотрудник Нпп = " & i & ". Код валюты не 643." : Exit Function
                    If CCur(Summ) = 0 Then _
                        strError = "Сотрудник Нпп = " & i & ". Не задана сумма." : Exit Function
                        
                    FIO = Trim(Trim(Surname & " " & Name) & " " & Patronymic)
                    TotalSumm = CCur(TotalSumm) + CCur(Summ)

                    Redim varRow(5)
                    varRow(IOP("InitialsClient")) = FIO
                    varRow(IOP("StrAccount")) = NumAcc
                    varRow(IOP("SummaPaym")) = Summ
                    varRow(IOP("CurrencyCod")) = CodeCur
                    varRow(IOP("RetainedAmount")) = RetainedAmount
                
                    AddRowToArray varData, 5, varRow
                End If
                TotalCount = i
            Next
        ElseIf nodeChild.nodeName = "КодВидаДохода"  Then
            If Trim(Scripter.Parameter("Вид дохода")) = "" Then _
                Scripter.Parameter("Вид дохода") = Trim(nodeChild.text)
        ElseIf nodeChild.nodeName = "КонтрольныеСуммы"  Then
            Dim tc, ts
            For Each nodeX in nodeChild.childNodes
                Select Case nodeX.nodeName
                    Case "КоличествоЗаписей" : tc = CLng(Trim(nodeX.text))
                    Case "СуммаИтого"
                        ts = SToM(Trim(nodeX.text))
                End Select
            Next

            If tc <> TotalCount Then _
                strError = "Количество записей (" & TotalCount & ") не совпадает с контрольным (" & tc & ")." : _
                Exit Function
            If ts <> TotalSumm Then _
                strError = "Общая сумма записей (" & MToS2(TotalSumm) & ") не совпадает с контрольной (" & MToS2(ts) & ")." : _
                Exit Function
        End If
    Next

    Read1C = True    
End Function

Function WriteLog(FileName, strBuff)
  Const ForAppending = 8
  Dim fso, f
  Set fso = CreateObject("Scripting.FileSystemObject")
  Set f = fso.OpenTextFile(FileName, ForAppending, True)
  f.Write strBuff
  f.Close
'==================================================================
End Function

Function IOP(ParamName)
    Select Case ParamName
        Case "NumCard"         : IOP = 0
        Case "StrAccount"      : IOP = 1 
        Case "SummaPaym"       : IOP = 2
        Case "CurrencyCod"     : IOP = 3
        Case "InitialsClient"  : IOP = 4
        Case "RetainedAmount"  : IOP = 5
        Case Else
            Err.Raise 1, "IOP", "Недопустимое имя параметра"
    End Select
End Function
