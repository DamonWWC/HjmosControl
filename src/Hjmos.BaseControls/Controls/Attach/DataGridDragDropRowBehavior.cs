using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Hjmos.BaseControls.Controls
{
    public class DataGridDragDropRowBehavior
    {
        public delegate Point GetDragDropPosition(IInputElement theElement);

        public static DependencyProperty DropFinishProperty =
       DependencyProperty.RegisterAttached("DropFinish", typeof(ICommand), typeof(DataGridDragDropRowBehavior),
                                           new UIPropertyMetadata(null));

        public static void SetDropFinish(UIElement target, ICommand value)
        {
            target.SetValue(DropFinishProperty, value);
        }

        public static bool GetEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnabledProperty);
        }

        public static void SetEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(EnabledProperty, value);
        }

        public static readonly DependencyProperty EnabledProperty =
            DependencyProperty.RegisterAttached("Enabled", typeof(bool), typeof(DataGridDragDropRowBehavior), new PropertyMetadata(false, OnEnableChanged));

        private static void OnEnableChanged(DependencyObject depObject, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = (System.Windows.Controls.DataGrid)depObject ;

            var enable = (bool)e.NewValue;
            if (enable)
            {
                dataGrid.AllowDrop = true;
                dataGrid.PreviewMouseLeftButtonDown += DataGrid_PreviewMouseLeftButtonDown;
                dataGrid.Drop += DataGrid_Drop;
            }
            else
            {
                dataGrid.PreviewMouseLeftButtonDown -= DataGrid_PreviewMouseLeftButtonDown;
                dataGrid.Drop -= DataGrid_Drop;
            }
        }

        private static void DataGrid_Drop(object sender, DragEventArgs e)
        {
            DragItem dragitem = e.Data.GetData("DragItem") as DragItem;

            if (dragitem.RowIndex < 0)
            {
                return;
            }
            System.Windows.Controls.DataGrid datagrid = (System.Windows.Controls.DataGrid)sender ;
            int index = GetDataGridItemCurrentRowIndex(e.GetPosition, datagrid);

            //The current Rowindex is -1 (No selected)
            if (index < 0)
            {
                return;
            }
            //If Drag-Drop Location are same
            if (index == dragitem.RowIndex)
            {
                return;
            }

            var targetItem = datagrid.Items[index];

            DropEventArgs arg = new DropEventArgs();
            arg.SourceRowIndex = dragitem.RowIndex;
            arg.TargetRowIndex = index;
            arg.Data = dragitem.Data;
            arg.TargetData = targetItem;

            var command = (ICommand)datagrid.GetValue(DropFinishProperty);
            if (command != null)
            {
                command.Execute(arg);
            }
        }

        private static void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.DataGrid datagrid =(System.Windows.Controls.DataGrid) sender ;
            var m_prevRowIndex = GetDataGridItemCurrentRowIndex(e.GetPosition, datagrid);

            if (m_prevRowIndex < 0)
            {
                return;
            }
            datagrid.SelectedIndex = m_prevRowIndex;

            var selectedItem = datagrid.Items[m_prevRowIndex];

            if (selectedItem == null)
            {
                return;
            }

            DragItem dragItem = new DragItem();
            dragItem.RowIndex = m_prevRowIndex;
            dragItem.Data = selectedItem;
            DataObject data = new DataObject("DragItem", dragItem);
            //Now Create a Drag Rectangle with Mouse Drag-Effect
            //Here you can select the Effect as per your choice

            DragDropEffects dragdropeffects = DragDropEffects.Move;

            if (DragDrop.DoDragDrop(datagrid, data, dragdropeffects) != DragDropEffects.None)
            {
                //Now This Item will be dropped at new location and so the new Selected Item
                datagrid.SelectedItem = selectedItem;
            }
        }

        /// <summary>
        /// Method checks whether the mouse is on the required Target
        /// Input Parameter (1) "Visual" -> Used to provide Rendering support to WPF
        /// Input Paraneter (2) "User Defined Delegate" positioning for Operation
        /// </summary>
        /// <param name="theTarget"></param>
        /// <param name="pos"></param>
        /// <returns>The "Rect" Information for specific Position</returns>
        private static bool IsTheMouseOnTargetRow(Visual theTarget, GetDragDropPosition pos)
        {
            Rect posBounds = VisualTreeHelper.GetDescendantBounds(theTarget);
            Point theMousePos = pos((IInputElement)theTarget);
            return posBounds.Contains(theMousePos);
        }

        private static DataGridRow GetDataGridRowItem(System.Windows.Controls.DataGrid dataGrid, int index)
        {
            if (dataGrid.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
            {
                return null;
            }
            return dataGrid.ItemContainerGenerator.ContainerFromIndex(index)
                                                            as DataGridRow;
        }

        private static int GetDataGridItemCurrentRowIndex(GetDragDropPosition pos, System.Windows.Controls.DataGrid dataGrid)
        {
            int curIndex = -1;
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                DataGridRow itm = GetDataGridRowItem(dataGrid, i);
                if (IsTheMouseOnTargetRow(itm, pos))
                {
                    curIndex = i;
                    break;
                }
            }
            return curIndex;
        }
    }

    public class DragItem
    {
        public int RowIndex { get; set; }
        public object Data { get; set; }
    }

    public class DropEventArgs
    {
        public int SourceRowIndex { get; set; }
        public object Data { get; set; }

        public int TargetRowIndex { get; set; }

        public object TargetData { get; set; }
    }
}
