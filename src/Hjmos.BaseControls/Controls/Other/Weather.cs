using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Hjmos.BaseControls.Controls
{

    [TemplatePart(Name =PART_Condition,Type =typeof(TextBlock))]
    public class Weather : Control
    {

        private const string PART_Condition = "PART_Condition";
        private readonly DispatcherTimer _dispatcherTimer;
        private bool _isDisposed;
        public Weather()
        {
            _dispatcherTimer = new DispatcherTimer(DispatcherPriority.Render)
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
        }

      

        ~Weather()
        {
            if (_isDisposed) return;
            _dispatcherTimer.Stop();
            _isDisposed = true;
            GC.SuppressFinalize(this);

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
            DependencyProperty.Register("WeatherData", typeof(WeatherData), typeof(Weather), new PropertyMetadata(default(WeatherData),(o,args)=>
            {
                var weather = (Weather)o;
                var weatherData = (WeatherData)args.NewValue;

                if (weatherData != null)
                {
                    weather._dispatcherTimer.Stop();                   
                    weather.UpdateTime = 0;                   
                    weather._dispatcherTimer.Start();
                }
                else
                {
                    weather._dispatcherTimer.Stop();
                             
                }

            }));

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            UpdateTime++;
        }

       


        internal int UpdateTime
        {
            get { return (int)GetValue(UpdateTimeProperty); }
            set { SetValue(UpdateTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpdateTime.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty UpdateTimeProperty =
            DependencyProperty.Register("UpdateTime", typeof(int), typeof(Weather), new PropertyMetadata(0));



        




    }


  
}
