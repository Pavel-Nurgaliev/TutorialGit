using System;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// Форма реквизитов бенефициара
    /// </summary>
    public partial class UbsBgDetailsBenificiarFrm : UbsFormBase
    {
        #region Блок объявления переменных

        private string m_command = string.Empty;
        private bool m_blnUseCountry = false;

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsBgDetailsBenificiarFrm()
        {
            InitializeComponent();

            base.Ubs_CommandLock = true;
        }

        public bool ApplyClicked { get; set; }
        public object[,] arrTypeObject { get; set; }
        public object[,] arrCountry { get; set; }

        #region Обработчики событий кнопок

        private void btnExit_Click(object sender, EventArgs e) 
        { 
            ApplyClicked = false;
            this.Close(); 
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }

                ApplyClicked = true;
                this.Close();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region Обработчики команд IUbs интерфейса

        #endregion

        private void cbCodeCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_blnUseCountry) return;

            if (arrCountry == null) return;

            m_blnUseCountry = true;

            for (int i = 0; i < arrCountry.GetLength(1); i++)
            {
                if (arrCountry[0, i].ToString().Trim() == cbCodeCountry.Text.Trim())
                {
                    cbCountry.Text = arrCountry[1, i].ToString();
                    break;
                }
            }

            ChangeTypeObject();

            m_blnUseCountry = false;
        }

        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_blnUseCountry) return;

            if (arrCountry == null) return;

            m_blnUseCountry = true;

            for (int i = 0; i < arrCountry.GetLength(1); i++)
            {
                if (arrCountry[1, i].ToString().Trim() == cbCountry.Text.Trim())
                {
                    cbCodeCountry.Text = arrCountry[0, i].ToString();
                    break;
                }
            }

            ChangeTypeObject();

            m_blnUseCountry = false;
        }

        private void ChangeTypeObject()
        {
            if (arrTypeObject == null) return;

            if (cbCodeCountry.Text != "643") // Не Россия
            {
                cbTypeRegion.Items.Clear();
                cbTypeArea.Items.Clear();
                cbTypeCity.Items.Clear();
                cbTypeSettl.Items.Clear();
                cbTypeStreet.Items.Clear();
                cbTypeHome.Items.Clear();
                cbTypeHousing.Items.Clear();
                cbTypeFlat.Items.Clear();
            }
            else // Россия
            {
                // Проверяем, не заполнены ли уже комбобоксы
                if (cbTypeRegion.Items.Count > 0 || cbTypeArea.Items.Count > 0 || 
                    cbTypeCity.Items.Count > 0 || cbTypeSettl.Items.Count > 0 ||
                    cbTypeStreet.Items.Count > 0 || cbTypeHome.Items.Count > 0 || 
                    cbTypeHousing.Items.Count > 0 || cbTypeFlat.Items.Count > 0)
                {
                    return;
                }

                // Заполняем комбобоксы типами объектов адреса
                for (int i = 0; i < arrTypeObject.GetLength(1); i++)
                {
                    int typeId = Convert.ToInt32(arrTypeObject[0, i]);
                    string typeName = arrTypeObject[1, i].ToString();

                    switch (typeId)
                    {
                        case 1:
                            cbTypeRegion.Items.Add(typeName);
                            break;
                        case 2:
                            cbTypeArea.Items.Add(typeName);
                            break;
                        case 3:
                            cbTypeCity.Items.Add(typeName);
                            break;
                        case 4:
                            cbTypeSettl.Items.Add(typeName);
                            break;
                        case 5:
                            cbTypeStreet.Items.Add(typeName);
                            break;
                        case 6:
                            cbTypeHome.Items.Add(typeName);
                            break;
                        case 7:
                            cbTypeHousing.Items.Add(typeName);
                            break;
                        case 8:
                            cbTypeFlat.Items.Add(typeName);
                            break;
                    }
                }
            }
        }
    }
}
