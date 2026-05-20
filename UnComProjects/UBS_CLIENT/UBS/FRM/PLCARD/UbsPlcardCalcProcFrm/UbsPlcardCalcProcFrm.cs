using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using UbsPlcardCalcProcFrm;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// Шаблон класса формы
    /// </summary>
    public partial class UbsPlcardCalcProcFrm : UbsFormBase
    {
        private const string CheckCaption = "Проверка";
        private const string DateTrnOverDateOperErrorMessage = "Дата операции {0} превышает дату операционного дня PLCARD {1}.";
        private const string DateTrnOverDateOverdraftErrorMessage = "В режиме накопления процентов дата проводки должна быть меньше даты окончания интервала расчета";

        #region Блок объявления переменных

        private string m_command = string.Empty;
        private UbsChannel m_ubsChannelAsync;
        private string m_collectedPercentsOverString;
        private FormData m_formData;
        private Label[] m_arrChannelLabel;
        private UbsParam m_paramIn;
        private Dictionary<Label, UbsChannel> m_dicChannel;
        private DateTime m_dateToday = DateTime.MinValue;
        private bool m_isNeedStopAsync;
        private DateTime m_dateStart;
        private int m_procTime;
        private int m_count;
        private int m_cardInSec;
        private string m_strParam;
        private object[,] m_RS;
        private object[] m_itemArray;
        private int m_countRec;
        private UbsChannel m_additionalChannel;
        private UbsParam m_additionalParamIn;
        private string m_localFileName = string.Empty;

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsPlcardCalcProcFrm()
        {
            m_addCommand();

            InitializeComponent();

            m_formData = new FormData();

            m_arrChannelLabel = new Label[] {
                                                 lblChannelInfo0
                                               , lblChannelInfo1
                                               , lblChannelInfo2
                                               , lblChannelInfo3
                                               , lblChannelInfo4
                                               , lblChannelInfo5
                                               , lblChannelInfo6
                                                };

            m_paramIn = new UbsParam();

            m_dicChannel = new Dictionary<Label, UbsChannel>();

            m_dicChannel.Add(m_arrChannelLabel[0], new UbsChannel());
            m_dicChannel.Add(m_arrChannelLabel[1], new UbsChannel());
            m_dicChannel.Add(m_arrChannelLabel[2], new UbsChannel());
            m_dicChannel.Add(m_arrChannelLabel[3], new UbsChannel());
            m_dicChannel.Add(m_arrChannelLabel[4], new UbsChannel());
            m_dicChannel.Add(m_arrChannelLabel[5], new UbsChannel());
            m_dicChannel.Add(m_arrChannelLabel[6], new UbsChannel());

            m_additionalChannel = new UbsChannel();
            m_additionalParamIn = new UbsParam();

            m_additionalChannel.LoadResource = @"VBS:UBS_VBS\PLCARD\PCCalcOverSelect.vbs";

            foreach (var chnl in m_dicChannel)
            {
                chnl.Value.Respond += IUbsChannel_Respond;
                chnl.Value.Error += IUbsChannel_Error;
                chnl.Value.Notice += IUbsChannel_Notice;
            }

            base.Ubs_CommandLock = true;
        }

        private void IUbsChannel_Notice(object sender, UbsChannelEventArgs args)
        {
            try
            {
                MethodInvoker d = delegate ()
                {
                    var channel = sender as UbsChannel;

                    var label = FindLabel(m_dicChannel, channel);

                    var numProcessor = Convert.ToInt32(label.Text);

                    var message = args.Message.ToString();

                    int value;
                    if (!int.TryParse(message, out value))
                        value = 0;

                    m_formData.ArrNumProcessor[numProcessor] = value;

                    m_formData.CountComplete = 0;

                    for (int i = 0; i < m_formData.ArrNumProcessor.Length; i++)
                    {
                        m_formData.CountComplete += m_formData.ArrNumProcessor[i];
                    }

                    label4.Text = $"Обработано {m_formData.CountComplete} из {m_formData.CountAll}";

                    timerChannel.Start();
                };
                if (this.IsDisposed) return;
                if (!this.IsHandleCreated) return;

                if (this.InvokeRequired)
                    this.BeginInvoke(d);
                else
                    d();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private readonly bool[] m_isCompletingError = new bool[7];
        private void IUbsChannel_Error(object sender, UbsChannelEventArgs args)
        {
            try
            {
                MethodInvoker d = delegate ()
                {
                    var channel = sender as UbsChannel;

                    var errorDescription = channel.ErrorDescription;

                    if (errorDescription.IndexOf("compilation", StringComparison.Ordinal) < 0)
                    {
                        var label = FindLabel(m_dicChannel, channel);

                        int channelIndex = Convert.ToInt32(label.Text);

                        label.BackColor = Color.FromArgb(255, 0, 0);

                        if (m_isCompletingError[channelIndex])
                        {
                            label.Tag = string.Empty;
                            m_isCompletingError[channelIndex] = false;

                            /*base.Ubs_ShowErrorBox(
                                "Повторная ошибка в канале " + channelIndex +
                                " при выполнении CompleteIfError:" + Environment.NewLine +
                                errorDescription);*/

                            CheckAllChannelsCompleted(channel);

                            return;
                        }

                        m_isCompletingError[channelIndex] = true;

                        var paramError = new UbsParam();
                        paramError["Ошибка"] = errorDescription;
                        paramError["Номер канала"] = channelIndex;

                        if (m_command == ModeText.AllCardOvd || m_command == ModeText.PaymOneOvd)
                        {
                            channel.RunAsync("PCProcDebtsPercAsync_CompleteIfError", paramError);
                        }
                        else
                        {
                            channel.RunAsync("CalcPrcSKSnSGPA7_CompleteIfError", paramError);
                        }
                    }
                    else
                    {
                        if (m_formData.IsInProcessing)
                        {
                            timerChannel.Enabled = false;
                            m_formData.IsInProcessing = false;
                            btnSave.Enabled = true;

                            foreach (var kvp in m_dicChannel)
                            {
                                kvp.Key.Tag = string.Empty;
                            }

                            base.Ubs_ShowErrorBox(errorDescription);
                        }
                    }
                };
                if (this.IsDisposed) return;
                if (!this.IsHandleCreated) return;

                if (this.InvokeRequired)
                    this.BeginInvoke(d);
                else
                    d();

            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }
        private void CheckAllChannelsCompleted(UbsChannel channel)
        {
            bool isOk = true;

            foreach (var kvp in m_dicChannel)
            {
                if (Convert.ToString(kvp.Key.Tag) != string.Empty)
                {
                    isOk = false;
                    break;
                }
            }

            if (isOk)
            {
                FinishAllChannelsAndShowProtocol(channel);
            }
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
                    bool isOk;
                    string strNameReport = string.Empty;
                    string strAction = string.Empty;

                    var channel = sender as UbsChannel;

                    var label = FindLabel(m_dicChannel, channel);
                    var index = Convert.ToInt32(label.Text);

                    m_isCompletingError[index] = false;

                    if (channel.ExistParamOut("Имя серверного файла"))
                    {
                        m_formData.ArrStrServerFile[index] = Convert.ToString(channel.ParamOut("Имя серверного файла"));
                    }

                    label.Tag = string.Empty;
                    isOk = true;

                    if (label.BackColor.ToArgb() != Color.FromArgb(255, 0, 0).ToArgb())
                    {
                        label.BackColor = Color.FromArgb(22, 214, 89);
                    }

                    foreach (var kvp in m_dicChannel)
                    {
                        if (Convert.ToString(kvp.Key.Tag) != string.Empty)
                            isOk = false;
                    }

                    if (isOk)
                    {
                        FinishAllChannelsAndShowProtocol(channel);
                    }

                };

                if (this.IsDisposed) return;
                if (!this.IsHandleCreated) return;

                if (this.InvokeRequired)
                    this.BeginInvoke(d);
                else
                    d();

            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }
        private void FinishAllChannelsAndShowProtocol(UbsChannel channel)
        {
            string content = string.Empty;
            string strNameReport = string.Empty;
            string strAction = string.Empty;

            if (m_command == ModeText.AllCardNotInTime)
            {
                strAction = ActionText.UbsPlcardPrecentAllCardNotInTime;
                strNameReport = ActionText.CalcultionPercentOutsideGraph;
            }
            else if (m_command == ModeText.AllCardOvd)
            {
                strAction = ActionText.UbsPlcardPrecentAllCardOvd;
                strNameReport = ActionText.CalcultionPercentDebt;
            }

            if (strAction.Length > 0)
                channel.LoadResource = @"VBS:UBS_VBS\PLCARD\Lib\PCLibGlobalProtocol.vbs";

            for (int i = 0; i <= m_formData.ArrStrServerFile.GetUpperBound(0); i++)
            {
                if (m_formData.ArrStrServerFile[i].Length == 0)
                    continue;

                if (strAction.Length > 0)
                {
                    string serverPath = string.Empty;

                    if (channel.ExistParamOut("Путь к серверному файлу"))
                    {
                        serverPath = Convert.ToString(channel.ParamOut("Путь к серверному файлу"));
                    }

                    var paramProtocol = new UbsParam();
                    paramProtocol["Имя серверного файла"] = serverPath + m_formData.ArrStrServerFile[i];
                    paramProtocol["Действие"] = strAction;
                    paramProtocol["Имя отчета"] = strNameReport;

                    channel.ParamsInParam = paramProtocol;
                    channel.Run("PC_WriteGlobalProtocol");
                }

                if (m_localFileName.Length == 0)
                {
                    m_localFileName = Path.GetTempFileName();
                    File.WriteAllText(m_localFileName, string.Empty);
                }

                var localFileTmp = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

                base.IUbsChannel.GetFile(localFileTmp, "Temp", m_formData.ArrStrServerFile[i]);

                var paramIn = new UbsParam();
                paramIn["VirtualFolder"] = "Temp";
                paramIn["ServerFile"] = m_formData.ArrStrServerFile[i];

                base.IUbsChannel.ParamsInParam = paramIn;
                base.IUbsChannel.Run("DeleteServerFileExStub");

                var tmpContent = File.ReadAllText(localFileTmp, Encoding.GetEncoding(1251));
                File.AppendAllText(m_localFileName, tmpContent + Environment.NewLine, Encoding.GetEncoding(1251));

                File.Delete(localFileTmp);

                content = File.ReadAllText(m_localFileName, Encoding.GetEncoding(1251));
            }

            timerChannel.Stop();

            if (!string.IsNullOrEmpty(content))
            {
                this.Ubs_ShowMsg(content);
            }

            m_formData.IsInProcessing = false;
            btnSave.Enabled = true;
        }
        #region Обработчики событий кнопок (с примерами)

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (m_formData.IsInProcessing)
            {
                foreach (var item in m_dicChannel)
                {
                    item.Value.StopRun();
                }

                var t = new Timer { Interval = 10 };
                t.Tick += (s, _) =>
                {
                    if (!m_formData.IsInProcessing)
                    {
                        t.Stop();
                        t.Dispose();
                    }
                };
                t.Start();
                return;
            }

            if (m_isNeedStopAsync)
            {
                m_ubsChannelAsync.StopRun();

                var t = new Timer { Interval = 10 };
                t.Tick += (s, _) =>
                {
                    if (!m_isNeedStopAsync)
                    {
                        t.Stop();
                        t.Dispose();
                    }
                };
                t.Start();
                return;
            }

            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }

                bool isAsync = false;
                string report = string.Empty;

                if (m_formData.IsInProcessing)
                    return;

                if (m_isNeedStopAsync)
                    return;

                ResetExecuteTime();

                var dateOdForPlcard = "Операционный день PLCARD";
                var dateKinds = new object[] { dateOdForPlcard };

                base.IUbsChannel.ParamIn("Виды дат", dateKinds);

                base.IUbsChannel.Run("COM_GetCommonDates");

                DateTime dateOperDay = Convert.ToDateTime(base.IUbsChannel.ParamOut("Операционный день PLCARD"));

                if (dateTrn.DateValue > dateOperDay)
                {
                    MessageBox.Show(
                        string.Format(DateTrnOverDateOperErrorMessage, dateTrn.DateValue, dateOperDay),
                        CheckCaption,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                btnSave.Enabled = false;

                DateTime dateStart = m_dateToday;
                bool isRunAsync = false;
                m_cardInSec = 4;

                label4.Text = string.Empty;
                lblTime.Text = string.Empty;
                label5.Text = string.Empty;

                var scripter = base.Ubs_VBScriptRunner();

                if (m_command == ModeText.AllCard || m_command == ModeText.AllCardNotInTime)
                {
                    m_strParam = GetMode();

                    m_paramIn["StrParam"] = m_strParam;
                    m_paramIn["Mode"] = m_command;

                    m_additionalParamIn["Запуск по графику"] = (m_command == ModeText.AllCard);

                    var paramInFunc = new UbsParam();

                    paramInFunc["DateFilter"] = dateOverDraft.DateValue;
                    paramInFunc["StrParam"] = m_strParam;

                    CopyParamsTo(paramInFunc, m_additionalParamIn);

                    m_additionalChannel.ParamsInParam = paramInFunc;

                    m_additionalChannel.Run("GetListAccountsAllCardByParam");

                    m_RS = m_additionalChannel.ParamOut("RS") as object[,];

                    if (m_strParam == ModeText.ACCUM)
                    {
                        if (dateTrn.DateValue >= dateOverDraft.DateValue)
                        {
                            MessageBox.Show(DateTrnOverDateOverdraftErrorMessage,
                                CheckCaption,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                            return;
                        }

                        m_paramIn["OperationKind"] = "Накопление";
                        m_paramIn["IsPlanned"] = false;
                    }
                    else if (m_strParam == ModeText.PAYM)
                    {
                        m_paramIn["OperationKind"] = "Уплата";
                        m_paramIn["IsPlanned"] = (m_command == ModeText.AllCard);
                    }

                    m_paramIn["IsAddOverdraft"] = false;
                    m_paramIn["DateTrn"] = dateTrn.DateValue;
                    m_paramIn["DateFilter"] = dateOverDraft.DateValue;

                    SetModeCalc();

                    isAsync = true;

                    ProcessingAllCard(m_RS);

                    return;
                }
                if (m_command == ModeText.AllCardOvd || m_command == ModeText.PaymOneOvd)
                {
                    m_strParam = GetMode();
                    m_paramIn["StrParam"] = m_strParam;
                    m_paramIn["Mode"] = m_command;

                    SetModeCalc();

                    if (m_command == ModeText.AllCardOvd)
                    {
                        var paramInFunc = new UbsParam();

                        paramInFunc["DateFilter"] = dateOverDraft.DateValue;
                        paramInFunc["StrParam"] = m_strParam;

                        CopyParamsTo(paramInFunc, m_additionalParamIn);

                        m_additionalChannel.ParamsInParam = paramInFunc;

                        m_additionalChannel.Run("GetListAccountsOvdByParam");

                        m_RS = m_additionalChannel.ParamOut("RS") as object[,];
                    }
                    else
                    {
                        if (m_itemArray is object[] arr)
                        {
                            m_RS = new object[arr.Length, 1];
                            for (int a = 0; a < arr.Length; a++)
                                ((object[,])m_RS)[a, 0] = Convert.ToInt32(arr[a]);
                        }
                    }

                    if (m_strParam == ModeText.ACCUM)
                    {
                        if (dateTrn.DateValue >= dateOverDraft.DateValue)
                        {
                            MessageBox.Show(DateTrnOverDateOverdraftErrorMessage,
                                CheckCaption,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                            return;
                        }

                        m_paramIn["OperationKind"] = "Накопление";
                    }
                    else
                    {
                        m_paramIn["OperationKind"] = "Уплата";
                    }

                    m_paramIn["DateTrn"] = dateTrn.DateValue;
                    m_paramIn["DateFilter"] = dateOverDraft.DateValue;

                    m_additionalParamIn["IsLoan"] = false;

                    isAsync = true;
                    ProcPercDebtsAsync(m_RS);

                    return;
                }
                if (m_command == ModeText.AllCard || m_command == ModeText.AllCardLoan)
                {
                    m_countRec = -1;

                    m_strParam = GetMode();

                    m_paramIn["StrParam"] = m_strParam;
                    m_paramIn["Mode"] = m_command;
                    m_paramIn["FirstExecute"] = true;

                    m_additionalParamIn["IgnoreGracePeriod"] = true;

                    if (m_command == ModeText.AllCard)
                    {
                        isRunAsync = true;
                        timer.Interval = 1000;
                        timer.Start();

                        m_additionalParamIn["IgnoreGracePeriod"] = true;

                        var paramInFunc = new UbsParam();

                        paramInFunc["DateFilter"] = dateOverDraft.DateValue;
                        paramInFunc["StrParam"] = m_strParam;

                        CopyParamsTo(paramInFunc, m_additionalParamIn);

                        m_additionalChannel.ParamsInParam = paramInFunc;

                        m_additionalChannel.Run("GetListAccountsAllCard");

                        m_RS = m_additionalChannel.ParamOut("RS") as object[,];
                    }
                    else if (m_command == ModeText.AllCardLoan)
                    {
                        m_additionalParamIn["IgnoreGracePeriod"] = true;

                        var paramInFunc = new UbsParam();

                        paramInFunc["DateFilter"] = dateOverDraft.DateValue;
                        paramInFunc["StrParam"] = m_strParam;

                        CopyParamsTo(paramInFunc, m_additionalParamIn);

                        m_additionalChannel.ParamsInParam = paramInFunc;

                        m_additionalChannel.Run("GetListAccountsLoanByParam");

                        m_RS = m_additionalChannel.ParamOut("RS") as object[,];
                    }
                    if (m_command == ModeText.AllCard)
                    {
                        var kvpSourceMethod = GetScriptByName("UBS_PLCARD_PERCENT_ONE_CARD");

                        scripter.LoadFiles(kvpSourceMethod.Key);

                        if (m_command == ModeText.ACCUM)
                        {
                            m_additionalParamIn["OperationKind"] = "Накопление";
                            m_paramIn["OperationKind"] = "Накопление";

                            if (dateTrn.DateValue >= dateOverDraft.DateValue)
                            {
                                MessageBox.Show(DateTrnOverDateOverdraftErrorMessage, CheckCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return;
                            }
                        }
                        else if (m_command == ModeText.PAYM)
                        {
                            m_additionalParamIn["OperationKind"] = "Уплата";
                            m_paramIn["OperationKind"] = "Уплата";

                            m_paramIn["IsPlanned"] = true;
                            m_additionalParamIn["IgnoreGracePeriod"] = true;
                            m_additionalParamIn["IsPlanned"] = true;
                        }

                        m_additionalParamIn["IsAddOverdraft"] = false;
                        m_paramIn["IsAddOverdraft"] = false;
                    }
                    else if (m_command == ModeText.AllCardLoan)
                    {
                        var kvpSourceMethod = GetScriptByName("UBS_PLCARD_PERCENT_ONE_CARD_OVD");

                        scripter.LoadFiles(kvpSourceMethod.Key);

                        if (m_command == ModeText.ACCUM)
                        {
                            m_additionalParamIn["OperationKind"] = "Накопление";

                            if (dateTrn.DateValue >= dateOverDraft.DateValue)
                            {
                                MessageBox.Show(DateTrnOverDateOverdraftErrorMessage, CheckCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return;
                            }
                        }
                        else if (m_command == ModeText.PAYM)
                        {
                            m_additionalParamIn["OperationKind"] = "Уплата";
                        }

                        m_additionalParamIn["IsLoan"] = true;
                    }

                    m_additionalParamIn["DateFilter"] = dateOverDraft.DateValue;
                    m_additionalParamIn["DateTrn"] = dateTrn.DateValue;

                    m_paramIn["DateTrn"] = dateTrn.DateValue;
                    m_paramIn["DateFilter"] = dateOverDraft.DateValue;

                    m_additionalParamIn["IgnoreGracePeriod"] = true;

                    SetModeCalc();

                    bool rsHasData = m_RS != null;
                    if (rsHasData && !isRunAsync)
                    {
                        m_countRec = m_RS.GetUpperBound(0);

                        double loopTime;
                        if (m_command == ModeText.AllCardOvd)
                        {
                            if (!isAsync)
                            {
                                btnSave.Enabled = true;
                            }

                            return;
                        }
                        else
                        {
                            for (int a = 0; a <= m_countRec; a++)
                            {
                                var sw = System.Diagnostics.Stopwatch.StartNew();

                                label4.Text = "Обработано " +
                                              Math.Round((double)(a + 1) / (m_countRec + 1) * 100, 0) + "% записей";
                                Application.DoEvents();

                                m_additionalParamIn["IdAccount"] = m_RS[a, 0];
                                m_additionalParamIn["IgnoreGracePeriod"] = true;

                                var scripterParamIn = new UbsParam();

                                CopyParamsTo(scripterParamIn, m_additionalParamIn);

                                scripter.UbsScriptParam = scripterParamIn;

                                if (m_command == ModeText.AllCard)
                                    scripter.Run("CalculatePercentsMnt");
                                else
                                    scripter.Run("Charge_procent_overdraft");

                                sw.Stop();
                                loopTime = sw.Elapsed.TotalSeconds;

                                m_paramIn["Loop"] = (double)m_paramIn["Loop"] + loopTime;
                                m_paramIn["Loop Avg "] =
                                    (double)m_paramIn["Loop"] / (a + 1);

                                m_additionalParamIn = scripter.UbsScriptParam;

                                report = m_additionalParamIn.Contains("Report") ? (string)m_additionalParamIn["Report"] : string.Empty;

                                if (!string.IsNullOrEmpty(report))
                                {
                                    base.Ubs_ShowMsg(report);
                                    m_additionalParamIn.Remove("Report");
                                }

                                m_additionalParamIn.Remove("IdAccount");

                                object[,] arrTrace = m_additionalParamIn.Items;

                                for (int i = 0; i <= arrTrace.GetUpperBound(0); i++)
                                {
                                    string name = Convert.ToString(arrTrace[i, 0]);

                                    if (name.IndexOf("Debug_", StringComparison.Ordinal) >= 0)
                                    {
                                        string callsKey = name + " Calls = ";
                                        string avgKey = name + " Avg ";

                                        m_paramIn[callsKey] =
                                            (double)m_paramIn[callsKey] + 1;

                                        m_paramIn[name] =
                                            (double)m_paramIn[name] +
                                            Convert.ToDouble(m_additionalParamIn[name]);

                                        m_paramIn[avgKey] =
                                            (double)m_paramIn[name] /
                                            (double)m_paramIn[callsKey];

                                        m_additionalParamIn.Remove(name);
                                    }
                                }
                            }
                        }

                        base.Ubs_ShowMsg("Операция завершена, обработано " + (m_countRec + 1) + " договор(а/ов)");
                    }
                    else
                    {
                        if (m_RS != null)
                        {
                            isAsync = true;
                            ProcessingAllCard(m_RS);
                            label5.Text = "Времени осталось: -- мин -- сек";
                        }
                        else
                        {
                            base.Ubs_ShowMsg("Операция завершена, обработано 0 договоров");
                        }
                    }
                }
                if (m_command == ModeText.PaymOneOvd || m_command == ModeText.OneCardLoan)
                {
                    m_countRec = -1;

                    m_strParam = GetMode();
                    m_paramIn["StrParam"] = m_strParam;

                    var kvpSourceMethod = GetScriptByName("UBS_PLCARD_PERCENT_ONE_CARD_OVD");

                    scripter.LoadFiles(kvpSourceMethod.Key);

                    m_additionalParamIn.Clear();

                    if (m_strParam == "ACCUM")
                    {
                        m_additionalParamIn["OperationKind"] = "Накопление";

                        if (dateTrn.DateValue >= dateOverDraft.DateValue)
                        {
                            MessageBox.Show(DateTrnOverDateOverdraftErrorMessage,
                                CheckCaption,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                            return;
                        }
                    }
                    else if (m_strParam == ModeText.PaymOneOvd)
                    {
                        m_additionalParamIn["OperationKind"] = "Уплата";
                    }

                    if (m_command == "PAYM_ONE_OVD")
                        m_additionalParamIn["IsLoan"] = false;
                    else
                        m_additionalParamIn["IsLoan"] = true;

                    m_additionalParamIn["DateFilter"] = dateOverDraft.DateValue;
                    m_additionalParamIn["DateTrn"] = dateTrn.DateValue;

                    SetModeCalc();

                    object[] rs1d = m_itemArray;
                    if (rs1d != null && rs1d.Length > 0)
                    {
                        m_countRec = rs1d.GetUpperBound(0);

                        for (int a = 0; a <= m_countRec; a++)
                        {
                            label4.Text = "Обработано " +
                                          Math.Round(((double)(a + 1) / (m_countRec + 1)) * 100.0, 0) + "% записей";
                            Application.DoEvents();

                            m_additionalParamIn["IdAccount"] = rs1d[a];
                            m_additionalParamIn["IgnoreGracePeriod"] = true;
                            m_additionalParamIn["Старый протокол расчета %"] = true;

                            scripter.UbsScriptParam = m_additionalParamIn;

                            scripter.Run("Charge_procent_overdraft");

                            m_additionalParamIn = scripter.UbsScriptParam;

                            if (m_additionalParamIn.Contains("Report"))
                            {
                                report = Convert.ToString(m_additionalParamIn["Report"]);

                                m_additionalParamIn.Remove("Report");
                            }
                        }

                        report = "Операция завершена, обработано " + (m_countRec + 1) + " договор(а/ов)";

                        base.Ubs_ShowMsg(report);
                    }
                    else
                    {
                        report = "Операция завершена, обработано  0 договоров";

                        base.Ubs_ShowMsg(report);
                    }

                    dateOverDraft.Focus();

                    return;
                }
                if (m_command == ModeText.ACCUM || m_command == ModeText.PaymInTime || m_command == ModeText.PaymNotInTime)
                {
                    m_countRec = -1;

                    m_strParam = GetMode();

                    if (m_strParam == ModeText.ACCUM)
                    {
                        m_paramIn["OperationKind"] = "Накопление";

                        if (dateTrn.DateValue >= dateOverDraft.DateValue)
                        {
                            MessageBox.Show(DateTrnOverDateOverdraftErrorMessage,
                                CheckCaption,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                            return;
                        }
                    }
                    else if (m_strParam == "PAYM")
                    {
                        m_paramIn["OperationKind"] = "Уплата";
                    }

                    m_paramIn["IsAddOverdraft"] = false;

                    if (m_command == ModeText.PaymNotInTime)
                        m_paramIn["IsPlanned"] = false;
                    else if (m_command == ModeText.PaymInTime)
                        m_paramIn["IsPlanned"] = true;

                    m_paramIn["DateTrn"] = dateTrn.DateValue;
                    m_paramIn["DateFilter"] = dateOverDraft.DateValue;
                    m_paramIn["StrParam"] = m_strParam;
                    m_paramIn["Mode"] = m_command;        // режим работы
                    m_paramIn["FirstExecute"] = true;

                    SetModeCalc();

                    if (m_itemArray != null)
                    {
                        label4.Text = "Подготовка данных...";

                        int ub = m_itemArray.GetUpperBound(0);
                        m_RS = new object[1, ub + 1];

                        for (int a = 0; a <= ub; a++)
                        {
                            m_RS[0, a] = m_itemArray.GetValue(a);
                        }

                        m_paramIn["RS"] = m_RS;

                        label4.Text = "";

                        isRunAsync = true;
                        timer.Interval = 1000;
                        timer.Enabled = true;
                    }

                    if (m_RS != null) // VB: Not IsEmpty(RS)
                    {
                        isRunAsync = true;
                        RunAsyncProc();
                        label5.Text = "Времени осталось: -- мин -- сек";
                    }
                    else
                    {
                        base.Ubs_ShowMsg("Операция завершена, обработано 0 договоров");
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }
        private void ResetExecuteTime()
        {
            timer.Stop();
            timerChannel.Stop();

            m_dateStart = DateTime.Now;
            m_localFileName = string.Empty;

            m_procTime = 0;
            m_count = 0;
            m_cardInSec = 4;

            for (int i = 0; i < m_isCompletingError.Length; i++)
            {
                m_isCompletingError[i] = false;
            }

            lblTime.Text = "Времени прошло:   0 сек";
            label5.Text = "Времени осталось: -- мин -- сек";
            label4.Text = string.Empty;
        }
        private void RunAsyncProc()
        {
            m_isNeedStopAsync = true;

            if (m_ubsChannelAsync.ErrorDescription.Length > 0)
            {
                m_paramIn["StrError"] = m_ubsChannelAsync.ErrorDescription;
                m_paramIn["FirstExecute"] = false;
            }

            m_ubsChannelAsync.RunAsync("CalcPrcSKSnSGPAsync", m_paramIn);
        }

        private void CopyParamsTo(UbsParam paramInFunc, UbsParam additionalParamIn)
        {
            foreach (var ap in additionalParamIn)
            {
                paramInFunc.Add(ap.Key, ap.Value);
            }
        }

        private KeyValuePair<string, string> GetScriptByName(string nameAction)
        {
            base.IUbsChannel.ParamIn("SIDAction", nameAction);

            base.IUbsChannel.Run("GetCommonResourceParam");

            string resource = Convert.ToString(base.IUbsChannel.ParamOut("StrResource"));

            string function = Convert.ToString(base.IUbsChannel.ParamOut("StrParam")).Split('(')[0];

            return new KeyValuePair<string, string>(resource, function);
        }
        private void ProcPercDebtsAsync(object[,] m_RS)
        {
            object[] arrRV;

            m_formData.IsInProcessing = true;

            for (int i = 0; i <= m_formData.ArrNumProcessor.GetUpperBound(0); i++)
            {
                m_formData.ArrNumProcessor[i] = 0;
                m_formData.ArrStrServerFile[i] = string.Empty;

                m_arrChannelLabel[i].Text = i.ToString();
                m_arrChannelLabel[i].ForeColor = Color.FromArgb(255, 255, 255);
                m_arrChannelLabel[i].Visible = false;
            }

            lblChannel.Visible = true;

            if (m_RS != null)
            {
                m_formData.CountAll = m_RS.GetUpperBound(0) + 1;

                arrRV = SplitArray(m_formData.CountProcessor, m_RS);

                m_formData.DateStart = m_dateStart;
                timerChannel.Start();

                for (int i = 0; i <= arrRV.GetUpperBound(0); i++)
                {
                    if (arrRV[i] is object[,])
                    {
                        var chunk = (object[,])arrRV[i];

                        var kvp = FindChannelKVP(m_dicChannel, i);

                        kvp.Value.LoadResource = @"VBS:UBS_VBS\PLCARD\PCProcDebtsPercAsync.vbs";

                        var paramForChannel = CloneParam(m_paramIn);

                        paramForChannel["RS"] = chunk;
                        paramForChannel["Номер канала"] = i;

                        kvp.Key.BackColor = Color.FromArgb(22, 181, 214);
                        kvp.Key.Visible = true;

                        kvp.Value.ServerNotice = true;
                        kvp.Key.Tag = "Активный";

                        kvp.Value.ParamsInParam = paramForChannel;

                        kvp.Value.RunAsync("PCProcDebtsPercAsync");
                    }
                }
            }
            else
            {
                timerChannel.Stop();
                m_formData.IsInProcessing = false;
                btnSave.Enabled = true;

                base.Ubs_ShowMsg("Операция завершена, обработано 0 договоров");
            }
        }

        private KeyValuePair<Label, UbsChannel> FindChannelKVP(Dictionary<Label, UbsChannel> dicChannel, int i)
        {
            var indexChannel = 0;

            foreach (var kvp in dicChannel)
            {
                if (i == indexChannel)
                {
                    return kvp;
                }

                indexChannel++;
            }

            throw new Exception("Канал не найден");
        }

        public static object[] SplitArray(int nParts, object[,] arrSource)
        {
            if (arrSource == null)
                return new object[0];

            int nRows = arrSource.GetUpperBound(0) + 1;
            if (nRows <= 0)
                return new object[0];

            int nCols = arrSource.GetUpperBound(1) + 1;

            if (nParts > nRows)
                nParts = nRows;

            int nPartRows = nRows / nParts;
            int nOver = nRows - (nParts * nPartRows);

            object[] arrResult = new object[nParts];

            int srcRow = 0;

            for (int i = 0; i < nParts; i++)
            {
                int curRows = nPartRows + (i < nOver ? 1 : 0);

                object[,] part = new object[curRows, nCols];

                for (int j = 0; j < curRows; j++)
                {
                    for (int k = 0; k < nCols; k++)
                    {
                        part[j, k] = arrSource[srcRow, k];
                    }
                    srcRow++;
                }

                arrResult[i] = part;
            }

            return arrResult;
        }

        private void ProcessingAllCard(object[,] RS)
        {
            object[] arrRV;

            m_formData.IsInProcessing = true;

            for (int i = 0; i <= m_formData.ArrNumProcessor.GetUpperBound(0); i++)
            {
                m_formData.ArrNumProcessor[i] = 0;
                m_formData.ArrStrServerFile[i] = string.Empty;

                m_arrChannelLabel[i].Text = i.ToString();
                m_arrChannelLabel[i].ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                m_arrChannelLabel[i].Visible = false;
            }

            lblChannel.Visible = true;

            if (RS != null)
            {
                m_formData.CountAll = RS.GetUpperBound(0) + 1;

                arrRV = SplitArray(m_formData.CountProcessor, RS);

                m_formData.DateStart = m_dateStart;
                timerChannel.Start();

                for (int i = 0; i <= arrRV.GetUpperBound(0); i++)
                {
                    object[,] chunk = arrRV[i] as object[,];
                    if (chunk != null)
                    {
                        var kvp = FindChannelKVP(m_dicChannel, i);

                        kvp.Value.LoadResource = @"VBS:UBS_VBS\PLCARD\PcCalcPrcAsync.vbs";

                        var paramForChannel = CloneParam(m_paramIn);

                        paramForChannel["RS"] = chunk;
                        paramForChannel["Номер канала"] = i;

                        kvp.Key.BackColor = Color.FromArgb(22, 181, 214);
                        kvp.Key.Visible = true;

                        kvp.Value.ServerNotice = true;
                        kvp.Key.Tag = "Активный";

                        kvp.Value.ParamsInParam = paramForChannel;

                        kvp.Value.RunAsync("CalcPrcSKSnSGPA7");
                    }
                }
            }
            else
            {
                m_formData.IsInProcessing = false;
                btnSave.Enabled = true;
                timerChannel.Stop();

                base.Ubs_ShowMsg("Операция завершена, обработано 0 договоров");
            }
        }
        private UbsParam CloneParam(UbsParam source)
        {
            var result = new UbsParam();

            foreach (var item in source)
            {
                result[item.Key] = item.Value;
            }

            return result;
        }

        private void SetModeCalc()
        {
            if (chkTestMode.Checked)
            {
                m_additionalParamIn["IsReportOnly"] = true;
                m_paramIn["IsReportOnly"] = true;
            }
            else
            {
                m_additionalParamIn["IsReportOnly"] = false;
                m_paramIn["IsReportOnly"] = false;
            }
        }

        private string GetMode()
        {
            if (rbColectedPercent.Visible == true && rbColectedPercent.Checked == true)
            {
                return ModeText.ACCUM;
            }

            if (rbPaymentPercent.Visible == true && rbPaymentPercent.Checked == true)
            {
                return ModeText.PAYM;
            }

            return string.Empty;
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
            // код реализации обработчика команды CommandLine
            m_command = Convert.ToString(param_in);

            InitChannel();

            m_dateToday = GetCurrentDate();

            dateOverDraft.DateValue = m_dateToday.AddDays(1);
            dateTrn.DateValue = m_dateToday;

            m_collectedPercentsOverString = string.Empty;

            base.IUbsChannel.ParamIn("Установки", new object[] { "Накопление % по овердрафту", "Количество потоков" });

            base.IUbsChannel.Run("InitForm");

            m_collectedPercentsOverString =
                Convert.ToString(base.IUbsChannel.ParamOut("Накопление % по овердрафту"));
            m_formData.CountProcessor =
                        Convert.ToInt32(base.IUbsChannel.ParamOut("Количество потоков"));

            if (m_formData.CountProcessor < 1)
            {
                m_formData.CountProcessor = 1;
            }
            if (m_formData.CountProcessor > 7)
            {
                m_formData.CountProcessor = 7;
            }

            m_collectedPercentsOverString = Convert.ToString(base.IUbsChannel.ParamOut("Накопление % по овердрафту"));
            m_formData.CountProcessor = Convert.ToInt32(base.IUbsChannel.ParamOut("Количество потоков"));

            if (m_command == "ALL_CARD")
            {
                rbColectedPercent.Checked = true;
                rbPaymentPercent.Checked = false;

                rbColectedPercent.Visible = true;
                rbPaymentPercent.Visible = true;

                this.Text = "Начисление процентов по всем счетам карт";
            }

            if (m_command == "ALL_CARD_NOT_IN_TIME")
            {
                rbColectedPercent.Checked = true;
                rbPaymentPercent.Checked = false;

                rbColectedPercent.Visible = true;
                rbPaymentPercent.Visible = true;

                this.Text = "Начисление процентов по всем счетам карт вне графика";
            }

            if (m_command == "ALL_CARD_LOAN")
            {
                rbColectedPercent.Visible = true;
                rbPaymentPercent.Visible = true;

                rbColectedPercent.Checked = true;
                rbPaymentPercent.Enabled = false;

                this.Text = "Начисление % по всем счетам просроченного долга";
            }

            if (m_command == "ALL_CARD_OVD")
            {
                rbColectedPercent.Visible = true;
                rbPaymentPercent.Visible = true;

                rbColectedPercent.Checked = true;
                rbPaymentPercent.Enabled = false;

                this.Text = "Начисление процентов по всем счетам овердрафта";
            }

            if (m_command == "PAYM_NOT_IN_TIME")
            {
                rbPaymentPercent.Checked = true;
                rbColectedPercent.Checked = false;

                rbColectedPercent.Visible = false;
                rbPaymentPercent.Visible = true;

                this.Text = "Уплата процентов по счетам карт вне графика";
                lblMainDateOverDraft.Text = "Проценты будут уплачены";
                lblDateOverDraft.Text = "за интервалы заканчивающиеся ДО";
            }

            if (rbColectedPercent.Visible)
                rbColectedPercent.Focus();

            if (rbColectedPercent.Visible && rbColectedPercent.Enabled)
                rbColectedPercent.Focus();
            else if (rbPaymentPercent.Visible && rbPaymentPercent.Enabled)
                rbPaymentPercent.Focus();

            return null;
        }
        private DateTime GetCurrentDate()
        {
            base.IUbsChannel.ParamIn("NameSetting", "Server");
            base.IUbsChannel.Run("GetCommonDate");

            return Convert.ToDateTime(base.IUbsChannel.ParamOut("DataSetting"));
        }
        private void InitChannel()
        {
            var resourceForAsync = @"VBS:UBS_VBS\PLCARD\PcCalcPrcAsync.vbs";
            var resource = @"VBS:UBS_VBD\PLCARD\PcCalcPrcInit.vbs";

            base.IUbsChannel.LoadResource = resource;

            m_ubsChannelAsync = new UbsChannel();
            m_ubsChannelAsync.LoadResource = resourceForAsync;

            m_ubsChannelAsync.Respond += ubsChannelAsync_Respond;
            m_ubsChannelAsync.Error += ubsChannelAsync_Error;
            m_ubsChannelAsync.Notice += ubsChannelAsync_Notice;
        }

        private void ubsChannelAsync_Notice(object sender, UbsChannelEventArgs args)
        {
            try
            {
                MethodInvoker d = delegate ()
                {
                    string[] arrString;
                    string strTime;
                    string strTemp;
                    int sec;

                    var textNotice = args.Message.ToString();

                    if (!string.IsNullOrEmpty(textNotice) &&
                        textNotice.IndexOf("INDEX", StringComparison.Ordinal) >= 0)
                    {
                        btnSave.Text = textNotice;
                    }

                    if (!string.IsNullOrEmpty(textNotice) &&
                        textNotice.IndexOf("SERVICE_MESSAGE:", StringComparison.Ordinal) >= 0)
                    {
                        string tail = textNotice.Length > 16 ? textNotice.Substring(16) : string.Empty;

                        int.TryParse(tail.Trim(), out m_procTime);

                        strTime = TimeExecuteProcess(m_dateStart, m_procTime, 4);

                        arrString = strTime.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                        if (arrString.Length == 3)
                        {
                            label4.Text = arrString[0];
                        }
                    }
                    else
                    {
                        arrString = textNotice.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                        if (arrString.Length == 3)
                        {
                            strTemp = arrString[0].Replace("Обработано ", "");
                            int posIz = strTemp.IndexOf("из", StringComparison.Ordinal);

                            if (posIz >= 2)
                            {
                                strTemp = strTemp.Substring(0, posIz - 2).Trim();
                            }
                            else
                            {
                                strTemp = strTemp.Trim();
                            }

                            if (int.TryParse(strTemp, out int parsedCount))
                                m_count = parsedCount;

                            sec = (int)(DateTime.Now - m_dateStart).TotalSeconds;

                            if (sec > 0 && m_count > 0)
                                m_cardInSec = (int)(m_count / sec);

                            label4.Text = arrString[0];
                        }
                        else
                        {
                            label4.Text = textNotice;
                        }
                    }
                };

                if (this.IsDisposed) return;
                if (!this.IsHandleCreated) return;

                if (this.InvokeRequired)
                    this.BeginInvoke(d);
                else
                    d();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void ubsChannelAsync_Error(object sender, UbsChannelEventArgs args)
        {
            try
            {
                MethodInvoker d = delegate ()
                {
                    var channel = sender as UbsChannel;
                    string errorDescription = channel.ErrorDescription + Environment.NewLine;

                    if (errorDescription.IndexOf("compilation", StringComparison.Ordinal) < 0)
                    {
                        m_isNeedStopAsync = false;
                        timer.Stop();

                        base.Ubs_ShowErrorBox(errorDescription);
                    }
                    else
                    {
                        m_isNeedStopAsync = false;
                        timer.Stop();

                        base.Ubs_ShowErrorBox(errorDescription);
                    }
                };

                if (this.IsDisposed) return;
                if (!this.IsHandleCreated) return;

                if (this.InvokeRequired)
                    this.BeginInvoke(d);
                else
                    d();

            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void ubsChannelAsync_Respond(object sender, UbsChannelEventArgs args)
        {
            try
            {
                string strTemp = string.Empty;

                MethodInvoker d = delegate ()
                {
                    int pos;
                    string serverFile;
                    string localFile;

                    timer.Interval = 1;
                    timer.Stop();

                    var asyncChannel = sender as UbsChannel;

                    string resultReport = (string)asyncChannel.ParamOut("ResultReport");
                    pos = resultReport.IndexOf("SERVER_FILE_NAME ", StringComparison.Ordinal);

                    if (pos >= 0)
                    {
                        int startIndex = 17;
                        serverFile = resultReport.Length > startIndex ? resultReport.Substring(startIndex) : string.Empty;

                        localFile = Path.GetTempFileName();

                        if (File.Exists(localFile))
                            File.Delete(localFile);

                        base.IUbsChannel.GetFile(localFile, "Temp", serverFile);

                        TimeExecuteProcess(m_dateStart, m_procTime, m_procTime);
                        if (m_procTime > 0)
                            label4.Text = $"Обработано {m_procTime} из {m_procTime}";

                        label5.Text = string.Empty;

                        var paramIn = new UbsParam();
                        paramIn["VirtualFolder"] = "Temp";
                        paramIn["ServerFile"] = serverFile;
                        base.IUbsChannel.ParamsInParam = paramIn;
                        base.IUbsChannel.Run("DeleteServerFileExStub");

                        m_isNeedStopAsync = false;

                        DateTime tmpDat = DateTime.Now;
                        int tmpDat2 = (int)(tmpDat - m_dateStart).TotalSeconds;

                        double cardsPerSec = tmpDat2 > 0 ? (double)m_procTime / tmpDat2 : 0.0;

                        strTemp =
                            "Время старта : " + m_dateStart + Environment.NewLine +
                            "Время завершения : " + tmpDat + Environment.NewLine +
                            "Прошло времени (сек): " + tmpDat2 + Environment.NewLine +
                            "Обработано карт: " + m_procTime + Environment.NewLine +
                            "Card/Sec : " + cardsPerSec + Environment.NewLine +
                            (string)File.ReadAllText(localFile, Encoding.GetEncoding(1251));

                        btnSave.Enabled = true;
                    }

                    m_isNeedStopAsync = false;

                    if (!string.IsNullOrEmpty(strTemp))
                    {
                        this.Ubs_ShowMsg(strTemp);
                    }
                };

                if (this.IsDisposed) return;
                if (!this.IsHandleCreated) return;

                if (this.InvokeRequired)
                    this.BeginInvoke(d);
                else
                    d();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
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
                m_itemArray = param_in as object[];

                if (m_itemArray == null)
                {
                    return null;
                }

                if (m_command == "ACCUM")
                {
                    rbColectedPercent.Checked = true;
                    rbPaymentPercent.Checked = false;

                    rbColectedPercent.Visible = true;
                    rbPaymentPercent.Visible = false;

                    this.Text = "Накопление процентов по счетам карт";
                    lblMainDateOverDraft.Text = "Накопленные проценты будут рассчитаны";
                    lblDateOverDraft.Text = "за интервалы заканчивающиеся ДО";
                }

                if (m_command == "PAYM_IN_TIME")
                {
                    rbPaymentPercent.Checked = true;
                    rbColectedPercent.Checked = false;

                    rbColectedPercent.Visible = false;
                    rbPaymentPercent.Visible = true;

                    this.Text = "Уплата процентов по счетам карт по графику";
                    lblMainDateOverDraft.Text = "Дата очередной уплаты процентов";
                    lblDateOverDraft.Text = "находится в интервале заканчивающемся ДО";
                }

                if (m_command == "PAYM_NOT_IN_TIME")
                {
                    rbPaymentPercent.Checked = true;
                    rbColectedPercent.Checked = false;

                    rbColectedPercent.Visible = false;
                    rbPaymentPercent.Visible = true;

                    this.Text = "Уплата процентов по счетам карт вне графика";
                    lblMainDateOverDraft.Text = "Проценты будут уплачены";
                    lblDateOverDraft.Text = "за интервалы заканчивающиеся ДО";
                }

                if (m_command == "PAYM_ONE_OVD")
                {
                    rbPaymentPercent.Checked = false;
                    rbColectedPercent.Checked = true;

                    rbColectedPercent.Visible = true;
                    rbPaymentPercent.Visible = true;

                    rbColectedPercent.Checked = true;
                    rbPaymentPercent.Enabled = false;

                    this.Text = "Начисление процентов по счету овердрафта";
                    lblMainDateOverDraft.Text = "Накопленные проценты за овердрафт будут рассчитаны";
                    lblDateOverDraft.Text = "за интервалы заканчивающиеся ДО";
                }

                if (m_command == "ONE_CARD_LOAN")
                {
                    rbPaymentPercent.Checked = false;
                    rbColectedPercent.Checked = true;

                    rbColectedPercent.Visible = true;
                    rbPaymentPercent.Visible = true;

                    rbColectedPercent.Checked = true;
                    rbPaymentPercent.Enabled = false;

                    this.Text = "Начисление процентов по счету просроченного долга";
                    lblMainDateOverDraft.Text = "Накопленные проценты за просрочку будут рассчитаны";
                    lblDateOverDraft.Text = "за интервалы заканчивающиеся ДО";
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        #endregion

        private void rbColectedPercent_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbColectedPercent.Checked) return;

            rbPaymentPercent.Checked = false;
            lblDateOverDraft.Text = UIText.IntervalBefore;

            if (m_command == ModeText.AllCard)
            {
                lblMainDateOverDraft.Text = UIText.AccumCommon;
            }
            else if (m_command == ModeText.AllCardOvd || m_command == ModeText.PaymOneOvd)
            {
                lblMainDateOverDraft.Text =
                    m_collectedPercentsOverString == YesNoText.Yes
                        ? UIText.AccumOvd
                        : UIText.Payment;
            }
            else if (m_command == ModeText.AllCardLoan || m_command == ModeText.OneCardLoan)
            {
                lblMainDateOverDraft.Text =
                    m_collectedPercentsOverString == YesNoText.Yes
                        ? UIText.AccumLoan
                        : UIText.Payment;
            }
        }

        private void rbPaymentPercent_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbPaymentPercent.Checked) return;

            rbColectedPercent.Checked = false;

            if (m_command == ModeText.PaymOneOvd ||
                m_command == ModeText.AllCardOvd ||
                m_command == ModeText.OneCardLoan ||
                m_command == ModeText.AllCardLoan)
            {
                lblMainDateOverDraft.Text = UIText.Payment;
                lblDateOverDraft.Text = UIText.IntervalBefore;
            }
            else
            {
                lblMainDateOverDraft.Text = UIText.PaymentDate;
                lblDateOverDraft.Text = UIText.IntervalContains;
            }
        }

        private void chkTestMode_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkTestMode.Checked)
                return;

            dateOverDraft.Focus();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (m_procTime > 0)
            {
                m_count += m_cardInSec;
            }

            TimeExecuteProcess(m_dateStart, m_procTime, m_count);
        }

        private void timerChannel_Tick(object sender, EventArgs e)
        {
            TimeExecuteProcess(m_formData.DateStart, m_formData.CountAll, m_formData.CountComplete);
        }
        private string TimeExecuteProcess(DateTime dateBegin, int unitsAll, int unitsComplete)
        {
            int elapsedSeconds = (int)(DateTime.Now - dateBegin).TotalSeconds;

            if (elapsedSeconds < 0) elapsedSeconds = 0;

            string elapsedText = FormatHms(elapsedSeconds);

            string result = "Времени прошло:   " + elapsedText + Environment.NewLine +
                            "Времени осталось: ";

            lblTime.Text = "Времени прошло:   " + elapsedText;

            if (unitsComplete > 0)
            {
                double remainSecondsD = elapsedSeconds * (double)(unitsAll - unitsComplete) / unitsComplete;
                int remainSeconds = (int)remainSecondsD;

                if (remainSeconds < 0) remainSeconds = 0;

                string remainText = FormatHms(remainSeconds);

                label5.Text = "Времени осталось: " + remainText;
                result += remainText;
            }

            return result;
        }

        private static string FormatHms(int totalSeconds)
        {
            if (totalSeconds < 0) totalSeconds = 0;

            int temp = totalSeconds;

            int sec = temp % 60;
            temp = (temp - sec) / 60;

            int min = temp % 60;
            temp = temp - min;

            int hour = temp / 60;

            var sb = new System.Text.StringBuilder();

            if (hour > 0)
                sb.Append(hour).Append(" ч ");

            if (min > 0 || hour > 0)
                sb.Append(min).Append(" мин ");

            sb.Append(sec).Append(" сек");

            return sb.ToString();
        }
    }
}