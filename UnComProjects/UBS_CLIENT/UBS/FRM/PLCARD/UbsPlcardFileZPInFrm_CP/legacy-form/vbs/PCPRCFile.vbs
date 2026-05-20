'#include UBS_VBS\PLCARD\Lib\PCLib.vbs
'#include UBS_VBS\COMMON\COM_ServiceForVBD.vbs
'#include UBS_VBS\PLCARD\LIB\PCLibGetDescription.vbs

' Ф-ия проверяет возвожность обработки загруженного файла с операциями/зарплатой
' 
Function PCPRCFile_CanProcessParam(objParamIn, objParamOut)
    Dim IdFile      : IdFile    = CLng(objParamIn.Parameter("IdFile"))
    Dim TypeFile    : TypeFile  = Trim(objParamIn.Parameter("TypeFile"))
    Dim strError
    Dim var
    
    PCPRCFile_CanProcessParam = False
	
    Call ReadBase(" select NAME_FILE, STATE  from CARD_FILE where ID_FILE = " & IdFile, var)
    objParamOut.Parameter("FileName")   = Trim(var(0, 0))
    objParamOut.Parameter("FileState")  = CLng(var(1, 0))
    
    Select Case CLng(var(1, 0))
        Case 0      ' Обработан
            ' Можно обрабатывать
        Case 1      ' Загружен
            ' Можно обрабатывать
        Case 254    ' Загружается
            objParamOut.Parameter("strError") = "Файл " & var(0, 0) & " не загружен."
            Exit Function
        Case 255    ' Обрабатывается
            If Not PCPRCFile_CanUndo(IdFile, TypeFile, strError) Then _
                objParamOut.Parameter("strError") = strError : Exit Function
        Case Else
            Err.Raise vbObjectError + 1, "PCPRCFile_CanProcessParam", "Недопустимый статус файла: " & CLng(var(1, 0)) & "."
    End Select
    
    PCPRCFile_CanProcessParam = True
End Function

Function PCPRCFile_CanUndo(ByVal IdFile, ByVal TypeFile, ByRef varReport)
    Dim bIsSql  : bIsSql = IsSqlClient
    Dim dat     : dat = OdbcDateTime(DateAdd("n", -2, GlobalUser.CommonDate("Server")))
    Dim sql
    Dim var
    
    PCPRCFile_CanUndo = False

    If ReadBase("select STATE from CARD_FILE where ID_FILE = " & CLng(IdFile), var) = 0 Then
        PCWriteError varReport, "Файл ид. " & IdFile & " не найден."
    Else
        Select Case CLng(var(0, 0))
            Case 255 ' Обработка
                Select Case TypeFile
                    Case "зарплата"
                        sql = " select " & IIf(bIsSql, "top 1", "") & " 0 from CARD_SALARY_PLAN S " & _
	                          "     inner join CARD_OPERATION_LOG L on S.ID_FILE = " & IdFile & " and L.ID_OPERATION = S.ID_OPERATION " & _
                              " where S.ID_OPERATION > 0 and L.TIME_CREATE > " & dat & _
                              IIf(bIsSql, "", " and ROWNUM = 1") 
                    Case "операции из ПЦ"
                        sql = " select " & IIf(bIsSql, "top 1", "") & " 0 from CARD_PROCESS_CENTRE PC " & _
	                          "     inner join CARD_OPERATION_LOG L on PC.ID_FILE = " & IdFile & " and L.ID_OPERATION = PC.ID_OPERATION " & _
                              " where PC.ID_OPERATION > 0 and L.TIME_CREATE > " & dat & _
                              IIf(bIsSql, "", " and ROWNUM = 1") 
                    Case Else
                        Err.Raise vbObjectError + 1, "PCPRCFile_CanUndo", "Недопустимый тип файла: '" & TypeFile & "'."
                End Select
                
                If ReadBase(sql, Empty) > 0 Then _
                    PCWriteError varReport, "Файл ид. " & IdFile & " в настоящее время обрабатывается." : Exit Function
            Case 254
                If Not CBool(Scripter.Parameter("Не проверять загрузку файла")) Then
                    Select Case TypeFile
                        Case "зарплата"
                            sql = " select " & IIf(bIsSql, "top 1", "") & " 0 from CARD_SALARY_PLAN " & _
                                  " where ID_FILE = " & IdFile & " and TIME_RECORD > " & dat & _
                                  IIf(bIsSql, "", " and ROWNUM = 1") 
                        Case "операции из ПЦ"
                            sql = " select " & IIf(bIsSql, "top 1", "") & " 0 from CARD_PROCESS_CENTRE " & _
                                  " where ID_FILE = " & IdFile & " and TIME_RECORD > " & dat & _
                                  IIf(bIsSql, "", " and ROWNUM = 1") 
                        Case Else
                            Err.Raise vbObjectError + 1, "PCPRCFile_CanUndo", "Недопустимый тип файла: '" & TypeFile & "'."
                    End Select
                    
                    If ReadBase(sql, Empty) > 0 Then _
                        PCWriteError varReport, "Файл ид. " & IdFile & " в настоящее время загружается." : Exit Function
                End If
        End Select
    End If
    
    PCPRCFile_CanUndo = True
End Function

Function PCPRCFile_ExistsFile(ByVal IdProcessing, ByVal datFile, ByVal FileName, ByRef BankCode, ByRef varReport)
    Dim datBegin    : datBegin = DateSerial(Year(datFile), 1, 1)
    Dim strError
    Dim var
    
    PCPRCFile_ExistsFile = False
    If InStr(1, FileName, "\") > 0 Or InStr(1, FileName, "/") > 0 Then _
        FileName = UbsFile.GetFileName(FileName)
    
    If IsEmpty(IdProcessing) Then _
        Err.Raise vbObjectError + 1, "PCPRCFile_ExistsFile", "Не задан идентификатор процессинга."

    If IsEmpty(BankCode) Then _
        If Not GetFileSender(IdProcessing, "Код банка в процессинге", "", BankCode, strError) Then _
            Err.Raise vbObjectError + 1, "PCPRCFile_ExistsFile", strError

    If ReadBase(" select ID_FILE, DATE_FILE from CARD_FILE where NAME_FILE = '" & FileName & "'" & _
        " and DATE_FILE >= " & OdbcDate(datBegin) & " and COD_IN_PC = '" & BankCode & "'" & _
        " and ID_PROCESSING = " & IdProcessing, var) > 0 Then _
            PCWriteError varReport, "Файл '" & FileName & "' уже загружен в систему " & _
                DToS4(var(1, 0)) & ". Ид. файла: " & CLng(var(0, 0)) : PCPRCFile_ExistsFile = True : Exit Function
End Function

' Регистрирует файл в состоянии обработан
Function PCPRCFile_RegisterFile(ByVal strFileName, ByVal datFile, ByVal lngProcID, ByVal strBankCode, ByVal intFileType, _
    ByVal intDirection, ByVal strFileUID, ByRef lngFileID, ByRef lngDayNumber, ByRef varReport)
    
    PCPRCFile_RegisterFile = PCPRCFile_RegisterFileEx(strFileName, datFile, lngProcID, strBankCode, intFileType, _
        intDirection, strFileUID, 0, lngFileID, lngDayNumber, varReport)
End Function

' FileState     - состояние обработки файла :   0   - обработан
'                                               1   - загружен
'                                               254 - загружается
'                                               255 - обрабатывается
Function PCPRCFile_RegisterFileEx(ByVal strFileName, ByVal datFile, ByVal lngProcID, ByVal strBankCode, ByVal intFileType, _
    ByVal intDirection, ByVal strFileUID, ByVal FileState, ByRef lngFileID, ByRef lngDayNumber, ByRef varReport)

    Dim WR : Set WR = UbsWriteRead
    Dim var, i

    PCPRCFile_RegisterFileEx = False
    datFile = CDate(datFile)
    strFileUID = Trim(strFileUID)

    If InStr(1, strFileName, "/") Or InStr(1, strFileName, "\") Or InStr(1, strFileName, ":") Then _
        strFileName = Trim(UbsFile.GetFileName(strFileName))

    If strFileName = "" Then _
        Err.Raise vbObjectError + 1, "PCPRCFile_RegisterFileEx", "Не задано имя файла."
        
    lngProcID = CLng(lngProcID)
    If lngProcID < 0 Then _
        Err.Raise vbObjectError + 1, "PCPRCFile_RegisterFileEx", "Не задан процессинг."
    
    strBankCode = Trim(strBankCode)
    If strBankCode = "" Then _
        Err.Raise vbObjectError + 1, "PCPRCFile_RegisterFileEx", "Не задан код банка в процессинге."

    If FileState = 255 Then
        If ReadBase(" select ID_FILE, DATE_FILE, NAME_FILE from CARD_FILE  where TYPE_FILE = " & intFileType & _
                    " and ID_PROCESSING = " & lngProcID & " and IN_OUT = " & intDirection & _
                    " and COD_IN_PC = '" & strBankCode & "' and STATE = 255", var) > 0 Then
                    
            PCWriteError varReport, "В системе имеются обрабатываемые файлы такого же типа:"
            
            For i = 0 To UBound(var, 2)
                PCWriteReportLine varReport, "    " & var(2, i) & " (ид. " & var(0, i) & ") от " & DToS4(var(1, i))
            Next
            
            PCWriteReportLine varReport, "Регистрация файла невозможна."
            Exit Function
        End If
    End If

    ' Был ли загружен файл с таким именем ранее (текущий год)
    If PCPRCFile_ExistsFile(lngProcID, datFile, strFileName, strBankCode, varReport) Then _
        Exit Function
    
    WR.ClearFileRecord
    WR.FileParam("NameFile")      = strFileName
    WR.FileParam("DateFile")      = datFile
    WR.FileParam("InputOutput")   = intDirection '1
    WR.FileParam("TypeFile")      = intFileType '0
    WR.FileParam("CodInPc")       = strBankCode
    WR.FileParam("IdProcessing")  = lngProcID
    WR.FileParam("State")         = FileState
    If strFileUID <> "" Then WR.FileParam("UIDFile") = strFileUID
    WR.SaveFileRecord

    lngFileID = WR.FileParam("Id_File")
    lngDayNumber = WR.FileParam("NumberOrder")
    PCPRCFile_RegisterFileEx = True
End Function

Function PCPRCFile_UnRegisterFile(ByVal lngFileID)
    Dim WR : Set WR = UbsWriteRead

    PCPRCFile_UnRegisterFile = False
    
    lngFileID = CLng(lngFileID)
    If lngFileID <= 0 Then _
        objReportBuffer.Buffer = "PCPRCFile_UnRegisterFile. Не задан ид. файла." & vbNewLine : Exit Function
    
    WR.ClearFileRecord
    WR.FileParam("Id_File") = lngFileID
    WR.DeleteFileRecord
    
    PCPRCFile_UnRegisterFile = True
End Function

Function PCPRCFile_SetStateParam(objParamIn, objParamOut)
    Dim strReport
    PCPRCFile_SetStateParam = PCPRCFile_SetState(CLng(objParamIn.Parameter("IdFile")), objParamIn.Parameter("State"), strReport)
    If Not PCPRCFile_SetStateParam Then objParamOut.Parameter("strError") = strReport
End Function

Function PCPRCFile_SetState(ByVal lngFileID, ByVal FileState, ByRef varReport)
    Dim WR : Set WR = UbsWriteRead

    PCPRCFile_SetState = False

    WR.ClearFileRecord
    WR.FileParam("Id_File") = lngFileID
    WR.ReadFileRecord
    WR.FileParam("State") = FileState
    WR.SaveFileRecord

    PCPRCFile_SetState = True
End Function

Sub GetFileInfo(objParamIn, objParamOut)
    Dim WR
    Set WR = UbsWriteRead
	
    WR.ClearFileRecord
    WR.FileParam("Id_File") = CLng(objParamIn.Parameter("Id_File"))
    WR.ReadFileRecord
	
    objParamOut.Parameter("DateFile") = WR.FileParam("DateFile")
    objParamOut.Parameter("NameFile") = WR.FileParam("NameFile")
    objParamOut.Parameter("TypeFile") = WR.FileParam("TypeFile")
End Sub

Function PCPRCFile_ChangeName(ByVal lngFileID, ByVal strNewFileName, ByRef varReport)
    Dim WR : Set WR = UbsWriteRead

    PCPRCFile_ChangeName = False
    
    lngFileID = CLng(lngFileID)
    If lngFileID <= 0 Then _
        PCWriteError varReport, "PCPRCFile_ChangeName. Не задан ид. файла." : Exit Function

    strNewFileName = Trim(strNewFileName)
    If strNewFileName = "" Then _
        PCWriteError varReport, "PCPRCFile_ChangeName. Не задано новое имя файла." : Exit Function

    WR.ClearFileRecord
    WR.FileParam("Id_File") = lngFileID
    WR.ReadFileRecord
    WR.FileParam("NameFile") = strNewFileName   'подменим имя файла
    WR.SaveFileRecord
    
    PCPRCFile_ChangeName = True
End Function

' Получить номер файла в дне, не создавая его
Function PCPRCFile_GetNewNumOrder(ByVal TypeFile, ByVal InOut, ByVal DateFile, ByVal IdProcessing, ByVal BankCode)
    Dim RS
    
    PCPRCFile_GetNewNumOrder = 1
    
    Call GlobalDataAccess.Read( UbsOdbc.DSN(GlobalUser.SourceName), _
                                " SELECT MAX(NUM_ORDER) FROM CARD_FILE " & _
                                " WHERE  TYPE_FILE = " & CLng(TypeFile) & _
                                "    AND IN_OUT = " & CLng(InOut) & _
                                "    AND ID_PROCESSING = " & CLng(IdProcessing) & _
                                "    AND COD_IN_PC = '" & BankCode & "' " & _
                                "    AND DATE_FILE = " & UbsOdbc.OdbcDate(DateFile), _
                                RS)
    
    If Not IsNull(RS(0, 0)) Then _
        PCPRCFile_GetNewNumOrder = CLng(RS(0, 0)) + 1
End Function

' FileName      - имя файла
' IdProcessing  - ид. процессинга
' FileType      - тип файла, необязательный
' FileDirect    - направление: 0 - входящий; 1 - исходящий, необязательный
' BankCode      - код отделения/банка в ПЦ, необязательный
' DateBegin     - дата начала поиска, необязательный
Function PCPRCFile_FindFile(ByVal FileName, ByVal FileType, ByVal FileDirect, ByVal IdProcessing, ByVal BankCode, ByVal DateBegin)
    Dim strSQL
    Dim var, i
    
    PCPRCFile_FindFile = CLng(0)
    
    strSQL = " select ID_FILE from CARD_FILE where NAME_FILE = '" & FileName & "' " & _
             IIf(IsEmpty(FileDirect), "", " and IN_OUT = " & CLng(FileDirect)) & _
             IIf(IsEmpty(FileType), "", " and TYPE_FILE = " & CLng(FileType)) & _
             IIf(IsEmpty(BankCode), "", " and COD_IN_PC = '" & BankCode & "' ") & _
             IIf(IsEmpty(DateBegin), "", " and DATE_FILE >= " & OdbcDate(DateBegin)) & _
             " and ID_PROCESSING = " & CLng(IdProcessing)
    
    If ReadBase(strSQL, var) > 0 Then _
        PCPRCFile_FindFile = CLng(var(0, 0))
End Function