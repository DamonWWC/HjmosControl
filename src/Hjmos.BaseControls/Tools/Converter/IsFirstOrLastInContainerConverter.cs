using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Hjmos.BaseControls.Tools.Converter
{
    public class IsFirstOrLastInContainerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DependencyObject item = (DependencyObject)value;
            ItemsControl itemscontrol = ItemsControl.ItemsControlFromItemContainer(item);
            var index = itemscontrol.ItemContainerGenerator.IndexFromContainer(item);           
            var lastindex = itemscontrol.Items.Count - 1;
          
            return index == 0 ? 0 : index == lastindex ? 1 : 2;        
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
