using Hjmos.BaseControls.Interactivity;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StructureMap;
using Prism.Commands;

namespace Controltest
{
    /// <summary>
    /// Window7.xaml 的交互逻辑
    /// </summary>
    public partial class Window7 : Window
    {
        private ItemsControlDragHelper _dragHelper;
        public Window7()
        {
            InitializeComponent();
            //ListSource = new List<string> { "获取突发事件情况", "向公司领导汇报", "向上级汇报", "协调指挥运营企业" };
            //_dragHelper = new ItemsControlDragHelper(LBoxSort, this);


            Window1 window1 = new Window1();
            //window1.Owner = mTopLevelGrid;
            window1.Show();
            dynamic stuff = JsonConvert.DeserializeObject("{ 'Name': 'Jon Smith', 'Address': { 'City': 'New York', 'State': 'NY' }, 'Age': 42 }");

            string name = stuff.Name;
            string address = stuff.Address.City;

            Items = new List<string> { "1", "2", "3" };

            
            Test();

            DataContext = this;
        }

        public List<string> Items { get; set; }
        private DelegateCommand<object> _SelectionChangedCommand;
        /// <summary>
        /// 条件选择命令
        /// </summary>
        public DelegateCommand<object> SelectionChangedCommand =>
            _SelectionChangedCommand ?? (_SelectionChangedCommand = new DelegateCommand<object>(SelectionChanged));
        private void SelectionChanged(object parameter)
        {

        }

        public async void Test()
        {
            var builder = new StringBuilder();
            var writer = new StringWriter(builder);

            var container = new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    scanner.AssemblyContainingType(typeof(Window7));
                    scanner.IncludeNamespaceContainingType<Ping>();
                    scanner.WithDefaultConventions();
                    scanner.AddAllTypesOf(typeof(INotificationHandler<>));
                });
                cfg.For<TextWriter>().Use(writer);
                cfg.For<IMediator>().Use<Mediator>();
                cfg.For<ServiceFactory>().Use<ServiceFactory>(ctx => t => ctx.GetInstance(t));
            });
        
            var mediator = container.GetInstance<IMediator>();
            Mediator mediatR = mediator as Mediator;

           


            await mediatR.Publish(new Ping { Message = "Ping" });

            var result = builder.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            //result.ShouldContain("Ping Pong");
            //result.ShouldContain("Ping Pung");
        }

        //public List<string> ListSource { get; set; }



        //private AdornerLayer mAdornerLayer;
        //private void LBoxSort_PreviewMouseMove(object sender, MouseEventArgs e)
        //{
        //    if (Mouse.LeftButton == MouseButtonState.Pressed)
        //    {
        //        var pos = e.GetPosition(LBoxSort);
        //        HitTestResult result = VisualTreeHelper.HitTest(LBoxSort, pos);
        //        if (result == null)
        //        {
        //            return;
        //        }
        //        var listBoxItem = Utils.FindVisualParent<ListBoxItem>(result.VisualHit);
        //        if (listBoxItem == null || listBoxItem.Content != LBoxSort.SelectedItem)
        //        {
        //            return;
        //        }

        //        DragDropAdorner adorner = new DragDropAdorner(listBoxItem);
        //        //adorner.Child = new TextBlock
        //        //{
        //        //    Text = "1111111",
        //        //    Foreground = Brushes.Red,
        //        //    FontSize = 20
        //        //};
        //        mAdornerLayer = AdornerLayer.GetAdornerLayer(mTopLevelGrid);
        //        mAdornerLayer.Add(adorner);


        //        DataObject dataObj = new DataObject(typeof(ListBoxItem), listBoxItem);
        //        // DataObject dataObj = new DataObject(listBoxItem.Content as TextBlock);
        //        DragDrop.DoDragDrop(LBoxSort, dataObj, DragDropEffects.Move);
        //        mAdornerLayer.Remove(adorner);
        //        mAdornerLayer = null;


        //    }


        //}


        //private void LBoxSort_Drop(object sender, DragEventArgs e)
        //{

        //    //string droppedData = e.Data.GetData(typeof(string)) as string;
        //    //string target = ((ListBoxItem)(sender)).DataContext as string;

        //    //int removedIdx = LBoxSort.Items.IndexOf(droppedData);
        //    //int targetIdx = LBoxSort.Items.IndexOf(target);

        //    //if (removedIdx < targetIdx)
        //    //{
        //    //    _empList.Insert(targetIdx + 1, droppedData);
        //    //    _empList.RemoveAt(removedIdx);
        //    //}
        //    //else
        //    //{
        //    //    int remIdx = removedIdx + 1;
        //    //    if (_empList.Count + 1 > remIdx)
        //    //    {
        //    //        _empList.Insert(targetIdx, droppedData);
        //    //        _empList.RemoveAt(remIdx);
        //    //    }
        //    //}







        //    var pos = e.GetPosition(LBoxSort);
        //    var result = VisualTreeHelper.HitTest(LBoxSort, pos);
        //    if (result == null)
        //    {
        //        return;
        //    }
        //    //查找元数据
        //    var sourcePerson = e.Data.GetData(typeof(TextBlock));
        //    if (sourcePerson == null)
        //    {
        //        return;
        //    }
        //    //查找目标数据
        //    var listBoxItem = Utils.FindVisualParent<ListBoxItem>(result.VisualHit);
        //    if (listBoxItem == null)
        //    {
        //        return;
        //    }
        //    var targetPerson = listBoxItem.Content as TextBlock;
        //    if (ReferenceEquals(targetPerson, sourcePerson))
        //    {
        //        return;
        //    }
        //    LBoxSort.Items.Remove(sourcePerson);
        //    LBoxSort.Items.Insert(LBoxSort.Items.IndexOf(targetPerson), sourcePerson);
        //}

        //private void LBoxSort_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (sender is ListBoxItem)
        //    {
        //        ListBoxItem draggedItem = sender as ListBoxItem;
        //        DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
        //        draggedItem.IsSelected = true;
        //    }
        //}

        //private void LBoxSort_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        //{
        //    mAdornerLayer.Update();
        //}
    }
    internal static class Utils
    {
        //根据子元素查找父元素
        public static T FindVisualParent<T>(DependencyObject obj) where T : class
        {
            while (obj != null)
            {
                if (obj is T)
                    return obj as T;

                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }
    }


    public class Ping : INotification
    {
        public string Message { get; set; }
    }

    public class PongHandler : INotificationHandler<Ping>
    {
        private readonly TextWriter _writer;

        public PongHandler(TextWriter writer)
        {
            _writer = writer;
        }

        public Task Handle(Ping notification, CancellationToken cancellationToken)
        {
            return _writer.WriteLineAsync(notification.Message + " Pong");
        }
    }

    public class PungHandler : INotificationHandler<Ping>
    {
        private readonly TextWriter _writer;

        public PungHandler(TextWriter writer)
        {
            _writer = writer;
        }

        public Task Handle(Ping notification, CancellationToken cancellationToken)
        {
            return _writer.WriteLineAsync(notification.Message + " Pung");
        }
    }
}

