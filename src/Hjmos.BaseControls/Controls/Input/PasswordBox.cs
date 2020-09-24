using Hjmos.BaseControls.Data;
using Hjmos.BaseControls.Interactivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Hjmos.BaseControls.Controls
{

    [TemplatePart(Name = ElementPasswordBox, Type = typeof(System.Windows.Controls.PasswordBox))]
    [TemplatePart(Name = ElementTextBox, Type = typeof(System.Windows.Controls.TextBox))]
    public class PasswordBox : Control, IDataInput
    {
        private const string ElementPasswordBox = "PART_PasswordBox";
        private const string ElementTextBox = "PART_TextBox";

        private SecureString _password;

        private System.Windows.Controls.TextBox _textBox;



        public Char PasswordChar
        {
            get { return (Char)GetValue(PasswordCharProperty); }
            set { SetValue(PasswordCharProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PasswordChar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordCharProperty =
            System.Windows.Controls.PasswordBox.PasswordCharProperty.AddOwner(typeof(PasswordBox), new FrameworkPropertyMetadata('●'));

      

        /// <summary>
        /// 数据是否有错误
        /// </summary>
        public static readonly DependencyProperty IsErrorProperty = DependencyProperty.Register("IsError", typeof(bool), typeof(PasswordBox), new PropertyMetadata(false));

        public bool IsError
        {
            get => (bool)GetValue(IsErrorProperty);
            set => SetValue(IsErrorProperty, value);
        }

        /// <summary>
        ///   错误提示
        /// </summary>
        public static readonly DependencyProperty ErrorStrProperty = DependencyProperty.Register(
            "ErrorStr", typeof(string), typeof(PasswordBox), new PropertyMetadata(default(string)));

        public string ErrorStr
        {
            get => (string)GetValue(ErrorStrProperty);
            set => SetValue(ErrorStrProperty, value);
        }


        /// <summary>
        ///   文本类型
        /// </summary>
        public static readonly DependencyProperty TextTypeProperty = DependencyProperty.Register(
            "TextType", typeof(TextType), typeof(PasswordBox), new PropertyMetadata(default(TextType)));
        public TextType TextType
        {
            get => (TextType)GetValue(TextTypeProperty);
            set => SetValue(TextTypeProperty, value);
        }

        /// <summary>
        ///  是否显示清楚按钮
        /// </summary>
        public static readonly DependencyProperty ShowClearButtonProperty = DependencyProperty.Register(
            "ShowClearButton", typeof(bool), typeof(PasswordBox), new PropertyMetadata(false));
        public bool ShowClearButton
        {
            get => (bool)GetValue(ShowClearButtonProperty);
            set => SetValue(ShowClearButtonProperty, value);
        }

        /// <summary>
        ///  是否显示可视密码按钮
        /// </summary>
        public bool ShowEyeButton
        {
            get { return (bool)GetValue(ShowEyeButtonProperty); }
            set { SetValue(ShowEyeButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowEyeButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowEyeButtonProperty =
            DependencyProperty.Register("ShowEyeButton", typeof(bool), typeof(PasswordBox), new PropertyMetadata(false));



        public bool ShowPassword
        {
            get { return (bool)GetValue(ShowPasswordProperty); }
            set { SetValue(ShowPasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowPassword.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowPasswordProperty =
            DependencyProperty.Register("ShowPassword", typeof(bool), typeof(PasswordBox), new PropertyMetadata(false, OnShowPasswordChanged));



        public bool IsSafeEnabled
        {
            get { return (bool)GetValue(IsSafeEnabledProperty); }
            set { SetValue(IsSafeEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSafeEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSafeEnabledProperty =
            DependencyProperty.Register("IsSafeEnabled", typeof(bool), typeof(PasswordBox), new PropertyMetadata(true, OnIsSafeEnableChanged));

        private static void OnIsSafeEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = (PasswordBox)d;
            p.SetCurrentValue(UnsafePasswordProperty, !(bool)e.NewValue ? p.Password : string.Empty);
        }



        public string UnsafePassword
        {
            get { return (string)GetValue(UnsafePasswordProperty); }
            set { SetValue(UnsafePasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnsafePassword.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnsafePasswordProperty =
            DependencyProperty.Register("UnsafePassword", typeof(string), typeof(PasswordBox), new FrameworkPropertyMetadata(default(string),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnUnsafePasswordChanged));

        private static void OnUnsafePasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = (PasswordBox)d;
            if (!p.IsSafeEnabled)
            {
                p.Password = e.NewValue != null ? e.NewValue.ToString() : string.Empty;
            }
        }



        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxLengthProperty =
            System.Windows.Controls.TextBox.MaxLengthProperty.AddOwner(typeof(PasswordBox));



        public Brush SelectiongBrush
        {
            get { return (Brush)GetValue(SelectiongBrushProperty); }
            set { SetValue(SelectiongBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectiongBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectiongBrushProperty =
            TextBoxBase.SelectionBrushProperty.AddOwner(typeof(PasswordBox));



        //#if !(NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472)

        //        public Brush SelectionTextBrush
        //        {
        //            get { return (Brush)GetValue(SelectionTextBrushProperty); }
        //            set { SetValue(SelectionTextBrushProperty, value); }
        //        }

        //        // Using a DependencyProperty as the backing store for SelectionTextBrush.  This enables animation, styling, binding, etc...
        //        public static readonly DependencyProperty SelectionTextBrushProperty =
        //             TextBoxBase.SelectionTextBrushProperty.AddOwner(typeof(PasswordBox));

        //#endif





        public double SelectionOpacity
        {
            get { return (double)GetValue(SelectionOpacityProperty); }
            set { SetValue(SelectionOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionOpacityProperty =
            TextBoxBase.SelectionOpacityProperty.AddOwner(typeof(PasswordBox));



        public Brush CaretBrush
        {
            get { return (Brush)GetValue(CaretBrushProperty); }
            set { SetValue(CaretBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CaretBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaretBrushProperty =
            TextBoxBase.CaretBrushProperty.AddOwner(typeof(PasswordBox));



        public static readonly DependencyProperty IsSelectionActiveProperty =
             TextBoxBase.IsSelectionActiveProperty.AddOwner(typeof(PasswordBox));

        public bool IsSelectionActive => ActualPasswordBox != null && (bool)ActualPasswordBox.GetValue(IsSelectionActiveProperty);

        public PasswordBox() => CommandBindings.Add(new CommandBinding(ControlCommands.Clear, (s, e) => Clear()));



        public System.Windows.Controls.PasswordBox ActualPasswordBox { get; set; }


        [DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Password
        {
            get
            {
                if (ShowEyeButton && ShowPassword)
                {
                    return _textBox.Text;
                }
                return ActualPasswordBox?.Password;
            }
            set
            {
                if (ActualPasswordBox == null)
                {
                    _password = new SecureString();
                    value ??= string.Empty;
                    foreach (var item in value)
                    {
                        _password.AppendChar(item);
                    }
                    return;
                }
                if (Equals(ActualPasswordBox.Password, value)) return;
                ActualPasswordBox.Password = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SecureString SecurePassword => ActualPasswordBox?.SecurePassword;

        public Func<string, OperationResult<bool>> VerifyFunc { get ; set; }

        public static void OnShowPasswordChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            var ctl = (PasswordBox)d;
            if (!ctl.ShowEyeButton) return;
            if((bool)e.NewValue)
            {
                ctl._textBox.Text = ctl.ActualPasswordBox.Password;
                ctl._textBox.Select(string.IsNullOrEmpty(ctl._textBox.Text) ? 0 : ctl._textBox.Text.Length, 0);
            }
            else
            {
                ctl.ActualPasswordBox.Password = ctl._textBox.Text;
                ctl._textBox.Clear();
            }
        }



        public bool VerifyData()
        {
            OperationResult<bool> result;

            if (VerifyFunc != null)
            {
                result = VerifyFunc.Invoke(ShowEyeButton && ShowPassword
                    ? _textBox.Text
                    : ActualPasswordBox.Password);
            }
            else
            {
                if (!string.IsNullOrEmpty(ShowEyeButton && ShowPassword
                    ? _textBox.Text
                    : ActualPasswordBox.Password))
                    result = OperationResult.Success();
                else if (InfoElement.GetNecessary(this))
                    result = OperationResult.Failed("不能为空！");
                else
                    result = OperationResult.Success();
            }
            IsError = !result.Data;
            ErrorStr = result.Message;
            return result.Data;
        }

        public override void OnApplyTemplate()
        {
            if (ActualPasswordBox != null)
                ActualPasswordBox.PasswordChanged -= PasswordBox_PasswordChanged;
            if (_textBox != null)
                _textBox.TextChanged -= TextBox_TextChanged;
            base.OnApplyTemplate();

            ActualPasswordBox = GetTemplateChild(ElementPasswordBox) as System.Windows.Controls.PasswordBox;
            _textBox = GetTemplateChild(ElementTextBox) as System.Windows.Controls.TextBox;
            if (ActualPasswordBox != null)
            {
                ActualPasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
                ActualPasswordBox.SetBinding(System.Windows.Controls.PasswordBox.MaxLengthProperty, new Binding(MaxLengthProperty.Name) { Source = this });
                ActualPasswordBox.SetBinding(System.Windows.Controls.PasswordBox.SelectionBrushProperty, new Binding(SelectiongBrushProperty.Name) { Source = this });
                //#if !(NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472)
                //                ActualPasswordBox.SetBinding(System.Windows.Controls.PasswordBox.SelectionTextBrushProperty, new Binding(SelectionTextBrushProperty.Name) { Source = this });
                //#endif
                ActualPasswordBox.SetBinding(System.Windows.Controls.PasswordBox.SelectionOpacityProperty, new Binding(SelectionOpacityProperty.Name) { Source = this });
                ActualPasswordBox.SetBinding(System.Windows.Controls.PasswordBox.CaretBrushProperty, new Binding(CaretBrushProperty.Name) { Source = this });

                if(_password!=null)
                {
                    if(_password.Length>0)
                    {
                        var valuePtr = IntPtr.Zero;
                        try
                        {
                            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(_password);
                            ActualPasswordBox.Password = Marshal.PtrToStringUni(valuePtr) ?? throw new InvalidOperationException();
                        }
                        finally
                        {
                            Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
                            _password.Clear();
                        }
                    }
                }
            }
            if(_textBox !=null)
            {
                _textBox.TextChanged += TextBox_TextChanged;
            }

        }

        public void Paste()
        {
            ActualPasswordBox.Paste();
            if(ShowEyeButton &&ShowPassword)
            {
                _textBox.Text = ActualPasswordBox.Password;
            }
        }

       public void SelectAll()
        {
            ActualPasswordBox.SelectAll();
            if(ShowEyeButton&&ShowPassword)
            {
                _textBox.SelectAll();
            }
        }

        public void Clear()
        {
            ActualPasswordBox.Clear();
            _textBox.Clear();
        }

        private void PasswordBox_PasswordChanged(object sender,RoutedEventArgs e)
        {
            if(VerifyData()&&!IsSafeEnabled)
            {
                SetCurrentValue(UnsafePasswordProperty, ActualPasswordBox.Password);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => VerifyData();
    }
}
