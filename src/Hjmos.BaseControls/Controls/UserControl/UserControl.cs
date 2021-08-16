using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hjmos.BaseControls.Controls
{
    public class UserControl : System.Windows.Controls.UserControl
    {




        /// <summary>
        /// 加载中
        /// </summary>
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsBusy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(UserControl), new PropertyMetadata(false));



        /// <summary>
        /// 有无数据
        /// </summary>
        public bool IsShowData
        {
            get { return (bool)GetValue(IsShowDataProperty); }
            set { SetValue(IsShowDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowDataProperty =
            DependencyProperty.Register("IsShowData", typeof(bool), typeof(UserControl), new PropertyMetadata(false));





    }
}
