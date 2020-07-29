using HjmosControl.Data;
using HjmosControl.Tools;
using HjmosControl.Interactivity;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HjmosControl.Controls.Attach;

namespace HjmosControl.Controls
{

    [TemplatePart(Name = ElementTextBox,Type =typeof(TextBox))]
    [TemplatePart(Name =ElementErrorTip,Type =typeof(UIElement))]
    public class NumericUpDown : Control, IDataInput
    {
        #region Constants
        private const string ElementTextBox = "PART_TextBox";
        private const string ElementErrorTip = "PART_ErrorTip";
        #endregion

        #region Data
        private TextBox _textBox;
        private UIElement _errorTip;
        private bool _updateText;
        #endregion

        public NumericUpDown()
        {
            CommandBindings.Add(new CommandBinding(ControlCommands.Prev, (s, e) =>
             {
                 if (IsReadOnly) return;
                 Value += Increment;
                 
             }));

            CommandBindings.Add(new CommandBinding(ControlCommands.Next, (s, e) =>
             {
                 if (IsReadOnly) return;
                 Value -= Increment;
             }));

            CommandBindings.Add(new CommandBinding(ControlCommands.Clear, (s, e) =>
             {
                 if (IsReadOnly) return;
                 SetCurrentValue(ValueProperty, 0d);
             }
            ));

            Loaded += (s, e) => OnApplyTemplate();
        }
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            if(_textBox!=null)
            {
                _textBox?.Focus();
                _textBox.Select(_textBox.Text.Length, 0);
            }
        }
        public override void OnApplyTemplate()
        {
            if(_textBox!=null)
            {
                TextCompositionManager.RemovePreviewTextInputHandler(_textBox, PreviewTextInputHandler);
                _textBox.TextChanged -= TextBox_TextChanged;
                _textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
                _textBox.LostFocus -= TextBox_LostFocus;
            }

            base.OnApplyTemplate();
            _textBox = GetTemplateChild(ElementTextBox) as TextBox;
            _errorTip = GetTemplateChild(ElementErrorTip) as UIElement;

            if(_textBox!=null)
            {
                TextCompositionManager.AddPreviewTextInputHandler(_textBox, PreviewTextInputHandler);
                _textBox.TextChanged += TextBox_TextChanged; ;
                _textBox.PreviewKeyDown += TextBox_PreviewKeyDown; ;
                _textBox.LostFocus += TextBox_LostFocus;
                _textBox.Text = CurrentText;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsError && _errorTip != null) return;
            _textBox.Text = CurrentText;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsReadOnly) return;
            if(e.Key==Key.Up)
            {
                Value += Increment;
            }
            else if(e.Key==Key.Down)
            {
                Value -= Increment;
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => UpdateData();
        
        private void UpdateData()
        {
            if (!VerifyData()) return;
            if(double.TryParse(_textBox.Text,out var value))
            {
                _updateText = false;
                Value = value;
                _updateText = true;
            }
        }
        private void PreviewTextInputHandler(object sender, TextCompositionEventArgs e) => UpdateData();

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            if(_textBox.IsFocused&&!IsReadOnly)
            {
                Value += e.Delta > 0 ? Increment : -Increment;
                e.Handled = true;
            }
        }
        
        private string CurrentText => DecimalPlaces.HasValue
            ? Value.ToString($"#0.{new string('0', DecimalPlaces.Value)}")
            : Value.ToString("#0");

        protected virtual void OnValueChanged(FunctionEventArgs<double> e) => RaiseEvent(e);


        /// <summary>
        /// 值改变事件
        /// </summary>
        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble,
                typeof(EventHandler<FunctionEventArgs<double>>), typeof(NumericUpDown));

        public event EventHandler<FunctionEventArgs<double>> ValueChanged
        {
            add => AddHandler(ValueChangedEvent, value);
            remove => RemoveHandler(ValueChangedEvent, value);
        }
        
      
        /// <summary>
        ///     当前值
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(NumericUpDown),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnValueChanged, CoerceValue), ValidateHelper.IsInRangeOfDouble);
        private static void OnValueChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            var ctl = (NumericUpDown)d;
            var v = (double)e.NewValue;
            if(ctl._updateText&&ctl._textBox!=null)
            {
                ctl._textBox.Text = ctl.CurrentText;
                ctl._textBox.Select(ctl._textBox.Text.Length, 0);
            }
            ctl.OnValueChanged(new FunctionEventArgs<double>(ValueChangedEvent, ctl)
            {
                Info = v
            }) ;
        }
        private static object CoerceValue(DependencyObject d, object basevalue)
        {
            var ctl = (NumericUpDown)d;
            var minimum = ctl.Minimum;
            var num = (double)basevalue;
            if(num<minimum)
            {
                ctl.Value = minimum;
                return minimum;
            }
            var maximum = ctl.Maximum;
            if(num>maximum)
            {
                ctl.Value = maximum;
            }
            return num > maximum ? maximum : num;
        }
        /// <summary>
        ///     当前值
        /// </summary>
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }



        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maximum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(NumericUpDown), new PropertyMetadata(double.MaxValue,OnMaximumChanged,CoerceMaximum),ValidateHelper.IsInRangeOfDouble);

        private static void OnMaximumChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            var ctl = (NumericUpDown)d;
            ctl.CoerceValue(MinimumProperty);
            ctl.CoerceValue(ValueProperty);

        }
        private static object CoerceMaximum(DependencyObject d,object basevalue)
        {
            var minmum = ((NumericUpDown)d).Minimum;
            return (double)basevalue < minmum ? minmum : basevalue;
        }



        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Minimum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(NumericUpDown), new PropertyMetadata(0d,OnMinimunChanged,CoerceMinimum),ValidateHelper.IsInRangeOfDouble);


        private static void OnMinimunChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            var ctl = (NumericUpDown)d;
            ctl.CoerceValue(MaximumProperty);
            ctl.CoerceValue(ValueProperty);
        }

        private static object CoerceMinimum(DependencyObject d,object basevalue)
        {
            var maximum = ((NumericUpDown)d).Maximum;
            return (double)basevalue > maximum ? maximum : basevalue;
        }

        /// <summary>
        ///     指示每单击一下按钮时增加或减少的数量
        /// </summary>
        public static readonly DependencyProperty IncrementProperty = DependencyProperty.Register(
            "Increment", typeof(double), typeof(NumericUpDown), new PropertyMetadata(1d));

        /// <summary>
        ///     指示每单击一下按钮时增加或减少的数量
        /// </summary>
        public double Increment
        {
            get => (double)GetValue(IncrementProperty);
            set => SetValue(IncrementProperty, value);
        }

        /// <summary>
        ///     标识 IsReadOnly 依赖属性。
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
            "IsReadOnly", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(false));

        /// <summary>
        ///     获取或设置一个值，该值指示NumericUpDown是否只读。
        /// </summary>
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        /// <summary>
        ///     指示要显示的小数位数
        /// </summary>
        public static readonly DependencyProperty DecimalPlacesProperty = DependencyProperty.Register(
            "DecimalPlaces", typeof(int?), typeof(NumericUpDown), new PropertyMetadata(default(int?)));

        /// <summary>
        ///     指示要显示的小数位数
        /// </summary>
        public int? DecimalPlaces
        {
            get => (int?)GetValue(DecimalPlacesProperty);
            set => SetValue(DecimalPlacesProperty, value);
        }


        /// <summary>
        /// 是否显示上下调节按钮
        /// </summary>
        public bool ShowUpDownButton
        {
            get { return (bool)GetValue(ShowUpDownButtonProperty); }
            set { SetValue(ShowUpDownButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowUpDownButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowUpDownButtonProperty =
            DependencyProperty.Register("ShowUpDownButton", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(true));



        /// <summary>
        /// 数据是否错误
        /// </summary>
        public bool IsError
        {
            get { return (bool)GetValue(IsErrrorProperty); }
            set { SetValue(IsErrrorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsErrror.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsErrrorProperty =
            DependencyProperty.Register("IsErrror", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(false));



        /// <summary>
        /// 错误提示
        /// </summary>
        public string ErrorStr
        {
            get { return (string)GetValue(ErrorStrProperty); }
            set { SetValue(ErrorStrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ErrorStr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorStrProperty =
            DependencyProperty.Register("ErrorStr", typeof(string), typeof(NumericUpDown), new PropertyMetadata(default(string)));


       
        // Using a DependencyProperty as the backing store for TextType.  This enables animation, styling, binding, etc...
        public static readonly DependencyPropertyKey TextTypePropertyKey =
            DependencyProperty.RegisterReadOnly("TextType", typeof(TextType), typeof(NumericUpDown), new PropertyMetadata(default(TextType)));

        public static readonly DependencyProperty TextTypeProperty = TextTypePropertyKey.DependencyProperty;



        /// <summary>
        /// 文本类型
        /// </summary>
        public TextType TextType
        {
            get { return (TextType)GetValue(TextTypeProperty); }
            set { SetValue(TextTypeProperty, value); }
        }

        /// <summary>
        /// 是否显示清楚按钮
        /// </summary>
        public bool ShowClearButton
        {
            get { return (bool)GetValue(ShowClearButtonProperty); }
            set { SetValue(ShowClearButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowClearButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowClearButtonProperty =
            DependencyProperty.Register("ShowClearButton", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(false));


        public Func<string, OperationResult<bool>> VerifyFunc { get; set; }
       

        public bool VerifyData()
        {
            OperationResult<bool> result;
            if(VerifyFunc!=null)
            {
                result = VerifyFunc.Invoke(_textBox.Text);
            }
            else
            {
                if(!string.IsNullOrEmpty(_textBox.Text))
                {
                    if(double.TryParse(_textBox.Text,out var value))
                    {
                        if(value<Minimum||value>Maximum)
                        {
                            result = OperationResult.Failed("超出范围");
                        }
                        else
                        {
                            result = OperationResult.Success();
                        }
                    }
                    else
                    {
                        result = OperationResult.Failed("数据格式错误");
                    }
                }
                else if (InfoElement.GetNecessary(this))
                {
                    result = OperationResult.Failed("必填！");
                }
                else
                {
                    result = OperationResult.Success();
                }
            }
            IsError = !result.Data;
            ErrorStr = result.Message;
            return result.Data;
           
        }
    }
}
