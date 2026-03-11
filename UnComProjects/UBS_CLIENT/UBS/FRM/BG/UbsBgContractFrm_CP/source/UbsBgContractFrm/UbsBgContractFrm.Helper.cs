using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Configuration;
using System.Runtime.Remoting;
using System.Security.Principal;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    partial class UbsBgContractFrm
    {
        private void RunUbsChannel(string functionName, UbsParam paramIn, UbsParam paramOut, bool needAddFields = false)
        {
            if (!needAddFields)
            {
                base.IUbsChannel.ParamsInParam = paramIn;

                base.IUbsChannel.Run(functionName);

                paramOut.ItemsVector = base.IUbsChannel.ParamsOut;
            }
            else
            {
                base.UbsChannel_ParamsIn = paramIn.ItemsVector;

                base.UbsChannel_Run(functionName);

                paramOut.ItemsVector = base.UbsChannel_ParamsOut;
            }
        }
        private bool IsCmbCurrencyEnabled() => (m_idState == 4 && m_idFrameContract == 0);
        private void SetComboValueText(ComboBox cmb, string value)
        {
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if (((KeyValuePair<int, string>)cmb.Items[i]).Value == value)
                {
                    cmb.SelectedIndex = i;
                }
            }
        }
        private void FillComboText(ComboBox cmbControl, object[] arrData)
        {
            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
            for (int i = 0; i < arrData.GetLength(0); i++)
                list.Add(new KeyValuePair<int, string>(i, (string)arrData[i]));

            cmbControl.DataSource = list;
            cmbControl.ValueMember = "Key";
            cmbControl.DisplayMember = "Value";

            cmbControl.SelectedIndex = 0;
        }
        private void SetComboText(ComboBox cmb, int v)
        {
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if ((int)cmb.SelectedValue == v)
                {
                    cmb.SelectedIndex = i;

                    return;
                }
            }
        }
        private void InitComboBoxes()
        {// === Инициализация комбобокса ОИ ===
            object[,] arrExecut = m_paramOut.Value("ОИ") as object[,];

            if (arrExecut != null)
            {
                var kvpList = MakeKvpList(arrExecut);

                InitComboBox(cmbExecutor, kvpList);
            }
            else
            {
                if (m_command == AddCommand || m_command.ToUpperInvariant() == PrepareCommand)
                {
                    MessageBox.Show(MsgNoRightsToCreateContract, MsgAccessError, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    btnExit_Click(this, EventArgs.Empty);

                    return;
                }
            }

            // === Инициализация комбобокса (УЛБ) ===
            object[,] arrWarrant = m_paramOut.Value("УЛБ") as object[,];

            if (arrWarrant != null)
            {
                var kvpList = MakeKvpList(arrWarrant);

                InitComboBox(cmbWarrant, kvpList);
            }

            object[] arrKindAddflSense = m_paramOut.Value("Виды гарантии") as object[];

            if (arrKindAddflSense != null)
            {
                object[] extended = new object[arrKindAddflSense.Length + 1];

                extended[extended.Length - 1] = string.Empty;

                for (int i = 0; i < arrKindAddflSense.Length; i++)
                {
                    extended[i + 1] = arrKindAddflSense[i];
                }

                arrKindAddflSense = extended;
            }
            else
            {
                arrKindAddflSense = new object[] { string.Empty };
            }

            m_kindAddflSense = arrKindAddflSense;

            ResetKindComboBox();

            // === Категории качества ===
            object[,] arrQualityCategory = m_paramOut.Value("Категории качества") as object[,];

            if (arrQualityCategory != null)
            {
                var kvpList = MakeKvpList(arrQualityCategory, 0, 0);

                InitComboBox(cmbQualityCategory, kvpList);
            }
            else
            {
                MessageBox.Show(MsgRiskSettingNotFilled,
                    MsgWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // === Портфели ===
            cmbPortfolio.Items.Clear();

            object[,] arrPortfolio = m_paramOut.Value("Портфели") as object[,];

            if (arrPortfolio != null)
            {
                var kvpList = MakeKvpList(arrPortfolio);

                kvpList.Insert(0, new KeyValuePair<int, string>(0, "Портфель не определен"));

                InitComboBox(cmbPortfolio, kvpList);
            }

            // === Тип оценки риска ===
            object[,] arrTypeValidationRisk = m_paramOut.Value("Тип оценки риска") as object[,];

            if (arrTypeValidationRisk != null)
            {
                var kvpList = MakeKvpList(arrTypeValidationRisk);

                InitComboBox(cmbTypeValidationRisk, kvpList);
            }
            else
            {
                MessageBox.Show(MsgRiskAssessmentTypesEmpty,
                    MsgWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // === Типы покрытия ===
            object[,] arrCoverTypes = m_paramOut.Value("Типы покрытия") as object[,];

            if (arrCoverTypes != null)
            {
                var kvpList = MakeKvpList(arrCoverTypes);

                InitComboBox(cmbTypeCover, kvpList);
            }
            else
            {
                MessageBox.Show(MsgCoverTypesEmpty,
                    MsgWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // === Валюты ===
            object[,] arrCurrency = m_paramOut.Value("Валюты") as object[,];
            if (arrCurrency != null)
            {
                int idxCurrency = 0;
                int idCurrency = m_idCurrency > 0 ? m_idCurrency : 810;

                cmbCurrencyGarant.Items.Clear();
                cmbCurrencyCover.Items.Clear();
                cmbCurrencyPayFee.Items.Clear();
                cmbCurrencyRewardGuarant.Items.Clear();

                int rows = arrCurrency.GetLength(0);
                for (int r = 0; r < rows; r++)
                {
                    int id = Convert.ToInt32(arrCurrency[r, 0]);
                    string text = Convert.ToString(arrCurrency[r, 3]);

                    cmbCurrencyGarant.Items.Add(new ComboItem(id, text));
                    cmbCurrencyCover.Items.Add(new ComboItem(id, text));
                    cmbCurrencyPayFee.Items.Add(new ComboItem(id, text));
                    cmbCurrencyRewardGuarant.Items.Add(new ComboItem(id, text));

                    if (id == idCurrency) idxCurrency = r;
                }

                if (cmbCurrencyGarant.Items.Count > 0)
                {
                    cmbCurrencyGarant.SelectedIndex = idxCurrency;
                    cmbCurrencyCover.SelectedIndex = idxCurrency;
                    cmbCurrencyPayFee.SelectedIndex = idxCurrency;
                    cmbCurrencyRewardGuarant.SelectedIndex = idxCurrency;
                }
            }
            else
            {
                MessageBox.Show(MsgCurrenciesEmpty, MsgWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // === Состояния ===
            object[,] arrState = m_paramOut.Value("Состояния") as object[,];

            if (arrState != null)
            {
                int idxState = 0;
                m_idState = 4;

                cmbState.Items.Clear();

                var kvpList = MakeKvpList(arrState);

                InitComboBox(cmbState, kvpList);

                for (int i = 0; i < arrState.GetLength(0); i++)
                {
                    if (Convert.ToInt32(arrState[i, 0]) == m_idState)
                        idxState = i;
                }

                if (cmbState.Items.Count > 0)
                    cmbState.SelectedIndex = idxState;

                cmbState.Enabled = false;
            }
            else
            {
                MessageBox.Show(MsgStatesEmpty, MsgWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // === Отделения ===
            object[,] arrDivs = m_paramOut.Value("Отделения") as object[,];

            if (arrDivs != null)
            {
                int idxDiv = 0;
                int gudiv = Convert.ToInt32(m_paramOut.Value("GUDiv"));

                cmbNumberDiv.Items.Clear();

                var kvpList = MakeKvpList(arrDivs);

                InitComboBox(cmbNumberDiv, kvpList);

                for (int i = 0; i < kvpList.Count; i++)
                {
                    if (kvpList[i].Key == gudiv)
                    {
                        idxDiv = i;
                    }
                }

                if (cmbNumberDiv.Items.Count > 0)
                    cmbNumberDiv.SelectedIndex = idxDiv;

                if (m_command.ToUpperInvariant() != AddCommand)
                {
                    if (m_command == EditCommand && m_idState == 2)
                    {
                        cmbNumberDiv.Enabled = true;

                        if (cmbTypePayFee.SelectedText == PercentSumGuarant)
                        {
                            cmbCurrencyPayFee.SelectedIndex = cmbCurrencyGarant.SelectedIndex;

                            cmbCurrencyPayFee.Enabled = false;
                            cmbCurrencyRewardGuarant.Enabled = false;
                        }
                        else
                        {
                            cmbCurrencyPayFee.Enabled = true;
                            cmbCurrencyRewardGuarant.Enabled = true;
                        }

                        if (cmbTypePayFee.SelectedText == PercentSumGuarant)
                        {
                            cmbCurrencyPayFee.SelectedIndex = cmbCurrencyGarant.SelectedIndex;

                            cmbCurrencyRewardGuarant.Enabled = false;
                        }
                        else
                        {
                            cmbCurrencyRewardGuarant.Enabled = true;
                        }
                    }
                    else
                    {
                        cmbNumberDiv.Enabled = false;
                    }
                }

                if (m_command.ToUpperInvariant() == PrepareCommand)
                    cmbNumberDiv.Enabled = true;
            }
            else
            {
                MessageBox.Show(MsgDivisionsEmpty, MsgWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private static void SetComboById(ComboBox combo, int id)
        {
            for (int i = 0; i < combo.Items.Count; i++)
            {
                ComboItem it = combo.Items[i] as ComboItem;
                if (it != null && it.Id == id)
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }
        }
        private void SetTabsEnabled(bool enabled, params int[] tabIndexes)
        {
            for (int i = 0; i < tabIndexes.Length; i++)
            {
                int idx = tabIndexes[i];
                if (idx >= 0 && idx < tabControl.TabPages.Count)
                {
                    foreach (Control c in tabControl.TabPages[idx].Controls)
                        c.Enabled = enabled;
                }
            }
        }
        private DateTime GetCurrentDate()
        {
            base.IUbsChannel.ParamIn("NameSetting", "Server");
            base.IUbsChannel.Run("GetCommonDate");

            return Convert.ToDateTime(base.IUbsChannel.ParamOut("DataSetting"));
        }
        private object[,] GetArrayForCombo(object[] kindAddflSense)
        {
            var result = new object[kindAddflSense.Length, 2];

            for (int i = 0; i < kindAddflSense.Length; i++)
            {
                result[i, 0] = i;
                result[i, 1] = kindAddflSense[i];
            }

            return result;
        }
        private void SetComboItem(ComboBox comboBox, KeyValuePair<int, string> selectedItem)
        {
            foreach (KeyValuePair<int, string> item in comboBox.Items)
            {
                if (item.Key == selectedItem.Key)
                {
                    comboBox.SelectedItem = item;
                }
            }
        }
        private void EnabledCmdControl(bool isEnabled)
        {
            linkBeneficiar.Enabled = isEnabled;

            if (m_idState != 0 && m_idState != 2)
            {
                btnManualBenificiar.Enabled = isEnabled;
            }
            if (m_modelType == UbsCounterGuarantTypeCommand)
            {
                linkGarant.Enabled = isEnabled;
            }

            linkModel.Enabled = isEnabled;
            linkAgent.Enabled = isEnabled;
            linkPreviousContract.Enabled = isEnabled;

            if (m_idFrameContract == 0)
            {
                txtPrincipal.Enabled = isEnabled;
            }
            if (linkFrameContract.Visible)
            {
                linkFrameContract.Enabled = isEnabled;
            }
            if (btnFrameContractDel.Visible)
            {
                btnFrameContractDel.Enabled = isEnabled;
            }
            if (linkAgent.Visible)
            {
                linkAgent.Enabled = isEnabled;
            }
            if (btnAgentDel.Visible)
            {
                btnAgentDel.Enabled = isEnabled;
            }

            if (m_setRekvBen == 1)
            {
                btnManualBenificiar.Enabled = false;
            }
        }
        private void InitComboBox(ComboBox cmb, object list)
        {
            cmb.DataSource = null;
            cmb.DataSource = list;
            cmb.ValueMember = "Key";
            cmb.DisplayMember = "Value";
            if (cmb.Items.Count > 0)
                cmb.SelectedIndex = 0;
        }
        private static List<KeyValuePair<int, string>> MakeKvpList(object[,] vbArr, int colId = 0, int colText = 1)
        {
            var res = new List<KeyValuePair<int, string>>();

            if (vbArr == null)
                return res;

            int rows = vbArr.GetLength(0);

            for (int r = 0; r < rows; r++)
            {
                int id = Convert.ToInt32(vbArr[r, colId]);
                string text = Convert.ToString(vbArr[r, colText]);

                res.Add(new KeyValuePair<int, string>(id, text));
            }

            return res;
        }
        private static List<KeyValuePair<int, string>> MakeKvpList(object[] vbArr)
        {
            var res = new List<KeyValuePair<int, string>>();

            if (vbArr == null)
                return res;

            int rows = vbArr.GetLength(0);

            for (int r = 0; r < rows; r++)
            {
                int id = r;
                string text = Convert.ToString(vbArr[r]);

                res.Add(new KeyValuePair<int, string>(id, text));
            }

            return res;
        }
        private void ResetListKeyState()
        {
            m_idContract = 0;
            m_idContractCopy = 0;
            m_isSecure = true;
        }
        private void SetControlsStateByCommand()
        {
            dateNextPayFee.Enabled = (m_idState >= 2);

            if (m_command == AddCommand || m_command.ToUpperInvariant() == PrepareCommand)
            {
                btnReRead.Enabled = false;
                SetTabsEnabled(false, 2, 3, 4);

                cmbOrderPayFee.Enabled = true;
                cmbTypePayFee.Enabled = true;
                cmbOrderPayFeeBonus.Enabled = true;
                cmbTypePayFeeBonus.Enabled = true;
            }
            else if (m_command == CopyCommand)
            {
                btnReRead.Enabled = false;
                SetTabsEnabled(true, 2, 3, 4);

                cmbOrderPayFee.Enabled = true;
                cmbTypePayFee.Enabled = true;
                cmbOrderPayFeeBonus.Enabled = true;
                cmbTypePayFeeBonus.Enabled = true;
                cmbOrderPayFeeGuarant.Enabled = true;
                cmbTypePayFeeGuarant.Enabled = true;
                linkModel.Enabled = true;
            }
            else
            {
                btnReRead.Enabled = true;
                SetTabsEnabled(true, 2, 3, 4);

                cmbOrderPayFee.Enabled = false;
                cmbTypePayFee.Enabled = false;
                cmbOrderPayFeeBonus.Enabled = false;
                cmbTypePayFeeBonus.Enabled = false;
                cmbOrderPayFeeGuarant.Enabled = false;
                cmbTypePayFeeGuarant.Enabled = false;
                cmbCurrencyRewardGuarant.Enabled = false;
                cmbCurrencyPayFee.Enabled = false;
            }
        }
        private void AddEditGuarContract(string mode)
        {
            var pIn = new UbsParam();
            var pOut = new UbsParam();

            var scripterParameters = new UbsParam();

            GuarCmdState(false, true);

            var scripter = base.Ubs_VBScriptRunner();

            UbsParent oParent = new UbsParent();
            oParent.Form32 = this;
            oParent.Loader = new UbsLoader();
            UbsParentStub oParentStub = new UbsParentStub(oParent);
            UbsUserDocument oUserDocument = new UbsUserDocument(oParentStub);

            scripterParameters.Value("UbsWaitBox", new WaitBox());
            scripterParameters.Value("DocObj", oUserDocument);

            scripter.Read(@"UBS_VBS\GUAR\GuarCallContr_Cli.vbs");

            pIn.Value("Номер окна", UbsShortNumerator.Number);
            pIn.Value("Тип объекта", "BG");

            if (mode == AddCommand)
            {
                pIn.Value("Идентификатор валюты", cmbCurrencyGarant.SelectedValue);
            }
            if (lvwGuarant.Items.Count > 0 &&
                mode == EditCommand)
            {
                pIn.Value("Идентификатор договора обеспечения", Convert.ToInt64(lvwGuarant.SelectedItems[0].Name.Substring(5)));
            }

            pIn.Value("Идентификатор клиента", m_idPrincipal);
            pIn.Value("Идентификатор объекта", m_idContract);
            pIn.Value("StrCommand", mode);

            m_isGuarWinOpened = true;

            scripter.UbsScriptParam = scripterParameters;

            scripter.Run("GuarAddContr", pIn, pOut);

            GuarCmdState(true);

            ReReadGuarCntracts();
        }

        private void ReReadGuarCntracts()
        {
            var pIn = new UbsParam();
            var pOut = new UbsParam();

            pIn.Value("Id", m_idContract);
            RunUbsChannel("GetListGuarant", pIn, pOut);

            if (pOut.Contains("Обеспечения"))
            {
                m_arrGuarant = pOut.Value("Обеспечения") as object[,];

                FillLvwGuarant();
            }
        }

        private void EnableControls(bool isState = true)
        {
            btnSave.Enabled = (isState && m_isSecure);
            btnExit.Enabled = isState;
        }

        private bool Check()
        {
            if (txtModel.Text.Length < 1)
            {
                MessageBox.Show("Необходимо выбрать типовой договор.", "Ошибка ввода параметров",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                EnableControls();
                tabControl.SelectedIndex = 0;
                linkModel.Focus();
                return false;
            }

            if (txtPrincipal.Text.Length < 1)
            {
                MessageBox.Show("Необходимо выбрать принципала.", "Ошибка ввода параметров",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                EnableControls();
                tabControl.SelectedIndex = 0;
                linkPreviousContract.Focus();
                return false;
            }

            if (txtBeneficiar.Text.Length < 1)
            {
                MessageBox.Show("Необходимо выбрать бенефициара.", "Ошибка ввода параметров",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                EnableControls();
                tabControl.SelectedIndex = 0;
                linkBeneficiar.Focus();
                return false;
            }

            if (txtNumberGarant.Text.Length < 1)
            {
                MessageBox.Show("Необходимо ввести номер договора.", "Ошибка ввода параметров",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                EnableControls();
                tabControl.SelectedIndex = 0;
                txtNumberGarant.Focus();
                return false;
            }

            if (dateOpenGarant.Enabled)
            {
                DateTime d = dateOpenGarant.DateValue;
                if (d <= MinDate || d >= MaxDate)
                {
                    MessageBox.Show("Необходимо указать дату заключения договора.", "Ошибка ввода параметров",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    EnableControls();
                    tabControl.SelectedIndex = 0;
                    dateOpenGarant.Focus();
                    return false;
                }
            }

            if (dateBeginGarant.Enabled)
            {
                DateTime d = dateBeginGarant.DateValue;
                if (d < dateOpenGarant.DateValue || d >= MaxDate)
                {
                    MessageBox.Show("Дата начала договора должна быть не ранее даты заключения.", "Ошибка ввода параметров",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    EnableControls();
                    tabControl.SelectedIndex = 0;
                    dateBeginGarant.Focus();
                    return false;
                }
            }

            if (dateEndGarant.Enabled)
            {
                DateTime d = dateEndGarant.DateValue;
                if (d <= dateBeginGarant.DateValue || d >= MaxDate)
                {
                    MessageBox.Show("Дата окончания договора должна быть больше даты начала договора.", "Ошибка ввода параметров",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    EnableControls();
                    tabControl.SelectedIndex = 0;
                    dateEndGarant.Focus();
                    return false;
                }
            }

            if (m_idFrameContract > 0)
            {
                if (dateOpenGarant.DateValue < m_dateBeginFrameContract)
                {
                    MessageBox.Show(
                        "Дата заключения должна быть не ранее даты начала действия рамочного договора — "
                        + m_dateBeginFrameContract.ToShortDateString() + ".",
                        "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    EnableControls();
                    tabControl.SelectedIndex = 0;
                    dateOpenGarant.Focus();
                    return false;
                }

                if (dateEndGarant.DateValue > m_dateEndFrameContract)
                {
                    MessageBox.Show(
                        "Дата окончания действия договора гарантии должна быть не позднее даты окончания действия рамочного договора — "
                        + m_dateEndFrameContract.ToShortDateString() + ".",
                        "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    EnableControls();
                    tabControl.SelectedIndex = 0;
                    dateEndGarant.Focus();
                    return false;
                }
            }

            if (ucdSumCover.Enabled)
            {
                if (ucdSumCover.DecimalValue <= 0m)
                {
                    MessageBox.Show("Необходимо указать сумму покрытия договора.", "Ошибка ввода параметров",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    EnableControls();
                    tabControl.SelectedIndex = 0;
                    ucdSumCover.Focus();
                    return false;
                }
            }

            if (dateNextPayFee.Enabled)
            {
                DateTime d = dateNextPayFee.DateValue;
                if (d <= MinDate || d >= MaxDate)
                {
                    MessageBox.Show("Необходимо указать дату следующей уплаты вознаграждения.", "Ошибка ввода параметров",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    EnableControls();
                    tabControl.SelectedIndex = 3;
                    dateNextPayFee.Focus();
                    return false;
                }
            }

            if (dateNextPayFeeBonus.Enabled)
            {
                DateTime d = dateNextPayFeeBonus.DateValue;
                if (d <= MinDate || d >= MaxDate)
                {
                    MessageBox.Show("Необходимо указать дату следующей уплаты вознаграждения.", "Ошибка ввода параметров",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    EnableControls();
                    tabControl.SelectedIndex = 3;
                    dateNextPayFeeBonus.Focus();
                    return false;
                }
            }

            if (btnPeriodPayFee.Enabled)
            {
                if (m_arrInterval == null)
                {
                    MessageBox.Show("Необходимо ввести данные по порядку уплаты вознаграждения.", "Ошибка ввода параметров",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    EnableControls();
                    tabControl.SelectedIndex = 3;
                    btnPeriodPayFee.Focus();
                    return false;
                }
            }

            if (cmbOrderPayFeeGuarant.Text == OrderPayPeriodically)
            {
                DateTime d = dateNextPayFeeGuarant.DateValue;
                if (d <= MinDate || d >= MaxDate)
                {
                    MessageBox.Show("Необходимо указать дату следующей уплаты вознаграждения (гарант).", "Ошибка ввода параметров",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    EnableControls();
                    tabControl.SelectedIndex = 3;
                    dateNextPayFeeGuarant.Focus();
                    return false;
                }

                if (m_arrIntervalGuarant == null)
                {
                    MessageBox.Show("Необходимо ввести данные по порядку уплаты вознаграждения (гарант).", "Ошибка ввода параметров",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    EnableControls();
                    tabControl.SelectedIndex = 3;
                    btnPeriodPayFeeGuarant.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool CheckTerm()
        {
            if (m_idFrameContract > 0 && m_termsFrameContract != null && cmbKindGarant.Text != string.Empty)
            {
                int term = (dateEndGarant.DateValue - dateBeginGarant.DateValue).Days + 1;
                string kind = cmbKindGarant.Text;

                for (int i = 0; i < m_termsFrameContract.GetLength(0); i++)
                {
                    if (kind == Convert.ToString(m_termsFrameContract[i, 0]))
                    {
                        int maxTerm = Convert.ToInt32(m_termsFrameContract[i, 1]);
                        if (term > maxTerm)
                        {
                            MessageBox.Show(
                                "Cрок гарантии — " + term + " д. — превышает максимальный срок, указанный в рамочном договоре, — "
                                + maxTerm + " д.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool CheckFrameLimit()
        {
            if (m_idFrameContract > 0 && m_limitExcessCheckOn)
            {
                if (ucdSumGarant.DecimalValue > m_limitSaldoFrameContract)
                {
                    MessageBox.Show(
                        "Сумма гарантии — " + ucdSumGarant.DecimalValue
                        + " — превышает остаток лимита рамочного договора — " + m_limitSaldoFrameContract,
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }

        private bool CheckIssueDate()
        {
            if (m_idFrameContract > 0 && m_issueEndDate != MaxDate)
            {
                if (dateOpenGarant.DateValue > m_issueEndDate)
                {
                    MessageBox.Show(
                        "Дата заключения — " + dateOpenGarant.DateValue.ToShortDateString()
                        + " — позже даты окончания выдачи гарантии, указанной в рамочном договоре, — "
                        + m_issueEndDate.ToShortDateString(),
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }

        private bool FindInExecutEx(int idOI)
        {
            if (m_arrExecutEx == null) return false;
            for (int i = 0; i < m_arrExecutEx.GetLength(0); i++)
            {
                if (Convert.ToInt32(m_arrExecutEx[i, 0]) == idOI)
                    return true;
            }
            return false;
        }

        private DateTime AskRiskChangeDate()
        {
            DateTime result = DateTime.MinValue;

            using (Form form = new Form())
            {
                form.Text = m_captionForm;
                form.Size = new System.Drawing.Size(300, 135);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                Label label = new Label
                {
                    Text = "Введите дату изменения параметров риска:",
                    Location = new System.Drawing.Point(10, 10),
                    Size = new System.Drawing.Size(270, 20)
                };
                DateTimePicker picker = new DateTimePicker
                {
                    Format = DateTimePickerFormat.Short,
                    Location = new System.Drawing.Point(10, 38),
                    Size = new System.Drawing.Size(265, 25)
                };
                Button btnOk = new Button
                {
                    Text = OK,
                    DialogResult = DialogResult.OK,
                    Location = new System.Drawing.Point(115, 72),
                    Size = new System.Drawing.Size(75, 25)
                };
                Button btnCancel = new Button
                {
                    Text = Cancel,
                    DialogResult = DialogResult.Cancel,
                    Location = new System.Drawing.Point(200, 72),
                    Size = new System.Drawing.Size(75, 25)
                };

                form.Controls.Add(label);
                form.Controls.Add(picker);
                form.Controls.Add(btnOk);
                form.Controls.Add(btnCancel);
                form.AcceptButton = btnOk;
                form.CancelButton = btnCancel;

                if (form.ShowDialog(this) == DialogResult.OK)
                    result = picker.Value;
            }

            return result;
        }

        #region Работа со списком счетов (вкладка Счета)

        /// <summary>
        /// Соответствует VB6 mnuListAccounts_Click.
        /// Считывает поля выбранной строки lvwAccounts в поля состояния
        /// и вызывает GetAccount для открытия окна выбора счёта.
        /// </summary>
        private void SelectAccount()
        {
            if (lvwAccounts.Items.Count == 0 || lvwAccounts.SelectedItems.Count == 0)
                return;

            ListViewItem selectedItem = lvwAccounts.SelectedItems[0];

            m_strSection = selectedItem.SubItems[1].Text;   // VB6: SubItems(1) = раздел ("А"/"В")
            m_accType = selectedItem.Text;               // VB6: SelectedItem.Text = тип счёта
            m_strListItemKey = selectedItem.Name;               // VB6: SelectedItem.Key

            GetAccount();
        }

        /// <summary>
        /// Соответствует VB6 DelAccount / Delete-ветка lstAccounts_KeyDown.
        /// Очищает номер счёта и остаток у выбранной строки lvwAccounts
        /// и снимает номер счёта из m_arrAccounts.
        /// </summary>
        private void ClearSelectedAccount()
        {
            if (lvwAccounts.SelectedItems.Count == 0)
                return;

            ListViewItem selectedItem = lvwAccounts.SelectedItems[0];

            selectedItem.SubItems[2].Text = string.Empty;   // номер счёта
            selectedItem.SubItems[3].Text = string.Empty;   // остаток

            if (m_arrAccounts != null)
            {
                int rows = m_arrAccounts.GetLength(0);
                for (int i = 0; i < rows; i++)
                {
                    if (Convert.ToString(m_arrAccounts[i, 0]) == selectedItem.Text)
                    {
                        m_arrAccounts[i, 2] = string.Empty;   // VB6: arrAccounts(2, i) — номер счёта
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Соответствует VB6 GetAccount.
        /// Открывает дочернее окно выбора счёта с фильтром по разделу (m_strSection)
        /// и балансовому счёту (определяется по m_strAccType).
        /// </summary>
        private void GetAccount()
        {
            var filterName = string.Empty;

            if (m_strSection == PartA)
            {
                filterName = @"UBS_FLT\OD\ACCOUNT0.flt";
            }
            else if (m_strSection == PartB)
            {
                filterName = @"UBS_FLT\OD\ACCOUNT2.flt";
            }

            object[] ids = this.Ubs_ActionRun(ActionUbsGuarOperationList, this, true) as object[];

            if (ids != null && ids.Length > 0)
            {
                m_paramIn.Clear();
                m_paramOut.Clear();

                m_paramIn.Value("IdPrevContract", m_idPrevContract);
                RunUbsChannel("BGReadPreviuosContract", m_paramIn, m_paramOut);
                txtPreviousContract.Text = Convert.ToString(m_paramOut.Value("InfoPrevContract"));

                if (tabPage2.Enabled)
                {
                    tabControl.SelectedIndex = 1;

                    cmbPortfolio.Focus();
                }
                else
                {
                    tabControl.SelectedIndex = 2;

                    lvwAccounts.Focus();
                }
            }
            else
            {
                MessageBox.Show(PreviousContractIsNotSelected, m_captionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
        }

        /// <summary>
        /// Соответствует VB6 GetReport.
        /// Открывает окно отчёта по счёту.
        /// </summary>
        /// <param name="reportRow">Индекс строки (0-based) в m_arrListRprByAcc.</param>
        /// <param name="accountNumber">Номер счёта из выбранной строки lvwAccounts.</param>
        private void GetReport(int reportRow)
        {
            //Информация об отчете
            var reportInfo = new object[4];

            //Строковый идентификатор отчета
            reportInfo[0] = Convert.ToString(m_arrListRprByAcc[reportRow, 0]);

            IntPtr intPtr = (IntPtr)base.Run("ParentHandle", null);
            IUbs formReport = (IUbs)Ubs_ActionRun((string)reportInfo[0]
                , Control.FromHandle(intPtr), false);
        }

        #endregion

    }
}
