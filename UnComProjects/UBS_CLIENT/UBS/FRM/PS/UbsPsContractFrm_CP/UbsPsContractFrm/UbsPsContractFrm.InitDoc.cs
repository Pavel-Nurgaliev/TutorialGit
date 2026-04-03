using System;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    public partial class UbsPsContractFrm
    {
        private sealed class ContractComboItem
        {
            public readonly int Id;
            public readonly string Caption;
            public ContractComboItem(int id, string caption)
            {
                Id = id;
                Caption = caption ?? string.Empty;
            }
            public override string ToString()
            {
                return Caption;
            }
        }

        private void InitDoc()
        {
            try
            {
                m_idClient = 0;
                m_idKind = string.Empty;
                m_blnArbitrary = false;
                m_blnMayBeArbitrary = false;
                m_blnIsPublicPayments = true;

                base.IUbsChannel.Run("InitFormContract");

                var initOut = base.IUbsChannel.ParamsOutParam;

                m_strOurBik = initOut.Contains("BIKBANK") ? Convert.ToString(initOut.Value("BIKBANK")) : string.Empty;
                m_nIdOiDefault = initOut.Contains("nIdOI") ? Convert.ToInt32(initOut.Value("nIdOI")) : 0;
                object arrExecutors = initOut.Contains("varExecutors") ? initOut.Value("varExecutors") : null;
                object varState = initOut.Contains("VARSTATE") ? initOut.Value("VARSTATE") : null;

                base.UbsInit();

                FillCommissionTypeCombos(varState);

                SetupContractStatusComboVisibility();

                if (string.Equals(m_command, CmdEdit, StringComparison.Ordinal))
                {
                    base.UbsChannel_ParamIn("IDCONTRACT", m_idContract);
                    base.UbsChannel_ParamIn("STRCOMMAND", ActionRead);
                    base.UbsChannel_Run("Contract");
                    UbsParam contractOut = new UbsParam(base.UbsChannel_ParamsOut);

                    int idExecutor = contractOut.Contains("nIdOI") ? Convert.ToInt32(contractOut.Value("nIdOI")) : 0;

                    ApplyContractReadParameters(contractOut);

                    if (contractOut.Contains("Метод расчета комиссии с получателя")
                        && string.Equals(Convert.ToString(contractOut.Value("Метод расчета комиссии с получателя")), "Обратный", StringComparison.Ordinal))
                    {
                        chkRecipientCommissionReverse.Checked = true;
                    }
                    else
                    {
                        chkRecipientCommissionReverse.Checked = false;
                    }

                    if (contractOut.Contains("TXTBIC"))
                    {
                        udcRecipientBik.Text = Convert.ToString(contractOut.Value("TXTBIC"));
                    }
                    if (contractOut.Contains("TXTINN"))
                    {
                        udcRecipientInn.Text = Convert.ToString(contractOut.Value("TXTINN"));
                    }

                    if (string.IsNullOrEmpty(udcRecipientBik.Text.Trim()))
                    {
                        m_blnArbitrary = true;
                        lblArbitraryContract.Visible = true;
                        udcRecipientBik.Enabled = false;
                        ucaRecipientAccount.Enabled = false;
                        linkRecipientClient.Enabled = false;
                        btnRecipientClientClear.Enabled = false;
                    }
                    else
                    {
                        m_blnMayBeArbitrary = false;
                    }

                    if (contractOut.Contains("STATE"))
                    {
                        SetContractStatusSelected(Convert.ToInt32(contractOut.Value("STATE")));
                    }

                    ucdContractClose.Visible = false;
                    lblCloseDate.Visible = false;
                    ContractComboItem stSel = cmbContractStatus.SelectedItem as ContractComboItem;
                    if (stSel != null && stSel.Id == 1)
                    {
                        ucdContractClose.Visible = true;
                        lblCloseDate.Visible = true;
                        if (contractOut.Contains("DATECLOSE"))
                        {
                            ucdContractClose.DateValue = Convert.ToDateTime(contractOut.Value("DATECLOSE"));
                        }
                    }

                    base.IUbsChannel.ParamIn("IDKINDPAYMENT", m_idKind);
                    base.IUbsChannel.ParamIn("STRCOMMAND", ActionRead);
                    base.IUbsChannel.Run("ReadKind");

                    var kindOut = base.IUbsChannel.ParamsOutParam;
                    if (kindOut.Contains("StrError") && kindOut.Value("StrError") != null
                        && !string.IsNullOrEmpty(Convert.ToString(kindOut.Value("StrError")).Trim()))
                    {
                        base.Ubs_ShowErrorBox(Convert.ToString(kindOut.Value("StrError")));
                    }
                    else
                    {
                        if (kindOut.Contains("TXTCODE"))
                        {
                            txtPaymentKindCode.Text = Convert.ToString(kindOut.Value("TXTCODE"));
                        }
                        if (kindOut.Contains("TXTCOMMENT"))
                        {
                            txtPaymentKindComment.Text = Convert.ToString(kindOut.Value("TXTCOMMENT"));
                        }
                        if (kindOut.Contains("IDKINDPAYMENT"))
                        {
                            m_idKind = Convert.ToString(kindOut.Value("IDKINDPAYMENT"));
                        }
                    }

                    base.IUbsChannel.ParamIn("IDCLIENT", m_idClient);
                    base.IUbsChannel.ParamIn("STRCOMMAND", ActionRead);
                    base.IUbsChannel.Run("ReadClient");

                    var clientOut = base.IUbsChannel.ParamsOutParam;
                    if (clientOut.Contains("StrError") && clientOut.Value("StrError") != null
                        && !string.IsNullOrEmpty(Convert.ToString(clientOut.Value("StrError")).Trim()))
                    {
                        base.Ubs_ShowErrorBox(Convert.ToString(clientOut.Value("StrError")));
                    }
                    else if (clientOut.Contains("NAME"))
                    {
                        txtRecipientClient.Text = Convert.ToString(clientOut.Value("NAME"));
                    }

                    FillExecutors(arrExecutors, idExecutor);
                }
                else
                {
                    ClearCommissionFieldsForAdd();
                    ucdContract.DateValue = DateTime.Now;
                    FillExecutors(arrExecutors, m_nIdOiDefault);
                }

                EnableSumCommissionControls();

                ucfAdditionalFields.Refresh();
                GetBankNameAccStub();
                EnableFieldsClStub();

                if (string.Equals(m_command, CmdAdd, StringComparison.Ordinal))
                {
                    ClearBankFields();
                }

                lblExecutor.Visible = true;
                cmbExecutor.Visible = true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void SetupContractStatusComboVisibility()
        {
            lblContractStatus.Visible = m_blnIsPublicPayments;
            cmbContractStatus.Visible = m_blnIsPublicPayments;
            cmbContractStatus.Items.Clear();
            cmbContractStatus.Items.Add(new ContractComboItem(0, "открыт"));
            cmbContractStatus.Items.Add(new ContractComboItem(1, "закрыт"));
            cmbContractStatus.SelectedIndex = 0;
        }

        private void SetContractStatusSelected(int stateId)
        {
            for (int i = 0; i < cmbContractStatus.Items.Count; i++)
            {
                ContractComboItem it = cmbContractStatus.Items[i] as ContractComboItem;
                if (it != null && it.Id == stateId)
                {
                    cmbContractStatus.SelectedIndex = i;
                    return;
                }
            }
        }

        private void FillCommissionTypeCombos(object varState)
        {
            cmbPayerCommissionType.Items.Clear();
            cmbRecipientCommissionType.Items.Clear();
            object[,] arr = varState as object[,];
            if (arr == null)
            {
                return;
            }
            int n = arr.GetLength(1);
            for (int i = 0; i < n; i++)
            {
                int id = Convert.ToInt32(arr[0, i]);
                string cap = Convert.ToString(arr[1, i]);
                cmbPayerCommissionType.Items.Add(new ContractComboItem(id, cap));
                cmbRecipientCommissionType.Items.Add(new ContractComboItem(id, cap));
            }
            if (cmbPayerCommissionType.Items.Count > 0)
            {
                cmbPayerCommissionType.SelectedIndex = 0;
            }
            if (cmbRecipientCommissionType.Items.Count > 0)
            {
                cmbRecipientCommissionType.SelectedIndex = 0;
            }
        }

        private void SetCommissionComboByTypeId(ComboBox combo, int typeId)
        {
            for (int i = 0; i < combo.Items.Count; i++)
            {
                ContractComboItem it = combo.Items[i] as ContractComboItem;
                if (it != null && it.Id == typeId)
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }
        }

        private void ApplyContractReadParameters(UbsParam contractOut)
        {
            if (contractOut.Contains("INTIDCLIENT"))
            {
                m_idClient = Convert.ToInt32(contractOut.Value("INTIDCLIENT"));
            }
            if (contractOut.Contains("TXTKIND"))
            {
                m_idKind = Convert.ToString(contractOut.Value("TXTKIND"));
            }
            if (contractOut.Contains("TXTCODE"))
            {
                txtContractCode.Text = Convert.ToString(contractOut.Value("TXTCODE"));
            }
            if (contractOut.Contains("TXTCOMMENT"))
            {
                txtComment.Text = Convert.ToString(contractOut.Value("TXTCOMMENT"));
            }
            if (contractOut.Contains("TXTNUM"))
            {
                txtContractNumber.Text = Convert.ToString(contractOut.Value("TXTNUM"));
            }
            if (contractOut.Contains("DATECONTRACT"))
            {
                ucdContract.DateValue = Convert.ToDateTime(contractOut.Value("DATECONTRACT"));
            }
            if (contractOut.Contains("TXTADRESS"))
            {
                txtRecipientAddress.Text = Convert.ToString(contractOut.Value("TXTADRESS"));
            }
            if (contractOut.Contains("TXTACC"))
            {
                ucaRecipientAccount.Text = Convert.ToString(contractOut.Value("TXTACC"));
            }
            if (contractOut.Contains("TXTACCCORR"))
            {
                ucaCorrespondentAccount.Text = Convert.ToString(contractOut.Value("TXTACCCORR"));
            }
            if (contractOut.Contains("TXTNAMEBANK"))
            {
                txtBankName.Text = Convert.ToString(contractOut.Value("TXTNAMEBANK"));
            }
            if (contractOut.Contains("CURPERCENTSEND"))
            {
                udcPayerCommissionPercent.Text = Convert.ToString(contractOut.Value("CURPERCENTSEND"));
            }
            if (contractOut.Contains("CURPERCENTREC"))
            {
                udcRecipientCommissionPercent.Text = Convert.ToString(contractOut.Value("CURPERCENTREC"));
            }
            if (contractOut.Contains("INTTYPESEND"))
            {
                SetCommissionComboByTypeId(cmbPayerCommissionType, Convert.ToInt32(contractOut.Value("INTTYPESEND")));
            }
            if (contractOut.Contains("INTTYPEREC"))
            {
                SetCommissionComboByTypeId(cmbRecipientCommissionType, Convert.ToInt32(contractOut.Value("INTTYPEREC")));
            }
        }

        private void FillExecutors(object arrExecutors, int idExecutor)
        {
            cmbExecutor.Items.Clear();
            object[,] arr = arrExecutors as object[,];
            if (arr == null)
            {
                return;
            }
            int n = arr.GetLength(1);
            int selectIndex = 0;
            for (int i = 0; i < n; i++)
            {
                int id = Convert.ToInt32(arr[0, i]);
                string name = Convert.ToString(arr[1, i]);
                cmbExecutor.Items.Add(new ContractComboItem(id, name));
                if (id == idExecutor)
                {
                    selectIndex = i;
                }
            }
            if (cmbExecutor.Items.Count > 0)
            {
                cmbExecutor.SelectedIndex = selectIndex;
            }
        }

        private void ClearBankFields()
        {
            txtRecipientClient.Text = string.Empty;
            udcRecipientBik.Text = string.Empty;
            ucaCorrespondentAccount.Text = "00000000000000000000";
            txtBankName.Text = string.Empty;
            ucaRecipientAccount.Text = "00000000000000000000";
            udcRecipientInn.Text = string.Empty;
            txtRecipientAddress.Text = string.Empty;
        }

        private void ClearCommissionFieldsForAdd()
        {
            if (cmbPayerCommissionType.Items.Count > 0)
            {
                cmbPayerCommissionType.SelectedIndex = 0;
            }
            if (cmbRecipientCommissionType.Items.Count > 0)
            {
                cmbRecipientCommissionType.SelectedIndex = 0;
            }
            udcPayerCommissionPercent.Text = "0";
            udcRecipientCommissionPercent.Text = "0";
            chkRecipientCommissionReverse.Checked = false;
        }

        private void GetBankNameAccStub()
        {
            if (m_blnMayBeArbitrary)
            {
            }
        }

        private void EnableFieldsClStub()
        {
            if (m_blnArbitrary)
            {
            }
        }
    }
}
