using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentFrm
    {
        /// <summary>
        /// Wires keyboard-related events from the constructor.
        /// </summary>
        private void WireKeyEvents()
        {
            this.KeyPreview = true;

            this.KeyDown += Form_KeyDown;

            txtPeriodMonthBeg.Leave += txtPeriodMonthBeg_Leave;
            txtPeriodMonthEnd.Leave += txtPeriodMonthEnd_Leave;
            txtPeriodYearBeg.Leave += txtPeriodYearBeg_Leave;
            txtPeriodYearEnd.Leave += txtPeriodYearEnd_Leave;

            txtContractCode.Leave += txtContractCode_Leave;
            txtContractCode.KeyDown += txtContractCode_KeyDown;

            txtPayerInn.KeyDown += CheckInt_KeyDown;
            txtRecipientInn.KeyDown += CheckInt_KeyDown;
            txtThirdPersonInn.KeyDown += CheckInt_KeyDown;
            txtCashSymbolPayment.KeyDown += CheckInt_KeyDown;
            txtCashSymbolCommission.KeyDown += CheckInt_KeyDown;
            txtCashSymbolNds.KeyDown += CheckInt_KeyDown;
            txtPeriodDayBeg.KeyDown += CheckInt_KeyDown;
            txtPeriodMonthBeg.KeyDown += CheckInt_KeyDown;
            txtPeriodYearBeg.KeyDown += CheckInt_KeyDown;
            txtPeriodDayEnd.KeyDown += CheckInt_KeyDown;
            txtPeriodMonthEnd.KeyDown += CheckInt_KeyDown;
            txtPeriodYearEnd.KeyDown += CheckInt_KeyDown;
        }

        #region ProcessCmdKey ˜ F7, Ctrl+Tab

        /// <summary>
        /// Intercepts command keys before normal processing.
        /// VB6: UserDocument_KeyDown (F7, Ctrl+Tab).
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F7)
            {
                btnCalc.PerformClick();
                return true;
            }

            // VB6: Ctrl+Tab ? Parent.LoaderParamInfo(0, "NextWindow") = NumWin
            // Managed by UBS parent loader; no direct .NET equivalent needed.
            if (keyData == (Keys.Control | Keys.Tab))
            {
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Form_KeyDown ˜ Enter-as-Tab and Escape backward

        /// <summary>
        /// Main keyboard handler: Enter?forward, Escape?backward.
        /// VB6: UserDocument_KeyPress Case 13 / Case 27.
        /// </summary>
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && e.Modifiers == Keys.None)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                m_isForward = true;
                GotoNextControlForEnter();
            }
            else if (e.KeyCode == Keys.Escape && e.Modifiers == Keys.None)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                m_isForward = false;
                EscapeBackwardNavigation();
            }
        }

        #endregion

        #region Enter-as-Tab forward navigation

        /// <summary>
        /// Forward focus chain on Enter key.
        /// VB6: Sub GotoNextControlForEnter() (lines 9568˜9865).
        /// </summary>
        private void GotoNextControlForEnter()
        {
            try
            {
                Control active = this.ActiveControl;
                if (active == null) return;

                if (active == txtPaymentCode)
                {
                    FillDataPayment("CODE");
                    if (udcPaymentAmount.Enabled)
                        udcPaymentAmount.Focus();
                    else
                        MoveToNextTab();
                    return;
                }

                if (active == txtPayerFullName)
                {
                    if (txtPayerInn.Enabled)
                        txtPayerInn.Focus();
                    else
                        SelectNextControl(active, true, true, true, true);
                    return;
                }

                if (active == txtPayerAccount)
                {
                    OnEnter_PayerAccount();
                    return;
                }

                if (active == txtCheckSum)
                {
                    OnEnter_CheckSum();
                    return;
                }

                if (active == txtRecipientBik)
                {
                    OnEnter_RecipientBik();
                    return;
                }

                if (active == ucaRecipientAccount)
                {
                    OnEnter_RecipientAccount();
                    return;
                }

                if (active == cmbPhone || active == cmbTariff)
                {
                    if (tabPageAddFields.Enabled)
                    {
                        tabPayment.SelectedTab = tabPageAddFields;
                        ucfAddProperties.Focus();
                    }
                    return;
                }

                if (active == udcPaymentAmount)
                {
                    OnEnter_PaymentAmount();
                    return;
                }

                if (active == udcPenaltyAmount)
                {
                    OnEnter_PenaltyAmount();
                    return;
                }

                if (active == txtPeriodYearEnd)
                {
                    OnEnter_YearEnd();
                    return;
                }

                if (active == txtThirdPersonInn)
                {
                    if (tabPageTax.Visible && !txtThirdPersonKpp.Enabled)
                    {
                        tabPayment.SelectedTab = tabPageTax;
                        txtTaxKbk.Focus();
                    }
                    else
                    {
                        SelectNextControl(active, true, true, true, true);
                    }
                    return;
                }

                if (active == txtThirdPersonKpp)
                {
                    if (tabPageTax.Visible)
                    {
                        tabPayment.SelectedTab = tabPageTax;
                        txtTaxKbk.Focus();
                    }
                    else
                    {
                        MoveToNextTab();
                    }
                    return;
                }

                if (active == txtTaxImns || active == txtTaxKbkNote)
                {
                    if (tabPageAddFields.Enabled)
                    {
                        tabPayment.SelectedTab = tabPageAddFields;
                        ucfAddProperties.Focus();
                    }
                    return;
                }

                if (active == txtContractCode)
                {
                    if (!m_isCodeEnter)
                    {
                        SelectNextControl(active, true, true, true, true);
                    }
                    return;
                }

                SelectNextControl(active, true, true, true, true);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void OnEnter_PayerAccount()
        {
            if (!CheckAndSplitAccount())
                return;

            if (m_bIncludeKey)
            {
                if (!PrepareAccount())
                {
                    txtPayerAccount.Focus();
                    return;
                }
            }

            if (txtCheckSum.Visible)
            {
                txtCheckSum.Focus();
            }
            else if (tabPageTelephone.Visible)
            {
                tabPayment.SelectedTab = tabPageTelephone;
                cmbPhone.Focus();
            }
            else
            {
                MoveToNextTab();
            }
        }

        private void OnEnter_CheckSum()
        {
            if (!PrepareAccount())
            {
                txtCheckSum.Focus();
                return;
            }

            if (tabPageTax.Visible)
            {
                tabPayment.SelectedTab = tabPageTax;
                if (txtTaxStatus.Enabled) txtTaxStatus.Focus();
                else txtTaxKbk.Focus();
            }
            else if (tabPageTelephone.Visible)
            {
                tabPayment.SelectedTab = tabPageTelephone;
                cmbPhone.Focus();
            }
            else if (tabPageTariff.Visible)
            {
                tabPayment.SelectedTab = tabPageTariff;
                cmbTariff.Focus();
            }
            else
            {
                MoveToNextTab();
            }
        }

        private void OnEnter_RecipientBik()
        {
            m_isNoMessage = true;
            if (!GetBankNameACC())
            {
                tabPayment.SelectedTab = tabPageGeneral;
                if (txtRecipientBik.Enabled) txtRecipientBik.Focus();
            }
            else
            {
                tabPayment.SelectedTab = tabPageGeneral;
                if (ucaRecipientAccount.Enabled)
                    ucaRecipientAccount.Focus();
                else if (txtRecipientInn.Enabled)
                    txtRecipientInn.Focus();
            }
        }

        private void OnEnter_RecipientAccount()
        {
            SelectNextControl(ucaRecipientAccount, true, true, true, true);
        }

        private void OnEnter_PaymentAmount()
        {
            if (m_idContract > 0)
            {
                m_prefCalcRate = "_2 8";
                CalcSumCommiss_2();
            }

            if (udcPenaltyAmount.Visible && udcPenaltyAmount.Enabled)
            {
                udcPenaltyAmount.Focus();
            }
            else if (txtPeriodMonthBeg.Visible || txtPayerAccount.Visible || txtCheckSum.Visible)
            {
                tabPayment.SelectedTab = tabPageGeneral;
                SelectNextControl(udcPaymentAmount, true, true, true, true);
            }
            else if (tabPageThirdPerson.Visible)
            {
                tabPayment.SelectedTab = tabPageThirdPerson;
                txtThirdPersonName.Focus();
            }
            else if (tabPageTax.Visible)
            {
                tabPayment.SelectedTab = tabPageTax;
                if (txtTaxStatus.Enabled) txtTaxStatus.Focus();
                else txtTaxKbk.Focus();
            }
            else
            {
                MoveToNextTab();
            }
        }

        private void OnEnter_PenaltyAmount()
        {
            if (m_idContract > 0)
            {
                m_prefCalcRate = "_2 9";
                CalcSumCommiss_2();
            }

            if (txtPeriodMonthBeg.Visible || txtPayerAccount.Visible || txtCheckSum.Visible)
            {
                tabPayment.SelectedTab = tabPageGeneral;
                SelectNextControl(udcPenaltyAmount, true, true, true, true);
            }
            else if (tabPageThirdPerson.Visible)
            {
                tabPayment.SelectedTab = tabPageThirdPerson;
                txtThirdPersonName.Focus();
            }
            else if (tabPageTax.Visible)
            {
                tabPayment.SelectedTab = tabPageTax;
                if (txtTaxStatus.Enabled) txtTaxStatus.Focus();
                else txtTaxKbk.Focus();
            }
            else
            {
                MoveToNextTab();
            }
        }

        private void OnEnter_YearEnd()
        {
            if (cmbCityCode.Visible && cmbCityCode.Enabled)
            {
                cmbCityCode.Focus();
            }
            else if (txtPayerAccount.Visible && txtPayerAccount.Enabled)
            {
                txtPayerAccount.Focus();
            }
            else if (tabPageThirdPerson.Visible)
            {
                tabPayment.SelectedTab = tabPageThirdPerson;
                txtThirdPersonName.Focus();
            }
            else if (tabPageAddFields.Enabled)
            {
                tabPayment.SelectedTab = tabPageAddFields;
                ucfAddProperties.Focus();
            }
            else
            {
                btnSave.Focus();
            }
        }

        private void MoveToNextTab()
        {
            int total = tabPayment.TabCount;
            int current = tabPayment.SelectedIndex;

            for (int i = current + 1; i < total; i++)
            {
                TabPage page = tabPayment.TabPages[i];
                if (page.Enabled && page.Visible)
                {
                    tabPayment.SelectedTab = page;
                    page.Focus();
                    return;
                }
            }

            btnSave.Focus();
        }

        #endregion

        #region Escape backward navigation

        /// <summary>
        /// Reverse focus chain on Escape key.
        /// VB6: UserDocument_KeyPress Case 27 (lines 6410˜6578).
        /// </summary>
        private void EscapeBackwardNavigation()
        {
            try
            {
                Control active = this.ActiveControl;
                if (active == null) return;

                if (active == txtPayerInn)
                {
                    if (txtPayerFullName.Enabled) txtPayerFullName.Focus();
                    else SelectNextControl(active, false, true, true, true);
                    return;
                }

                if (active == cmbPurpose)
                {
                    if (txtRecipientInn.Enabled) txtRecipientInn.Focus();
                    else if (ucaRecipientAccount.Enabled) ucaRecipientAccount.Focus();
                    else if (txtRecipientBik.Enabled) txtRecipientBik.Focus();
                    else if (txtContractCode.Enabled) txtContractCode.Focus();
                    return;
                }

                if (active == txtContractCode)
                {
                    if (linkFindFilter.Visible && linkFindFilter.Enabled)
                        linkFindFilter.Focus();
                    else if (txtPayerCardNumber.Visible && txtPayerCardNumber.Enabled)
                        txtPayerCardNumber.Focus();
                    else if (ucaPayerAccount.Visible && ucaPayerAccount.Enabled)
                        ucaPayerAccount.Focus();
                    else if (txtPayerClientInfo.Visible && txtPayerClientInfo.Enabled)
                        txtPayerClientInfo.Focus();
                    else
                        txtPayerFullName.Focus();
                    return;
                }

                if (active == txtPayerCardNumber)
                {
                    ucaPayerAccount.Focus();
                    return;
                }

                if (active == ucaPayerAccount)
                {
                    txtPayerAddress.Focus();
                    return;
                }

                if (active == txtPayerAddress)
                {
                    txtPayerInn.Focus();
                    return;
                }

                if (active == txtPayerFullName)
                {
                    btnExit.Focus();
                    return;
                }

                if (active == txtRecipientInn)
                {
                    if (ucaRecipientAccount.Enabled) ucaRecipientAccount.Focus();
                    return;
                }

                if (active == ucaRecipientAccount)
                {
                    if (txtRecipientBik.Enabled) txtRecipientBik.Focus();
                    return;
                }

                if (active == cmbTariff || active == cmbPhone)
                {
                    tabPayment.SelectedTab = tabPageGeneral;
                    if (txtCheckSum.Visible)
                        txtCheckSum.Focus();
                    else
                        txtPayerAccount.Focus();
                    return;
                }

                if (active == txtThirdPersonName)
                {
                    tabPayment.SelectedTab = tabPageGeneral;
                    if (txtCheckSum.Visible) txtCheckSum.Focus();
                    else if (udcPaymentAmount.Enabled) udcPaymentAmount.Focus();
                    else txtCashSymbolPayment.Focus();
                    return;
                }

                if (active == txtTaxKbk)
                {
                    if (txtTaxStatus.Enabled)
                    {
                        txtTaxStatus.Focus();
                    }
                    else if (tabPageThirdPerson.Visible)
                    {
                        tabPayment.SelectedTab = tabPageThirdPerson;
                        if (txtThirdPersonKpp.Enabled)
                            txtThirdPersonKpp.Focus();
                        else
                            txtThirdPersonInn.Focus();
                    }
                    else
                    {
                        tabPayment.SelectedTab = tabPageGeneral;
                        if (txtCheckSum.Visible) txtCheckSum.Focus();
                        else if (udcPaymentAmount.Enabled) udcPaymentAmount.Focus();
                        else txtCashSymbolPayment.Focus();
                    }
                    return;
                }

                if (active == txtTaxStatus)
                {
                    if (tabPageThirdPerson.Visible)
                    {
                        tabPayment.SelectedTab = tabPageThirdPerson;
                        if (txtThirdPersonKpp.Enabled) txtThirdPersonKpp.Focus();
                        else txtThirdPersonInn.Focus();
                    }
                    else
                    {
                        tabPayment.SelectedTab = tabPageGeneral;
                        if (txtCheckSum.Visible) txtCheckSum.Focus();
                        else if (udcPaymentAmount.Enabled) udcPaymentAmount.Focus();
                        else txtCashSymbolPayment.Focus();
                    }
                    return;
                }

                if (tabPayment.SelectedTab == tabPageAddFields)
                {
                    GotoPreviousTab();
                    return;
                }

                SelectNextControl(active, false, true, true, true);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region CheckInt ˜ digits-only filter

        /// <summary>
        /// Restricts input to digits, Backspace, and Delete.
        /// VB6: Private Sub CheckInt(Control As TextBox, KeyCode As Integer) (lines 8870˜8885).
        /// </summary>
        private void CheckInt_KeyDown(object sender, KeyEventArgs e)
        {
            bool isDigit = (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9);
            bool isControl = e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete
                || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right
                || e.KeyCode == Keys.Home || e.KeyCode == Keys.End
                || e.KeyCode == Keys.Tab || e.KeyCode == Keys.Return
                || e.KeyCode == Keys.Escape;

            if (!isDigit && !isControl)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region Period field validation

        /// <summary>
        /// VB6: txtMonthBeg_LostFocus (lines 4742˜4756).
        /// </summary>
        private void txtPeriodMonthBeg_Leave(object sender, EventArgs e)
        {
            if (txtPeriodMonthBeg.Text.Trim().Length == 0) return;

            int month;
            if (!int.TryParse(txtPeriodMonthBeg.Text, out month)) return;

            if (month < 1 || month > 12)
            {
                MessageBox.Show(MsgDateError, CaptionForm,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPeriodMonthBeg.Focus();
            }
        }

        /// <summary>
        /// VB6: txtMonthEnd_LostFocus (lines 4760˜4778).
        /// </summary>
        private void txtPeriodMonthEnd_Leave(object sender, EventArgs e)
        {
            if (txtPeriodMonthEnd.Text.Trim().Length == 0) return;

            int month;
            if (!int.TryParse(txtPeriodMonthEnd.Text, out month)) return;

            if (month < 1 || month > 12)
            {
                MessageBox.Show(MsgDateError, CaptionForm,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPeriodMonthEnd.Focus();
                return;
            }

            if (txtPeriodYearEnd.Text.Trim().Length > 0)
            {
                txtPeriodDayEnd.Text = GetDayEnd(txtPeriodMonthEnd.Text, txtPeriodYearEnd.Text).ToString();
            }
        }

        /// <summary>
        /// VB6: txtYearBeg_LostFocus (lines 4782˜4793) ˜ no-op validation placeholder.
        /// </summary>
        private void txtPeriodYearBeg_Leave(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// VB6: txtYearEnd_LostFocus (lines 4796˜4811).
        /// </summary>
        private void txtPeriodYearEnd_Leave(object sender, EventArgs e)
        {
            if (txtPeriodYearEnd.Text.Trim().Length == 0) return;

            if (txtPeriodMonthEnd.Text.Trim().Length > 0)
            {
                txtPeriodDayEnd.Text = GetDayEnd(txtPeriodMonthEnd.Text, txtPeriodYearEnd.Text).ToString();
            }
        }

        #endregion

        #region Contract code focus handlers

        /// <summary>
        /// VB6: txtCode_LostFocus ˜ triggers FindContract when code loses focus.
        /// </summary>
        private void txtContractCode_Leave(object sender, EventArgs e)
        {
            if (txtRecipientComment.Text.Length == 0)
            {
                FindContract();
            }
        }

        /// <summary>
        /// Enter on contract code triggers FindContract directly.
        /// VB6: txtCode_KeyDown (lines 2485˜2492).
        /// </summary>
        private void txtContractCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && e.Modifiers == Keys.None)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                m_isCodeEnter = true;
                FindContract();
            }
        }

        #endregion

        #region GotoPreviousTab

        /// <summary>
        /// Navigates to the previous visible/enabled tab page.
        /// VB6: Function gotoPreviousTab() (lines 10147˜10164).
        /// </summary>
        private bool GotoPreviousTab()
        {
            int current = tabPayment.SelectedIndex;
            if (current <= 0) return false;

            for (int i = current - 1; i >= 0; i--)
            {
                TabPage page = tabPayment.TabPages[i];
                if (page.Enabled && page.Visible)
                {
                    tabPayment.SelectedTab = page;
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
