using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Hjmos.BaseControls.Tools
{
    public class ListBoxItemStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (container is ListBoxItem listBoxitem && VisualHelper.GetParent<Hjmos.BaseControls.Controls.ListBox>(listBoxitem) is { } listbox)
            {
                var count = listbox.Items.Count;

                if(count==1)
                {
                    return ResourceHelper.GetResource<Style>("ListBoxItemSingle");
                }

                var index = listbox.ItemContainerGenerator.IndexFromContainer(listBoxitem);
                return listbox.Orientation == Orientation.Horizontal
                    ? index == 0
                    ? ResourceHelper.GetResource<Style>("ListBoxItemHorizontalFirst")
                    : ResourceHelper.GetResource<Style>(index == count - 1 ? "ListBoxItemHorizontalLast" : "ListBoxItemBaseStyle")
                    : index == 0
                    ? ResourceHelper.GetResource<Style>("ListBoxItemVerticalFirst")
                    : ResourceHelper.GetResource<Style>(index == count - 1 ? "ListBoxItemVerticalLast" : "ListBoxItemBaseStyle");
            }
            return null;
        }
    }
}
