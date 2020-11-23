using Hjmos.BaseControls.Data;
using Hjmos.BaseControls.Interactivity;
using System;
using System.Windows;
using System.Windows.Input;

namespace Hjmos.BaseControls.Controls
{
    public class SearchBar : TextBox, ICommandSource
    {

        public SearchBar()
        {
            CommandBindings.Add(new CommandBinding(ControlCommands.Search, (s, e) => OnSearchStarted()));
        }


        public static readonly RoutedEvent SearchStartedEvent =
            EventManager.RegisterRoutedEvent("SearchStarted", RoutingStrategy.Bubble,
                typeof(EventHandler<FunctionEventArgs<string>>), typeof(SearchBar));

        public event EventHandler<FunctionEventArgs<string>> SearchStarted
        {
            add => AddHandler(SearchStartedEvent, value);
            remove => RemoveHandler(SearchStartedEvent, value);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if(e.Key==Key.Enter)
            {
                OnSearchStarted();
            }
        }

        protected override void OnTextChanged(System.Windows.Controls.TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            if(IsRealTime)
            {
                OnSearchStarted();
            }
            VerifyData();
        }

        private void OnSearchStarted()
        {
            RaiseEvent(new FunctionEventArgs<string>(SearchStartedEvent, this)
            {
                Info = Text
            }) ;

            switch (Command)
            {
                case null:
                    return;
                case RoutedCommand command:
                    command.Execute(CommandParameter, CommandTarget);
                    break;
                default:
                    Command.Execute(CommandParameter);
                    break;
            }
        }



        /// <summary>
        /// 是否实时搜索
        /// </summary>
        public bool IsRealTime
        {
            get { return (bool)GetValue(IsRealTimeProperty); }
            set { SetValue(IsRealTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsRealTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRealTimeProperty =
            DependencyProperty.Register("IsRealTime", typeof(bool), typeof(SearchBar), new PropertyMetadata(false));

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof(ICommand), typeof(SearchBar), new PropertyMetadata(default(ICommand), OnCommandChanged));

        private static void OnCommandChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            var ct1 = (SearchBar)d;
            if(e.OldValue is ICommand oldCommand)
            {
                oldCommand.CanExecuteChanged -= ct1.CanExecuteChanged;
            }
            if(e.NewValue is ICommand newCommand)
            {
                newCommand.CanExecuteChanged += ct1.CanExecuteChanged;
            }

        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }


        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter", typeof(object), typeof(SearchBar), new PropertyMetadata(default(object)));


        public object CommandParameter 
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }


        public static readonly DependencyProperty CommandTargetProperty = DependencyProperty.Register(
            "CommandTarget", typeof(IInputElement), typeof(SearchBar), new PropertyMetadata(default(IInputElement)));
        public IInputElement CommandTarget
        {
            get => (IInputElement)GetValue(CommandTargetProperty);
            set => SetValue(CommandTargetProperty, value);
        }
            


        private void CanExecuteChanged(object sender, EventArgs e)
        {
            if (Command == null) return;
            IsEnabled = Command is RoutedCommand command
                ? command.CanExecute(CommandParameter, CommandTarget)
                : Command.CanExecute(CommandParameter);
        }
    }
}
