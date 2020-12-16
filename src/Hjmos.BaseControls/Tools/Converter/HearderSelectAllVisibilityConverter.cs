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
    public class HearderSelectAllVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
           if (values.Length == 3 && values[0] is bool IsSelectAll && values[1] is DataGridHeadersVisibility headervis &&values[2] is string name)
            {
                if(headervis!=DataGridHeadersVisibility.All)
                    return Visibility.Collapsed;
                if (IsSelectAll && name == "CheckAll") 
                    return Visibility.Visible;
                else if(!IsSelectAll && name== "Number")
                    return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
