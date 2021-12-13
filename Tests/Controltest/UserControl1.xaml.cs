using Hjmos.BaseControls.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controltest
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += UserControl1_MouseLeftButtonDown;
        }

        private void UserControl1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var paren = VisualHelper.GetParent<ContentPresenter>(this);
            var par1 = VisualHelper.GetParent<StackPanel>(paren);
            int maxIndex = 0;
            if (par1 is Panel panel)
            {
                foreach (UIElement item in panel.Children)
                {
                    if (maxIndex < Panel.GetZIndex(item))
                        maxIndex = Panel.GetZIndex(item);
                }
            }
            Panel.SetZIndex(paren, maxIndex + 1);

            //Panel.SetZIndex(paren, 100);
            //if(Parent is StackPanel parent)
            //{
            //    Canvas.SetZIndex(parent, 100);
            //    //Panel.SetZIndex(parent, 100);
            //}
        }
    }
}