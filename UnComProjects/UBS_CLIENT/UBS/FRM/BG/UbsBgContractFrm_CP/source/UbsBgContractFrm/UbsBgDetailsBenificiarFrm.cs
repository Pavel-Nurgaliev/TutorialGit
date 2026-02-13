using System;
using System.Windows.Forms;
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

        // Public properties to expose control values for main form access
        public string NameValue { get { return tbName.Text; } set { tbName.Text = value; } }
        public string INNValue { get { return tbINN.Text; } set { tbINN.Text = value; } }
        public string IndexValue { get { return tbIndex.Text; } set { tbIndex.Text = value; } }
        public string CodeCountryValue { get { return cbCodeCountry.Text; } set { cbCodeCountry.Text = value; } }
        public string CountryValue { get { return cbCountry.Text; } set { cbCountry.Text = value; } }
        public string TypeRegionValue { get { return cbTypeRegion.Text; } set { cbTypeRegion.Text = value; } }
        public string RegionValue { get { return tbRegion.Text; } set { tbRegion.Text = value; } }
        public string TypeAreaValue { get { return cbTypeArea.Text; } set { cbTypeArea.Text = value; } }
        public string AreaValue { get { return tbArea.Text; } set { tbArea.Text = value; } }
        public string TypeCityValue { get { return cbTypeCity.Text; } set { cbTypeCity.Text = value; } }
        public string CityValue { get { return tbCity.Text; } set { tbCity.Text = value; } }
        public string TypeSettlValue { get { return cbTypeSettl.Text; } set { cbTypeSettl.Text = value; } }
        public string SettlValue { get { return tbSettl.Text; } set { tbSettl.Text = value; } }
        public string TypeStreetValue { get { return cbTypeStreet.Text; } set { cbTypeStreet.Text = value; } }
        public string StreetValue { get { return tbStreet.Text; } set { tbStreet.Text = value; } }
        public string TypeHomeValue { get { return cbTypeHome.Text; } set { cbTypeHome.Text = value; } }
        public string HomeValue { get { return tbHome.Text; } set { tbHome.Text = value; } }
        public string TypeHousingValue { get { return cbTypeHousing.Text; } set { cbTypeHousing.Text = value; } }
        public string HousingValue { get { return tbHousing.Text; } set { tbHousing.Text = value; } }
        public string TypeFlatValue { get { return cbTypeFlat.Text; } set { cbTypeFlat.Text = value; } }
        public string FlatValue { get { return tbFlat.Text; } set { tbFlat.Text = value; } }

        // Expose combo boxes for Items manipulation
        public System.Windows.Forms.ComboBox CbCodeCountry { get { return cbCodeCountry; } }
        public System.Windows.Forms.ComboBox CbCountry { get { return cbCountry; } }
        public System.Windows.Forms.ComboBox CbTypeRegion { get { return cbTypeRegion; } }
        public System.Windows.Forms.ComboBox CbTypeArea { get { return cbTypeArea; } }
        public System.Windows.Forms.ComboBox CbTypeCity { get { return cbTypeCity; } }
        public System.Windows.Forms.ComboBox CbTypeSettl { get { return cbTypeSettl; } }
        public System.Windows.Forms.ComboBox CbTypeStreet { get { return cbTypeStreet; } }
        public System.Windows.Forms.ComboBox CbTypeHome { get { return cbTypeHome; } }
        public System.Windows.Forms.ComboBox CbTypeHousing { get { return cbTypeHousing; } }
        public System.Windows.Forms.ComboBox CbTypeFlat { get { return cbTypeFlat; } }

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

            // Arrays from channel are in VB.NET format (col, row), convert to C# [row, col] access
            // VB.NET: arrCountry(0, i) → C#: arrCountry[i, 0]
            for (int i = 0; i < arrCountry.GetLength(0); i++)
            {
                if (arrCountry[i, 0].ToString().Trim() == cbCodeCountry.Text.Trim())
                {
                    cbCountry.Text = arrCountry[i, 1].ToString();
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

            // Arrays from channel are in VB.NET format (col, row), convert to C# [row, col] access
            // VB.NET: arrCountry(1, i) → C#: arrCountry[i, 1]
            for (int i = 0; i < arrCountry.GetLength(0); i++)
            {
                if (arrCountry[i, 1].ToString().Trim() == cbCountry.Text.Trim())
                {
                    cbCodeCountry.Text = arrCountry[i, 0].ToString();
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
                // Arrays from channel are in VB.NET format (col, row), convert to C# [row, col] access
                // VB.NET: arrTypeObject(0, i) → C#: arrTypeObject[i, 0]
                for (int i = 0; i < arrTypeObject.GetLength(0); i++)
                {
                    int typeId = Convert.ToInt32(arrTypeObject[i, 0]);
                    string typeName = arrTypeObject[i, 1].ToString();

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
