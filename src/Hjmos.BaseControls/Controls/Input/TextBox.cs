﻿using Hjmos.BaseControls.Controls;
using Hjmos.BaseControls.Data;
using Hjmos.BaseControls.Interactivity;
using Hjmos.BaseControls.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hjmos.BaseControls.Controls
{
    public class TextBox : System.Windows.Controls.TextBox, IDataInput
    {
        public TextBox()
        {
            CommandBindings.Add(new CommandBinding(ControlCommands.Clear, (s, e) =>
            {
                SetCurrentValue(TextProperty, "");
            }));
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            VerifyData();
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            //VerifyData();
        }

        public Func<string, OperationResult<bool>> VerifyFunc { get; set; }

        /// <summary>
        ///     数据是否错误
        /// </summary>
        public static readonly DependencyProperty IsErrorProperty = DependencyProperty.Register(
            "IsError", typeof(bool), typeof(TextBox), new PropertyMetadata(false));

        public bool IsError
        {
            get => (bool)GetValue(IsErrorProperty);
            set => SetValue(IsErrorProperty, value);
        }

        /// <summary>
        ///     错误提示
        /// </summary>
        public static readonly DependencyProperty ErrorStrProperty = DependencyProperty.Register(
            "ErrorStr", typeof(string), typeof(TextBox), new PropertyMetadata(default(string)));

        public string ErrorStr
        {
            get => (string)GetValue(ErrorStrProperty);
            set => SetValue(ErrorStrProperty, value);
        }

        /// <summary>
        ///     文本类型
        /// </summary>
        public static readonly DependencyProperty TextTypeProperty = DependencyProperty.Register(
            "TextType", typeof(TextType), typeof(TextBox), new PropertyMetadata(default(TextType)));

        public TextType TextType
        {
            get => (TextType)GetValue(TextTypeProperty);
            set => SetValue(TextTypeProperty, value);
        }       
        /// <summary>
        ///     是否显示清除按钮
        /// </summary>
        public static readonly DependencyProperty ShowClearButtonProperty = DependencyProperty.Register(
            "ShowClearButton", typeof(bool), typeof(TextBox), new PropertyMetadata(false));

        public bool ShowClearButton
        {
            get => (bool)GetValue(ShowClearButtonProperty);
            set => SetValue(ShowClearButtonProperty, value);
        }

        public virtual bool VerifyData()
        {
            OperationResult<bool> result;

            if (VerifyFunc != null)
            {
                result = VerifyFunc.Invoke(Text);
            }
            else
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    if (TextType != TextType.Common)
                    {
                        result = Text.IsKindOf(TextType) ? OperationResult.Success() : OperationResult.Failed("格式错误！");
                    }
                    else
                    {
                        result = OperationResult.Success();
                    }
                }
                else if (InfoElement.GetNecessary(this))
                {
                    result = OperationResult.Failed("不能为空");
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
