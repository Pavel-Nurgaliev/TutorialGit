using System;
using System.Collections.Generic;
using UbsService;

namespace UbsBusiness
{
    public partial class UbsPsContractFrm
    {
        private enum BrowseListKind
        {
            None,
            Client,
            Account,
            KindPayment
        }

        private BrowseListKind m_browseListKind;
        private string m_pendingKindPaymentListAction;

        private void UbsPsContractFrm_Ubs_ActionRunBegin(object sender, UbsActionRunEventArgs args)
        {
            try
            {
                if (string.Equals(args.Action, ActionListCommonClient, StringComparison.Ordinal))
                {
                    args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("наименование", "Тип"),
                        new KeyValuePair<string, object>("значение по умолчанию", 1),
                        new KeyValuePair<string, object>("условие по умолчанию", "="),
                        new KeyValuePair<string, object>("скрытый", true) }));
                    args.IUbs.Run("UbsItemsRefresh", null);
                    return;
                }
                if (string.Equals(args.Action, ActionListOdAccount0, StringComparison.Ordinal))
                {
                    args.IUbs.Run("UbsItemsRemove", null);
                    if (m_idClient > 0)
                    {
                        args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                            new KeyValuePair<string, object>("наименование", "Идентификатор клиента"),
                            new KeyValuePair<string, object>("значение по умолчанию", m_idClient),
                            new KeyValuePair<string, object>("условие по умолчанию", "="),
                            new KeyValuePair<string, object>("скрытый", true) }));
                    }
                    args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("наименование", "Балансовый счет"),
                        new KeyValuePair<string, object>("значение по умолчанию", string.Empty),
                        new KeyValuePair<string, object>("условие по умолчанию", "="),
                        new KeyValuePair<string, object>("скрытый", true) }));
                    args.IUbs.Run("UbsItemsRefresh", null);
                    return;
                }
                if (m_browseListKind == BrowseListKind.KindPayment
                    && m_pendingKindPaymentListAction != null
                    && string.Equals(args.Action, m_pendingKindPaymentListAction, StringComparison.Ordinal))
                {
                    args.IUbs.Run("UbsItemsRefresh", null);
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void OpenBrowseClientList()
        {
            m_browseListKind = BrowseListKind.Client;
            try
            {
                object[] ids = this.Ubs_ActionRun(ActionListCommonClient, this, true) as object[];
                if (ids != null && ids.Length > 0)
                {
                    ApplyBrowseSelectedClient(Convert.ToInt32(ids[0]));
                }
            }
            finally
            {
                m_browseListKind = BrowseListKind.None;
            }
        }

        private void OpenBrowseAccountList()
        {
            m_browseListKind = BrowseListKind.Account;
            try
            {
                object[] ids = this.Ubs_ActionRun(ActionListOdAccount0, this, true) as object[];
                if (ids != null && ids.Length > 0)
                {
                    ApplyBrowseSelectedAccount(Convert.ToInt32(ids[0]));
                }
            }
            finally
            {
                m_browseListKind = BrowseListKind.None;
            }
        }

        private void OpenBrowseKindPaymentList()
        {
            string listAction = UBS_PS_UT_LIST_KIND_PAYMENT;
            if (listAction == null || listAction.Trim().Length == 0)
            {
                base.Ubs_ShowErrorBox(MsgKindListFilterMissing);
                return;
            }
            m_browseListKind = BrowseListKind.KindPayment;
            m_pendingKindPaymentListAction = listAction;
            try
            {
                object[] ids = this.Ubs_ActionRun(listAction, this, true) as object[];
                if (ids != null && ids.Length > 0)
                {
                    ApplyBrowseSelectedKindPayment(Convert.ToInt32(ids[0]));
                }
            }
            finally
            {
                m_pendingKindPaymentListAction = null;
                m_browseListKind = BrowseListKind.None;
            }
        }

        private string ResolveKindPaymentListActionName()
        {
            try
            {
                string filterContract = m_contractListFilterName != null ? m_contractListFilterName : string.Empty;
                base.IUbsChannel.ParamIn("FILTERCONTRACT", filterContract);
                base.IUbsChannel.Run("GetNameFilterKindPaym");
                UbsParam po = base.IUbsChannel.ParamsOutParam;
                if (po.Contains("FILTERKINDPAYM"))
                {
                    string f = Convert.ToString(po.Value("FILTERKINDPAYM"));
                    if (f != null && f.Trim().Length > 0)
                    {
                        return f.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
            return FilterKindPaymentListFallback;
        }
    }
}
