using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;


namespace Hjmos.CommonControls.Controls
{
    /// <summary>
    /// RollingNumberItem.xaml 的交互逻辑
    /// </summary>
    public partial class RollingNumberItem : INotifyPropertyChanged
    {
        public RollingNumberItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        private Thickness _MarginTop = new Thickness(0, 0, 0, 0);
        /// <summary>
        /// 边距
        /// </summary>
        public Thickness MarginTop
        {
            get { return _MarginTop; }
            set
            {
                _MarginTop = value;
                OnPropertyChanged("MarginTop");
            }
        }

        private double _Num;
        /// <summary>
        /// 数字
        /// </summary>
        public double Num
        {
            get { return _Num; }
            set
            {
                var ease = new ExponentialEase()
                {
                    EasingMode = EasingMode.EaseOut,
                };

                ThicknessAnimation animation = new ThicknessAnimation();
                animation.EasingFunction = ease;
                animation.From = new Thickness(0, 0 - _Num * Height, 0, 0);
                double top1 = MarginTop.Top;

                double d = 0 - value * Height;
                if (value < _Num)
                {
                    d = 0 - (value + 10) * Height;
                }


                _Num = value;
                OnPropertyChanged("Num");


                MarginTop = new Thickness(0, d, 0, 0);
                double top2 = MarginTop.Top;

                animation.To = MarginTop;

                //animation.Duration = TimeSpan.FromMilliseconds((top1 - top2) * 10);
                if (top1 != top2)
                {
                    animation.Duration = TimeSpan.FromMilliseconds(1500);
                    this.stackPanel.BeginAnimation(StackPanel.MarginProperty, animation);
                }

                MarginTop = new Thickness(0, 0 - value * Height, 0, 0);
            }
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
        #endregion
    }
}
