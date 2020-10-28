using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;


namespace Hjmos.CommonControls.Controls
{
    /// <summary>
    /// RollingNumber.xaml 的交互逻辑
    /// </summary>
    public partial class RollingNumber : INotifyPropertyChanged
    {
        public RollingNumber()
        {
            InitializeComponent();
        }
        private bool _firstLoaded = true;

        public double ItemHeight
        {
            get { return (double)this.GetValue(RollingNumber.ItemHeightProperty); }
            set
            { this.SetValue(RollingNumber.ItemHeightProperty, value); }
        }
        private static DependencyProperty ItemHeightProperty = DependencyProperty.Register("ItemHeight", typeof(double), typeof(RollingNumber));

        public string NumStr
        {
            get { return (string)this.GetValue(RollingNumber.NumStrProperty); }
            set
            { this.SetValue(RollingNumber.NumStrProperty, value); }
        }
        private static DependencyProperty NumStrProperty = DependencyProperty.Register("NumStr", typeof(string), typeof(RollingNumber), new PropertyMetadata("0", new PropertyChangedCallback(NumStrChanged)));

        private static void NumStrChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as RollingNumber).UpdateNumStr((sender as RollingNumber).NumStr);
        }

        private void UpdateNumStr(string numStr)
        {
            Text = numStr;
        }

        private string _Text;
        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                OnPropertyChanged("Text");

                if (!_firstLoaded)
                {
                    if (_Text.Length > stackPanel.Children.Count)
                    {
                        int k = _Text.Length - stackPanel.Children.Count;
                        while (k > 0)
                        {
                            CreateControl();
                            k--;
                        }
                    }
                    RollingNumberItem[] numArr = new RollingNumberItem[stackPanel.Children.Count];
                    int index = 1;

                    foreach (Border num in stackPanel.Children)
                    {
                        RollingNumberItem rni = (RollingNumberItem)num.Child;
                        numArr[numArr.Length - index] = rni;
                        index++;
                    }

                    if (_Text != null)
                    {
                        int i = 0;
                        foreach (char c in _Text.Reverse())
                        {
                            double d = Convert.ToInt32(c - 48); ;

                            numArr[i++].Num = d;
                        }
                    }
                }
            }
        }
        void CreateControl()
        {
            Border border = new Border
            {
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF11D4FF")),
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2511D4FF")),
                CornerRadius = new CornerRadius(2),
                Margin = new Thickness(4)
            };


            RollingNumberItem rollingNumberItem = new RollingNumberItem
            {
                Height = this.ItemHeight,
                Width = this.ItemHeight * 0.75,
                FontWeight = this.FontWeight,
                FontSize = 22,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0AC7F1")),
                Num = 0,
                Margin = new Thickness(2),
                Effect = new DropShadowEffect
                {
                    Color = (Color)ColorConverter.ConvertFromString("#FF0AC7F1"),
                    ShadowDepth = 2,
                    Direction = 270
                }
            };
            border.Child = rollingNumberItem;
            stackPanel.Children.Add(border);
        }


        #region INotifyPropertyChanged接口
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_firstLoaded) _firstLoaded = false;
            Text = NumStr;
        }
        #endregion
    }
}
