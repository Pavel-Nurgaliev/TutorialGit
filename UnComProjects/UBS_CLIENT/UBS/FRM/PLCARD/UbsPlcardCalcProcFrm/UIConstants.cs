using System;
using System.Collections.Generic;
using System.Text;

namespace UbsPlcardCalcProcFrm
{
    public static class UIText
    {
        public const string AccumCommon =
            "Накопленные проценты будут рассчитаны";

        public const string AccumOvd =
            "Накопленные проценты за овердрафт будут рассчитаны";

        public const string AccumLoan =
            "Накопленные проценты за просрочку будут рассчитаны";

        public const string Payment =
            "Проценты будут уплачены";

        public const string PaymentDate =
            "Дата очередной уплаты процентов";

        public const string IntervalBefore =
            "за интервалы заканчивающиеся ДО";

        public const string IntervalContains =
            "находится в интервале заканчивающемся ДО";
    }

    public static class ModeText
    {
        public const string AllCard = "ALL_CARD";
        public const string AllCardOvd = "ALL_CARD_OVD";
        public const string AllCardLoan = "ALL_CARD_LOAN";

        public const string PaymOneOvd = "PAYM_ONE_OVD";
        public const string OneCardLoan = "ONE_CARD_LOAN";

        public const string PaymInTime = "PAYM_IN_TIME";
        public const string PaymNotInTime = "PAYM_NOT_IN_TIME";

        public const string AllCardNotInTime = "ALL_CARD_NOT_IN_TIME";

        public const string ACCUM = "ACCUM";
        public const string PAYM = "PAYM";
    }
    public static class ActionText
    {
        public const string UbsPlcardPrecentAllCardNotInTime = "UBS_PLCARD_PERCENT_ALL_CARD_NOT_IN_TIME";
        public const string CalcultionPercentOutsideGraph = "Начисление % по всем счетам карт вне графика";
        public const string UbsPlcardPrecentAllCardOvd = "UBS_PLCARD_PERCENT_ALL_CARD_OVD";
        public const string CalcultionPercentDebt = "Начисление % по всем счетам задолженности";
    }
    public static class YesNoText
    {
        public const string Yes = "Да";
        public const string No = "Нет";
    }
}
