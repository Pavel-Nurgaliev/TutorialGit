Attribute VB_Name = "modWinAPI"
'---------------------- Novopashin V.M. 09-02-2006 -------------------

'Координаты точки в твипах по x и y
Public Type POINTAPI
    x As Long
    y As Long
End Type

'Получить координаты курсора в твипах по x и y
Public Declare Function GetCursorPos Lib "user32" (lpPoint As POINTAPI) As Long

'
Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
'--------------------------------------------------------------------
