using System;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    public partial class UbsPsContractFrm
    {
        private static readonly DateTime ContractSentinelEmptyDate = new DateTime(2222, 1, 1);

        private void ExecuteSaveContract()
        {
            ApplyPreSaveArbitraryContractFlags();
            if (!TryValidateBikForSave())
            {
                return;
            }
            if (!TryValidateCloseDateIfContractClosed())
            {
                return;
            }
            if (m_blnArbitrary)
            {
                ApplyArbitraryAddFieldDefaults();
            }
            FillDependFieldsBeforeSave();
            if (!TryValidateContractBusinessRules())
            {
                return;
            }
            ApplyContractSaveParamInForReadF();
            base.UbsChannel_ParamIn("STRCOMMAND", "READF");
            base.UbsChannel_Run("Contract");
            UbsParam readFOut = new UbsParam(base.UbsChannel_ParamsOut);
            int idFromReadF = readFOut.Contains("IDCONTRACT") ? Convert.ToInt32(readFOut.Value("IDCONTRACT")) : 0;
            if (idFromReadF != m_idContract)
            {
                MessageBox.Show(this, MsgDuplicateContractCode, MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageMain;
                if (txtContractCode.Enabled)
                {
                    txtContractCode.Focus();
                }
                return;
            }
            string commandBeforeSave = m_command;
            ApplyContractSaveParamInForPersist(commandBeforeSave);
            base.UbsChannel_Run("Contract");
            UbsParam saveOut = new UbsParam(base.UbsChannel_ParamsOut);
            string saveErr = GetContractSaveErrorText(saveOut);
            if (saveErr.Length > 0)
            {
                uciContract.Text = saveErr;
                uciContract.Show();
                if (txtContractCode.Enabled)
                {
                    txtContractCode.Focus();
                }
                return;
            }
            if (!string.Equals(commandBeforeSave, EditCommand, StringComparison.Ordinal))
            {
                m_command = EditCommand;
                if (saveOut.Contains("IDCONTRACT"))
                {
                    m_idContract = Convert.ToInt32(saveOut.Value("IDCONTRACT"));
                }
                ucfAdditionalFields.Refresh();
            }
            ApplyContractKindAndCodeEditability();
            uciContract.Text = MsgContractSaved;
            uciContract.Show();
        }

        private void ApplyPreSaveArbitraryContractFlags()
        {
            string bic = txtRecipientBik.Text != null ? txtRecipientBik.Text.Trim() : string.Empty;
            if (bic.Length == 0 && m_blnIsPublicPayments && m_idClient <= 0 && m_blnMayBeArbitrary)
            {
                m_blnArbitrary = true;
                lblArbitraryContract.Visible = true;
            }
        }

        private bool TryValidateCloseDateIfContractClosed()
        {
            if (!cmbContractStatus.Visible)
            {
                return true;
            }
            ContractComboItem st = cmbContractStatus.SelectedItem as ContractComboItem;
            if (st == null || st.Id != 1)
            {
                return true;
            }
            if (ucdContractClose.DateValue.Date == ContractSentinelEmptyDate.Date)
            {
                MessageBox.Show(this, MsgCloseDateRequired, MsgCloseDateTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ucdContractClose.Focus();
                return false;
            }
            return true;
        }

        private void ApplyArbitraryAddFieldDefaults()
        {
            try
            {
                TrySetAddFieldCollectionValue("Отдельный документ на каждый платеж", "да");
                if (string.Equals(GetAddFieldCollectionString("Подготовка платежных документов", string.Empty),
                        "Подготовка документов по спец. договорам", StringComparison.Ordinal))
                {
                    TrySetAddFieldCollectionValue("Подготовка платежных документов", string.Empty);
                }
                TrySetAddFieldCollectionValue("Ссылка на спец. договор", null);
                if (!string.Equals(GetAddFieldCollectionString("Признак ввода кода платежа", string.Empty),
                        "отсутствует", StringComparison.Ordinal))
                {
                    TrySetAddFieldCollectionValue("Признак ввода кода платежа", "отсутствует");
                }
                ucfAdditionalFields.Refresh();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private string GetAddFieldCollectionString(string fieldName, string defaultIfMissing)
        {
            try
            {
                if (!CheckAddFieldExists(fieldName))
                {
                    return defaultIfMissing;
                }
                object v = ucfAdditionalFields.Collection[fieldName].Value;
                return v != null ? Convert.ToString(v) : defaultIfMissing;
            }
            catch (Exception)
            {
                return defaultIfMissing;
            }
        }

        private void TrySetAddFieldCollectionValue(string fieldName, object value)
        {
            try
            {
                if (!CheckAddFieldExists(fieldName))
                {
                    return;
                }
                ucfAdditionalFields.Collection[fieldName].Value = value;
            }
            catch (Exception)
            {
            }
        }

        private void FillDependFieldsBeforeSave()
        {
            try
            {
                if (CheckAddFieldExists("Доп. параметры комиссии с плательщика"))
                {
                    ContractComboItem payer = cmbPayerCommissionType.SelectedItem as ContractComboItem;
                    if (payer != null && string.Equals(payer.Caption, "отсутствует", StringComparison.Ordinal))
                    {
                        ucfAdditionalFields.Collection["Доп. параметры комиссии с плательщика"].Value = null;
                    }
                }
                if (CheckAddFieldExists("Доп. параметры комиссии с получателя"))
                {
                    ContractComboItem rec = cmbRecipientCommissionType.SelectedItem as ContractComboItem;
                    if (rec != null && string.Equals(rec.Caption, "отсутствует", StringComparison.Ordinal))
                    {
                        ucfAdditionalFields.Collection["Доп. параметры комиссии с получателя"].Value = null;
                    }
                }
                ucfAdditionalFields.Refresh();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void ApplyContractSaveParamInForReadF()
        {
            if (string.Equals(m_command, EditCommand, StringComparison.Ordinal))
            {
                base.UbsChannel_ParamIn("IDCONTRACT", m_idContract);
            }
            base.UbsChannel_ParamIn("DATECLOSE", ucdContractClose.DateValue);
            base.UbsChannel_ParamIn("TXTCODE", txtContractCode.Text != null ? txtContractCode.Text.Trim() : string.Empty);
            base.UbsChannel_ParamIn("TXTCOMMENT", txtComment.Text != null ? txtComment.Text.Trim() : string.Empty);
            base.UbsChannel_ParamIn("CURPERCENTSEND", udcPayerCommissionPercent.DecimalValue);
            base.UbsChannel_ParamIn("CURPERCENTREC", udcRecipientCommissionPercent.DecimalValue);
            base.UbsChannel_ParamIn("TXTNUM", txtContractNumber.Text != null ? txtContractNumber.Text.Trim() : string.Empty);
            base.UbsChannel_ParamIn("TXTINN", txtRecipientInn.Text != null ? txtRecipientInn.Text.Trim() : string.Empty);
            base.UbsChannel_ParamIn("TXTADRESS", txtRecipientAddress.Text != null ? txtRecipientAddress.Text.Trim() : string.Empty);
            base.UbsChannel_ParamIn("TXTBIC", txtRecipientBik.Text != null ? txtRecipientBik.Text.Trim() : string.Empty);
            base.UbsChannel_ParamIn("TXTACCCORR", ucaCorrespondentAccount.Text != null ? ucaCorrespondentAccount.Text.Trim() : string.Empty);
            base.UbsChannel_ParamIn("TXTACC", ucaRecipientAccount.Text != null ? ucaRecipientAccount.Text.Trim() : string.Empty);
            base.UbsChannel_ParamIn("DATECONTRACT", ucdContract.DateValue);
            base.UbsChannel_ParamIn("INTIDCLIENT", m_idClient);
            base.UbsChannel_ParamIn("TXTKIND", m_idKind != null ? m_idKind.Trim() : string.Empty);
            int typeSend = GetSelectedCommissionTypeId(cmbPayerCommissionType);
            int typeRec = GetSelectedCommissionTypeId(cmbRecipientCommissionType);
            base.UbsChannel_ParamIn("INTTYPESEND", typeSend);
            base.UbsChannel_ParamIn("INTTYPEREC", typeRec);
        }

        private void ApplyContractSaveParamInForPersist(string strCommandForSave)
        {
            ApplyContractSaveParamInForReadF();
            base.UbsChannel_ParamIn("STRCOMMAND", strCommandForSave);
            base.UbsChannel_ParamIn("TXTBIC", txtRecipientBik.Text != null ? txtRecipientBik.Text.Trim() : string.Empty);
            base.UbsChannel_ParamIn("TXTINN", txtRecipientInn.Text != null ? txtRecipientInn.Text.Trim() : string.Empty);
            if (cmbContractStatus.Visible)
            {
                ContractComboItem st = cmbContractStatus.SelectedItem as ContractComboItem;
                int stateVal = st != null ? st.Id : 0;
                base.UbsChannel_ParamIn("STATE", stateVal);
            }
            base.UbsChannel_ParamIn("Метод расчета комиссии с получателя",
                chkRecipientCommissionReverse.Checked ? "Обратный" : "Прямой");
            if (cmbExecutor.Enabled && cmbExecutor.SelectedIndex >= 0)
            {
                ContractComboItem ex = cmbExecutor.SelectedItem as ContractComboItem;
                if (ex != null)
                {
                    base.UbsChannel_ParamIn("nIdOI", ex.Id);
                }
            }
        }

        private static int GetSelectedCommissionTypeId(ComboBox combo)
        {
            ContractComboItem it = combo.SelectedItem as ContractComboItem;
            return it != null ? it.Id : 0;
        }

        private void ApplyContractKindAndCodeEditability()
        {
            txtContractCode.Enabled = string.Equals(m_command, "ADD", StringComparison.Ordinal);
            linkPaymentKind.Enabled = string.Equals(m_command, "ADD", StringComparison.Ordinal);
        }

        private static string GetContractSaveErrorText(UbsParam saveOut)
        {
            if (saveOut.Contains("strError"))
            {
                string s = Convert.ToString(saveOut.Value("strError"));
                if (s != null && s.Trim().Length > 0)
                {
                    return s.Trim();
                }
            }
            if (saveOut.Contains("StrError"))
            {
                string s = Convert.ToString(saveOut.Value("StrError"));
                if (s != null && s.Trim().Length > 0)
                {
                    return s.Trim();
                }
            }
            return string.Empty;
        }

        private bool TryValidateContractBusinessRules()
        {
            string code = txtContractCode.Text != null ? txtContractCode.Text.Trim() : string.Empty;
            if (code.Length == 0)
            {
                MessageBox.Show(this, MsgInvalidContractCode, MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageMain;
                txtContractCode.Focus();
                return false;
            }
            if (code.Length > 20)
            {
                MessageBox.Show(this, MsgInvalidContractCodeLength, MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageMain;
                txtContractCode.Focus();
                return false;
            }
            string comment = txtComment.Text != null ? txtComment.Text : string.Empty;
            if (comment.Length > 255)
            {
                MessageBox.Show(this, MsgInvalidCommentLength, MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageMain;
                txtComment.Focus();
                return false;
            }
            if (comment.Trim().Length == 0)
            {
                MessageBox.Show(this, MsgInvalidCommentEmpty, MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageMain;
                txtComment.Focus();
                return false;
            }
            string num = txtContractNumber.Text != null ? txtContractNumber.Text.Trim() : string.Empty;
            if (num.Length > 20)
            {
                MessageBox.Show(this, MsgInvalidContractNumberLength, MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageMain;
                txtContractNumber.Focus();
                return false;
            }
            if (!TryValidateKindIdForSave())
            {
                MessageBox.Show(this, MsgInvalidPaymentKind, MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageMain;
                linkPaymentKind.Focus();
                return false;
            }
            string inn = txtRecipientInn.Text != null ? txtRecipientInn.Text.Trim() : string.Empty;
            if (inn.Length == 0 && m_idClient > 0)
            {
                MessageBox.Show(this, MsgInnRequiredForBankClient, MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageMain;
                if (txtRecipientInn.Enabled)
                {
                    txtRecipientInn.Focus();
                }
                return false;
            }
            string bic = txtRecipientBik.Text != null ? txtRecipientBik.Text.Trim() : string.Empty;
            if (bic.Length > 9)
            {
                MessageBox.Show(this, MsgInvalidBikLength, MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageMain;
                if (txtRecipientBik.Enabled)
                {
                    txtRecipientBik.Focus();
                }
                return false;
            }
            if (inn.Length > 12)
            {
                MessageBox.Show(this, MsgInvalidInnLength, MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageMain;
                if (txtRecipientInn.Enabled)
                {
                    txtRecipientInn.Focus();
                }
                return false;
            }
            string addr = txtRecipientAddress.Text != null ? txtRecipientAddress.Text.Trim() : string.Empty;
            if (addr.Length > 255)
            {
                MessageBox.Show(this, MsgInvalidAddressLength, MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageMain;
                if (txtRecipientAddress.Enabled)
                {
                    txtRecipientAddress.Focus();
                }
                return false;
            }
            string accText = ucaRecipientAccount.Text != null ? ucaRecipientAccount.Text.Trim() : string.Empty;
            if (m_idClient > 0)
            {
                if (accText == CorrespondentAccountPlaceholder || accText.Length == 0)
                {
                    MessageBox.Show(this, MsgInvalidClientAccount, MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabContract.SelectedTab = tabPageMain;
                    ucaRecipientAccount.Focus();
                    return false;
                }
                if (!TryValidateCheckKeyForCurrentAccounts())
                {
                    return false;
                }
                if (m_blnIsOurBik && !TryValidateClientAccountOwnership())
                {
                    return false;
                }
            }
            else
            {
                if (accText.Length > 0 && accText != CorrespondentAccountPlaceholder)
                {
                    if (!TryValidateCheckKeyForCurrentAccounts())
                    {
                        return false;
                    }
                }
            }
            if (!TryValidateTransitAccountsAddField())
            {
                return false;
            }
            if (!TryValidateCommissionExtraFieldPair(true))
            {
                return false;
            }
            if (!TryValidateCommissionExtraFieldPair(false))
            {
                return false;
            }
            return true;
        }

        private bool TryValidateKindIdForSave()
        {
            if (m_idKind == null || m_idKind.Trim().Length == 0)
            {
                return false;
            }
            int k;
            if (int.TryParse(m_idKind.Trim(), out k) && k == 0)
            {
                return false;
            }
            return true;
        }

        private bool TryValidateCheckKeyForCurrentAccounts()
        {
            base.IUbsChannel.ParamIn("STRACC", ucaRecipientAccount.Text != null ? ucaRecipientAccount.Text.Trim() : string.Empty);
            base.IUbsChannel.ParamIn("BIC", txtRecipientBik.Text != null ? txtRecipientBik.Text.Trim() : string.Empty);
            base.IUbsChannel.ParamIn("CORRACC", ucaCorrespondentAccount.Text != null ? ucaCorrespondentAccount.Text.Trim() : string.Empty);
            base.IUbsChannel.Run("CheckKey");
            UbsParam ck = base.IUbsChannel.ParamsOutParam;
            bool ok = ck.Contains("RETVAL") && Convert.ToBoolean(ck.Value("RETVAL"));
            if (!ok)
            {
                MessageBox.Show(this, MsgInvalidAccountKey, MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageMain;
                if (linkRecipientClient.Enabled)
                {
                    linkRecipientClient.Focus();
                }
                else
                {
                    ucaRecipientAccount.Focus();
                }
                return false;
            }
            return true;
        }

        private bool TryValidateClientAccountOwnership()
        {
            base.IUbsChannel.ParamIn("IDCLIENT", m_idClient);
            base.IUbsChannel.ParamIn("STRACC", ucaRecipientAccount.Text != null ? ucaRecipientAccount.Text.Trim() : string.Empty);
            base.IUbsChannel.Run("CheckClientAcc");
            UbsParam o = base.IUbsChannel.ParamsOutParam;
            int outClient = o.Contains("IDCLIENT") ? Convert.ToInt32(o.Value("IDCLIENT")) : 0;
            if (outClient != m_idClient)
            {
                MessageBox.Show(this, MsgAccountNotForClient, MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageMain;
                if (linkRecipientClient.Enabled)
                {
                    linkRecipientClient.Focus();
                }
                else
                {
                    ucaRecipientAccount.Focus();
                }
                return false;
            }
            return true;
        }

        private bool TryValidateTransitAccountsAddField()
        {
            if (!CheckAddFieldExists("Транзитные счета"))
            {
                return true;
            }
            try
            {
                object varAccounts = ucfAdditionalFields.Collection["Транзитные счета"].Value;
                base.IUbsChannel.ParamIn("varAccounts", varAccounts);
                base.IUbsChannel.Run("PSCheckAccounts");
                UbsParam o = base.IUbsChannel.ParamsOutParam;
                bool ok = o.Contains("RETVAL") && Convert.ToBoolean(o.Value("RETVAL"));
                if (!ok)
                {
                    string err = o.Contains("strError") ? Convert.ToString(o.Value("strError")) : string.Empty;
                    MessageBox.Show(this, err, MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabContract.SelectedTab = tabPageAddFields;
                    ucfAdditionalFields.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
            return true;
        }

        private bool TryValidateCommissionExtraFieldPair(bool payer)
        {
            string fieldName = payer
                ? "Доп. параметры комиссии с плательщика"
                : "Доп. параметры комиссии с получателя";
            if (!CheckAddFieldExists(fieldName))
            {
                return true;
            }
            object raw;
            try
            {
                raw = ucfAdditionalFields.Collection[fieldName].Value;
            }
            catch (Exception)
            {
                return true;
            }
            object[,] arr = raw as object[,];
            if (arr == null)
            {
                return true;
            }
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);
            if (rows < 2 || cols < 1)
            {
                return true;
            }
            object minO = arr[0, 0];
            object maxO = arr[1, 0];
            bool minEmpty = IsEmptyVariantSlot(minO);
            bool maxEmpty = IsEmptyVariantSlot(maxO);
            decimal minVal = 0m;
            decimal maxVal = 0m;
            bool minNum = !minEmpty && TryToDecimal(minO, out minVal);
            bool maxNum = !maxEmpty && TryToDecimal(maxO, out maxVal);
            if (minNum && maxNum && minVal > maxVal)
            {
                MessageBox.Show(this, payer ? MsgCommissionPayerMinMaxOrder : MsgCommissionRecMinMaxOrder,
                    MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageAddFields;
                ucfAdditionalFields.Focus();
                return false;
            }
            if (minEmpty && !maxEmpty)
            {
                MessageBox.Show(this, payer ? MsgCommissionPayerMinRequired : MsgCommissionRecMinRequired,
                    MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageAddFields;
                ucfAdditionalFields.Focus();
                return false;
            }
            if (!minEmpty && maxEmpty)
            {
                MessageBox.Show(this, payer ? MsgCommissionPayerMaxRequired : MsgCommissionRecMaxRequired,
                    MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageAddFields;
                ucfAdditionalFields.Focus();
                return false;
            }
            if (minEmpty && maxEmpty)
            {
                MessageBox.Show(this, payer ? MsgCommissionPayerBothRequired : MsgCommissionRecBothRequired,
                    MsgValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabContract.SelectedTab = tabPageAddFields;
                ucfAdditionalFields.Focus();
                return false;
            }
            return true;
        }

        private static bool IsEmptyVariantSlot(object o)
        {
            if (o == null || o is DBNull)
            {
                return true;
            }
            string s = Convert.ToString(o);
            return s == null || s.Trim().Length == 0;
        }

        private static bool TryToDecimal(object o, out decimal d)
        {
            d = 0m;
            if (o == null || o is DBNull)
            {
                return false;
            }
            try
            {
                d = Convert.ToDecimal(o);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
