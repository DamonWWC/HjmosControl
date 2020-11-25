using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Hjmos.BaseControls.Controls
{

    [TemplatePart(Name =PART_Condition,Type =typeof(TextBlock))]
    public class Weather : Control
    {

        private const string PART_Condition = "PART_Condition";

        public Weather()
        {

        }
        TextBlock textBlock;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            textBlock = GetTemplateChild(PART_Condition) as TextBlock;

            if(textBlock!=null)
            {
                textBlock.FontSize = this.FontSize + 5;
            }

        }


        public WeatherData WeatherData
        {
            get { return (WeatherData)GetValue(WeatherDataProperty); }
            set { SetValue(WeatherDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WeatherData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WeatherDataProperty =
            DependencyProperty.Register("WeatherData", typeof(WeatherData), typeof(Weather), new PropertyMetadata(default(WeatherData)));



        public int UpdateTime
        {
            get { return (int)GetValue(UpdateTimeProperty); }
            set { SetValue(UpdateTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpdateTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpdateTimeProperty =
            DependencyProperty.Register("UpdateTime", typeof(int), typeof(Weather), new PropertyMetadata(0));



        




    }


  
}
