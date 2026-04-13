using System;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// ������ ������ �����
    /// </summary>
    public partial class UbsPsUtPaymentFrm : UbsFormBase
    {
        #region ���� ���������� ����������

        private string m_command = string.Empty;    //��������� ������� �����
        private int m_idPayment;
        private int m_idContract;
        private int m_idClient;
        private bool m_blnInitialized;
        private bool m_blnSave;
        private bool m_blnIPDL;
        private bool m_blnTerror;
        private string m_strOurBankBic = string.Empty;
        private DateTime m_dateBeg = DateTime.MinValue;
        private DateTime m_dateEnd = DateTime.MinValue;

        #endregion

        /// <summary>
        /// �����������
        /// </summary>
        public UbsPsUtPaymentFrm()
        {
            m_addCommand(); //���������������� ����������� ������ ���������� IUbs

            InitializeComponent();

            // ���������� ��� �������, � ������� ����� �������� �����
            this.IUbsChannel.LoadResource = LoadResource;
            this.Load += new EventHandler(UbsPsUtPaymentFrm_Load);

            base.Ubs_CommandLock = true;
        }


        #region ����������� ������� ������ (� ���������)

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
                //��� ���������� ����������� ������� ListKey
                return null;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        #endregion
    }
}
