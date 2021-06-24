using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
namespace Controltest
{
   public class UserControltest : UserControl
    {
        public UserControltest()
        {
            this.Loaded += PopupUserControl_Loaded;
        }

        private void PopupUserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //MessageBox.Show("11");
            if (this.DataContext is Interface1 interface1)
            {
               
                    interface1.Call();
              
            }
        }
    }
}
