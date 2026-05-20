'#include UBS_VBS\PLCARD\Lib\PcLib.vbs

' Ид. типа файла
' 1         - Payment, Транзакции
' 2         - зарплатные файлы
' 3,10,13   - Application and MasterCard
' 4         - авторизации
' 9         - файл со списанием
' 11        - MasterCard - Клиенты
' 12        - MasterCard - Счета
' 14        - лимиты в MasterCard
' 16        - Файлы на де/активацию услуги SMS-инфо
' 17        - файл с пополнением

Function PCLibFile_RegisterFile(ByVal strFileName, ByVal datFile, ByVal lngProcID, ByVal strBankCode, ByVal intFileType, _
    ByVal intDirection, ByVal strFileUID, ByRef lngFileID, ByRef lngDayNumber, ByRef varReport)

    PCLibFile_RegisterFile = False
    datFile = CDate(datFile)
    strFileUID = Trim(strFileUID)

    If InStr(1, ";1;2;3;4;9;10;11;12;13;14;16;17;", ";" & CStr(intFileType) & ";") = 0 Then _
        Err.Raise vbObjectError + 1, "PCLibFile_RegisterFile", "Недопустимый тип файла: " & intFileType &  "."

    If InStr(1, strFileName, "/") Or InStr(1, strFileName, "\") Or InStr(1, strFileName, ":") Then _
        strFileName = Trim(UbsFile.GetFileName(strFileName))

    If strFileName = "" Then _
        Err.Raise vbObjectError + 1, "PCLibFile_RegisterFile", "Не задано имя файла."
        
    lngProcID = CLng(lngProcID)
    If lngProcID < 0 Then _
        Err.Raise vbObjectError + 1, "PCLibFile_RegisterFile", "Не задан процессинг."
    
    strBankCode = Trim(strBankCode)
    If strBankCode = "" Then _
        Err.Raise vbObjectError + 1, "PCLibFile_RegisterFile", "Не задан код банка в процессинге." : Exit Function

    Dim WR : Set WR = UbsWriteRead
    WR.ClearFileRecord
    WR.FileParam("NameFile")     = strFileName
    WR.FileParam("DateFile")     = datFile
    WR.FileParam("InputOutput")  = intDirection '1
    WR.FileParam("TypeFile")     = intFileType '0
    WR.FileParam("CodInPc")      = strBankCode
    WR.FileParam("IdProcessing") = lngProcID
    If strFileUID <> "" Then WR.FileParam("UIDFile") = strFileUID
    WR.SaveFileRecord

    lngFileID = WR.FileParam("Id_File")
    lngDayNumber = WR.FileParam("NumberOrder")
    
    PCLibFile_RegisterFile = True
End Function

Function PCLibFile_RegisterFileSource(ByVal IdFile, ByVal CodeBusiness, ByVal IdObject, ByRef varReport)
    PCLibFile_RegisterFileSource = False
    
    Call WriteTransact(" insert into CARD_FILE_SOURCE (ID_FILE, COD_BUSINESS, ID_OBJECT) " & _
                   " values (" & CLng(IdFile) & ", '" & Trim(CodeBusiness) & "', " & CLng(IdObject) & ")", 0)
    
    PCLibFile_RegisterFileSource = True
End Function

Function PCLibFile_ReadFileSource(ByVal IdFile, ByRef CodeBusiness, ByRef IdObject, ByRef varReport)
    Dim var
    
    PCLibFile_ReadFileSource = False
    CodeBusiness = Empty
    IdObject = CLng(0)
    
    If ReadBase(" select COD_BUSINESS, ID_OBJECT from CARD_FILE_SOURCE where ID_FILE = " & CLng(IdFile), var) > 0 Then _
        CodeBusiness = var(0, 0) : IdObject = CLng(var(1, 0))
    
    PCLibFile_ReadFileSource = True
End Function

Sub PCLibFile_FileDelete(ByVal IdFile)
    Dim bInTransact : bInTransact = InTransact
    
    If Not bInTransact Then BeginTransact
    ' Удаление записи об источнике файла
    Call WriteTransact(" delete from CARD_FILE_SOURCE where ID_FILE = " & CLng(IdFile), 0)

    ' Удаление самого файла
    Dim WR : Set WR = UbsWriteRead
    WR.ClearFileRecord
    WR.FileParam("Id_File") = CLng(IdFile)
    WR.DeleteFileRecord
    If Not bInTransact Then CompleteTransact
End Sub

Function PCLibFile_CheckFileNameForMask(ByVal MaskType, ByVal FileType, ByVal FileName, ByVal IdProcessing, ByRef varReport)
    Dim var
    Dim i

    PCLibFile_CheckFileNameForMask = False
    
    ' Возможно передано полное имя файла. Убраем путь...
    If InStr(1, FileName, "\") <> 0 Then _
        FileName = UbsFile.GetFileName(FileName)

    If InStr(1, ";Маски файлов контрагентов;Маски файлов процессинга;", ";" & MaskType & ";") = 0 Then _
        Err.Raise vbObjectError + 1, "PCLibFile_CheckFileNameForMask", "Недопустимый тип маски файлов '" & MaskType & "'."

    If MaskType = "Маски файлов контрагентов" Then IdProcessing = 0 ' Только установка для всех процессингов
    var = PCGetSettingValue(MaskType, CLng(IdProcessing))

    If Not IsArray(var) Then _
        PCLibFile_CheckFileNameForMask = True : Exit Function
        
    i = UbsArray.AScan(var, Trim(FileType), 2, 0, 0)
    If i < 0 Then _
        PCLibFile_CheckFileNameForMask = True : Exit Function
    
    Dim b_find : b_find = False
    Dim s_mask : s_mask = var(1, i)
    var = Split(UCase(s_mask), ";")
    For i = 0 To UBound(var)
        If Trim(var(i)) <> "" Then _
            If UbsFormat.LikeStr(UCase(FileName), UCase(Trim(var(i)))) Then _
                b_find = True : Exit For
    Next
    
    If Not b_find Then _
        PCWriteError varReport, "Имя файла '" & FileName & "' не удовлетворяет маске '" & s_mask & _
            "', установленной для файлов типа '" & FileType & "' в установке '" & MaskType & "'." : Exit Function
    
    PCLibFile_CheckFileNameForMask = True
End Function
