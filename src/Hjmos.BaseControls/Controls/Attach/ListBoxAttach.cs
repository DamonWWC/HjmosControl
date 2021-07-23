using Hjmos.BaseControls.Interactivity;
using Hjmos.BaseControls.Tools;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Hjmos.BaseControls.Controls
{
    public class ListBoxAttach : BorderElement
    {
        public static CornerRadius GetItemCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(ItemCornerRadiusProperty);
        }

        public static void SetItemCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(ItemCornerRadiusProperty, value);
        }
        /// <summary>
        /// 子项的边角
        /// </summary>
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCornerRadiusProperty =
            DependencyProperty.RegisterAttached("ItemCornerRadius", typeof(CornerRadius), typeof(ListBoxAttach), new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.Inherits));


        public static double GetListBoxItemWith(DependencyObject obj)
        {
            return (double)obj.GetValue(ListBoxItemWithProperty);
        }

        public static void SetListBoxItemWith(DependencyObject obj, double value)
        {
            obj.SetValue(ListBoxItemWithProperty, value);
        }

        // Using a DependencyProperty as the backing store for ListBoxItemWith.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListBoxItemWithProperty =
            DependencyProperty.RegisterAttached("ListBoxItemWith", typeof(double), typeof(ListBoxAttach), new PropertyMetadata(50d));

        public static readonly DependencyProperty IsOddEvenRowProperty = DependencyProperty.RegisterAttached(
         "IsOddEvenRow", typeof(bool), typeof(ListBoxAttach), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetIsOddEvenRow(DependencyObject element, bool value)
            => element.SetValue(IsOddEvenRowProperty, value);

        public static bool GetIsOddEvenRow(DependencyObject element)
            => (bool)element.GetValue(IsOddEvenRowProperty);






        public static DependencyProperty DropFinishProperty =
      DependencyProperty.RegisterAttached("DropFinish", typeof(ICommand), typeof(ListBoxAttach),
                                          new UIPropertyMetadata(null));

        public static void SetDropFinish(UIElement target, ICommand value)
        {
            target.SetValue(DropFinishProperty, value);
        }

        public static ICommand GetDropFinish(UIElement target)
        {
            return (ICommand)target.GetValue(DropFinishProperty);
        }

        public static bool GetEnabledDrop(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnabledDropProperty);
        }

        public static void SetEnabledDrop(DependencyObject obj, bool value)
        {
            obj.SetValue(EnabledDropProperty, value);
        }
        /// <summary>
        /// 是否能拖拽
        /// </summary>
        public static readonly DependencyProperty EnabledDropProperty =
            DependencyProperty.RegisterAttached("EnabledDrop", typeof(bool), typeof(ListBoxAttach), new PropertyMetadata(false, OnEnabledDropChanged));


        private static void OnEnabledDropChanged(DependencyObject depObject, DependencyPropertyChangedEventArgs e)
        {
            var listbox = (System.Windows.Controls.ListBox)depObject;

            var value = (bool)e.NewValue;
            if(value)
            {
                listbox.AllowDrop = true;
                listbox.PreviewMouseMove += Listbox_PreviewMouseMove;
                listbox.Drop += Listbox_Drop;
                listbox.QueryContinueDrag += Listbox_QueryContinueDrag;
            }
            else
            {
                listbox.AllowDrop = false;
                listbox.PreviewMouseMove -= Listbox_PreviewMouseMove;
                listbox.Drop -= Listbox_Drop;
                listbox.QueryContinueDrag -= Listbox_QueryContinueDrag;
            }
        }

        private static void Listbox_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if (mAdornerLayer == null)
            {
                return;
            }
            if (sender is System.Windows.Controls.ListBox listbox)
            {
                var mAdornerLayer = AdornerLayer.GetAdornerLayer(listbox);
                mAdornerLayer.Update();
            }

        }

        private static void Listbox_Drop(object sender, DragEventArgs e)
        {
            if (sender is System.Windows.Controls.ListBox listbox)
            {
                var pos = e.GetPosition(listbox);
                var result = VisualTreeHelper.HitTest(listbox, pos);
                if (result == null)
                {
                    return;
                }
                var sourcePerson = e.Data.GetData(typeof(ListBoxEditableItem));
                if (sourcePerson == null) return;

                var targetPerson = VisualHelper.GetParent<ListBoxItem>(result.VisualHit);
                if (targetPerson ==null|| ReferenceEquals(targetPerson, sourcePerson))
                {
                    return;
                }

                var list = GetList(listbox.ItemsSource);
                var sourcepIndex = listbox.ItemContainerGenerator.IndexFromContainer((ListBoxItem)sourcePerson);
                var targetIndex = listbox.ItemContainerGenerator.IndexFromContainer(targetPerson);
                var w = list[sourcepIndex];
                list.RemoveAt(sourcepIndex);
               
                list.Insert(targetIndex, w);

                
                //DropEventArgs arg = new DropEventArgs();
                //arg.SourceRowIndex = sourcepIndex;
                //arg.TargetRowIndex = targetIndex;
                //arg.Data = ((ListBoxItem)sourcePerson).Content;
                //arg.TargetData = targetPerson.Content;
                //var command = (ICommand)listbox.GetValue(DropFinishProperty);
                //if(command!=null)
                //{
                //    command.Execute(arg);
                //}
            }
        }


        protected static IList GetList(IEnumerable enumerable)
        {
            if (enumerable is ICollectionView)
            {
                return ((ICollectionView)enumerable).SourceCollection as IList;
            }

            return enumerable as IList;
        }





        private static int num = 1;
        private static AdornerLayer mAdornerLayer;
        private static void Listbox_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed) return;

            if (sender is System.Windows.Controls.ListBox listBox)
            {
                var pos = e.GetPosition(listBox);
                HitTestResult result = VisualTreeHelper.HitTest(listBox, pos);
                if (result == null) return;
                var listBoxItem = VisualHelper.GetParent<ListBoxItem>(result.VisualHit);
                if (listBoxItem == null || listBoxItem.Content != listBox.SelectedItem)
                {
                    return;
                }
                //if (num == 2) MessageBox.Show("111");
                DragDropAdorner adorner = new DragDropAdorner(listBoxItem);
                mAdornerLayer = AdornerLayer.GetAdornerLayer(listBox);

                mAdornerLayer.Add(adorner);

                num++;
                DragDrop.DoDragDrop(listBox, listBoxItem, DragDropEffects.Move);
                MessageBox.Show(num.ToString());
                if (mAdornerLayer == null)
                {
                    mAdornerLayer = AdornerLayer.GetAdornerLayer(listBox);
                }
                mAdornerLayer.Remove(adorner);
                mAdornerLayer = null;
            }
        }
    }


  
}
