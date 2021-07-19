using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Hjmos.BaseControls.Controls
{
    public class ListBoxEditable : System.Windows.Controls.ListBox
    {

        public ListBoxEditable()
        {
            AddHandler(Controls.ListBoxEditableItem.ClosedEvent, new RoutedEventHandler(ItemClosed));
        }


        private void ItemClosed(object sender, RoutedEventArgs e)
        {
            var source = e.OriginalSource;
            var sourcepIndex = this.ItemContainerGenerator.IndexFromContainer((ListBoxItem)source);
            var list = GetList(this.ItemsSource);
            list.RemoveAt(sourcepIndex);
        }

        protected static IList GetList(IEnumerable enumerable)
        {
            if (enumerable is ICollectionView)
            {
                return ((ICollectionView)enumerable).SourceCollection as IList;
            }

            return enumerable as IList;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => item is ListBoxEditableItem;

        protected override DependencyObject GetContainerForItemOverride() => new ListBoxEditableItem();


       
    }
}
