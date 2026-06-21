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
                m_idOiDefault = initOut.Contains("nIdOI") ? Convert.ToInt32(initOut.Value("nIdOI")) : 0;
                object arrExecutors = initOut.Contains("varExecutors") ? initOut.Value("varExecutors") : null;
                object arrState = initOut.Contains("VARSTATE") ? initOut.Value("VARSTATE") : null;

                base.UbsInit();

                FillCommissionTypeCombos(arrState);

                SetupContractStatusComboVisibility();

                if (string.Equals(m_command, EditCommand, StringComparison.Ordinal))
                {
                    base.UbsChannel_ParamIn("IDCONTRACT", m_idContract);
                    base.UbsChannel_ParamIn("STRCOMMAND", "READ");
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
                        txtRecipientBik.Text = Convert.ToString(contractOut.Value("TXTBIC"));
                    }
                    if (contractOut.Contains("TXTINN"))
                    {
                        txtRecipientInn.Text = Convert.ToString(contractOut.Value("TXTINN"));
                    }

                    if (string.IsNullOrEmpty(txtRecipientBik.Text.Trim()))
                    {
                        m_blnArbitrary = true;
                        lblArbitraryContract.Visible = true;
                        txtRecipientBik.Enabled = false;
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

                    ApplyContractCloseVisibilityFromStatus();
                    if (contractOut.Contains("DATECLOSE"))
                    {
                        ContractComboItem stSel = cmbContractStatus.SelectedItem as ContractComboItem;
                        if (stSel != null && stSel.Id == 1)
                        {
                            ucdContractClose.DateValue = Convert.ToDateTime(contractOut.Value("DATECLOSE"));
                        }
                    }

                    base.IUbsChannel.ParamIn("IDKINDPAYMENT", m_idKind);
                    base.IUbsChannel.ParamIn("STRCOMMAND", "READ");
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
                    base.IUbsChannel.ParamIn("STRCOMMAND", "READ");
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
                    ucdContract.DateValue = GetCurrentDate();
                    FillExecutors(arrExecutors, m_idOiDefault);
                }

                EnableSumCommissionControls();

                ucfAdditionalFields.Refresh();
                GetBankNameAcc();
                EnableFieldsCl();

                if (string.Equals(m_command, "ADD", StringComparison.Ordinal))
                {
                    ClearBankFields();
                }

                lblExecutor.Visible = true;
                cmbExecutor.Visible = true;

                ApplyContractKindAndCodeEditability();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private DateTime GetCurrentDate()
        {
            base.IUbsChannel.ParamIn("NameSetting", "Server");

            base.IUbsChannel.Run("GetCommonDate");

            return Convert.ToDateTime(base.IUbsChannel.ParamOut("DataSetting"));
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

        private void cmbContractStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyContractCloseVisibilityFromStatus();
        }

        private void ApplyContractCloseVisibilityFromStatus()
        {
            if (!cmbContractStatus.Visible)
            {
                return;
            }
            ContractComboItem it = cmbContractStatus.SelectedItem as ContractComboItem;
            bool closed = it != null && it.Id == 1;
            lblCloseDate.Visible = closed;
            ucdContractClose.Visible = closed;
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
            int n = arr.GetLength(0);
            for (int rowIndex = 0; rowIndex < n; rowIndex++)
            {
                int id = Convert.ToInt32(arr[rowIndex, 0]);
                string cap = Convert.ToString(arr[rowIndex, 1]);
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
            int n = arr.GetLength(0);
            int selectIndex = 0;
            for (int rowIndex = 0; rowIndex < n; rowIndex++)
            {
                int id = Convert.ToInt32(arr[rowIndex, 0]);
                string name = Convert.ToString(arr[rowIndex, 1]);
                cmbExecutor.Items.Add(new ContractComboItem(id, name));
                if (id == idExecutor)
                {
                    selectIndex = rowIndex;
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
            txtRecipientBik.Text = string.Empty;
            ucaCorrespondentAccount.Text = CorrespondentAccountPlaceholder;
            txtBankName.Text = string.Empty;
            ucaRecipientAccount.Text = CorrespondentAccountPlaceholder;
            txtRecipientInn.Text = string.Empty;
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
            udcPayerCommissionPercent.DecimalValue = 0m;
            udcRecipientCommissionPercent.DecimalValue = 0m;
            chkRecipientCommissionReverse.Checked = false;
        }

        private bool GetBankNameAcc()
        {
            bool ok = false;
            try
            {
                string bic = txtRecipientBik.Text != null ? txtRecipientBik.Text.Trim() : string.Empty;
                if (bic.Length > 0)
                {
                    base.IUbsChannel.ParamIn("BIC", bic);
                    base.IUbsChannel.Run("ReadBankBIK");

                    var bankOut = base.IUbsChannel.ParamsOutParam;

                    if (bankOut.Contains("NUM") && Convert.ToInt32(bankOut.Value("NUM")) > 0)
                    {
                        ApplyReadBankBikFields(bankOut);
                        ok = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
            finally
            {
                SetSignOurBik();
            }
            return ok;
        }

        private void SetSignOurBik()
        {
            string cur = txtRecipientBik.Text != null ? txtRecipientBik.Text.Trim() : string.Empty;
            string ours = m_strOurBik != null ? m_strOurBik.Trim() : string.Empty;
            m_blnIsOurBik = cur.Length > 0 && ours.Length > 0
                && string.Equals(cur, ours, StringComparison.Ordinal);
            if (!m_blnArbitrary)
            {
                linkRecipientClient.Enabled = m_blnIsOurBik;
            }
        }

        private void ApplyReadBankBikFields(UbsParam bankOut)
        {
            if (bankOut.Contains("BANKNAME"))
            {
                txtBankName.Text = Convert.ToString(bankOut.Value("BANKNAME"));
            }
            if (bankOut.Contains("CORRACC"))
            {
                ApplyCorrAccountIfPlaceholder(Convert.ToString(bankOut.Value("CORRACC")));
            }
        }

        private void ApplyCorrAccountIfPlaceholder(string corr)
        {
            if (corr == null)
            {
                return;
            }
            string t = ucaCorrespondentAccount.Text != null ? ucaCorrespondentAccount.Text.Trim() : string.Empty;
            if (t.Length == 0 || t == CorrespondentAccountPlaceholder)
            {
                ucaCorrespondentAccount.Text = corr;
            }
        }

        private bool ApplyBankBikPair(string key, string val)
        {
            if (string.Equals(key, "BANKNAME", StringComparison.Ordinal))
            {
                txtBankName.Text = val;
                return true;
            }
            if (string.Equals(key, "CORRACC", StringComparison.Ordinal))
            {
                ApplyCorrAccountIfPlaceholder(val);
                return true;
            }
            return false;
        }

        private void EnableFieldsCl()
        {
            bool allowManualRecipientDetails = m_idClient <= 0;
            txtRecipientInn.Enabled = allowManualRecipientDetails;
            txtRecipientAddress.Enabled = allowManualRecipientDetails;
        }

        private static bool BikStrictGateApplies(bool isPublicPayments, bool isArbitrary, string command)
        {
            return !isPublicPayments || (!isArbitrary && string.Equals(command, EditCommand, StringComparison.Ordinal));
        }

        private bool RecipientAccountHasNonEmptyText()
        {
            string acc = ucaRecipientAccount.Text != null ? ucaRecipientAccount.Text.Trim() : string.Empty;
            return acc.Length > 0;
        }

        private bool RecipientBikOrAccountTriggersBikCheck()
        {
            string bic = txtRecipientBik.Text != null ? txtRecipientBik.Text.Trim() : string.Empty;
            if (bic.Length > 0)
            {
                return true;
            }
            string acc = ucaRecipientAccount.Text != null ? ucaRecipientAccount.Text.Trim() : string.Empty;
            return acc.Length > 0 && acc != CorrespondentAccountPlaceholder;
        }

        private bool ShouldShowBikNotFoundAfterFailedLookup()
        {
            return RecipientBikOrAccountTriggersBikCheck() && RecipientAccountHasNonEmptyText();
        }

        private void OnRecipientBikEnterKey()
        {
            bool strict = BikStrictGateApplies(m_blnIsPublicPayments, m_blnArbitrary, m_command);
            bool ok = GetBankNameAcc();
            if (!ok && strict)
            {
                if (ShouldShowBikNotFoundAfterFailedLookup())
                {
                    MessageBox.Show(this, MsgBikNotFound, MsgBikNotFoundTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabContract.SelectedTab = tabPageMain;
                    if (txtRecipientBik.Enabled)
                    {
                        txtRecipientBik.Focus();
                    }
                }
                return;
            }

            tabContract.SelectedTab = tabPageMain;
            if (txtRecipientInn.Enabled)
            {
                txtRecipientInn.Focus();
            }
            else if (ucaRecipientAccount.Enabled)
            {
                ucaRecipientAccount.Focus();
            }

            ApplyRecipientAccountAfterOurBikRule();
        }

        private void ApplyRecipientAccountAfterOurBikRule()
        {
            if (!m_blnIsOurBik)
            {
                ucaRecipientAccount.Text = CorrespondentAccountPlaceholder;
            }
            else
            {
                SearchOneAccClientIfLinked();
            }
        }

        private void SearchOneAccClientIfLinked()
        {
            if (m_idClient <= 0)
            {
                return;
            }
            try
            {
                base.IUbsChannel.ParamIn("IDCLIENT", m_idClient);
                base.IUbsChannel.Run("SearchAccClient");
                var accOut = base.IUbsChannel.ParamsOutParam;
                if (accOut.Contains("ACCCLIENT"))
                {
                    string acc = Convert.ToString(accOut.Value("ACCCLIENT"));
                    if (acc != null && acc.Trim().Length > 0)
                    {
                        ucaRecipientAccount.Text = acc.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private bool CheckAddFieldExists(string nameField)
        {
            try
            {
                base.IUbsChannel.ParamIn("NAMEFIELD", nameField);
                base.IUbsChannel.Run("CheckExistAddFieldContract");
                UbsParam o = base.IUbsChannel.ParamsOutParam;
                return o.Contains("BLNEXIST") && Convert.ToBoolean(o.Value("BLNEXIST"));
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        private void TrySetUcfFieldFromOutParam(string fieldName, string outKey, UbsParam po)
        {
            if (!CheckAddFieldExists(fieldName) || !po.Contains(outKey))
            {
                return;
            }
            ucfAdditionalFields.Collection[fieldName].Value = po.Value(outKey);
        }

        private void ApplyBrowseSelectedClient(int idClient)
        {
            m_idClient = idClient;
            base.IUbsChannel.ParamIn("IDCLIENT", m_idClient);
            base.IUbsChannel.ParamIn("STRCOMMAND", "READ");
            base.IUbsChannel.Run("ReadClient");
            UbsParam clientOut = base.IUbsChannel.ParamsOutParam;
            string err = clientOut.Contains("StrError") ? Convert.ToString(clientOut.Value("StrError")).Trim() : string.Empty;
            if (err.Length > 0)
            {
                MessageBox.Show(this, err, MsgContractCardTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clientOut.Contains("NAME"))
            {
                txtRecipientClient.Text = Convert.ToString(clientOut.Value("NAME"));
            }
            if (clientOut.Contains("BIC"))
            {
                txtRecipientBik.Text = Convert.ToString(clientOut.Value("BIC"));
            }
            if (clientOut.Contains("ADRESS"))
            {
                txtRecipientAddress.Text = Convert.ToString(clientOut.Value("ADRESS"));
            }
            if (clientOut.Contains("INN"))
            {
                txtRecipientInn.Text = Convert.ToString(clientOut.Value("INN"));
            }
            TrySetUcfFieldFromOutParam("КППУ", "KPPU", clientOut);
            ucfAdditionalFields.Refresh();
            GetBankNameAcc();
            EnableFieldsCl();
            if (m_blnIsOurBik)
            {
                SearchOneAccClientIfLinked();
            }
            linkLabel1.Focus();
        }

        private void ApplyBrowseSelectedAccount(int idAcc)
        {
            base.IUbsChannel.ParamIn("IDACC", idAcc);
            base.IUbsChannel.Run("ReadAcc");
            UbsParam accOut = base.IUbsChannel.ParamsOutParam;
            string err = accOut.Contains("StrError") ? Convert.ToString(accOut.Value("StrError")).Trim() : string.Empty;
            if (err.Length > 0)
            {
                MessageBox.Show(this, err, MsgContractCardTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (accOut.Contains("TXTACC"))
            {
                ucaRecipientAccount.Text = Convert.ToString(accOut.Value("TXTACC"));
            }
            linkRecipientClient.Focus();
        }

        private void ApplyBrowseSelectedKindPayment(int idKindPayment)
        {
            m_idKind = Convert.ToString(idKindPayment);
            base.IUbsChannel.ParamIn("STRCOMMAND", "CHANGEKIND");
            base.IUbsChannel.ParamIn("INTIDKIND", idKindPayment);
            base.IUbsChannel.Run("Contract");
            var changeOut = base.IUbsChannel.ParamsOutParam;
            ucfAdditionalFields.Refresh();
            string errKind;
            if (!TryFillKindFromReadKindChannel(out errKind))
            {
                string msg = errKind;
                if (msg == null || msg.Length == 0)
                {
                    msg = changeOut.Contains("StrError") ? Convert.ToString(changeOut.Value("StrError")).Trim() : string.Empty;
                }
                if (msg.Length > 0)
                {
                    MessageBox.Show(this, msg, MsgContractCardTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            if (cmbContractStatus.Visible)
            {
                cmbContractStatus.Focus();
            }
            else if (txtRecipientClient.Enabled)
            {
                txtRecipientClient.Focus();
            }
            else
            {
                linkLabel1.Focus();
            }
        }

        private bool TryFillKindFromReadKindChannel(out string errorMessage)
        {
            errorMessage = string.Empty;
            base.IUbsChannel.ParamIn("IDKINDPAYMENT", m_idKind);
            base.IUbsChannel.ParamIn("STRCOMMAND", "READ");
            base.IUbsChannel.Run("ReadKind");
            UbsParam kindOut = base.IUbsChannel.ParamsOutParam;
            if (kindOut.Contains("StrError"))
            {
                errorMessage = Convert.ToString(kindOut.Value("StrError")).Trim();
                if (errorMessage.Length > 0)
                {
                    return false;
                }
            }
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
            if (kindOut.Contains("INTTYPESEND"))
            {
                SetCommissionComboByTypeId(cmbPayerCommissionType, Convert.ToInt32(kindOut.Value("INTTYPESEND")));
            }
            if (kindOut.Contains("INTTYPEREC"))
            {
                SetCommissionComboByTypeId(cmbRecipientCommissionType, Convert.ToInt32(kindOut.Value("INTTYPEREC")));
            }
            if (kindOut.Contains("CURPERCENTSEND"))
            {
                udcPayerCommissionPercent.Text = Convert.ToString(kindOut.Value("CURPERCENTSEND"));
            }
            if (kindOut.Contains("CURPERCENTREC"))
            {
                udcRecipientCommissionPercent.Text = Convert.ToString(kindOut.Value("CURPERCENTREC"));
            }
            if (kindOut.Contains("MayBeArbitrary"))
            {
                m_blnMayBeArbitrary = Convert.ToBoolean(kindOut.Value("MayBeArbitrary"));
            }
            int typeSend = kindOut.Contains("INTTYPESEND") ? Convert.ToInt32(kindOut.Value("INTTYPESEND")) : -1;
            int typeRec = kindOut.Contains("INTTYPEREC") ? Convert.ToInt32(kindOut.Value("INTTYPEREC")) : -1;
            if (typeSend == 3)
            {
                TrySetUcfFieldFromOutParam("Комиссии с плательщика-тарифная сетка", "Комиссии с плательщика-тарифная сетка", kindOut);
                TrySetUcfFieldFromOutParam("Комиссии с плательщика-признак ставки", "Комиссии с плательщика-признак ставки", kindOut);
            }
            if (typeRec == 3)
            {
                TrySetUcfFieldFromOutParam("Комиссии с получателя-тарифная сетка", "Комиссии с получателя-тарифная сетка", kindOut);
                TrySetUcfFieldFromOutParam("Комиссии с получателя-признак ставки", "Комиссии с получателя-признак ставки", kindOut);
            }
            TrySetUcfFieldFromOutParam("Доп. параметры комиссии с получателя", "VARCOMMISREC", kindOut);
            TrySetUcfFieldFromOutParam("Доп. параметры комиссии с плательщика", "VARCOMMISSEND", kindOut);
            ucfAdditionalFields.Refresh();
            EnableSumCommissionControls();
            return true;
        }

        private bool TryValidateBikForSave()
        {
            if (!RecipientBikOrAccountTriggersBikCheck())
            {
                return true;
            }
            if (!RecipientAccountHasNonEmptyText())
            {
                return true;
            }
            bool strict = BikStrictGateApplies(m_blnIsPublicPayments, m_blnArbitrary, m_command);
            bool ok = GetBankNameAcc();
            if (!ok && strict)
            {
                if (ShouldShowBikNotFoundAfterFailedLookup())
                {
                    MessageBox.Show(this, MsgBikNotFound, MsgBikNotFoundTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                tabContract.SelectedTab = tabPageMain;
                if (txtRecipientBik.Enabled)
                {
                    txtRecipientBik.Focus();
                }
                return false;
            }
            return true;
        }
    }
}
