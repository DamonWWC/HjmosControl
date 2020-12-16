using Hjmos.BaseControls.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Hjmos.BaseControls.Controls
{
    public class DataGrid : System.Windows.Controls.DataGrid
    {

        public DataGrid()
        {
            CommandBindings.Add(new System.Windows.Input.CommandBinding(ControlCommands.UnSelected, CheckBox_UnChecked));
            CommandBindings.Add(new System.Windows.Input.CommandBinding(ControlCommands.Selected, CheckBox_Checked));
            this.SelectionChanged += DataGrid_SelectionChanged;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsCheck) return;
            DataGrid dataGrid = (DataGrid)sender;
            var items = dataGrid.SelectedItems;
            var count = items.Count;
            var allcount = Items.Count;
            if(count<allcount)
            {
                IsMultiSelected = false;
                IsCheck = false;             
            }

          
        }
        bool IsMultiSelected = true;
        private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            if (!(e.OriginalSource is CheckBox checkBox)) return;
            if(!IsMultiSelected)
            {
                IsMultiSelected = true;
                return;
            }
                
            this.UnselectAll();
           
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!(e.OriginalSource is CheckBox checkBox)) return;

            this.SelectAll();
           
        }


        public bool IsCheck
        {
            get { return (bool)GetValue(IsCheckProperty); }
            set { SetValue(IsCheckProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCheck.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckProperty =
            DependencyProperty.Register("IsCheck", typeof(bool), typeof(DataGrid), new PropertyMetadata(false));




        public bool ShowCheckBox
        {
            get { return (bool)GetValue(ShowCheckBoxProperty); }
            set { SetValue(ShowCheckBoxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowCheckBox.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowCheckBoxProperty =
            DependencyProperty.Register("ShowCheckBox", typeof(bool), typeof(DataGrid), new PropertyMetadata(true));




    }
}
