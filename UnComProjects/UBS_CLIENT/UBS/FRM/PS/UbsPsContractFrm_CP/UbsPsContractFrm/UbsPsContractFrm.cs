using System;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// Шаблон класса формы
    /// </summary>
    public partial class UbsPsContractFrm : UbsFormBase
    {
        #region Блок объявления переменных

        private string m_command = "";    //параметер запуска формы

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsPsContractFrm()
        {
            m_addCommand(); //зарегистрировать обработчики команд интерфейса IUbs

            InitializeComponent();

            // установить имя ресурса, с которым будет работать канал
            this.IUbsChannel.LoadResource = "ASM:UBS_ASM\\Business\\DllName.dll->UbsBusiness.NameClass";

            m_addFields(); //заполнить коллекцию полей формы

            base.Ubs_CommandLock = true;
        }


        #region Обработчики событий кнопок (с примерами)

        private void btnExit_Click(object sender, EventArgs e) { this.Close(); }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }

            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion


        #region Обработчики команд IUbs интерфейса (с примерами)

        /// <summary>
        /// Процедура регистрации обработчиков команд интерфейса IUbs в базовом классе
        /// </summary>
        private void m_addCommand()
        {
            base.Ubs_AddName(new UbsDelegate(CommandLine));
            base.Ubs_AddName(new UbsDelegate(ListKey));
        }
        /// <summary>
        /// Процедура обработки команды CommandLine
        /// </summary>
        /// <param name="param_in">Входной параметер</param>
        /// <param name="param_out">Выходной параметер</param>
        /// <returns></returns>
        private object CommandLine(object param_in, ref object param_out)
        {
            //код реализации обработчика команды CommandLine
            m_command = (string)param_in;
            return null;
        }
        /// <summary>
        /// Процедура обработки команды ListKey
        /// </summary>
        /// <param name="param_in">Входной параметер</param>
        /// <param name="param_out">Выходной параметер</param>
        /// <returns></returns>
        private object ListKey(object param_in, ref object param_out)
        {
            try
            {
                //код реализации обработчика команды ListKey
                return null;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        #endregion

        private void m_addFields()
        {
            base.IUbsFieldCollection.Add("Имя поля", new UbsFormField(btnSave, "Text"));
            base.IUbsFieldCollection["Имя поля"].ReadOnly = true;
        }

    }
}
