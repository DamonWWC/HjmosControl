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
    public class EditButton : Control
    {
        public EditButton()
        {
            //编辑命令
            CommandBindings.Add(new System.Windows.Input.CommandBinding(ControlCommands.EditCommand, (s, e) =>
             {
                 IsEdit = true;
                 RaiseEvent(new FunctionEventArgs<EditType>(OperationEvent, this)
                 {
                     Info = EditType.Edit
                 }) ;
             }));
            //增加命令
            CommandBindings.Add(new System.Windows.Input.CommandBinding(ControlCommands.AddCommand, (s, e) =>
            {
                RaiseEvent(new FunctionEventArgs<EditType>(OperationEvent, this)
                {
                    Info = EditType.Add
                });
            }));
            //确认命令
            CommandBindings.Add(new System.Windows.Input.CommandBinding(ControlCommands.Confirm, (s, e) =>
            {
                IsEdit = false;
                RaiseEvent(new FunctionEventArgs<EditType>(OperationEvent, this)
                {
                    Info = EditType.Confirm
                });
            }));
        }




        public static readonly RoutedEvent OperationEvent =
      EventManager.RegisterRoutedEvent("Operation", RoutingStrategy.Bubble,
          typeof(EventHandler<FunctionEventArgs<EditType>>), typeof(ContentComboBox));
        /// <summary>
        /// 操作事件
        /// </summary>
        public event EventHandler Operation
        {
            add => AddHandler(OperationEvent, value);
            remove => RemoveHandler(OperationEvent, value);
        }


        internal static readonly DependencyProperty IsEditProperty = DependencyProperty.Register(
          "IsEdit", typeof(bool), typeof(EditButton), new PropertyMetadata(false));

        internal bool IsEdit
        {
            get => (bool)GetValue(IsEditProperty);
            set => SetValue(IsEditProperty, value);
        }

    }


}
