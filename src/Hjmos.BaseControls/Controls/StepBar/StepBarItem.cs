using Hjmos.BaseControls.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hjmos.BaseControls.Controls
{
    /// <summary>
    /// 步骤条单元项
    /// </summary>
    public class StepBarItem : ContentControl
    {

        /// <summary>
        /// 步骤编号
        /// </summary>
        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Index.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.Register("Index", typeof(int), typeof(StepBarItem), new PropertyMetadata(-1));


        /// <summary>
        /// 步骤状态
        /// </summary>
        public StepStatus Status
        {
            get { return (StepStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(StepStatus), typeof(StepBarItem), new PropertyMetadata(StepStatus.Waiting));


        /// <summary>
        /// 中间过渡线显示
        /// </summary>
        public Visibility LineVisibility
        {
            get { return (Visibility)GetValue(LineVisibilityProperty); }
            set { SetValue(LineVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineVisibilityProperty =
            DependencyProperty.Register("LineVisibility", typeof(Visibility), typeof(StepBarItem), new PropertyMetadata(Visibility.Visible));
  
    }
}
