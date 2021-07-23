using Hjmos.BaseControls.Data;
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

    [TemplatePart(Name =ElementDragButton,Type =typeof(Button))]
    public class ListBoxEditableItem : System.Windows.Controls.ListBoxItem
    {

        private const string ElementDragButton = "PART_DragButton";
        private Button _dragButton;
        public ListBoxEditableItem()
        {
            CommandBindings.Add(new System.Windows.Input.CommandBinding(ControlCommands.Close, (s, e) =>
             {
                 RaiseEvent(new RoutedEventArgs(ClosedEvent, this));
             }));
        }



        public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(EventHandler), typeof(ListBoxEditableItem));

        public event EventHandler Closed
        {
            add => AddHandler(ClosedEvent, value);
            remove => RemoveHandler(ClosedEvent, value);
        }

        public static readonly RoutedEvent DragEvent = EventManager.RegisterRoutedEvent("Drag", RoutingStrategy.Bubble, typeof(EventHandler), typeof(ListBoxEditableItem));

        public event EventHandler Drag
        {
            add => AddHandler(DragEvent, value);
            remove => RemoveHandler(DragEvent, value);
        }


        public override void OnApplyTemplate()
        {
            if(_dragButton!=null)
            {
                _dragButton.PreviewMouseLeftButtonDown -= DragButton_PreviewMouseLeftButtonDown;
                _dragButton.PreviewMouseLeftButtonUp -= DragButton_PreviewMouseLeftButtonUp;
            }
            base.OnApplyTemplate();
            _dragButton = GetTemplateChild(ElementDragButton) as Button;
            if(_dragButton!=null)
            {
                _dragButton.PreviewMouseLeftButtonDown += DragButton_PreviewMouseLeftButtonDown;
                _dragButton.PreviewMouseLeftButtonUp += DragButton_PreviewMouseLeftButtonUp;
            }
            
        }

        private void DragButton_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //RaiseEvent(new FunctionEventArgs<bool>(DragEvent,this)
            //{
            //    Info = false
            //});
        }

        private void DragButton_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(DragEvent, this));
            ////RaiseEvent(new FunctionEventArgs<bool>(DragEvent, this)
            ////{
            ////    Info = true
            ////});
        }
    }
}
