﻿using System;
using System.Windows;
using System.Windows.Input;

namespace Hjmos.BaseControls.Interactivity
{
    public class EventToCommand : TriggerAction<DependencyObject>
    {
        public const string EventArgsConverterParameterPropertyName = "EventArgsConverterParameter";

        public const string AlwaysInvokeCommandPropertyName = "AlwaysInvokeCommand";

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            nameof(CommandParameter), typeof(object), typeof(EventToCommand), new PropertyMetadata(null,
                (s, e) =>
                {
                    var eventToCommand = s as EventToCommand;
                    if (eventToCommand?.AssociatedObject == null)
                        return;
                    eventToCommand.EnableDisableElement();
                }));

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(EventToCommand),
                new PropertyMetadata(null, (s, e) => OnCommandChanged(s as EventToCommand, e)));

        public static readonly DependencyProperty MustToggleIsEnabledProperty = DependencyProperty.Register(
            nameof(MustToggleIsEnabled), typeof(bool), typeof(EventToCommand), new PropertyMetadata(false,
                (s, e) =>
                {
                    var eventToCommand = s as EventToCommand;
                    if (eventToCommand?.AssociatedObject == null)
                        return;
                    eventToCommand.EnableDisableElement();
                }));

        public static readonly DependencyProperty EventArgsConverterParameterProperty =
            DependencyProperty.Register(nameof(EventArgsConverterParameter), typeof(object),
                typeof(EventToCommand), new PropertyMetadata(null));

        public static readonly DependencyProperty AlwaysInvokeCommandProperty =
            DependencyProperty.Register(nameof(AlwaysInvokeCommand), typeof(bool), typeof(EventToCommand),
                new PropertyMetadata(false));

        private object _commandParameterValue;

        private bool? _mustToggleValue;

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public object CommandParameterValue
        {
            get => _commandParameterValue ?? CommandParameter;
            set
            {
                _commandParameterValue = value;
                EnableDisableElement();
            }
        }

        public bool MustToggleIsEnabled
        {
            get => (bool)GetValue(MustToggleIsEnabledProperty);
            set => SetValue(MustToggleIsEnabledProperty, value ? true : false);
        }

        public bool MustToggleIsEnabledValue
        {
            get
            {
                if (_mustToggleValue.HasValue)
                    return _mustToggleValue.Value;
                return MustToggleIsEnabled;
            }
            set
            {
                _mustToggleValue = value;
                EnableDisableElement();
            }
        }

        public bool PassEventArgsToCommand { get; set; }

        public IEventArgsConverter EventArgsConverter { get; set; }

        public object EventArgsConverterParameter
        {
            get => GetValue(EventArgsConverterParameterProperty);
            set => SetValue(EventArgsConverterParameterProperty, value);
        }

        public bool AlwaysInvokeCommand
        {
            get => (bool)GetValue(AlwaysInvokeCommandProperty);
            set => SetValue(AlwaysInvokeCommandProperty, value ? true : false);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            EnableDisableElement();
        }

        private FrameworkElement GetAssociatedObject() => AssociatedObject as FrameworkElement;

        private ICommand GetCommand() => Command;

        public void Invoke() => Invoke(null);

        protected override void Invoke(object parameter)
        {
            if (AssociatedElementIsDisabled() && !AlwaysInvokeCommand)
                return;
            var command = GetCommand();
            var parameter1 = CommandParameterValue;
            if (parameter1 == null && PassEventArgsToCommand)
                parameter1 = EventArgsConverter == null
                    ? parameter
                    : EventArgsConverter.Convert(parameter, EventArgsConverterParameter);
            if (command == null || !command.CanExecute(parameter1))
                return;
            command.Execute(parameter1);
        }

        private static void OnCommandChanged(EventToCommand element, DependencyPropertyChangedEventArgs e)
        {
            if (element == null)
                return;
            if (e.OldValue != null)
                ((ICommand)e.OldValue).CanExecuteChanged -= element.OnCommandCanExecuteChanged;
            var newValue = (ICommand)e.NewValue;
            if (newValue != null)
                newValue.CanExecuteChanged += element.OnCommandCanExecuteChanged;
            element.EnableDisableElement();
        }

        private bool AssociatedElementIsDisabled()
        {
            var associatedObject = GetAssociatedObject();
            if (AssociatedObject == null)
                return true;
            if (associatedObject != null)
                return !associatedObject.IsEnabled;
            return false;
        }

        private void EnableDisableElement()
        {
            var associatedObject = GetAssociatedObject();
            if (associatedObject == null)
                return;
            var command = GetCommand();
            if (!MustToggleIsEnabledValue || command == null)
                return;
            associatedObject.IsEnabled = command.CanExecute(CommandParameterValue);
        }

        private void OnCommandCanExecuteChanged(object sender, EventArgs e) => EnableDisableElement();
    }
}
