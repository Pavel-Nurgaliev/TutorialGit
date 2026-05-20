using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using UbsPlcardFileZPInFrm;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// Шаблон класса формы
    /// </summary>
    public partial class UbsPlcardFileZPInFrm : UbsFormBase
    {
        #region Блок объявления переменных

        private string m_command = string.Empty;
        private IUbsScript m_scriptRunner;
        private DateTime m_dateMinValue = new DateTime(2222, 1, 1);
        private DateTime m_dateToday = new DateTime(2222, 1, 1);
        private int m_division;
        private int m_countChanel;
        private UbsParam m_paramIn;
        private Label[] m_arrChannelLabel;
        private Dictionary<Label, UbsChannel> m_dicChannel;
        private PaymentOrderFrm m_frmPayment;
        private int m_recordUp;
        private object m_recordCounter;
        private string m_clientFileName;
        private object[,] m_paymentData;
        private bool m_isOk;
        private object[,] m_arrDoc;
        private string m_paymentOrderTypeTmp;
        private object[,] m_arrTypePaymentOrder;
        private int m_paymentOrderId;
        private string m_paymentOrderType;
        private int m_idFile;
        private object[,] m_arrProfitKind;
        private bool m_resourcesDisposed;

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsPlcardFileZPInFrm()
        {
            m_addCommand(); //зарегистрировать обработчики команд интерфейса IUbs

            InitializeComponent();

            m_paramIn = new UbsParam();

            m_arrChannelLabel = new Label[]
            {
                lblChannelInfo1
              , lblChannelInfo2
              , lblChannelInfo3
              , lblChannelInfo4
              , lblChannelInfo5
              , lblChannelInfo6
              , lblChannelInfo7
            };

            m_dicChannel = new Dictionary<Label, UbsChannel>();

            m_dicChannel.Add(m_arrChannelLabel[0], new UbsChannel());
            m_dicChannel.Add(m_arrChannelLabel[1], new UbsChannel());
            m_dicChannel.Add(m_arrChannelLabel[2], new UbsChannel());
            m_dicChannel.Add(m_arrChannelLabel[3], new UbsChannel());
            m_dicChannel.Add(m_arrChannelLabel[4], new UbsChannel());
            m_dicChannel.Add(m_arrChannelLabel[5], new UbsChannel());
            m_dicChannel.Add(m_arrChannelLabel[6], new UbsChannel());

            foreach (var chnl in m_dicChannel)
            {
                chnl.Value.LoadResource = @"VBS:UBS_VBS\PLCARD\OPENWAY\PCSalaryImport.vbs";

                chnl.Value.Respond += IUbsChannel_Respond;
                chnl.Value.Error += IUbsChannel_Error;
                chnl.Value.Notice += IUbsChannel_Notice;
            }

            this.FormClosed += UbsPlcardFileIOFrm_FormClosed;

            base.Ubs_CommandLock = true;
        }

        private void UbsPlcardFileIOFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_resourcesDisposed)
                return;

            try
            {
                if (m_scriptRunner != null)
                {
                    m_scriptRunner.Dispose();
                    m_scriptRunner = null;
                }

                m_resourcesDisposed = true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }
        private void IUbsChannel_Notice(object sender, UbsChannelEventArgs args)
        {
            try
            {
                string textNotice = args != null && args.Message != null
                    ? args.Message.ToString()
                    : string.Empty;

                if (textNotice.Length <= 3 || !textNotice.StartsWith("OK:")) return;

                string numberPart = textNotice.Substring(3).Trim();
                int parsed;
                if (!int.TryParse(numberPart, out parsed)) return;

                MethodInvoker d = delegate ()
                {
                    m_recordUp += parsed;
                    lblProgress.Text = "Обработано " + m_recordUp + " сообщений из " + m_recordCounter;
                };

                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.Invoke(d);
                }
            }
            catch
            {
                // Ни при каких условиях не пробрасываем исключение наружу —
                // иначе оно уйдёт обратно на сервер по тому же callback-каналу,
                // и серверный скрипт ляжет с COMException.
            }
        }

        private void IUbsChannel_Error(object sender, UbsChannelEventArgs args)
        {
            try
            {
                MethodInvoker d = delegate ()
                {
                    var channel = sender as UbsChannel;

                    var paramOut = channel.ParamsOutParam;

                    Label labelChannel = FindLabel(m_dicChannel, channel);

                    MessageBox.Show(args.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    labelChannel.BackColor = Color.FromArgb(255, 0, 0);

                    var paramIn = new UbsParam();

                    var index = Convert.ToInt32(labelChannel.Text);

                    paramIn["Error"] = args.Message.ToString();
                    paramIn["NC"] = index + 1;

                    channel.RunAsync("PCSalaryImport__AsyncError", paramIn);

                    btnSave.Enabled = true;
                    btnExit.Text = "Выход";
                    btnExit.Enabled = true;

                    if (m_idFile > 0)
                    {
                        try { SetFileStatus(0); } catch { }
                    }
                };
                this.Invoke(d);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }
        private Label FindLabel(Dictionary<Label, UbsChannel> dicChannel, UbsChannel currentChannel)
        {
            Label result = null;

            foreach (var chnl in dicChannel)
            {
                if (chnl.Value == currentChannel)
                {
                    result = chnl.Key;
                }
            }

            return result;
        }

        private void IUbsChannel_Respond(object sender, UbsChannelEventArgs args)
        {
            try
            {
                string content = string.Empty;

                MethodInvoker d = delegate ()
                {
                    var channel = sender as UbsChannel;
                    var paramOut = channel.ParamsOutParam;

                    Label labelChannel = FindLabel(m_dicChannel, channel);
                    if (labelChannel == null)
                        return;

                    if (!int.TryParse(labelChannel.Text, out int index))
                    {
                        throw new Exception($"Не найден номер канала. {labelChannel.Text}");
                    }

                    if (paramOut.Contains("ReportError"))
                    {
                        // Один канал сообщил, что у него отчётная ошибка.
                        // Затирать здесь m_clientFileName нельзя — там уже могли
                        // накопиться записи протокола от других каналов
                        // (см. п.11: "половина протокола" получалась как раз
                        // из-за обнуления общего файла на ошибке одного канала).
                        // Просто покажем сообщение и сбросим UI; общий протокол
                        // соберём и отобразим стандартным путём, когда дойдёт
                        // последний канал.
                        MessageBox.Show(Convert.ToString(m_paramIn.Value("ReportError")), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnExit.Text = "Выход";
                        btnExit.Enabled = true;
                        btnSave.Text = "Загрузка!";
                        btnSave.Enabled = true;

                        for (int i = 0; i < m_arrChannelLabel.GetLength(0); i++)
                        {
                            m_arrChannelLabel[i].BackColor = btnSave.BackColor;
                        }

                        return;
                    }

                    if (string.IsNullOrEmpty(m_clientFileName))
                    {
                        m_clientFileName = Path.GetTempFileName();
                    }

                    if (chkControlRun.Checked)
                    {
                        if (paramOut.Contains("varZPData"))
                        {
                            var zpTmp = paramOut["varZPData"] as object[,];

                            if (zpTmp != null)
                            {
                                int zpTmpRecordCount = zpTmp.GetLength(0);

                                if (m_paymentData is null)
                                {
                                    m_paymentData = new object[1, 5];

                                    m_paymentData[0, 0] = 0;             //ID счёта
                                    m_paymentData[0, 1] = string.Empty;  //счёт
                                    m_paymentData[0, 2] = 0;             //сальдо
                                    m_paymentData[0, 3] = 0;             //сумма зп
                                    m_paymentData[0, 4] = 0;             //сумма комиссии
                                }

                                bool[] zpTmpMerged = new bool[zpTmpRecordCount];

                                for (int indexZP = 0; indexZP < m_paymentData.GetLength(0); indexZP++)
                                {
                                    var zpdIdAccount = Convert.ToInt32(m_paymentData[indexZP, 0]);

                                    for (int indexZPTmp = 0; indexZPTmp < zpTmpRecordCount; indexZPTmp++)
                                    {
                                        if (zpTmpMerged[indexZPTmp]) continue;

                                        var zptdIdAccount = Convert.ToInt32(zpTmp[indexZPTmp, 0]);

                                        if (zpdIdAccount > 0 && zpdIdAccount == zptdIdAccount)
                                        {
                                            var zpdSum = Convert.ToDecimal(m_paymentData[indexZP, 3]);
                                            var zptdSum = Convert.ToDecimal(zpTmp[indexZPTmp, 3]);

                                            var zpdSumCom = Convert.ToDecimal(m_paymentData[indexZP, 4]);
                                            var zptdSumCom = Convert.ToDecimal(zpTmp[indexZPTmp, 4]);

                                            m_paymentData[indexZP, 3] = zpdSum + zptdSum;
                                            m_paymentData[indexZP, 4] = zpdSumCom + zptdSumCom;

                                            zpTmpMerged[indexZPTmp] = true;
                                        }
                                    }
                                }

                                for (int indexZPTmp = 0; indexZPTmp < zpTmpRecordCount; indexZPTmp++)
                                {
                                    if (zpTmpMerged[indexZPTmp]) continue;

                                    var zptdIdAccount = Convert.ToInt32(zpTmp[indexZPTmp, 0]);

                                    if (zptdIdAccount > 0)
                                    {
                                        var firstZpdIdAccount = Convert.ToInt32(m_paymentData[0, 0]);

                                        if (firstZpdIdAccount == 0)
                                        {
                                            m_paymentData[0, 0] = zpTmp[indexZPTmp, 0];
                                            m_paymentData[0, 1] = zpTmp[indexZPTmp, 1];
                                            m_paymentData[0, 2] = zpTmp[indexZPTmp, 2];
                                            m_paymentData[0, 3] = zpTmp[indexZPTmp, 3];
                                            m_paymentData[0, 4] = zpTmp[indexZPTmp, 4];
                                        }
                                        else
                                        {
                                            int oldRows = m_paymentData.GetLength(0);
                                            int cols = m_paymentData.GetLength(1);
                                            var tmp = new object[oldRows + 1, cols];

                                            for (int r = 0; r < oldRows; r++)
                                            {
                                                for (int c = 0; c < cols; c++)
                                                {
                                                    tmp[r, c] = m_paymentData[r, c];
                                                }
                                            }

                                            tmp[oldRows, 0] = zpTmp[indexZPTmp, 0];
                                            tmp[oldRows, 1] = zpTmp[indexZPTmp, 1];
                                            tmp[oldRows, 2] = zpTmp[indexZPTmp, 2];
                                            tmp[oldRows, 3] = zpTmp[indexZPTmp, 3];
                                            tmp[oldRows, 4] = zpTmp[indexZPTmp, 4];

                                            m_paymentData = tmp;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    string srvFileNameFull = paramOut.Contains("Имя файла")
                        ? Convert.ToString(paramOut["Имя файла"]) ?? string.Empty
                        : string.Empty;
                    string srvFileName = srvFileNameFull.Trim();

                    if (m_clientFileName == string.Empty)
                    {
                        m_clientFileName = Path.GetTempFileName();

                        File.WriteAllText(m_clientFileName, string.Empty);
                    }

                    int nPos = srvFileName.LastIndexOf('\\');

                    if (nPos >= 0 && nPos + 1 < srvFileName.Length)
                    {
                        srvFileName = srvFileName.Substring(nPos + 1);
                    }

                    string clientFileNameTmp = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

                    if (File.Exists(clientFileNameTmp))
                        File.Delete(clientFileNameTmp);

                    // 1) Сначала забираем файл-протокол с сервера.
                    bool fileFetched = false;
                    if (!string.IsNullOrEmpty(srvFileName))
                    {
                        try
                        {
                            base.IUbsChannel.GetFile(clientFileNameTmp, "Temp", srvFileName);
                            fileFetched = File.Exists(clientFileNameTmp);
                        }
                        catch (Exception exGetFile)
                        {
                            File.AppendAllText(m_clientFileName,
                                $"!!! Ошибка получения файла-протокола канала {index}: {exGetFile.Message}{Environment.NewLine}",
                                Encoding.GetEncoding(1251));
                        }
                    }

                    // 2) ВАЖНО: дозапись в общий m_clientFileName делаем ДО удаления
                    // серверного файла. Если DeleteServerFileEx упадёт (а он стабильно
                    // даёт "DeleteServerFileExStub уже запущена" при быстром
                    // последовательном вызове на том же базовом канале — см. п.8.1),
                    // содержимое второго канала всё равно уже окажется в общем
                    // протоколе. Иначе при половинном протоколе теряются записи
                    // одного из каналов (п.11).
                    if (fileFetched)
                    {
                        try
                        {
                            string tmpContent = File.ReadAllText(clientFileNameTmp, Encoding.GetEncoding(1251));
                            File.AppendAllText(m_clientFileName, tmpContent + Environment.NewLine, Encoding.GetEncoding(1251));
                        }
                        catch (Exception exRead)
                        {
                            File.AppendAllText(m_clientFileName,
                                $"!!! Ошибка чтения файла-протокола канала {index}: {exRead.Message}{Environment.NewLine}",
                                Encoding.GetEncoding(1251));
                        }
                        finally
                        {
                            try { if (File.Exists(clientFileNameTmp)) File.Delete(clientFileNameTmp); }
                            catch { }
                        }
                    }

                    // 3) И только после успешной дозаписи — пытаемся удалить
                    // серверный temp-файл. Это побочная очистка, поэтому
                    // оборачиваем в try/catch: коллизия по "DeleteServerFileExStub
                    // уже запущена" не должна валить весь Respond и приводить к
                    // тому, что файл застревает в статусе "Загружается".
                    if (!string.IsNullOrEmpty(srvFileName))
                    {
                        try
                        {
                            m_scriptRunner.Run("DeleteServerFileEx", "Temp", srvFileName);
                        }
                        catch
                        {
                            // Серверный temp оставим как есть — он чистится
                            // штатной фоновой очисткой папки Temp на сервере.
                        }
                    }

                    m_isOk = true;

                    labelChannel.BackColor = Color.White;

                    labelChannel.Tag = string.Empty;

                    for (int i = 0; i < m_arrChannelLabel.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(m_arrChannelLabel[i].Tag)))
                        {
                            m_isOk = false;
                            break;
                        }
                    }

                    if (paramOut.Contains("Сводный платеж.Массив документов"))
                    {
                        var arrDocTmp = paramOut["Сводный платеж.Массив документов"] as object[,];

                        if (arrDocTmp != null)
                        {
                            for (int i = 0; i < arrDocTmp.GetLength(0); i++)
                            {
                                int newRow;
                                if (m_arrDoc == null)
                                {
                                    m_arrDoc = new object[1, 3];
                                    newRow = 0;
                                }
                                else
                                {
                                    int oldRows = m_arrDoc.GetLength(0);
                                    var grown = new object[oldRows + 1, 3];
                                    for (int r = 0; r < oldRows; r++)
                                    {
                                        grown[r, 0] = m_arrDoc[r, 0];
                                        grown[r, 1] = m_arrDoc[r, 1];
                                        grown[r, 2] = m_arrDoc[r, 2];
                                    }
                                    m_arrDoc = grown;
                                    newRow = oldRows;
                                }

                                m_arrDoc[newRow, 0] = arrDocTmp[i, 0];
                                m_arrDoc[newRow, 1] = arrDocTmp[i, 1];
                                m_arrDoc[newRow, 2] = arrDocTmp[i, 2];
                            }
                        }
                    }

                    bool isSetFileState = true;

                    if (m_isOk)
                    {
                        if (chkControlRun.Checked)
                        {
                            if (m_paymentData != null)
                            {
                                using (var sw = new StreamWriter(m_clientFileName, true, Encoding.GetEncoding(1251)))
                                {
                                    for (int i = 0; i < m_paymentData.GetLength(0); i++)
                                    {
                                        sw.WriteLine($"Со счета '{m_paymentData[i, 1]}', сальдо = {Math.Abs(Convert.ToDecimal(m_paymentData[i, 2])):N2}, списывается заработная плата общей суммой = {Math.Abs(Convert.ToDecimal(m_paymentData[i, 3])):N2} и суммой комиссии = {Math.Abs(Convert.ToDecimal(m_paymentData[i, 4])):N2}");

                                        var paramInLocal = new UbsParam();

                                        paramInLocal["DateTrn"] = dateLoad.DateValue;
                                        paramInLocal["Salary.Payment.IdAccDB"] = m_paymentData[i, 0];
                                        paramInLocal["Salary.Payment.AccDB"] = m_paymentData[i, 1];
                                        paramInLocal["Salary.Payment.SaldoAccDB"] = m_paymentData[i, 2];
                                        paramInLocal["Salary.Payment.curDB"] = m_paymentData[i, 3];
                                        paramInLocal["Salary.Fee.curDB"] = m_paymentData[i, 4];

                                        base.IUbsChannel.ParamsInParam = paramInLocal;

                                        base.IUbsChannel.Run("PCSalaryImport_CheckMoveFunds");

                                        if (base.IUbsChannel.ExistParamOut("bResult"))
                                        {
                                            if (!Convert.ToBoolean(base.IUbsChannel.ParamOut("bResult")))
                                            {
                                                sw.WriteLine($"Ошибка. {base.IUbsChannel.ParamOut("StrReport")}");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Реальная загрузка: если есть RC-сводный платёж, создаём
                            // сводное авизо. В legacy это ветка Else от chkControlRun.
                            if (m_paymentOrderId > 0 && m_paymentOrderType == "RC")
                            {
                                var paramInLocal = new UbsParam();

                                paramInLocal["Сводный платеж.Идентификатор"] = m_paymentOrderId;
                                paramInLocal["Сводный платеж.Массив документов"] = m_arrDoc;

                                base.IUbsChannel.ParamsInParam = paramInLocal;

                                base.IUbsChannel.Run("PCSalaryImport_CreateSvodAvizo");

                                // Аналогично: дозапись к уже накопленному протоколу,
                                // а не перезапись (VB6: UbsFile.WriteLine vbNewLine & strReport).
                                using (var sw = new StreamWriter(m_clientFileName, true, Encoding.GetEncoding(1251)))
                                {
                                    sw.WriteLine();
                                    sw.WriteLine(Convert.ToString(base.IUbsChannel.ParamOut("strReport")));
                                }

                                if (!Convert.ToBoolean(base.IUbsChannel.ParamOut("bResult")))
                                {
                                    paramInLocal["Идентификатор файла"] = m_idFile;
                                    paramInLocal["UndoOnImportError"] = true;

                                    base.IUbsChannel.ParamsInParam = paramInLocal;
                                    base.IUbsChannel.Run("PCSalaryImport_Undo");

                                    isSetFileState = false;

                                    MessageBox.Show("Ошибка создания сводного авизо по сводному платежу!" + Environment.NewLine + "Загрузка отменена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }

                        btnExit.Text = "Выход";
                        btnExit.Enabled = true;

                        btnSave.Text = "Загрузка!";
                        btnSave.Enabled = true;

                        for (int i = 0; i < m_arrChannelLabel.Length; i++)
                        {
                            m_arrChannelLabel[i].BackColor = btnSave.BackColor;
                        }

                        if (isSetFileState)
                        {
                            SetFileStatus(0);
                        }

                        if (File.Exists(m_clientFileName))
                        {
                            content += File.ReadAllText(m_clientFileName, Encoding.GetEncoding(1251));

                            try { File.Delete(m_clientFileName); } catch { }
                        }
                        m_clientFileName = string.Empty;

                        timer.Interval = 100;
                        timer.Start();

                        if (!string.IsNullOrEmpty(content))
                        {
                            this.Ubs_ShowMsg(content);
                        }
                    }
                };
                this.Invoke(d);

            }
            catch (Exception ex)
            {
                try
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            btnSave.Enabled = true;
                            btnExit.Text = "Выход";
                            btnExit.Enabled = true;

                            if (m_idFile > 0)
                            {
                                try { SetFileStatus(0); } catch { }
                            }
                        });
                    }
                }
                catch { }

                this.Ubs_ShowError(ex);
            }
        }


        private void SetFileStatus(int status)
        {
            var paramIn = new UbsParam();

            paramIn.Value("IdFile", m_idFile);
            paramIn.Value("State", status);

            base.IUbsChannel.ParamsInParam = paramIn;

            base.IUbsChannel.Run("PCPRCFile_SetStateParam");
        }

        // Снимок имён файлов вида tmp????.tmp в Path.GetTempPath() ДО вызова
        // legacy-скрипта Handle_FileIn. Нужен, чтобы понять, какие временные
        // файлы появились в результате работы UbsWait.UbsWaitBox (см. п.10).
        // UbsWait в legacy-vbs создаёт внутренний temp-файл для отображения
        // сообщения, выводит его модальным окном и НЕ удаляет после закрытия.
        // Вернуть пустой набор при любой ошибке (отсутствие папки, отказ в
        // доступе и т.п.) — это не критично, просто пропустим уборку.
        private HashSet<string> SnapshotTempTmpFiles()
        {
            var snapshot = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            try
            {
                var tempDir = Path.GetTempPath();
                if (!Directory.Exists(tempDir)) return snapshot;

                foreach (var path in Directory.GetFiles(tempDir, "tmp*.tmp"))
                {
                    snapshot.Add(path);
                }
            }
            catch
            {
                // Не критично — просто откажемся от пост-уборки в этот раз.
            }

            return snapshot;
        }

        // Дозачистка временных файлов, созданных UbsWait.UbsWaitBox во время
        // Handle_FileIn. Удаляем только то, чего не было ДО запуска скрипта,
        // и что не является нашим собственным m_clientFileName (он управляется
        // отдельно). Чтобы случайно не зацепить чужой tmp, появившийся в это
        // же окно времени, ограничиваем возраст файла парой минут — UbsWait
        // создаёт свой файл и тут же показывает его пользователю синхронно
        // (Wait.ViewFile блокирует), так что к моменту возврата управления
        // файл свежий, ему точно меньше нескольких минут.
        private void CleanupNewTempTmpFiles(HashSet<string> before, string except)
        {
            if (before == null) return;

            try
            {
                var tempDir = Path.GetTempPath();
                if (!Directory.Exists(tempDir)) return;

                var now = DateTime.Now;

                foreach (var path in Directory.GetFiles(tempDir, "tmp*.tmp"))
                {
                    if (before.Contains(path)) continue;

                    if (!string.IsNullOrEmpty(except)
                        && string.Equals(path, except, StringComparison.OrdinalIgnoreCase))
                        continue;

                    try
                    {
                        var info = new FileInfo(path);
                        if ((now - info.CreationTime).TotalMinutes > 5
                            && (now - info.LastWriteTime).TotalMinutes > 5)
                            continue;

                        File.Delete(path);
                    }
                    catch
                    {
                        // Файл может быть занят другим процессом — пропускаем.
                    }
                }
            }
            catch
            {
                // Чистка чисто косметическая, ошибки не пробрасываем.
            }
        }


        #region Обработчики событий кнопок (с примерами)

        private void btnExit_Click(object sender, EventArgs e) { this.Close(); }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }

                m_idFile = 0;
                m_arrDoc = null;

                if (dateLoad.DateValue == m_dateMinValue)
                {
                    MessageBox.Show("Не установлена дата загрузки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    dateLoad.Focus();

                    return;
                }

                if (txtFileLoad.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Не установлено имя файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtFileLoad.Focus();

                    return;
                }

                if (!File.Exists(txtFileLoad.Text.Trim()))
                {
                    MessageBox.Show("Неверное имя или путь к файлу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtFileLoad.Focus();

                    return;
                }

                m_paymentData = null;

                m_recordCounter = 0;
                m_recordUp = 0;

                // Сбрасываем остаточное состояние после предыдущего запуска,
                // иначе при повторной загрузке проверки "все каналы завершены"
                // и копирование файла-протокола могут отработать с устаревшими
                // данными (см. ошибку GetFile при повторной загрузке).
                m_isOk = false;

                if (!string.IsNullOrEmpty(m_clientFileName))
                {
                    try
                    {
                        if (File.Exists(m_clientFileName)) File.Delete(m_clientFileName);
                    }
                    catch { }
                    m_clientFileName = string.Empty;
                }

                for (int i = 0; i < m_arrChannelLabel.Length; i++)
                {
                    m_arrChannelLabel[i].Tag = string.Empty;
                    m_arrChannelLabel[i].BackColor = btnSave.BackColor;
                }

                IUbsChannel_Notice(0, new UbsChannelEventArgs("OK: 0"));

                var kvpSourceMethod = GetScriptByName("UBS_PLCARD_SALARY_FILE_LOAD");

                m_scriptRunner.LoadFiles(kvpSourceMethod.Key);

                var paramInScripter = new UbsParam();

                paramInScripter["DateIn"] = dateLoad.DateValue;
                paramInScripter["FileIn"] = txtFileLoad.Text;
                paramInScripter["SearchByAccount"] = cmbCardSearch.SelectedValue;
                paramInScripter["MultiThread"] = true;
                paramInScripter["lblProgress"] = lblProgress;
                paramInScripter["UbsUser"] = UbsLoader.User;

                if (m_paymentOrderId > 0)
                {
                    paramInScripter["Сводный платеж.Идентификатор"] = m_paymentOrderId;
                    paramInScripter["Сводный платеж.Бизнес"] = m_paymentOrderType;
                }

                m_scriptRunner.UbsScriptParam = paramInScripter;

                // Снимаем "до"-список tmp*.tmp в %TEMP%, чтобы после
                // Handle_FileIn зачистить временные файлы, оставленные
                // legacy-COM UbsWait.UbsWaitBox (он не удаляет свой
                // tmp????.tmp после закрытия окна — см. п.10).
                var tmpSnapshotBefore = SnapshotTempTmpFiles();

                try
                {
                    m_scriptRunner.Run("Handle_FileIn");
                }
                finally
                {
                    CleanupNewTempTmpFiles(tmpSnapshotBefore, m_clientFileName);
                }

                var paramOutScripter = m_scriptRunner.UbsScriptParam;

                m_idFile = paramOutScripter.Contains("IdFile") ? Convert.ToInt32(paramOutScripter["IdFile"]) : 0;

                bool blnResult = paramOutScripter.Contains("blnResult")
                    && Convert.ToBoolean(paramOutScripter["blnResult"]);

                if (!blnResult)
                {
                    if (m_idFile > 0)
                    {
                        try
                        {
                            var paramUndo = new UbsParam();
                            paramUndo["Идентификатор файла"] = m_idFile;
                            paramUndo["UndoOnImportError"] = true;

                            base.IUbsChannel.ParamsInParam = paramUndo;
                            base.IUbsChannel.Run("PCSalaryImport_Undo");
                        }
                        catch (Exception exUndo)
                        {
                            this.Ubs_ShowError(exUndo);
                        }
                    }

                    btnSave.Enabled = true;
                    btnExit.Text = "Выход";
                    btnExit.Enabled = true;

                    return;
                }

                if (m_idFile > 0)
                {
                    if (m_paymentOrderId > 0)
                    {
                        base.IUbsChannel.ParamIn("Идентификатор файла", m_idFile);
                        base.IUbsChannel.ParamIn("Сводный платеж.Идентификатор", m_paymentOrderId);
                        base.IUbsChannel.ParamIn("Сводный платеж.Бизнес", m_paymentOrderType);

                        base.IUbsChannel.Run("PCSalaryImport_RegisterFileSourceParam");
                    }
                }
                SetFileStatus(1);

                var paramIn = new UbsParam();
                var paramOut = new UbsParam();

                paramIn["IdFile"] = m_idFile;

                m_scriptRunner.Run("PCSalaryImport_GetDataByParam", paramIn, paramOut);

                var arrData = paramOut["arrData"] as object[,];
                if (arrData != null)
                {
                    btnSave.Enabled = false;
                    btnExit.Text = "Отмена";

                    m_recordCounter = arrData.GetLength(0) + 1;

                    IUbsChannel_Notice(0, new UbsChannelEventArgs("OK: 0"));

                    SetFileStatus(255);

                    var arrThread = SplitArray(m_countChanel, arrData);

                    for (int i = 0; i < arrThread.Length; i++)
                    {
                        if (arrThread[i] != null)
                        {
                            m_paramIn["arrData"] = Transpose(arrThread[i] as object[,]);
                            m_paramIn["intThread"] = i;
                            m_paramIn["datOper"] = this.dateLoad.DateValue;
                            m_paramIn["Ид.файла"] = m_idFile;
                            m_paramIn["blnControlRun"] = chkControlRun.Checked;

                            if (m_paymentOrderId > 0)
                            {
                                m_paramIn["Сводный платеж.Идентификатор"] = m_paymentOrderId;
                                m_paramIn["Сводный платеж.Бизнес"] = m_paymentOrderType;
                            }

                            m_paramIn["Вид дохода"] = string.Empty;

                            if (cmbProfitKind.SelectedIndex > -1)
                            {
                                var kvp = ((KeyValuePair<int, string>)cmbProfitKind.SelectedItem);

                                if (kvp.Key >= 0)
                                {
                                    m_paramIn["Вид дохода"] = m_arrProfitKind[kvp.Key, 1];
                                }
                            }

                            m_paramIn.Remove("ReportError");

                            m_arrChannelLabel[i].BackColor = Color.FromArgb(165, 80, 100);

                            m_arrChannelLabel[i].Tag = "Активный";

                            m_dicChannel[m_arrChannelLabel[i]].ParamsInParam = m_paramIn;
                            m_dicChannel[m_arrChannelLabel[i]].RunAsync("PCSalaryImport_ProcessData");
                        }
                    }
                }
                else
                {
                    btnSave.Enabled = true;

                    SetFileStatus(0);

                    MessageBox.Show("Данных для обработки нет!", "Довыполнение операций по файлу", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                if (m_idFile > 0)
                {
                    try { SetFileStatus(0); } catch { }
                }

                btnSave.Enabled = true;
                btnExit.Text = "Выход";
                btnExit.Enabled = true;

                this.Ubs_ShowError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(m_clientFileName))
                {
                    try
                    {
                        if (File.Exists(m_clientFileName)) File.Delete(m_clientFileName);
                    }
                    catch { }
                    m_clientFileName = string.Empty;
                }
            }
        }
        public static object[,] Transpose(object[,] source)
        {
            if (source == null)
                return null;

            int rows = source.GetLength(0);
            int cols = source.GetLength(1);

            object[,] result = new object[cols, rows];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    result[c, r] = source[r, c];
                }
            }

            return result;
        }
        public static object[] SplitArray(int nParts, object[,] arrSource)
        {
            if (arrSource == null)
                return new object[0];

            int nRows = arrSource.GetUpperBound(1) + 1;
            if (nRows <= 0)
                return new object[0];

            int nCols = arrSource.GetUpperBound(0) + 1;

            if (nParts > nRows) nParts = nRows;

            int nPartRows = nRows / nParts;
            int nOver = nRows - (nParts * nPartRows);

            object[] arrResult = new object[nParts];

            int srcRow = 0; // VB: n

            for (int i = 0; i < nParts; i++)
            {
                int curRows = nPartRows + (i < nOver ? 1 : 0);

                // ВАЖНО: part[col, row] как VB
                object[,] part = new object[nCols, curRows];

                for (int j = 0; j < curRows; j++)
                {
                    for (int k = 0; k < nCols; k++)
                    {
                        part[k, j] = arrSource[k, srcRow];
                    }
                    srcRow++;
                }

                arrResult[i] = part;
            }

            return arrResult;
        }

        private KeyValuePair<string, string> GetScriptByName(string nameAction)
        {
            base.IUbsChannel.ParamIn("SIDAction", nameAction);

            base.IUbsChannel.Run("GetCommonResourceParam");

            string resource = Convert.ToString(base.IUbsChannel.ParamOut("StrResource"));

            string function = Convert.ToString(base.IUbsChannel.ParamOut("StrParam")).Split('(')[0];

            return new KeyValuePair<string, string>(resource, function);
        }
        #endregion


        #region Обработчики команд IUbs интерфейса (с примерами)

        /// <summary>
        /// Процедура регистрации обработчиков команд интерфейса IUbs в базовом классе
        /// </summary>
        private void m_addCommand()
        {
            base.Ubs_AddName(new UbsDelegate(CommandLine));
            base.Ubs_AddName(new UbsDelegate(ListKey));
        }
        /// <summary>
        /// Процедура обработки команды CommandLine
        /// </summary>
        /// <param name="param_in">Входной параметер</param>
        /// <param name="param_out">Выходной параметер</param>
        /// <returns></returns>
        private object CommandLine(object param_in, ref object param_out)
        {
            m_command = (string)param_in;

            InitFile();

            txtFileLoad.Focus();

            return null;
        }

        private void InitFile()
        {
            base.IUbsChannel.LoadResource = @"VBS:UBS_VBS\PLCARD\OPENWAY\PCSalaryImport.vbs";

            m_scriptRunner = base.Ubs_VBScriptRunner();

            dateLoad.DateValue = GetCurrentDate();

            base.IUbsChannel.ParamIn("NameSection", "Пластиковые карты");
            base.IUbsChannel.ParamIn("NameSetting", "Прием и выгрузка файлов");
            base.IUbsChannel.ParamIn("TypeSetting", 1);
            base.IUbsChannel.ParamIn("StrTypeSetting", "SS");

            base.IUbsChannel.Run("GetUbsSetting");

            var arrPath = base.IUbsChannel.ParamOut("DataSetting") as object[,];

            base.IUbsChannel.Run("GetUserDivision");

            m_division = Convert.ToInt32(base.IUbsChannel.ParamOut("Номер отделения"));

            txtFileLoad.Text = GetSettingFilePath("Прием файла заработной платы", arrPath, m_division);

            var listCardSearch = new List<KeyValuePair<int, string>>();

            listCardSearch.Add(new KeyValuePair<int, string>(0, "по номеру карты"));
            listCardSearch.Add(new KeyValuePair<int, string>(1, "по номеру счета"));

            InitComboBox(cmbCardSearch, listCardSearch);

            base.IUbsChannel.ParamIn("NameSection", "Пластиковые карты");
            base.IUbsChannel.ParamIn("NameSetting", "Форматы файлов");
            base.IUbsChannel.ParamIn("TypeSetting", 0);
            base.IUbsChannel.ParamIn("StrTypeSetting", "SS");

            base.IUbsChannel.Run("GetUbsSetting");

            var arrFileFormats = base.IUbsChannel.ParamOut("DataSetting") as object[,];

            string fileFormat = string.Empty;

            if (arrFileFormats != null)
            {
                for (int i = 0; i < arrFileFormats.GetLength(0); i++)
                {
                    if (Convert.ToString(arrFileFormats[i, 0]) == "Файл с заработной платой")
                    {
                        fileFormat = Convert.ToString(arrFileFormats[i, 1]);

                        break;
                    }
                }
            }

            if (fileFormat.Length > 0 && fileFormat.ToUpperInvariant() == "ТАТФОНД")
                cmbCardSearch.SelectedIndex = 0;
            else
                cmbCardSearch.SelectedIndex = 1;

            base.IUbsChannel.ParamIn("NameSection", "Пластиковые карты");
            base.IUbsChannel.ParamIn("NameSetting", "Количество потоков");
            base.IUbsChannel.ParamIn("TypeSetting", 0);
            base.IUbsChannel.ParamIn("StrTypeSetting", "SI");

            base.IUbsChannel.Run("GetUbsSetting");

            var arrSetting = base.IUbsChannel.ParamOut("DataSetting") as object[,];

            m_countChanel = 0;

            if (arrSetting != null && arrSetting.Rank == 2)
            {
                for (int j = 0; j < arrSetting.GetLength(0); j++)
                {
                    string name = Convert.ToString(arrSetting[j, 0]);
                    if (name == "Обработка файла по зарплате/пополнению/списанию")
                    {
                        m_countChanel = Convert.ToInt32(arrSetting[j, 1]);
                    }
                }

                if (m_countChanel == 0)
                {
                    MessageBox.Show(
                        "Неправильно настроена установка 'Количество потоков' для операции 'Обработка файла по зарплате/пополнению/списанию'",
                        "Ошибка!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }
            }
            else
            {
                MessageBox.Show(
                    "Не найдена установка 'Количество потоков'",
                    "Ошибка!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (m_countChanel < 1) m_countChanel = 1;
            if (m_countChanel > 7) m_countChanel = 7;

            for (int j = 0; j <= 6; j++)
            {
                m_arrChannelLabel[j].Visible = (j <= m_countChanel - 1);
            }

            m_frmPayment = new PaymentOrderFrm();

            var lvw = m_frmPayment.LvwPayment;

            lvw.Items.Clear();

            base.IUbsChannel.Run("PCSalaryImport_GetTypePaymentOrder");

            m_arrTypePaymentOrder = base.IUbsChannel.ParamOut("Типы платежных поручений") as object[,];

            if (m_arrTypePaymentOrder != null)
            {
                for (int i = 0; i < m_arrTypePaymentOrder.GetLength(0); i++)
                {
                    string text = Convert.ToString(m_arrTypePaymentOrder[i, 2]);

                    var it = lvw.Items.Add(text);

                    it.Tag = m_arrTypePaymentOrder[i, 0];
                }

                if (lvw.Items.Count > 0)
                    lvw.Items[0].Selected = true;
            }


            base.IUbsChannel.ParamIn("NameSection", "Операционный день");
            base.IUbsChannel.ParamIn("NameSetting", "Кодовые назначения платежа");
            base.IUbsChannel.ParamIn("TypeSetting", 0);
            base.IUbsChannel.ParamIn("StrTypeSetting", "SSS");

            base.IUbsChannel.Run("GetUbsSetting");

            m_arrProfitKind = base.IUbsChannel.ParamOut("DataSetting") as object[,];

            cmbProfitKind.Items.Clear();
            //todo:consts
            var listProfitKind = new List<KeyValuePair<int, string>>();

            listProfitKind.Add(new KeyValuePair<int, string>(
                            -1, string.Empty));

            if (m_arrProfitKind != null)
            {
                for (int i = 0; i < m_arrProfitKind.GetLength(0); i++)
                {
                    string kind = Convert.ToString(m_arrProfitKind[i, 0]);
                    string path = Convert.ToString(m_arrProfitKind[i, 1]);
                    if (kind != null && kind.ToUpperInvariant() == "ВИД ДОХОДА")
                    {
                        listProfitKind.Add(new KeyValuePair<int, string>(
                            i
                          , Convert.ToString(m_arrProfitKind[i, 2])));

                        if (path == "1")
                        {
                            cmbProfitKind.SelectedIndex = cmbProfitKind.Items.Count - 1;
                        }
                    }
                }
            }

            InitComboBox(cmbProfitKind, listProfitKind);
        }
        private void InitComboBox(ComboBox cmb, List<KeyValuePair<int, string>> list)
        {
            cmb.DataSource = list;
            cmb.ValueMember = "Key";
            cmb.DisplayMember = "Value";
        }
        public static string GetSettingFilePath(string settingName, object[,] arrSetting, int division)
        {
            string strFilePath = string.Empty;

            if (settingName == null) settingName = string.Empty;
            if (settingName.Trim().Length == 0)
                throw new ApplicationException("GetSettingFilePath: Не задана установка.");

            for (int i = 0; i < arrSetting.GetLength(0); i++)
            {
                string name = Convert.ToString(arrSetting[i, 0]);
                if (name == settingName)
                {
                    string divList = Convert.ToString(arrSetting[i, 2]);
                    string path = Convert.ToString(arrSetting[i, 1]);

                    string[] parts = (divList ?? string.Empty).Split(',');

                    for (int j = 0; j < parts.Length; j++)
                    {
                        string tmp = (parts[j] ?? string.Empty).Trim();

                        if (tmp == "*")
                        {
                            strFilePath = path;
                        }
                        else if (tmp == division.ToString())
                        {
                            strFilePath = path;
                            break;
                        }
                    }
                }
            }

            return strFilePath;
        }

        private DateTime GetCurrentDate()
        {
            base.IUbsChannel.ParamIn("NameSetting", "Server");
            base.IUbsChannel.Run("GetCommonDate");

            return Convert.ToDateTime(base.IUbsChannel.ParamOut("DataSetting"));
        }

        /// <summary>
        /// Процедура обработки команды ListKey
        /// </summary>
        /// <param name="param_in">Входной параметер</param>
        /// <param name="param_out">Выходной параметер</param>
        /// <returns></returns>
        private object ListKey(object param_in, ref object param_out)
        {
            try
            {
                return null;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        #endregion

        private void linkFileLoad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.InitialDirectory = txtFileLoad.Text;
                dlg.CheckFileExists = true;
                dlg.CheckPathExists = true;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    txtFileLoad.Text = dlg.FileName;
                }
            }
        }

        private void linkPayment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_paymentOrderTypeTmp = string.Empty;

            var lvw = m_frmPayment.LvwPayment;
            if (lvw.Items.Count == 0)
            {
                if (m_arrTypePaymentOrder != null)
                {
                    for (int i = 0; i < m_arrTypePaymentOrder.GetLength(0); i++)
                    {
                        var it = lvw.Items.Add(m_arrTypePaymentOrder[i, 2].ToString());

                        it.Tag = m_arrTypePaymentOrder[i, 0];
                    }

                    lvw.Items[0].Selected = true;
                }
            }

            var dialogResult = m_frmPayment.ShowDialog();

            if (dialogResult == DialogResult.Cancel || lvw.SelectedItems.Count == 0)
            {
                return;
            }

            for (int i = 0; i < m_arrTypePaymentOrder.GetLength(0); i++)
            {
                if (m_arrTypePaymentOrder[i, 0] == lvw.SelectedItems[0].Tag)
                {
                    m_paymentOrderTypeTmp = Convert.ToString(m_arrTypePaymentOrder[i, 1]);
                }
            }

            var templ = string.Empty;

            if (m_paymentOrderTypeTmp == "RC")
            {
                templ = "UBS_RC_LIST_PAYM";
            }
            if (m_paymentOrderTypeTmp == "CLBANK")
            {
                templ = "UBS_CLBANK_LIST_DOCUMENT_RUB_FULL";
            }

            var ret = base.Ubs_ActionRun(templ, this, true) as object[];

            if (ret == null) return;

            var idPayment = Convert.ToInt32(ret[0]);
            var paramIn = new UbsParam();
            paramIn["Сводный платеж.Идентификатор"] = Convert.ToInt32(ret[0]);
            paramIn["Сводный платеж.Бизнес"] = m_paymentOrderTypeTmp;

            base.IUbsChannel.ParamsInParam = paramIn;

            base.IUbsChannel.Run("PCSalaryImport_ReadPaymOrder");

            if (base.IUbsChannel.ExistParamOut("bResult"))
            {
                if (Convert.ToBoolean(base.IUbsChannel.ParamOut("bResult")))
                {
                    m_paymentOrderId = idPayment;
                    m_paymentOrderType = m_paymentOrderTypeTmp;
                    txtPayment.Text = Convert.ToString(base.IUbsChannel.ParamOut("Сводный платеж.Описание"));
                }
            }
            else
            {
                MessageBox.Show(Convert.ToString(base.IUbsChannel.ParamOut("StrReport")), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UbsPlcardFileZPInFrm_Ubs_ActionRunBegin(object sender, UbsActionRunEventArgs args)
        {
            try
            {
                if (args.Action == "UBS_CLBANK_LIST_DOCUMENT_RUB_FULL")
                {
                    if (dateLoad.IsValidDate())
                    {
                        args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                                                             new KeyValuePair<string, object>("наименование", "Дата создания"),
                                                             new KeyValuePair<string, object>("значение по умолчанию", dateLoad.DateValue),
                                                             new KeyValuePair<string, object>("условие по умолчанию", "<="),
                                                             new KeyValuePair<string, object>("скрытый", true) }));
                    }

                    args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                                                             new KeyValuePair<string, object>("наименование", "Счет получателя"),
                                                             new KeyValuePair<string, object>("значение по умолчанию", new object[] { "00000000000000000000", ""}),
                                                             new KeyValuePair<string, object>("условие по умолчанию", "один из"),
                                                             new KeyValuePair<string, object>("скрытый", true) }));

                    args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                                                             new KeyValuePair<string, object>("наименование", "Ид. состояния обработки"),
                                                             new KeyValuePair<string, object>("значение по умолчанию", new[] {4, 7}),
                                                             new KeyValuePair<string, object>("условие по умолчанию", "один из"),
                                                             new KeyValuePair<string, object>("скрытый", true) }));
                }
                else if (args.Action == "UBS_RC_LIST_PAYM")
                {
                    if (dateLoad.IsValidDate())
                    {
                        args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                                                             new KeyValuePair<string, object>("наименование", "Дата проведения платежа"),
                                                             new KeyValuePair<string, object>("значение по умолчанию", dateLoad.DateValue),
                                                             new KeyValuePair<string, object>("условие по умолчанию", "<="),
                                                             new KeyValuePair<string, object>("скрытый", true) }));
                    }

                    args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                                                             new KeyValuePair<string, object>("наименование", "Л/с получателя"),
                                                             new KeyValuePair<string, object>("значение по умолчанию", new object[] { "00000000000000000000", ""}),
                                                             new KeyValuePair<string, object>("условие по умолчанию", "один из"),
                                                             new KeyValuePair<string, object>("скрытый", true) }));

                    args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                                                             new KeyValuePair<string, object>("наименование", "Список стадий обработки"),
                                                             new KeyValuePair<string, object>("значение по умолчанию", new [] { "UBS_UNKNOWN", "UBS_REC_ERROR" }),
                                                             new KeyValuePair<string, object>("условие по умолчанию", "один из"),
                                                             new KeyValuePair<string, object>("скрытый", true) }));
                }

                args.IUbs.Run("UbsItemsRefresh", null);
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();

            if (m_idFile > 0 && chkControlRun.Checked)
            {
                m_scriptRunner.Run("DeleteFileData", m_idFile);
            }
        }
    }
}