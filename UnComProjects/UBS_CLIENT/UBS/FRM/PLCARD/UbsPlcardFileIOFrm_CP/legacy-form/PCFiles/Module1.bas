Attribute VB_Name = "Module1"
Option Explicit

Public Function GetSettingFilePath(strSetting As String, arrSetting As Variant, lngDivisionID As Long)
     Dim ArrTmp As Variant, i As Integer, j As Integer
     Dim strTMP As String, ch As String
     Dim srtErrHeader, strErr
     Dim strFilePath
    
     GetSettingFilePath = ""
     ch = Chr(&H22)
   
     srtErrHeader = "GetSettingFilePath"
     If Trim(strSetting) = "" Then
         Err.Raise 1, srtErrHeader, "Не задана установка."
         Exit Function
     End If
    
     If Not IsArray(arrSetting) Then Exit Function
  
     For i = 0 To UBound(arrSetting, 2)
         If strSetting = arrSetting(0, i) Then
             ArrTmp = Split(arrSetting(2, i), ",")
             For j = 0 To UBound(ArrTmp, 1)
                 strTMP = Trim(CStr(ArrTmp(j)))
                 If strTMP = "*" Then
                   strFilePath = arrSetting(1, i)
                 ElseIf strTMP = CStr(lngDivisionID) Then
                   strFilePath = arrSetting(1, i)
                   Exit For
                 End If
             Next
         End If
     Next
    
     GetSettingFilePath = strFilePath
End Function

'=============================================================='
'======     Деление массива на несколько частей          ======
'=============================================================='
Public Function DivideIntoParts(varDataOper, nCountChanel)
     Dim ArrPotok, ArrTmp
     Dim i, k, j
    
     j = 1
     For i = 0 To UBound(varDataOper, 2)

        ' ----------
         If Not IsArray(ArrTmp) Then
            ReDim ArrTmp(UBound(varDataOper, 1), 0)
        Else
            ReDim Preserve ArrTmp(UBound(ArrTmp, 1), UBound(ArrTmp, 2) + 1)
         End If
        ' ----------
         For k = 0 To UBound(varDataOper, 1)
             ArrTmp(k, UBound(ArrTmp, 2)) = varDataOper(k, i)
         Next

        If i = Round(j * UBound(varDataOper, 2) / nCountChanel) Or i = UBound(varDataOper, 2) Then
            ' ----------
             If Not IsArray(ArrPotok) Then
                 ReDim ArrPotok(0)
             Else
                 ReDim Preserve ArrPotok(UBound(ArrPotok) + 1)
             End If
            ' ----------
             ArrPotok(UBound(ArrPotok)) = ArrTmp
             ArrTmp = Empty
             j = j + 1
         End If

     Next
     DivideIntoParts = ArrPotok
End Function

'=============================================================='
'======     Инициализация ассинхронных каналов           ======
'=============================================================='
Public Sub InitChannelAsync(strResource, nCountChanel, Parent, NumWin, UbsChnlAsync)
 Dim k As Integer
       
     For k = 0 To nCountChanel - 1
   
        UbsChnlAsync(k).HostAddress = Parent.LoaderParamInfo(NumWin, "HostAddress")
         UbsChnlAsync(k).UserGUID = Parent.LoaderParamInfo(NumWin, "GuidSession")
         UbsChnlAsync(k).LoadResource = strResource
         'UbsChnlAsync(k).LoadResource = "VBS:UBS_VBS\PLCARD\OPENWAY\PCFileOperOW.vbs"
         UbsChnlAsync(k).FormatParamOut = "XML"
         UbsChnlAsync(k).FormatParamIn = "XML"
         UbsChnlAsync(k).ServerNotice = True
        
     Next
    
End Sub
