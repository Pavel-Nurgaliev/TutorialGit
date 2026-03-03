using System;
using System.Collections.Generic;
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
    }
}
