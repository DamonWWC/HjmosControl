using Hjmos.BaseControls.Data;
using Hjmos.BaseControls.Interactivity;
using Hjmos.BaseControls.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Hjmos.BaseControls.Controls
{
    public class ListBoxEditable : System.Windows.Controls.ListBox
    {

        public ListBoxEditable()
        {
            AddHandler(Controls.ListBoxEditableItem.ClosedEvent, new RoutedEventHandler(ItemClosed));
            AddHandler(ListBoxEditableItem.DragEvent, new RoutedEventHandler(IsDrag));
        }
        private bool _isDrop=false;
        private void IsDrag(object sender, RoutedEventArgs e)
        {
            //if (e is FunctionEventArgs<bool> va)
            //{
            //    _isDrop = va.Info;
            //}

            if(e.OriginalSource is ListBoxEditableItem listBoxEditableItem)
            {
                listBoxEditableItem.SetCurrentValue(IsSelectedProperty, true);
            }


        }

        private void ItemClosed(object sender, RoutedEventArgs e)
        {
            var source = e.OriginalSource;
            var sourcepIndex = this.ItemContainerGenerator.IndexFromContainer((ListBoxItem)source);
            var list = GetList(this.ItemsSource);
            list.RemoveAt(sourcepIndex);
        }

        protected static IList GetList(IEnumerable enumerable)
        {
            if (enumerable is ICollectionView)
            {
                return ((ICollectionView)enumerable).SourceCollection as IList;
            }

            return enumerable as IList;
        }

        public override void OnApplyTemplate()
        {          
            base.OnApplyTemplate();
        }
        private AdornerLayer mAdornerLayer;
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (IsEditable  && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var pos = e.GetPosition(this);
                HitTestResult result = VisualTreeHelper.HitTest(this, pos);
                if (result == null) return;

                    
                var button = VisualHelper.GetParent<Button>(result.VisualHit);

                if(button==null||button.Name!= "PART_DragButton")
                {
                    return;
                }
                var listBoxItem = VisualHelper.GetParent<ListBoxItem>(result.VisualHit);
                if (listBoxItem == null || listBoxItem.Content != this.SelectedItem) 
                {
                    return;
                }
                DragDropAdorner adorner = new DragDropAdorner(listBoxItem);
                mAdornerLayer = AdornerLayer.GetAdornerLayer(this);

                mAdornerLayer.Add(adorner);


                DragDrop.DoDragDrop(this, listBoxItem, DragDropEffects.Move);

                if (mAdornerLayer == null)
                {
                    mAdornerLayer = AdornerLayer.GetAdornerLayer(this);
                }
                mAdornerLayer.Remove(adorner);
                mAdornerLayer = null;
            }   
        }

        protected override void OnDrop(DragEventArgs e)
        {
            try
            {
                var pos = e.GetPosition(this);
                var result = VisualTreeHelper.HitTest(this, pos);
                if (result == null)
                {
                    return;
                }
                var sourcePerson = e.Data.GetData(typeof(ListBoxEditableItem));
                if (sourcePerson == null) return;

                var targetPerson = VisualHelper.GetParent<ListBoxEditableItem>(result.VisualHit);
                if (targetPerson == null || ReferenceEquals(targetPerson, sourcePerson))
                {
                    return;
                }               
                var sourcepIndex = this.ItemContainerGenerator.IndexFromContainer((ListBoxEditableItem)sourcePerson);
                if (sourcepIndex == -1) return;
                var targetIndex = this.ItemContainerGenerator.IndexFromContainer(targetPerson);
                var list = GetList(this.ItemsSource);
                var sourceItem = list[sourcepIndex];
                list.RemoveAt(sourcepIndex);

                list.Insert(targetIndex, sourceItem);
                //if(sourcePerson is ListBoxEditableItem source)
                //{
                //    source.SetCurrentValue(IsSelectedProperty, true);
                //}
            }
            catch(Exception ex)
            {

            }
            

        }

        protected override void OnQueryContinueDrag(QueryContinueDragEventArgs e)
        {
            if (mAdornerLayer == null)
            {
                return;
            }

            var mAdornerLayer1 = AdornerLayer.GetAdornerLayer(this);
            mAdornerLayer1.Update();
        }
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => item is ListBoxEditableItem;

        protected override DependencyObject GetContainerForItemOverride() => new ListBoxEditableItem();

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool IsEditable
        {
            get { return (bool)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnableDrop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register("IsEditable", typeof(bool), typeof(ListBoxEditable), new PropertyMetadata(false, IsEditableChanged));



        private static void IsEditableChanged(DependencyObject depObject, DependencyPropertyChangedEventArgs e)
        {
            var listbox = (System.Windows.Controls.ListBox)depObject;
            var value = (bool)e.NewValue;
            listbox.AllowDrop = value;
            
        }

    }
}
