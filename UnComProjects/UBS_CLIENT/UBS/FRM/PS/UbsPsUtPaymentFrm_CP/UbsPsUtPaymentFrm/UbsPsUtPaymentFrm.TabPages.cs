using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentFrm
    {
        // In WinForms TabPage.Hide()/Visible=false does NOT remove the tab
        // header from the parent TabControl tab-strip. The only reliable way
        // to actually hide a tab is to remove the page from
        // TabControl.TabPages, and to re-insert it (at its designer-defined
        // index) when it should reappear.
        //
        // We deliberately do NOT use TabPages.Insert(index, page): in this
        // WinForms version it can silently no-op (the call returns normally
        // but TabPages.Count stays unchanged and the new tab header doesn't
        // appear in the strip). Instead we always use TabPages.Add(page) -
        // which is reliable - and then SortTabPages() reshuffles the
        // collection back into the canonical designer-defined order.
        //
        // We also never set TabPage.Visible to false: keeping it true at all
        // times is the simplest way to stay clear of WinForms TabPage
        // visibility quirks. The canonical "is this tab page currently
        // shown?" check is IsTabPageShown(...), which just looks up
        // tabPayment.TabPages.Contains(page).

        private TabPage[] m_orderedTabPages;

        /// <summary>
        /// Returns the canonical (designer-defined) order of the tab pages
        /// that may be conditionally shown/hidden on the payment form.
        /// </summary>
        private TabPage[] GetOrderedTabPages()
        {
            if (m_orderedTabPages == null)
            {
                m_orderedTabPages = new TabPage[]
                {
                    tabPageGeneral,
                    tabPageThirdPerson,
                    tabPageTariff,
                    tabPageTelephone,
                    tabPageTax,
                    tabPageAddFields
                };
            }
            return m_orderedTabPages;
        }

        /// <summary>
        /// Returns true if the given tab page is currently attached to
        /// tabPayment (i.e. its header is visible in the tab-strip). Use this
        /// instead of <c>tabPageX.Visible</c> checks.
        /// </summary>
        private bool IsTabPageShown(TabPage page)
        {
            if (page == null) return false;
            return tabPayment.TabPages.Contains(page);
        }

        /// <summary>
        /// Adds <paramref name="page"/> to tabPayment.TabPages (using Add,
        /// then reshuffling the collection back into designer order). Safe
        /// to call when the page is already present (no-op in that case).
        /// </summary>
        private void ShowTabPage(TabPage page)
        {
            if (page == null) return;

            // Belt-and-braces: TabPages.Add silently refuses to add a page
            // whose Visible == false. We never set Visible=false ourselves,
            // but external code might; flip it back to true defensively.
            if (!page.Visible)
            {
                page.Visible = true;
            }

            if (tabPayment.TabPages.Contains(page))
            {
                return;
            }

            tabPayment.TabPages.Add(page);
            SortTabPages();
        }

        /// <summary>
        /// Removes <paramref name="page"/> from tabPayment.TabPages so that
        /// its tab header disappears from the tab-strip. Safe to call when
        /// the page is already absent.
        /// </summary>
        private void HideTabPage(TabPage page)
        {
            if (page == null) return;

            if (tabPayment.TabPages.Contains(page))
            {
                tabPayment.TabPages.Remove(page);
            }
        }

        /// <summary>
        /// Reshuffles tabPayment.TabPages so that the currently attached
        /// pages appear in the canonical designer-defined order
        /// (<see cref="GetOrderedTabPages"/>). No-op when the collection is
        /// already in order.
        /// </summary>
        private void SortTabPages()
        {
            TabPage[] order = GetOrderedTabPages();

            // Snapshot current order.
            List<TabPage> current = new List<TabPage>(tabPayment.TabPages.Count);
            foreach (TabPage p in tabPayment.TabPages)
            {
                current.Add(p);
            }

            // Build desired order: same pages, sorted by canonical position.
            List<TabPage> sorted = new List<TabPage>(current);
            sorted.Sort(delegate(TabPage a, TabPage b)
            {
                int ia = Array.IndexOf(order, a);
                int ib = Array.IndexOf(order, b);
                if (ia < 0) ia = int.MaxValue;
                if (ib < 0) ib = int.MaxValue;
                return ia.CompareTo(ib);
            });

            // Already in order? Avoid the (relatively expensive) clear+add cycle.
            bool inOrder = true;
            for (int i = 0; i < current.Count; i++)
            {
                if (current[i] != sorted[i])
                {
                    inOrder = false;
                    break;
                }
            }
            if (inOrder)
            {
                return;
            }

            // Rebuild the collection. Preserve the currently selected tab.
            TabPage selected = tabPayment.SelectedTab;
            tabPayment.SuspendLayout();
            try
            {
                tabPayment.TabPages.Clear();
                for (int i = 0; i < sorted.Count; i++)
                {
                    tabPayment.TabPages.Add(sorted[i]);
                }
                if (selected != null && sorted.Contains(selected))
                {
                    tabPayment.SelectedTab = selected;
                }
            }
            finally
            {
                tabPayment.ResumeLayout();
            }
        }
    }
}
