using System;
using UbsService;
using System.Windows.Forms;

namespace UbsBusiness
{
    /// <summary>
    /// Main payment form.
    /// </summary>
    public partial class UbsPsUtPaymentFrm : UbsFormBase
    {
        #region Shared state

        private string m_command = string.Empty;
        private int m_idPayment;
        private int m_idContract;
        private int m_idClient;
        private bool m_isInitialized;
        private bool m_isSave;
        private bool m_isIPDL;
        private bool m_isTerror;
        private bool m_isChangeContract;
        private bool m_isLic;
        private bool m_isCashier;
        private bool m_isGroup;
        private int m_idGroup;
        private int m_idGroupIncoming;
        private int m_idMainIncoming;
        private bool m_isAddClient;
        private int m_idPaymentDic;
        private int m_idAttributeRecip;
        private string m_checkPayer = string.Empty;
        private bool m_isFR;
        private bool m_isClickSave;
        private string m_sidPattern = string.Empty;
        private int m_idContractOld;
        private bool m_isIsFrmPrn;
        private bool m_isGuest;
        private bool m_isAddparam;
        private bool m_calledFromFrontOffice;
        private int m_foSettingValue = -1;
        private int m_fromFoAsClient = -1;
        private string m_docNumber = string.Empty;
        private string m_docSeries = string.Empty;
        private string m_RegNumber = string.Empty;
        private string m_prefCalcRate = string.Empty;
        private bool m_contractFilterLimitations;
        private string m_searchTemplate = string.Empty;
        private string m_searchKBK = string.Empty;
        private string m_searchBIK = string.Empty;
        private bool m_isCodeEnter;
        private bool m_outerSystemInfo;
        private string m_commandSource = string.Empty;
        private bool m_isCheckIncoming;
        private int m_idPaymentCopy;
        private string m_ourBankBic = string.Empty;
        private DateTime m_dateBeg = DateTime.MinValue;
        private DateTime m_dateEnd = DateTime.MinValue;
        private int m_idTariff;
        private int m_idPhone;
        private bool m_isEndGroup;
        private bool m_isErrorKey;
        private bool m_isSourceMeansVisible;
        private bool m_isSourceMeans;
        private bool m_isAlready;
        private bool m_isPenyPresent;
        private bool m_isComissPeniPayer;
        private bool m_isRegimCashSymb;
        private decimal m_curSumRec;
        private decimal m_curSumRateRec;
        private int m_numTabAddFl = 5;
        private int m_idClientOld;
        private string m_strFIOOld = string.Empty;
        private string m_strAddressOld = string.Empty;
        private string m_strINNOld = string.Empty;
        private object m_arrRateSend;
        private bool m_isAutoPeriodFlag;
        private bool m_forbidTaxStatusChanges;
        private string m_savedTaxStatusValue = string.Empty;
        private string m_bicOld = string.Empty;
        private bool m_isNoMessage;
        private int m_blnSecondPayment;
        private bool m_isPeriodEnable;
        private int m_codeEnergy;
        private object m_varTariff;
        private bool m_isForward;

        #endregion

        /// <summary>
        /// Initializes the form.
        /// </summary>
        public UbsPsUtPaymentFrm()
        {
            m_addCommand();

            InitializeComponent();

            this.IUbsChannel.LoadResource = LoadResource;

            base.Ubs_CommandLock = true;

            this.Load += UbsPsUtPaymentFrm_Load;
            this.FormClosing += UbsPsUtPaymentFrm_FormClosing;
        }


        #region Main form events

        private void UbsPsUtPaymentFrm_Load(object sender, EventArgs e)
        {
            m_isInitialized = true;
        }

        private void UbsPsUtPaymentFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CanCloseForm())
            {
                e.Cancel = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e) { this.Close(); }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSave_ClickImpl();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion


        #region IUbs delegates

        /// <summary>
        /// Registers IUbs delegates handled by this form.
        /// </summary>
        private void m_addCommand()
        {
            base.Ubs_AddName(new UbsDelegate(CommandLine));
            base.Ubs_AddName(new UbsDelegate(ListKey));
        }
        /// <summary>
        /// Handles the `CommandLine` IUbs callback.
        /// </summary>
        /// <param name="param_in">Input parameters.</param>
        /// <param name="param_out">Output parameters.</param>
        /// <returns></returns>
        private object CommandLine(object param_in, ref object param_out)
        {
            m_command = param_in != null ? Convert.ToString(param_in) : string.Empty;

            return null;
        }
        /// <summary>
        /// Handles the ListKey IUbs callback.
        /// Corresponds to VB6 UBSChild_ParamInfo("InitParamForm"):
        /// resets state, routes by m_command, calls InitDoc or AddProcInit.
        /// </summary>
        private object ListKey(object param_in, ref object param_out)
        {
            try
            {
                object[] itemArray = (param_in is object[]) ? (object[])param_in : null;
                bool isRecordsExist = (itemArray != null && itemArray.Length > 0);

                m_commandSource = m_command;

                m_idContract = 0;
                m_isChangeContract = false;
                m_isGroup = false;
                m_idGroup = 0;
                m_isAddClient = false;
                m_idPayment = 0;
                m_isAddparam = false;
                m_calledFromFrontOffice = false;
                m_foSettingValue = -1;

                if (string.Equals(m_commandSource, StrCommandAdd, StringComparison.Ordinal)
                    || string.Equals(m_commandSource, StrCommandGroupAdd, StringComparison.Ordinal)
                    || string.Equals(m_commandSource, StrCommandGroupProceed, StringComparison.Ordinal)
                    || string.Equals(m_commandSource, StrCommandAddFromClient, StringComparison.Ordinal))
                {
                    ListKey_Add(itemArray, isRecordsExist);
                }
                else if (string.Equals(m_commandSource, StrCommandAddIncoming, StringComparison.Ordinal))
                {
                    ListKey_AddIncoming(itemArray);
                }
                else if (string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal))
                {
                    ListKey_AddParam(itemArray);
                }
                else if (string.Equals(m_commandSource, StrCommandView, StringComparison.Ordinal)
                    || string.Equals(m_commandSource, StrCommandGroupView, StringComparison.Ordinal))
                {
                    ListKey_View(itemArray, isRecordsExist);
                }
                else if (string.Equals(m_commandSource, StrCommandCopy, StringComparison.Ordinal))
                {
                    ListKey_Copy(itemArray, isRecordsExist);
                }
                else if (string.Equals(m_commandSource, StrCommandChangePart, StringComparison.Ordinal)
                    || string.Equals(m_commandSource, StrCommandGroupChange, StringComparison.Ordinal)
                    || string.Equals(m_commandSource, StrCommandChangePartIncoming, StringComparison.Ordinal))
                {
                    ListKey_ChangePart(itemArray, isRecordsExist);
                }
                else
                {
                    if (isRecordsExist)
                    {
                        m_idPayment = Convert.ToInt32(itemArray[0]);
                    }

                    InitDoc();
                }

                lblCommonAmount.Visible = true;
                udcCommonAmount.Visible = true;
                udcTotalAmount.Visible = true;

                if (txtPayerFullName.Enabled)
                {
                    txtPayerFullName.Focus();
                }

                return null;
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); return null; }
        }

        #region ListKey command branches

        /// <summary>ADD / GROUP_ADD / GROUP_PROCEED / ADD_FROM_CLIENT</summary>
        private void ListKey_Add(object[] itemArray, bool isRecordsExist)
        {
            if (string.Equals(m_commandSource, StrCommandAddFromClient, StringComparison.Ordinal))
            {
                if (isRecordsExist)
                {
                    m_idClient = Convert.ToInt32(itemArray[0]);
                    if (itemArray.Length >= 3)
                    {
                        m_calledFromFrontOffice = Convert.ToBoolean(itemArray[1]);
                        m_foSettingValue = Convert.ToInt32(itemArray[1]);
                        txtPayerAccount.Text = Convert.ToString(itemArray[2]);
                    }
                    if (itemArray.Length >= 4)
                    {
                        m_fromFoAsClient = Convert.ToInt32(itemArray[3]) + 1;
                    }
                }
                else
                {
                    MessageBox.Show(MsgNotBankClient, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                m_isAddClient = true;
                m_command = StrCommandAdd;
            }

            if (string.Equals(m_commandSource, StrCommandGroupProceed, StringComparison.Ordinal))
            {
                if (isRecordsExist)
                {
                    m_idPayment = Convert.ToInt32(itemArray[0]);
                }
                else
                {
                    MessageBox.Show(MsgPaymentNotSelectedForAdd, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
            }

            if (string.Equals(m_commandSource, StrCommandGroupAdd, StringComparison.Ordinal)
                || string.Equals(m_commandSource, StrCommandGroupProceed, StringComparison.Ordinal))
            {
                m_isGroup = true;
                if (string.Equals(m_commandSource, StrCommandGroupProceed, StringComparison.Ordinal))
                {
                    GetGroupIDByPaymentID(Convert.ToInt32(itemArray[0]));
                    if (m_idGroup == -1)
                    {
                        MessageBox.Show(MsgPaymentCancelled, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }
                }
                m_command = StrCommandAdd;
            }

            AddProcInit();
            IsAutoPeriod();
        }

        /// <summary>ADD_INCOMING</summary>
        private void ListKey_AddIncoming(object[] itemArray)
        {
            m_command = StrCommandAdd;

            if (itemArray != null && itemArray.Length >= 2)
            {
                m_idGroupIncoming = Convert.ToInt32(itemArray[0]);
                m_idMainIncoming = Convert.ToInt32(itemArray[1]);
            }

            this.Text = CaptionGroupInputPrefix + m_idGroupIncoming;

            AddProcInit();

            if (!m_isCheckIncoming)
            {
                return;
            }

            UpdateGroupInfo();
            IsAutoPeriod();
        }

        /// <summary>ADD_PARAM � fills controls from parameter tuples in itemArray.</summary>
        private void ListKey_AddParam(object[] itemArray)
        {
            m_isAddparam = true;
            m_command = StrCommandAdd;

            AddProcInit();
            ProcessAddParam(itemArray);
            IsAutoPeriod();
        }

        /// <summary>VIEW / GROUP_VIEW</summary>
        private void ListKey_View(object[] itemArray, bool isRecordsExist)
        {
            if (isRecordsExist)
            {
                m_idPayment = Convert.ToInt32(itemArray[0]);
            }
            else
            {
                MessageBox.Show(MsgPaymentNotSelected, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (string.Equals(m_commandSource, StrCommandGroupView, StringComparison.Ordinal))
            {
                m_isGroup = true;
                m_command = StrCommandView;
            }

            InitDoc();
        }

        /// <summary>COPY � AddProcInit first, then InitDoc with COPY, revert to ADD.</summary>
        private void ListKey_Copy(object[] itemArray, bool isRecordsExist)
        {
            m_command = StrCommandAdd;
            AddProcInit();

            if (isRecordsExist)
            {
                m_idPayment = Convert.ToInt32(itemArray[0]);
            }
            else
            {
                MessageBox.Show(MsgPaymentNotSelected, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            m_command = StrCommandCopy;
            InitDoc();
            m_command = StrCommandAdd;
        }

        /// <summary>CHANGE_PART / GROUP_CHANGE / CHANGE_PART_INCOMING</summary>
        private void ListKey_ChangePart(object[] itemArray, bool isRecordsExist)
        {
            if (isRecordsExist)
            {
                m_idPayment = Convert.ToInt32(itemArray[0]);
            }
            else
            {
                MessageBox.Show(MsgPaymentNotSelected, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (string.Equals(m_commandSource, StrCommandGroupChange, StringComparison.Ordinal))
            {
                m_isGroup = true;
                m_command = StrCommandChangePart;
            }

            if (string.Equals(m_commandSource, StrCommandChangePartIncoming, StringComparison.Ordinal))
            {
                if (itemArray.Length >= 3)
                {
                    m_idGroupIncoming = Convert.ToInt32(itemArray[1]);
                    m_idMainIncoming = Convert.ToInt32(itemArray[2]);
                }
            }

            InitDoc();

            if (m_idGroupIncoming > 0)
            {
                UpdateGroupInfo();
            }
        }

        #endregion

        #endregion

        private void linkPayerFullName_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkFindFilter_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkContractCode_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkRecipientBankName_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkPaymentAccount_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
