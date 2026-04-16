using System;
using System.Reflection;
using System.Windows.Forms;

namespace UbsBusiness
{
    public partial class FrmCashOrd : Form
    {
        private object[,] m_documentsMatrix;
        private object m_documentsPayload;
        private string m_cashDebitAccount = string.Empty;
        private int m_numDiv;
        private DateTime m_docDate = DateTime.MinValue;
        private bool m_executePressed;

        public object UbsChannelRef { get; set; }
        public object[,] PaymentsData { get; set; }
        public object[,] ContractsData { get; set; }
        public long PaymentId { get; set; }
        public object[,] PaymentIdArray { get; set; }
        public bool AutoExecute { get; set; }

        public bool IsConfirmed { get; private set; }
        public bool LoadedSuccessfully { get; private set; }
        public bool WasCreated { get; private set; }

        public FrmCashOrd()
        {
            InitializeComponent();
        }

        private void FrmCashOrd_Load(object sender, EventArgs e)
        {
            try
            {
                LoadedSuccessfully = LoadContext() && LoadDocuments();
                FillDocuments();

                if (!LoadedSuccessfully)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.btnExit_Click(this, EventArgs.Empty);
                    return;
                }

                if (AutoExecute)
                {
                    ExecuteCashOrder();
                    return;
                }

                btnExecute.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.btnExit_Click(this, EventArgs.Empty);
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            ExecuteCashOrder();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            IsConfirmed = false;
            WasCreated = false;
            this.DialogResult = DialogResult.Cancel;
            this.btnExit_Click(this, EventArgs.Empty);
        }

        private bool LoadContext()
        {
            if (UbsChannelRef == null)
            {
                return false;
            }

            object paramIn = CreateParamObject();
            object paramOut = CreateParamObject();
            if (paramIn == null || paramOut == null)
            {
                return false;
            }

            RunChannel("UtGetGlobalUserData", paramIn, paramOut);
            object datedoc = GetParameter(paramOut, "DATEDOC");
            if (datedoc != null)
            {
                m_docDate = Convert.ToDateTime(datedoc);
            }

            ClearParameters(paramIn);
            ClearParameters(paramOut);
            RunChannel("Ps_FindAccCash1", paramIn, paramOut);

            bool bRetVal = GetBoolParameter(paramOut, "bRetVal");
            if (!bRetVal)
            {
                string strError = Convert.ToString(GetParameter(paramOut, "StrError"));
                if (!string.IsNullOrEmpty(strError))
                {
                    MessageBox.Show(strError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                btnExecute.Enabled = false;
                return false;
            }

            m_cashDebitAccount = Convert.ToString(GetParameter(paramOut, "StrAccCash"));
            object numDiv = GetParameter(paramOut, "NumDiv");
            if (numDiv != null)
            {
                m_numDiv = Convert.ToInt32(numDiv);
            }

            return true;
        }

        private bool LoadDocuments()
        {
            if (UbsChannelRef == null)
            {
                return false;
            }

            object paramIn = CreateParamObject();
            object paramOut = CreateParamObject();
            if (paramIn == null || paramOut == null)
            {
                return false;
            }

            SetParameter(paramIn, "REGIM", 1);
            SetParameter(paramIn, "NUMDIV", m_numDiv);
            SetParameter(paramIn, "DATEDOC", m_docDate);
            SetParameter(paramIn, "DATETRN", m_docDate);
            SetParameter(paramIn, "ACCDB", m_cashDebitAccount);
            SetParameter(paramIn, "VARPAYMENTS", PaymentsData);
            SetParameter(paramIn, "VARCONTRACT", ContractsData);

            RunChannel("UtGetDataCashOrder", paramIn, paramOut);

            bool bRetVal = GetBoolParameter(paramOut, "BRETVAL");
            if (!bRetVal)
            {
                string strError = Convert.ToString(GetParameter(paramOut, "StrError"));
                if (!string.IsNullOrEmpty(strError))
                {
                    MessageBox.Show(strError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                btnExecute.Enabled = false;
                return false;
            }

            m_documentsMatrix = GetParameter(paramOut, "VARDOC") as object[,];
            m_documentsPayload = GetParameter(paramOut, "\u0414\u043e\u043a\u0443\u043c\u0435\u043d\u0442\u044b");
            return true;
        }

        private void FillDocuments()
        {
            lvwDocuments.Items.Clear();
            if (m_documentsMatrix == null)
            {
                return;
            }

            int rowCount = m_documentsMatrix.GetLength(0);
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                ListViewItem item = new ListViewItem(Convert.ToString(m_documentsMatrix[rowIndex, 0]));
                item.SubItems.Add(Convert.ToString(m_documentsMatrix[rowIndex, 1]));
                item.SubItems.Add(FormatMoney(m_documentsMatrix[rowIndex, 2]));
                item.SubItems.Add(Convert.ToString(m_documentsMatrix[rowIndex, 3]));
                lvwDocuments.Items.Add(item);
            }
        }

        private void ExecuteCashOrder()
        {
            if (m_executePressed)
            {
                return;
            }

            m_executePressed = true;
            try
            {
                object paramIn = CreateParamObject();
                object paramOut = CreateParamObject();
                if (paramIn == null || paramOut == null)
                {
                    btnExecute.Enabled = false;
                    this.DialogResult = DialogResult.Cancel;
                    this.btnExit_Click(this, EventArgs.Empty);
                    return;
                }

                SetParameter(paramIn, "VARPAYMENTS", PaymentsData);
                SetParameter(paramIn, "ACCDB", m_cashDebitAccount);
                SetParameter(paramIn, "DATEDOC", m_docDate);
                SetParameter(paramIn, "NUMDIV", m_numDiv);
                SetParameter(paramIn, "IsBySeparPaym", true);
                if (PaymentIdArray != null)
                {
                    SetParameter(paramIn, "IdPayment", PaymentIdArray);
                }
                else
                {
                    SetParameter(paramIn, "IdPayment", PaymentId);
                }
                SetParameter(paramIn, "\u0414\u043e\u043a\u0443\u043c\u0435\u043d\u0442\u044b", m_documentsPayload);

                RunChannel("UtMainCashOrder", paramIn, paramOut);

                bool bRetVal = GetBoolParameter(paramOut, "BRETVAL");
                if (!bRetVal)
                {
                    string strError = Convert.ToString(GetParameter(paramOut, "StrError"));
                    if (!string.IsNullOrEmpty(strError))
                    {
                        MessageBox.Show(strError, "\u0421\u043e\u0437\u0434\u0430\u043d\u0438\u0435 \u043a\u0430\u0441\u0441\u043e\u0432\u044b\u0445 \u043e\u0440\u0434\u0435\u0440\u043e\u0432",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    WasCreated = false;
                    IsConfirmed = false;
                    if (AutoExecute)
                    {
                        this.DialogResult = DialogResult.Cancel;
                        this.btnExit_Click(this, EventArgs.Empty);
                    }
                    return;
                }

                WasCreated = true;
                IsConfirmed = true;
                lvwDocuments.Items.Clear();
                this.DialogResult = DialogResult.OK;
                this.btnExit_Click(this, EventArgs.Empty);
            }
            finally
            {
                m_executePressed = false;
            }
        }

        private static string FormatMoney(object value)
        {
            if (value == null)
            {
                return "0.00";
            }

            try
            {
                return Convert.ToDecimal(value).ToString("0.00");
            }
            catch (Exception)
            {
                return Convert.ToString(value);
            }
        }

        private object CreateParamObject()
        {
            Type type = Type.GetTypeFromProgID("ToolPubs.IUbsParam");
            if (type == null)
            {
                return null;
            }

            return Activator.CreateInstance(type);
        }

        private static void ClearParameters(object param)
        {
            if (param == null)
            {
                return;
            }

            param.GetType().InvokeMember("ClearParameters", BindingFlags.InvokeMethod, null, param, new object[0]);
        }

        private static void SetParameter(object param, string name, object value)
        {
            if (param == null)
            {
                return;
            }

            param.GetType().InvokeMember("Parameter", BindingFlags.SetProperty, null, param, new object[] { name, value });
        }

        private static object GetParameter(object param, string name)
        {
            if (param == null)
            {
                return null;
            }

            return param.GetType().InvokeMember("Parameter", BindingFlags.GetProperty, null, param, new object[] { name });
        }

        private bool GetBoolParameter(object param, string name)
        {
            object value = GetParameter(param, name);
            return value != null && Convert.ToBoolean(value);
        }

        private void RunChannel(string command, object paramIn, object paramOut)
        {
            UbsChannelRef.GetType().InvokeMember("Run", BindingFlags.InvokeMethod, null, UbsChannelRef,
                new object[] { command, paramIn, paramOut });
        }
    }
}
