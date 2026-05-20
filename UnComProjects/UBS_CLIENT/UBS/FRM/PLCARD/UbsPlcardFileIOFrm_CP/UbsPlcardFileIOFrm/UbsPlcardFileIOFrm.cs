using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// ������ ������ �����
    /// </summary>
    public partial class UbsPlcardFileIOFrm : UbsFormBase
    {
        #region ���� ���������� ����������

        private string m_command = "";    //��������� ������� �����
        private readonly Label[] m_arrChannelLabel;
        private bool m_needToUseAsyncChannels;
        private IUbsScript m_scripter;
        private string m_inOutText;
        private string m_sidAction;
        private string m_sidScript;
        private string m_nameFunction;
        private object m_arrayParams;
        private object[,] m_arrProcPath;
        private string m_pathFile;

        private DateTime MinDate = new DateTime(2222, 1, 1);
        private object[] m_itemArray;
        private int m_countChanel;
        private bool m_isRespond;
        private string[] m_arrFileNameServer;
        private int m_idProcessing;
        private int m_division;
        private int m_idFile;
        private int m_recordCounter;
        private int m_recordUp;
        private string m_importReport;
        private bool m_resourcesDisposed;
        private readonly UbsParam m_paramIn;
        private readonly Dictionary<Label, UbsChannel> m_dicChannel;

        #endregion

        /// <summary>
        /// �����������
        /// </summary>
        public UbsPlcardFileIOFrm()
        {
            m_addCommand(); //���������������� ����������� ������ ���������� IUbs

            InitializeComponent();

            m_arrChannelLabel = new Label[] {
                                                 lblChannel1
                                               , lblChannel2
                                               , lblChannel3
                                               , lblChannel4
                                               , lblChannel5
                                               , lblChannel6
                                               , lblChannel7
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

            foreach (var chnl in m_dicChannel)
            {
                chnl.Value.Respond += IUbsChannel_Respond;
                chnl.Value.Error += IUbsChannel_Error;
                chnl.Value.Notice += IUbsChannel_Notice;
            }

            this.FormClosed += UbsPlcardFileIOFrm_FormClosed;

            base.Ubs_CommandLock = true;
        }
        private void UbsPlcardFileIOFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // ������ �� ���������� ������������ �������� (�� ������, ���� ������� ������ ��������� ���)
            if (m_resourcesDisposed)
                return;

            try
            {
                // ��� ������� �������� ������������ �� ������� ������� � ��������� �� �������������
                // (��� ��� ��� ��������� Respond/Error/Notice �������� this.Invoke(...) �����
                // �������� handle �����, ��� ��� NullReferenceException ������ Control.MarshaledInvoke).
                if (m_dicChannel != null)
                {
                    foreach (var kvpChan in m_dicChannel)
                    {
                        var channel = kvpChan.Value;
                        if (channel == null) continue;

                        channel.Respond -= IUbsChannel_Respond;
                        channel.Error -= IUbsChannel_Error;
                        channel.Notice -= IUbsChannel_Notice;

                        try
                        {
                            channel.StopRun();
                        }
                        catch (Exception stopEx)
                        {
                            // ����� ��� �� ��������� ��� ��� ���������� � ��� ��� ��� ��������.
                            Debug.WriteLine($"UbsPlcardFileIOFrm: StopRun ��� �������� �����: {stopEx}");
                        }
                    }
                }

                m_isRespond = false;

                if (m_scripter != null)
                {
                    m_scripter.Dispose();
                    m_scripter = null;
                }

                m_resourcesDisposed = true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        /// <summary>
        /// ���������� ��������� ��������� ��� UI-�����.
        /// ���������� ��� (this.IsDisposed/IsHandleCreated) ����� ��������, ��� Control.Invoke
        /// �� ������ NullReferenceException ������ MarshaledInvoke, ���� ����� ��� ������/��������������.
        /// ��� ��� ����� IsHandleCreated � Invoke ��� ��� ���������� �����, ��������� ��������
        /// ObjectDisposedException/InvalidOperationException, ��������� � ��� �������������� �����.
        /// </summary>
        private void SafeInvoke(MethodInvoker d)
        {
            if (d == null)
                return;

            if (this.IsDisposed || this.Disposing || !this.IsHandleCreated)
                return;

            try
            {
                this.Invoke(d);
            }
            catch (Exception ex) when (ex is ObjectDisposedException || ex is InvalidOperationException)
            {
                // ����� ��� ������/���������������/handle ��� ��������� ����� ��������� IsHandleCreated
                // � ������ Invoke. ��� ��������� ��� ��� ��������, ������� ��� �����������, � �� �����.
                Debug.WriteLine($"UbsPlcardFileIOFrm.SafeInvoke: ����� �������� �����: {ex}");
            }
        }

        #region ����������� ������� ������ (� ���������)

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                // ������ legacy cmdExit_Click: ���� ��� ����������� ��������� �
                // ������� ������������� ��� ������ � ���, ���� ��� ����������,
                // � ������ ����� ��������� �����. ����� MessageBox-� �� ���
                // ����� ������� ������� ����� � ����������������� ���������.
                if (m_isRespond)
                {
                    foreach (var kvpChan in m_dicChannel)
                    {
                        try { kvpChan.Value.StopRun(); }
                        catch { /* ����� ����� ���� �� ������� */ }
                    }

                    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                    while (m_isRespond && stopwatch.Elapsed.TotalSeconds < 30)
                    {
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(50);
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
            finally
            {
                this.Close();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }

                m_idFile = 0;

                m_recordCounter = 0;
                m_recordUp = 0;
                lblResult.Text = "���������� 0 ��������� �� 0";
                var mportReport = string.Empty;

                var nameGo = btnSave.Text;

                SetButtonGoAndcmdExit("���������...", false);

                if (txtPathFile.Text == string.Empty)
                {
                    MessageBox.Show("�� ����������� ��� �����", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    SetButtonGoAndcmdExit(nameGo, true);

                    txtPathFile.Focus();

                    return;
                }

                if (dtLoadDate.Visible)
                {
                    if (dtLoadDate.DateValue == MinDate)
                    {
                        MessageBox.Show("�� ����������� ���� ��������", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        SetButtonGoAndcmdExit(nameGo, true);

                        dtLoadDate.Focus();

                        return;
                    }

                    var dateOdForPlcard = "������������ ���� PLCARD";
                    var dateKinds = new object[] { dateOdForPlcard };

                    base.IUbsChannel.ParamIn("���� ���", dateKinds);

                    base.IUbsChannel.Run("COM_GetCommonDates");

                    var dateOperDay = Convert.ToDateTime(base.IUbsChannel.ParamOut(dateOdForPlcard));

                    if (dtLoadDate.DateValue > dateOperDay)
                    {
                        MessageBox.Show($"���� ���������� {dtLoadDate.DateValue} ������ ���� ������������� ��� PLCARD {dateOperDay.ToShortDateString()}.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        SetButtonGoAndcmdExit(nameGo, true);

                        dtLoadDate.Focus();

                        return;
                    }
                }
                KeyValuePair<string, string> kvpSourceMethod = new KeyValuePair<string, string>(string.Empty, string.Empty);

                if (m_sidAction == string.Empty)
                {
                    m_scripter.Read(m_sidScript);
                }
                else
                {
                    kvpSourceMethod = GetScriptByName(m_sidAction);

                    m_scripter.LoadFiles(kvpSourceMethod.Key);
                }

                var scripterParameters = new UbsParam();

                if (dtLoadDate.Visible)
                {
                    scripterParameters["DateOper"] = dtLoadDate.DateValue;
                }

                if (m_inOutText != "In")
                {
                    if (txtPathFile.Text != string.Empty)
                    {
                        txtPathFile.Text = BuildPath(txtPathFile.Text, string.Empty);
                    }
                }

                scripterParameters["PathFile"] = txtPathFile.Text;

                scripterParameters["StrCommand"] = m_command;

                if (m_arrayParams != null)
                {
                    scripterParameters["ArrayParams"] = m_arrayParams;
                }

                scripterParameters["KeyArray"] = m_itemArray;

                if (cmbProcessing.Visible)
                {
                    scripterParameters["IdProcessing"] = cmbProcessing.SelectedValue;
                }
                if (cmbProcessing.Visible)
                {
                    scripterParameters["FormMode"] = cmbMode.SelectedValue;
                }

                m_scripter.UbsScriptParam = scripterParameters;

                if (m_nameFunction == string.Empty)
                {
                    if (m_sidAction == string.Empty)
                    {
                        m_scripter.ExecuteScript();
                    }
                }
                else
                {
                    m_scripter.Run(m_nameFunction);
                }

                scripterParameters = m_scripter.UbsScriptParam;

                if (!Convert.ToBoolean(scripterParameters["ResultReport"]) || !m_needToUseAsyncChannels)
                {
                    if (!Convert.ToBoolean(scripterParameters["NotShowReport"]))
                    {
                        base.Ubs_ShowMsg(Convert.ToString(scripterParameters["StrReport"]));
                    }

                    SetButtonGoAndcmdExit(nameGo, true);

                    return;
                }

                m_importReport = Convert.ToString(scripterParameters["ReportImport_Async"]).Trim();


                //            '================= ��� ��������� �������� � ��������� (�� 1-7) �������
                //' �� ������ ������ ������ ��� ������ "�������� ����� � ���������� �� �����������"
                if (m_needToUseAsyncChannels)
                {
                    var arrDataOper = scripterParameters["varDataOper"] as object[,];

                    m_idFile = Convert.ToInt32(scripterParameters["IdFile"]);

                    scripterParameters["nParts"] = Convert.ToInt32(m_countChanel);

                    m_scripter.UbsScriptParam = scripterParameters;

                    m_scripter.Run("SplitOperArray");

                    scripterParameters = m_scripter.UbsScriptParam;

                    var arrResult = scripterParameters["arrResult"] as object[];

                    if (arrDataOper != null)
                    {
                        m_paramIn.Value("IdFile", m_idFile);
                        m_paramIn.Value("State", 255);

                        base.IUbsChannel.ParamsInParam = m_paramIn;

                        base.IUbsChannel.Run("PCPRCFile_SetStateParam");

                        m_recordCounter = arrDataOper.GetLength(1);

                        lblResult.Text = "���������� 0 ��������� �� 0";

                        m_isRespond = true;
                        m_arrFileNameServer = new[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };

                        for (int i = 0; i < arrResult.Length; i++)
                        {
                            if (arrResult[i] is Array)
                            {
                                var rs = arrResult[i] as object[,];
                                m_paramIn.Value("RS", Transpose(rs));
                                m_paramIn.Value("����� ������", i);
                                m_paramIn.Value("DateOper", dtLoadDate.DateValue);
                                m_paramIn.Value("IdFile", m_idFile);

                                m_paramIn.Remove("ReportError");

                                m_dicChannel[m_arrChannelLabel[i]].UnLoadResource(-1);

                                m_arrChannelLabel[i].BackColor = Color.FromArgb(22, 181, 214);

                                m_dicChannel[m_arrChannelLabel[i]].LoadResource = @"VBS:UBS_VBS\PLCARD\OPENWAY\PCFileOperOW.vbs";

                                m_dicChannel[m_arrChannelLabel[i]].ParamsInParam = m_paramIn;

                                // ��������� ����� ��� �������� �� RunAsync, ����� ������� ��� ���������
                                // ������, ��� ����� ������ ������ ����� �� ������ (Respond/Error
                                // ����������� � ������ ������).
                                m_arrChannelLabel[i].Tag = "��������";

                                m_dicChannel[m_arrChannelLabel[i]].RunAsync("ProcessOperationsOfProcessing_Async");
                            }
                        }

                        btnSave.Text = "��������� ��������...";
                        btnSave.Enabled = false;
                        btnExit.Text = "������";
                    }
                    else
                    {
                        SetButtonGoAndcmdExit(nameGo, true);

                        if (m_importReport != string.Empty)
                        {
                            m_importReport += Environment.NewLine;
                        }

                        m_importReport += "������ ��� ��������� ���!";

                        base.Ubs_ShowMsg(m_importReport);
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void IUbsChannel_Notice(object sender, UbsChannelEventArgs args)
        {
            try
            {
                MethodInvoker d = delegate ()
                {
                    int nOK = 0;
                    string report = "���������� {0} ��������� �� " + m_recordCounter;

                    var textNotice = args.Message.ToString();
                    if (textNotice.Length > 3)
                    {
                        if (textNotice.StartsWith("OK:"))
                        {
                            string numberPart = textNotice.Substring(3).Trim();

                            if (int.TryParse(numberPart, out int parsed))
                            {
                                nOK = parsed;
                            }

                            m_recordUp += nOK;

                            lblResult.Text = string.Format(report, m_recordUp.ToString());
                        }
                    }
                };
                SafeInvoke(d);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
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

                    MessageBox.Show(args.Message.ToString(), "������", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    labelChannel.BackColor = Color.FromArgb(255, 0, 0);

                    var paramIn = new UbsParam();

                    // ���� 0-based ������ ������ ��� ��, ��� � legacy (Index � VB control-array
                    // index), � �� �� caption labelChannel.Text ("1".."7").
                    var index = Array.IndexOf(m_arrChannelLabel, labelChannel);
                    if (index < 0) index = 0;

                    paramIn["Error"] = args.Message.ToString();
                    paramIn["NC"] = index + 1;

                    channel.RunAsync("ProcessOperationsOfProcessing_AsyncIfError", paramIn);
                };
                SafeInvoke(d);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void IUbsChannel_Respond(object sender, UbsChannelEventArgs args)
        {
            try
            {
                string content = string.Empty;

                MethodInvoker d = delegate ()
                {
                    string fileNameServer = string.Empty;
                    int nPos = 0;
                    bool isOk;

                    var channel = sender as UbsChannel;
                    var paramOut = channel.ParamsOutParam;

                    Label labelChannel = FindLabel(m_dicChannel, channel);
                    if (labelChannel == null)
                        return;

                    // labelChannel.Text = "1".."7" (caption), � ������� 0-based.
                    // ���� ������ ������ � ������� m_arrChannelLabel, �����
                    // ��� ������ �7 ����� IndexOutOfRangeException, � ��� ��������� �
                    // ����� �������� �� �������.
                    int index = Array.IndexOf(m_arrChannelLabel, labelChannel);
                    if (index < 0)
                    {
                        throw new Exception($"�� ������ ����� ������. {labelChannel.Text}");
                    }

                    if (paramOut.Contains("ReportError"))
                    {
                        MessageBox.Show(Convert.ToString(paramOut["ReportError"]),
                                        "������!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        btnExit.Text = "�����";
                        labelChannel.BackColor = btnSave.BackColor;
                        SetButtonGoAndcmdExit("��������!", true);
                    }
                    else
                    {
                        fileNameServer = Convert.ToString(paramOut["��� �����"]);

                        nPos = fileNameServer.LastIndexOf('\\');

                        if (nPos >= 0 && nPos < fileNameServer.Length - 1)
                        {
                            m_arrFileNameServer[index] = fileNameServer.Substring(nPos + 1);
                        }
                        else
                        {
                            m_arrFileNameServer[index] = fileNameServer;
                        }

                        labelChannel.BackColor = Color.FromArgb(255, 255, 255);
                    }

                    // ����� ��������� � ������� ������� ����������
                    m_arrChannelLabel[index].Tag = string.Empty;

                    isOk = true;
                    for (int i = 0; i < m_arrChannelLabel.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(m_arrChannelLabel[i].Tag)))
                        {
                            isOk = false;
                            break;
                        }
                    }

                    if (isOk)
                    {
                        // ��� ������ �������� � �������� ��������
                        content = DoAsincReport(channel);

                        SetButtonGoAndcmdExit("��������!", true);

                        for (int i = 0; i < m_arrChannelLabel.Length; i++)
                        {
                            m_arrChannelLabel[i].BackColor = btnSave.BackColor;
                        }
                        btnExit.Text = "�����";

                        m_isRespond = false;
                    }
                };

                SafeInvoke(d);

                // The protocol viewer is shown AFTER the synchronous SafeInvoke(d) has
                // unwound, via BeginInvoke. From the IL of UbsFormBase.Ubs_ShowMsg:
                //   UbsFormBase.Ubs_ShowMsg(text) -> UbsFormTextView.Ubs_ShowMsg(text, null)
                //                                  -> new UbsFormTextView().ShowDialog(null)
                // The owner is hard-coded to null, so WinForms falls back to Win32
                // GetActiveWindow to pick the parent. If we call this.Ubs_ShowMsg(content)
                // directly from inside the COM callback IUbsChannel_Respond (which itself
                // runs on a background thread spawned by RunAsync), GetActiveWindow returns
                // "the wrong" window (or none of ours), and the viewer pops up as a stray
                // top-level window (its own taskbar entry, no Z-order link to our form).
                // BeginInvoke posts the work onto the next UI-thread message pump tick;
                // and inside that delegate we also explicitly Activate() the form and call
                // UbsFormTextView.Ubs_ShowMsg(content, this) via reflection with `this` as
                // the explicit owner. That makes the viewer become a child window of our
                // form deterministically, regardless of which window is currently active.
                // UbsFormTextView lives in UbsFormTextView.dll (already loaded transitively
                // through UbsFormBase); we use reflection to avoid having to add a direct
                // reference in the .csproj.
                if (!string.IsNullOrEmpty(content) && this.IsHandleCreated && !this.IsDisposed)
                {
                    string contentToShow = content;
                    try
                    {
                        this.BeginInvoke((MethodInvoker)delegate ()
                        {
                            try
                            {
                                ShowReportInOwnedWindow(contentToShow);
                            }
                            catch (Exception ex)
                            {
                                this.Ubs_ShowError(ex);
                            }
                        });
                    }
                    catch (Exception ex) when (ex is ObjectDisposedException || ex is InvalidOperationException)
                    {
                        // Form was closed/disposed/handle gone between SafeInvoke and BeginInvoke.
                        Debug.WriteLine($"UbsPlcardFileIOFrm.IUbsChannel_Respond: BeginInvoke skipped: {ex}");
                    }
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        // Reflection-based binding to UbsFormTextView.Ubs_ShowMsg(string, IWin32Window)
        // and its static modal-flag setter. The DLL is loaded transitively through
        // UbsFormBase but we deliberately avoid an explicit reference in the .csproj.
        private static volatile Type s_ubsFormTextViewType;
        private static volatile MethodInfo s_ubsFormTextViewShowMsg;
        private static volatile MethodInfo s_ubsFormTextViewSetModal;

        private static void CacheUbsFormTextViewMembers()
        {
            if (s_ubsFormTextViewShowMsg != null)
                return;

            Type type = null;

            try
            {
                var baseAsm = typeof(UbsFormBase).Assembly;
                type = baseAsm.GetType("UbsService.UbsFormTextView", false, false);

                if (type == null)
                {
                    foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        try
                        {
                            type = asm.GetType("UbsService.UbsFormTextView", false, false);
                            if (type != null) break;
                        }
                        catch { /* ignore */ }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UbsPlcardFileIOFrm: failed to locate UbsFormTextView via reflection: {ex}");
            }

            if (type == null)
                return;

            try
            {
                var showMsg = type.GetMethod(
                    "Ubs_ShowMsg",
                    BindingFlags.Public | BindingFlags.Static,
                    null,
                    new[] { typeof(string), typeof(IWin32Window) },
                    null);

                var setModal = type.GetMethod(
                    "set_UbsShow_Modal",
                    BindingFlags.Public | BindingFlags.Static,
                    null,
                    new[] { typeof(bool) },
                    null);

                s_ubsFormTextViewType = type;
                s_ubsFormTextViewShowMsg = showMsg;
                s_ubsFormTextViewSetModal = setModal;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UbsPlcardFileIOFrm: failed to resolve UbsFormTextView.Ubs_ShowMsg via reflection: {ex}");
            }
        }

        /// <summary>
        /// Shows the protocol viewer with this form as the explicit owner, so the viewer
        /// becomes a child window of our form (proper Z-order, no extra taskbar entry,
        /// SaveAs dialog inside the viewer works correctly). Falls back to the base
        /// Ubs_ShowMsg(string) if reflection-binding to UbsFormTextView could not resolve.
        /// </summary>
        private void ShowReportInOwnedWindow(string content)
        {
            if (string.IsNullOrEmpty(content))
                return;

            // Make our form the active top-level window so that, even if the user has
            // alt-tabbed away during the asynchronous load, Win32 GetActiveWindow inside
            // ShowDialog will return our HWND if reflection-binding fails and we fall
            // back to base.Ubs_ShowMsg(string).
            try
            {
                if (!this.IsDisposed && this.IsHandleCreated)
                {
                    if (this.WindowState == FormWindowState.Minimized)
                        this.WindowState = FormWindowState.Normal;

                    this.Activate();
                    this.BringToFront();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UbsPlcardFileIOFrm.ShowReportInOwnedWindow: Activate/BringToFront failed: {ex}");
            }

            CacheUbsFormTextViewMembers();

            try
            {
                if (s_ubsFormTextViewShowMsg != null)
                {
                    // Match what UbsFormBase.Ubs_ShowMsg does internally: set the
                    // static UbsShow_Modal flag to true, then call the (string, owner)
                    // overload � but with `this` as the explicit owner instead of null.
                    if (s_ubsFormTextViewSetModal != null)
                    {
                        s_ubsFormTextViewSetModal.Invoke(null, new object[] { true });
                    }

                    s_ubsFormTextViewShowMsg.Invoke(null, new object[] { content, (IWin32Window)this });
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UbsPlcardFileIOFrm.ShowReportInOwnedWindow: reflection call failed, falling back: {ex}");
            }

            // Fallback: invoke the base implementation. Z-order may not be ideal if
            // GetActiveWindow returns something else, but at least the message is shown.
            this.Ubs_ShowMsg(content);
        }

        private string DoAsincReport(UbsChannel channel)
        {
            string content = string.Empty;

            var clientFileName = string.Empty;
            var clientFileNameTmp = string.Empty;

            // Полный аналог legacy (PCFileIO.dob, DoAsincReport):
            //   UbsChannel.NumFrame = 0
            //   UbsChannel.LoadResource = "VBS:UBS_VBS\PLCARD\Processing\PCPRCFile.vbs"
            // Без этого сброса главный канал формы накапливает состояние от
            // предыдущих синхронных и асинхронных операций (PCPRCFile_SetStateParam
            // с State=255, RunAsync по PCFileOperOW.vbs и т.п.), и при повторных
            // запусках начинает вести себя нестабильно:
            //   - на 3-м запуске одна из итераций цикла ниже молча отрабатывает не
            //     до конца: GetFile/DeleteServerFileExStub не дочитывают файл-протокол
            //     одного из каналов, и в итоговом протоколе оказывается только
            //     половина записей (по числу одного канала вместо обоих);
            //   - на 4-м запуске Run("DeleteServerFileExStub") уже валится с серверной
            //     ошибкой "Попытка запуска функции DeleteServerFileExStub методом Run."
            //     (она же "Метод Run: уже запущена функция DeleteServerFileExStub" /
            //     HTTP 500 — точно такой же эффект задокументирован в
            //     UbsPlcardFileZPInFrm.cs, IUbsChannel_Respond).
            // Полный эквивалент VB6 NumFrame=0 + LoadResource в .NET-обёртке —
            // это пара UnLoadResource(-1) + присваивание LoadResource: в VB6
            // NumFrame=0 сбрасывал счётчик кадров, после чего LoadResource спокойно
            // перезагружал скрипт; в .NET-обёртке прямое присваивание LoadResource
            // поверх уже загруженного ресурса бросает исключение «нельзя менять
            // ресурс канала», поэтому сначала нужен явный UnLoadResource(-1) —
            // тем же шаблоном, что и для асинхронных каналов в btnSave_Click
            // выше (UnLoadResource(-1) → LoadResource = "...").
            // NumFrame and (Un)LoadResource are TWO INDEPENDENT properties of the .NET
            // IUbsChannel wrapper, not a "VB6-equivalent pair":
            //   * NumFrame - server-side child-window association of the channel.
            //   * LoadResource / UnLoadResource - the .vbs resource currently mounted.
            // Legacy PCFileIO.dob, Sub DoAsincReport, did:
            //     UbsChannel.NumFrame = 0
            //     UbsChannel.LoadResource = "VBS:UBS_VBS\PLCARD\Processing\PCPRCFile.vbs"
            // We mirror both here. The UnLoadResource(-1) call is a .NET-wrapper-only
            // extra: assigning LoadResource on top of an already-mounted resource throws
            // "the channel resource cannot be changed" in the wrapper, so we unmount
            // first - the same pattern used for the per-channel async setup in
            // btnSave_Click above (UnLoadResource(-1) -> LoadResource = "...").
            base.IUbsChannel.NumFrame = 0;
            base.IUbsChannel.UnLoadResource(-1);
            base.IUbsChannel.LoadResource = @"VBS:UBS_VBS\PLCARD\Processing\PCPRCFile.vbs";

            base.IUbsChannel.ParamIn("IdFile", m_idFile);
            base.IUbsChannel.ParamIn("State", 0);
            base.IUbsChannel.Run("PCPRCFile_SetStateParam");

            for (int i = 0; i < m_arrFileNameServer.Length; i++)
            {
                string serverFileName = (m_arrFileNameServer[i] ?? string.Empty).ToString().Trim();

                if (serverFileName.Length == 0)
                    continue;

                if (string.IsNullOrEmpty(clientFileName))
                {
                    clientFileName = Path.GetTempFileName();
                }

                clientFileNameTmp = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

                if (File.Exists(clientFileNameTmp))
                    File.Delete(clientFileNameTmp);

                base.IUbsChannel.GetFile(clientFileNameTmp, "Temp", serverFileName);

                var paramIn = new UbsParam();
                paramIn["VirtualFolder"] = "Temp";
                paramIn["ServerFile"] = m_arrFileNameServer[i];
                base.IUbsChannel.ParamsInParam = paramIn;
                base.IUbsChannel.Run("DeleteServerFileExStub");

                if (!string.IsNullOrEmpty(m_importReport))
                {
                    File.AppendAllText(clientFileName, m_importReport + Environment.NewLine, Encoding.GetEncoding(1251));
                    m_importReport = string.Empty;
                }

                string tmpContent = File.ReadAllText(clientFileNameTmp, Encoding.GetEncoding(1251));

                File.AppendAllText(clientFileName, tmpContent, Encoding.GetEncoding(1251));
                File.Delete(clientFileNameTmp);
            }

            if (!string.IsNullOrEmpty(clientFileName) && File.Exists(clientFileName))
            {
                try
                {
                    content = File.ReadAllText(clientFileName, Encoding.GetEncoding(1251));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "�� ������� ������� ����: " + ex.Message,
                        "������",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                File.Delete(clientFileName);
            }

            return content;
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

        private static string BuildPath(string strOne, string strTwo)
        {
            strOne = (strOne ?? string.Empty).TrimEnd();
            strTwo = (strTwo ?? string.Empty).TrimStart();

            if (strOne.EndsWith("\\"))
                strOne = strOne.Substring(0, strOne.Length - 1);

            if (strTwo.StartsWith("\\"))
                strTwo = strTwo.Substring(1);

            return strOne + "\\" + strTwo;
        }


        private void SetButtonGoAndcmdExit(string nameButtonGo, bool isEnable)
        {
            btnSave.Text = nameButtonGo;
            btnSave.Enabled = isEnable;
            btnExit.Enabled = isEnable;
            linkFileLoad.Enabled = isEnable;
        }

        #endregion


        #region ����������� ������ IUbs ���������� (� ���������)

        /// <summary>
        /// ��������� ����������� ������������ ������ ���������� IUbs � ������� ������
        /// </summary>
        private void m_addCommand()
        {
            base.Ubs_AddName(new UbsDelegate(CommandLine));
            base.Ubs_AddName(new UbsDelegate(ListKey));
        }
        /// <summary>
        /// ��������� ��������� ������� CommandLine
        /// </summary>
        /// <param name="param_in">������� ���������</param>
        /// <param name="param_out">�������� ���������</param>
        /// <returns></returns>
        private object CommandLine(object param_in, ref object param_out)
        {
            //��� ���������� ����������� ������� CommandLine
            m_command = (string)param_in;

            InitFile();

            return null;
        }
        /// <summary>
        /// ��������� ��������� ������� ListKey
        /// </summary>
        /// <param name="param_in">������� ���������</param>
        /// <param name="param_out">�������� ���������</param>
        /// <returns></returns>
        private object ListKey(object param_in, ref object param_out)
        {
            try
            {
                m_itemArray = (object[])param_in;

                return null;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        private void InitFile()
        {
            base.IUbsChannel.LoadResource = @"VBS:UBS_VBS\PLCARD\Processing\PCPRCFile.vbs";

            object[,] arrSetting = null;

            if (m_command == "PCFileInTransactionsExportOws")
            {
                base.IUbsChannel.ParamIn("NameSection", "����������� �����");
                base.IUbsChannel.ParamIn("NameSetting", "���������� �������");
                base.IUbsChannel.ParamIn("TypeSetting", 0);
                base.IUbsChannel.ParamIn("StrTypeSetting", "SI");

                base.IUbsChannel.Run("GetUbsSetting");

                arrSetting = base.IUbsChannel.ParamOut("DataSetting") as object[,];

                m_countChanel = 0;

                if (arrSetting != null)
                {
                    for (int i = 0; i < arrSetting.GetLength(0); i++)
                    {
                        if (Convert.ToString(arrSetting[i, 0]) == Convert.ToString("��������� �������� �� ��"))
                        {
                            m_countChanel = Convert.ToInt32(arrSetting[i, 1]);
                        }
                    }

                    if (m_countChanel == 0)
                    {
                        MessageBox.Show("����������� ��������� ��������� '���������� �������' ��� �������� '��������� �������� �� ��'", "������!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                }
                else
                {
                    MessageBox.Show("�� ������� ��������� '���������� �������'", "������!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                if (m_countChanel < 1)
                {
                    m_countChanel = 1;
                }
                if (m_countChanel > 7)
                {
                    m_countChanel = 7;
                }

                for (int i = 0; i < m_arrChannelLabel.Length; i++)
                {
                    if (i < m_countChanel)
                        m_arrChannelLabel[i].Visible = true;
                    else
                        m_arrChannelLabel[i].Visible = false;
                }

                m_needToUseAsyncChannels = true;
            }
            else
            {
                lblChannel.Visible = false;
                lblResult.Visible = false;
                m_needToUseAsyncChannels = false;

                for (int i = 0; i < m_arrChannelLabel.Length; i++)
                {
                    m_arrChannelLabel[i].Visible = false;
                }
            }

            dtLoadDate.DateValue = GetCurrentDate();

            var nameAction = "UBS_PLCARD_FILE_INP_OUT_INIT";

            var arrStr = m_command.Split(',');

            if (arrStr is null)
            {
                MessageBox.Show("�������� ������� �������� �� ���������", "������!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            else
            {
                if (arrStr.Length > 1)
                {
                    if (arrStr[1] != string.Empty)
                    {
                        nameAction = arrStr[1].Trim();
                    }
                }
            }

            var kvpSourceMethod = GetScriptByName(nameAction);

            m_scripter = base.Ubs_VBScriptRunner();

            m_scripter.LoadFiles(kvpSourceMethod.Key);

            var scriptParameters = new UbsParam();

            scriptParameters.Value("StrCommand", m_command);

            m_scripter.UbsScriptParam = scriptParameters;

            m_scripter.Run(kvpSourceMethod.Value);

            scriptParameters = m_scripter.UbsScriptParam;

            if (!scriptParameters.Contains("InOut") || Convert.ToString(scriptParameters["InOut"]) == string.Empty)
            {
                throw new Exception("InitFile");
            }

            linkFileLoad.Text = Convert.ToString(scriptParameters["LabelFile"]);
            lblDateLoad.Text = Convert.ToString(scriptParameters["LabelDate"]);
            if (Convert.ToString(scriptParameters["InOut"]) == "In")
            {
                btnSave.Text = "���������";
                m_inOutText = "In";
            }
            else
            {
                btnSave.Text = "���������";
                m_inOutText = "Out";
            }

            if (Convert.ToBoolean(scriptParameters["NeedDateOper"]) == true)
            {
                lblDateLoad.Visible = true;
                dtLoadDate.Visible = true;
            }
            else
            {
                lblDateLoad.Visible = false;
                dtLoadDate.Visible = false;
            }
            if (scriptParameters.Contains("ArrayParams") && scriptParameters["ArrayParams"].ToString() == "UBS_VBS_PLCARD_FILE_OUT_SRV_2")
            {
                lblResult.Visible = false;
                lblMode.Visible = true;
                cmbMode.Visible = true;

                cmbMode.Items.Clear();

                var list = new List<KeyValuePair<int, string>>();

                list.Add(new KeyValuePair<int, string>(0, "��� ��������"));
                list.Add(new KeyValuePair<int, string>(1, "�������� �������� ����� ����, ������� ������� ��������"));
                list.Add(new KeyValuePair<int, string>(2, "�������� �������� �������� ����� ����"));

                InitComboBox(cmbMode, list);

                cmbMode.SelectedIndex = 0;
            }

            m_sidAction = Convert.ToString(scriptParameters["SID_Action"]);
            m_sidScript = Convert.ToString(scriptParameters["SID_Script"]);
            m_nameFunction = Convert.ToString(scriptParameters["NameFunction"]);

            m_arrayParams = null;

            if (scriptParameters.Contains("ArrayParams"))
            {
                m_arrayParams = scriptParameters["ArrayParams"];
            }

            var arrProcTmp = scriptParameters["ProcList"] as object[,];

            var arrProc = Transpose(arrProcTmp);

            var arrProcPathTmp = scriptParameters["FileOutInPath"] as object[,];

            m_arrProcPath = Transpose(arrProcPathTmp);

            var listTmp = new List<KeyValuePair<int, string>>();

            listTmp.Add(new KeyValuePair<int, string>(0, "(�� ���������)"));

            if (arrProc != null)
            {
                for (int i = 0; i < arrProc.GetLength(0); i++)
                {
                    listTmp.Add(new KeyValuePair<int, string>(
                          Convert.ToInt32(arrProc[i, 0])
                        , Convert.ToString(arrProc[i, 1])));
                }

                InitComboBox(cmbProcessing, listTmp);

                cmbProcessing.SelectedIndex = 0;
            }

            base.IUbsChannel.ParamIn("NameSection", "����������� �����");
            base.IUbsChannel.ParamIn("NameSetting", "����� � �������� ������");
            base.IUbsChannel.ParamIn("TypeSetting", 1);
            base.IUbsChannel.ParamIn("StrTypeSetting", "SS");

            base.IUbsChannel.Run("GetUbsSetting");

            var arrPath = base.IUbsChannel.ParamOut("DataSetting") as object[,];

            m_pathFile = Convert.ToString(scriptParameters["SetupPathFile"]);

            base.IUbsChannel.Run("GetUserDivision");

            m_division = Convert.ToInt32(base.IUbsChannel.ParamOut("����� ���������"));

            txtPathFile.Text = GetSettingFilePath(m_pathFile, arrPath, m_division);

            arrProcPathTmp = null;
            if (m_arrProcPath is null)
            {
                if (arrPath != null)
                {
                    arrProcPathTmp = new object[1, 2];
                }
            }
            else
            {
                if (arrPath != null)
                {
                    arrProcPathTmp = new object[m_arrProcPath.GetLength(0) + 1, 2];

                    for (int i = 0; i < m_arrProcPath.GetLength(0); i++)
                    {
                        for (int j = 0; j < arrProcPathTmp.GetLength(1); j++)
                        {
                            arrProcPathTmp[i, j] = m_arrProcPath[i, j];
                        }
                    }
                }
            }

            if (arrProcPathTmp != null)
            {
                arrProcPathTmp[arrProcPathTmp.GetLength(0) - 1, 0] = 0;
                arrProcPathTmp[arrProcPathTmp.GetLength(0) - 1, 1] = arrPath;
            }

            m_arrProcPath = arrProcPathTmp;
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

        private static string GetSettingFilePath(string setting, object arrSetting, int DivisionID)
        {
            if (string.IsNullOrEmpty(setting))
                throw new Exception("�� ������ ���������.");

            var settings = arrSetting as object[,];
            if (settings == null)
                return string.Empty;

            string strFilePath = string.Empty;

            int rowCount = settings.GetLength(0);

            for (int i = 0; i < rowCount; i++)
            {
                if (Convert.ToString(settings[i, 0]).Trim() == setting.Trim())
                {
                    var raw = Convert.ToString(settings[i, 2]);
                    if (string.IsNullOrEmpty(raw))
                        continue;

                    var arrTmp = raw.Split(',');

                    for (int j = 0; j < arrTmp.Length; j++)
                    {
                        string strTMP = arrTmp[j].Trim();

                        if (strTMP == "*")
                        {
                            strFilePath = Convert.ToString(settings[i, 1]);
                        }
                        else if (strTMP == DivisionID.ToString())
                        {
                            strFilePath = Convert.ToString(settings[i, 1]);
                            break;
                        }
                    }
                }
            }

            return strFilePath;
        }


        private void InitComboBox(ComboBox cmb, object list)
        {
            cmb.DataSource = list;
            cmb.ValueMember = "Key";
            cmb.DisplayMember = "Value";
        }
        private KeyValuePair<string, string> GetScriptByName(string nameAction)
        {
            base.IUbsChannel.ParamIn("SIDAction", nameAction);

            base.IUbsChannel.Run("GetCommonResourceParam");

            string resource = Convert.ToString(base.IUbsChannel.ParamOut("StrResource"));

            string function = Convert.ToString(base.IUbsChannel.ParamOut("StrParam")).Split('(')[0];

            return new KeyValuePair<string, string>(resource, function);
        }

        private DateTime GetCurrentDate()
        {
            base.IUbsChannel.ParamIn("NameSetting", "Server");
            base.IUbsChannel.Run("GetCommonDate");

            return Convert.ToDateTime(base.IUbsChannel.ParamOut("DataSetting"));
        }

        #endregion

        private void linkFileLoad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_inOutText == "In")
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.CheckFileExists = true;
                    dialog.Multiselect = false;
                    dialog.InitialDirectory = txtPathFile.Text;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (txtPathFile.Text.Trim().Length > 0)
                        {
                            txtPathFile.Text = dialog.FileName;

                            btnSave.Focus();
                        }
                    }
                }
            }
            else
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "������� ��� ��������";
                    dialog.ShowNewFolderButton = true;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        txtPathFile.Text = dialog.SelectedPath;
                    }
                }
            }

        }
        private void cmbProcessing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_idProcessing != Convert.ToInt32(cmbProcessing.SelectedValue))
            {
                if (m_arrProcPath != null)
                {
                    for (int i = 0; i < m_arrProcPath.GetLength(0); i++)
                    {
                        if (Convert.ToInt32(cmbProcessing.SelectedValue) == Convert.ToInt32(m_arrProcPath[i, 0]))
                        {
                            var arrProcPathTmp = m_arrProcPath[i, 1] as object[,];

                            object[,] arrPath = null;

                            if (arrProcPathTmp.GetLength(1) == 2)
                            {
                                arrPath = arrProcPathTmp;
                            }
                            else
                            {
                                arrPath = Transpose(arrProcPathTmp);
                            }

                            txtPathFile.Text = GetSettingFilePath(m_pathFile, arrPath, m_division);
                        }
                    }
                }

                m_idProcessing = Convert.ToInt32(cmbProcessing.SelectedValue);
            }
        }
    }
}