using Hjmos.BaseControls.Interactivity;
using Hjmos.BaseControls.Tools;
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
            //DataList = new ObservableCollection<string>();
            DataList1 = GetDataList();
            Isread = true;
            DataString = "111112222";
            IsDrop = false;
           
         //   this.Carousels.ItemsSource = DataList;
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

        private bool _IsDrop=false;
        public bool IsDrop
        {
            get { return _IsDrop; }
            set { _IsDrop = value;OnPropertyChanged(); }
        }


        private ObservableCollection<string> _DataList;
        public ObservableCollection<string> DataList
        {
            get { return _DataList; }
            set { _DataList = value; OnPropertyChanged(); }
        }

        private ICommand _DropFinishCommand;
        public ICommand DropFinishCommand
        {
            get { return new RelayCommand((m) => Drop1(m)); }
           

        }


        public ICommand DropButton
        {
            get { return new RelayCommand((p) => DropButton1()); }
        }

        public void DropButton1()
        {
            IsDrop = true;
        }


        private void Drop1(object obj)
        {

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

            //DataList = new ObservableCollection<string> { "获取突发事件情况", "向公司领导汇报" };
            //DataList.Add("wwwww");
            DataList.RemoveAt(0);
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
        private AdornerLayer mAdornerLayer;
        private void DisposalPoint_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed) return;

            if (sender is ListBox listBox)
            {
                var pos = e.GetPosition(listBox);
                HitTestResult result = VisualTreeHelper.HitTest(listBox, pos);
                if (result == null) return;
                var listBoxItem = VisualHelper.GetParent<ListBoxItem>(result.VisualHit);
                if (listBoxItem == null || listBoxItem.Content != listBox.SelectedItem)
                {
                    return;
                }
                DragDropAdorner adorner = new DragDropAdorner(listBoxItem);
                mAdornerLayer = AdornerLayer.GetAdornerLayer(listBox);
                mAdornerLayer.Add(adorner);

                DragDrop.DoDragDrop(listBox, listBoxItem, DragDropEffects.Move);

                mAdornerLayer.Remove(adorner);
                mAdornerLayer = null;

            }
        }

        private void DisposalPoint_Drop(object sender, DragEventArgs e)
        {
             if(sender is ListBox listbox)
            {
                var pos = e.GetPosition(listbox);
                var result = VisualTreeHelper.HitTest(listbox, pos);
                if(result==null)
                {
                    return;
                }
                var sourcePerson = e.Data.GetData(typeof(ListBoxItem));
                if (sourcePerson == null) return;

                var targetPerson = VisualHelper.GetParent<ListBoxItem>(result.VisualHit);
                if(ReferenceEquals(targetPerson,sourcePerson))
                {
                    return;
                }
                //DataList1
                
              
                
              
                var sourcepIndex = listbox.ItemContainerGenerator.IndexFromContainer((ListBoxItem)sourcePerson);
                var targetIndex = listbox.ItemContainerGenerator.IndexFromContainer(targetPerson);
                
                var temp = DataList1[sourcepIndex];
                DataList1[sourcepIndex] = DataList1[targetIndex];
                DataList1[targetIndex] = temp;

            }
        }

        private void DisposalPoint_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            mAdornerLayer.Update();
        }

        private void DisposalPoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataList.Add("获取突发事件情");
        }
    }

    public class RelayCommand : ICommand
    {
        private Action<object> _Execute;
        private Predicate<object> _CanExecute;

        public RelayCommand(Action<object> execte)
            : this(execte, null)
        {
        }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("Execute");
            _Execute = execute;
            _CanExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return _CanExecute == null ? true : _CanExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _Execute(parameter);
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
