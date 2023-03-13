using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Controltest
{
    public class Datatemplate1Selector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate
        {
            get;set;
        }
        public DataTemplate ChangeStationTemplate
        {
            get;set;
        }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FloodInformation floodInformation = (FloodInformation)item;
            


            return floodInformation.IsChangeStation?ChangeStationTemplate:DefaultTemplate;
        }
    }
}
