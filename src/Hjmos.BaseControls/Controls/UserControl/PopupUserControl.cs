﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
namespace Hjmos.BaseControls.Controls
{
    public class PopupUserControl : UserControl
    {

        public PopupUserControl()
        {
            this.Loaded += PopupUserControl_Loaded;
        }

        private void PopupUserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //MessageBox.Show("11");
           // if(this.DataContext is Interface1)
        }
    }
}
