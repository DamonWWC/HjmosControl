﻿using System;
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

namespace Controltest
{
    /// <summary>
    /// UserControl4.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl4 : UserControl, INotifyPropertyChanged
    {
        public UserControl4()
        {
            InitializeComponent();
            DataList=new ObservableCollection<string> {  "获取突发事件情况", "向公司领导汇报", "向上级汇报", "协调指挥运营企业" };
            DataList1 = GetDataList();
            Isread = true;
            DataString = "111112222";
            DataContext = this;
        }

        private string _DataString;
        public string DataString
        {
            get { return _DataString; }
            set { _DataString = value; OnPropertyChanged(); }
        }

        private bool _Isread;
        public bool Isread
        {
            get { return _Isread; }
            set { _Isread = value; OnPropertyChanged(); }
        }


        private ObservableCollection<string> _DataList;
        public ObservableCollection<string> DataList
        {
            get { return _DataList; }
            set { _DataList = value; OnPropertyChanged(); }
        }



        private ObservableCollection<DemoDataModel> _DataList1;
        public ObservableCollection<DemoDataModel> DataList1
        {
            get { return _DataList1; }
            set { _DataList1 = value; OnPropertyChanged(); }
        }
        private ObservableCollection<DemoDataModel> GetDataList()
        {
            return new ObservableCollection<DemoDataModel>
            {
                new DemoDataModel{ Index = 1,  Name = "Name1", IsSelected = false,  Remark = "111" },
                new DemoDataModel{ Index = 2,  Name = "Name2", IsSelected = true,  Remark = "222" },
                new DemoDataModel{ Index = 3,  Name = "Name3", IsSelected = true,  Remark = "333" },
                new DemoDataModel{ Index = 4,  Name = "Name4", IsSelected = false,  Remark = "444" },
                new DemoDataModel{ Index = 5,  Name = "Name5", IsSelected = false,  Remark = "555" },
                new DemoDataModel{ Index = 6,  Name = "Name6", IsSelected = false,  Remark = "666" },
                new DemoDataModel{ Index = 7,  Name = "Name7", IsSelected = true,  Remark = "777" },
                new DemoDataModel{ Index = 8,  Name = "Name8", IsSelected = false,  Remark = "888" },
                new DemoDataModel{ Index = 9,  Name = "Name9", IsSelected = false,  Remark = "999" },
            };
        }




        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Isread)
                Isread = false;
            else
                Isread = true;
        }

        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DataGrid_Drop(object sender, DragEventArgs e)
        {
          
        }
    }


    public static class DataGridDragDropRowBehavior
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
            var dataGrid = depObject as DataGrid;

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
            DataGrid datagrid = sender as DataGrid;
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
            DataGrid datagrid = sender as DataGrid;
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

        private static DataGridRow GetDataGridRowItem(DataGrid dataGrid, int index)
        {
            if (dataGrid.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
            {
                return null;
            }
            return dataGrid.ItemContainerGenerator.ContainerFromIndex(index)
                                                            as DataGridRow;
        }

        private static int GetDataGridItemCurrentRowIndex(GetDragDropPosition pos, DataGrid dataGrid)
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
