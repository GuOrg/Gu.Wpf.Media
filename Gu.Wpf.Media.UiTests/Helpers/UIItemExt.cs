namespace Gu.Wpf.Media.UiTests.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    using TestStack.White.UIItems;
    using TestStack.White.UIItems.Finders;

    public static class UiItemExt
    {
        public static string ItemStatus(this IUIItem item)
        {
            return
                (string)item.AutomationElement.GetCurrentPropertyValue(AutomationElementIdentifiers.ItemStatusProperty);
        }

        public static IReadOnlyList<T> GetMultiple<T>(this UIItemContainer container, string automationId)
            where T : IUIItem
        {
            return container.GetMultiple(SearchCriteria.ByAutomationId(automationId))
                            .OfType<T>()
                            .ToList();
        }

        public static T GetByText<T>(this UIItemContainer container, string text)
            where T : UIItem
        {
            return container.Get<T>(SearchCriteria.ByText(text));
        }

        public static T GetByIndex<T>(this UIItemContainer container, int index)
            where T : UIItem
        {
            return container.Get<T>(SearchCriteria.Indexed(index));
        }

        public static IEnumerable<IUIItem> Ancestors(this IUIItem item)
        {
            var parent = item.GetParent<IUIItemContainer>();
            while (parent != null)
            {
                yield return parent;
                parent = parent.GetParent<IUIItemContainer>();
            }
        }
    }
}