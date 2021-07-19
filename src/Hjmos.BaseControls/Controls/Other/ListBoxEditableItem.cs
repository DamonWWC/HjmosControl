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
    public class ListBoxEditableItem : System.Windows.Controls.ListBoxItem
    {
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

    }
}
