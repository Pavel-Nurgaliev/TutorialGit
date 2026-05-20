' ScriptRunner.Parameter("StrCommand")
' ScriptRunner.Run "Handle_FileIO"

Sub Handle_FileIO()
  Dim StrCommand
  StrCommand = CStr(Scripter.Parameter("StrCommand"))
  Select Case StrCommand
    ' передаваемые формой параметры
    ' Scripter.Parameter("DateOper") - если дата затребована
    ' Scripter.Parameter("PathFile")
    ' Scripter.Parameter("KeyArray")
    ' Scripter.Parameter("ArrayParams")

    ' пример
    '
    'Case "PCFileOdOutUBS"
    '  Scripter.Parameter("InOut") = "In" '"Out"
    '  Scripter.Parameter("NeedDateOper") = True 'False
    '  Scripter.Parameter("SID_Action") = "UBS_PLCARD_FILE_INP_OPERATION_LOAD" '""
    '  Scripter.Parameter("SID_Script") = "" '"UBS_VBS_PLCARD_FILE_IN_L"
    '  Scripter.Parameter("NameFunction") = "Handle_FileIn" '""
    '  Scripter.Parameter("SetupPathFile") = "Прием файла из PRIME"
    '  Scripter.Parameter("LabelFile") = "Файл для загрузки"
    '  Scripter.Parameter("LabelDate") = "Дата загрузки"
    '  'Scripter.Parameter("SetupPathArch") = "..."
    Case "PCFileOdOutUBS"
      Scripter.Parameter("InOut") = "Out"
      Scripter.Parameter("NeedDateOper") = False
      Scripter.Parameter("SID_Action") = ""
      Scripter.Parameter("SID_Script") = "UBS_VBS_PLCARD_FILE_OD_OUT_CULTIV"
      Scripter.Parameter("NameFunction") = ""
      Scripter.Parameter("SetupPathFile") = ""
      Scripter.Parameter("LabelFile") = "Путь к файлу для выгрузки"
      Scripter.Parameter("LabelDate") = ""
      'Scripter.Parameter("SetupPathArch") = "..."
    Case "PCFileOutBalanceCard"
      Scripter.Parameter("InOut") = "Out"
      Scripter.Parameter("NeedDateOper") = True
      Scripter.Parameter("SID_Action") = "UBS_PLCARD_FILE_OUT_LIMIT_MAKE"
      Scripter.Parameter("SID_Script") = ""
      Scripter.Parameter("NameFunction") = "Handle_FileOut"
      Scripter.Parameter("SetupPathFile") = "Выгрузка файла с балансами карт в OpenWay"
      Scripter.Parameter("LabelFile") = "Файл для выгрузки"
      Scripter.Parameter("LabelDate") = "Остатки за "
      'Scripter.Parameter("SetupPathArch") = "..."

    Case "PCFileInBalanceCard"
      Scripter.Parameter("InOut") = "In"
      Scripter.Parameter("NeedDateOper") = False
      Scripter.Parameter("SID_Action") = "UBS_PLCARD_FILE_INP_BALCARD_MAKE"
      Scripter.Parameter("SID_Script") = ""
      Scripter.Parameter("NameFunction") = "Handle_FileIn"
      Scripter.Parameter("SetupPathFile") = "Прием файла с балансами карт из OpenWay"
      Scripter.Parameter("LabelFile") = "Файл для загрузки"
      'Scripter.Parameter("LabelDate") = "Остатки за "
      'Scripter.Parameter("SetupPathArch") = "..."

    Case "PCFileInExtBalanceCard"
      Scripter.Parameter("InOut") = "In"
      Scripter.Parameter("NeedDateOper") = False
      Scripter.Parameter("SID_Action") = "UBS_PLCARD_FILE_INP_BALCARD_EXT_MAKE"
      Scripter.Parameter("SID_Script") = ""
      Scripter.Parameter("NameFunction") = "Handle_FileIn"
      Scripter.Parameter("SetupPathFile") = "Прием файла с расширенными балансами карт из OpenWay"
      Scripter.Parameter("LabelFile") = "Файл для загрузки"
      'Scripter.Parameter("LabelDate") = "Остатки за "
      'Scripter.Parameter("SetupPathArch") = "..."

    Case "PCFileOutNewCardOws"	'выгрузка файла с заявками на выпуск карт для OpenWay
      Scripter.Parameter("InOut") = "Out"
      Scripter.Parameter("NeedDateOper") = False
      Scripter.Parameter("SID_Action") = ""
      Scripter.Parameter("SID_Script") = "UBS_VBS_PLCARD_FILE_IO_OUT_UNI"
      Scripter.Parameter("NameFunction") = "Handle_File_Out"
      Scripter.Parameter("SetupPathFile") = "Выгрузка файла на выпуск/изменение карт в OpenWay"
      Scripter.Parameter("LabelFile") = "Каталог для выгрузки "
      Scripter.Parameter("LabelDate") = "Дата выгрузки "
      'Scripter.Parameter("SetupPathArch") = "..."
      Scripter.Parameter("ArrayParams") = "UBS_VBS_PLCARD_FILE_OUT_SRV_2"



    Case "PCFileInNewCardResponseOws"	'закачка файла ответа на заявки на выпуск карт для OpenWay
      Scripter.Parameter("InOut") = "In"
      Scripter.Parameter("NeedDateOper") = False
      Scripter.Parameter("SID_Action") = ""
      Scripter.Parameter("SID_Script") = "UBS_VBS_PLCARD_FILE_IO_IN_UNI"
      Scripter.Parameter("NameFunction") = "Handle_File_In"
      Scripter.Parameter("SetupPathFile") = "Прием файла-ответа на выпуск карт из OpenWay"
      Scripter.Parameter("LabelFile") = "Файл ответа  выгрузки карт в процессинг"
      Scripter.Parameter("LabelDate") = "Дата выгрузки "
      'Scripter.Parameter("SetupPathArch") = "..."
      Scripter.Parameter("ArrayParams") = "UBS_VBS_PLCARD_IMPORT_ANSW_NEW_CARD"

    Case "PCFileOutNewCardUKard"	'выгрузка файла с заявками на выпуск карт для Union Card
      Scripter.Parameter("InOut") = "Out"
      Scripter.Parameter("NeedDateOper") = False
      Scripter.Parameter("SID_Action") = ""
      Scripter.Parameter("SID_Script") = "UBS_VBS_PLCARD_FILE_IO_OUT_UNI"
      Scripter.Parameter("NameFunction") = "Handle_File_Out"
      Scripter.Parameter("SetupPathFile") = ""
      Scripter.Parameter("LabelFile") = "Каталог для выгрузки "
      Scripter.Parameter("LabelDate") = "Дата выгрузки "
      'Scripter.Parameter("SetupPathArch") = "..."
      Scripter.Parameter("ArrayParams") = "UBS_VBS_PLCARD_FILE_OUT_SRV_3"


    Case "PCFileInTransactionsExportOws"	'закачка файла c операциями из процессинга OpenWay
      Scripter.Parameter("InOut") = "In"
      Scripter.Parameter("NeedDateOper") = True
      Scripter.Parameter("SID_Action") = ""
      Scripter.Parameter("SID_Script") = "UBS_VBS_PLCARD_FILE_IN_OPERATION_OWS_L"
      Scripter.Parameter("NameFunction") = "Handle_File_In"
      Scripter.Parameter("SetupPathFile") = "Прием файла с транзакциями из OpenWay"
      Scripter.Parameter("LabelFile") = "Файл с операциями"
      Scripter.Parameter("LabelDate") = "Дата проведения "
      'Scripter.Parameter("SetupPathArch") = "..."

    Case "PCFileOutPaymentsImportOws"	'выгрузка файла c операциями в процессинг OpenWay
      Scripter.Parameter("InOut") = "Out"
      Scripter.Parameter("NeedDateOper") = False
      Scripter.Parameter("SID_Action") = ""
      Scripter.Parameter("SID_Script") = "UBS_VBS_PLCARD_FILE_IO_OUT_UNI"
      Scripter.Parameter("NameFunction") = "Handle_File_Out"
      Scripter.Parameter("SetupPathFile") = "Выгрузка файла с операциями в OpenWay"
      Scripter.Parameter("LabelFile") = "Каталог для выгрузки"
      Scripter.Parameter("LabelDate") = "Дата проведения "
      'Scripter.Parameter("SetupPathArch") = "..."
      Scripter.Parameter("ArrayParams") = "UBS_VBS_PLCARD_OPERATION_OUT_OWS"

    Case "PCFileInAuthorization" ' закачка файла с авторизационными сообщениями
      Scripter.Parameter("InOut") = "In"
      Scripter.Parameter("NeedDateOper") = False
      Scripter.Parameter("SID_Action") = ""
      Scripter.Parameter("SID_Script") = "UBS_VBS_PLCARD_FILE_IO_IN_UNI"
      Scripter.Parameter("NameFunction") = "Handle_File_In"
      Scripter.Parameter("SetupPathFile") = "Прием файла с авторизациями из OpenWay"
      Scripter.Parameter("LabelFile") = "Файл с авторизационными сообщениями:"
      Scripter.Parameter("LabelDate") = ""
      'Scripter.Parameter("SetupPathArch") = "..."
      Scripter.Parameter("ArrayParams") = "UBS_VBS_PLCARD_PCFileInAuthorization"

  Case Else
    MsgBox "Параметр запуска '" & StrCommand & "' не поддерживается. (Не настроен загрузочный скрипт)"
    Scripter.Parameter("InOut") = ""
  End Select
End Sub

