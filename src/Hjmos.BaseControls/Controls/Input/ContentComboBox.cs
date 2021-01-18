using Hjmos.BaseControls.Data;
using Hjmos.BaseControls.Interactivity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hjmos.BaseControls.Controls
{

    [TemplatePart(Name = ElementTextBlock, Type = typeof(Panel))]
    public class ContentComboBox : ContentControl
    {

        private const string ElementTextBlock = "PART_TextBlock";
        private TextBlock _textBlock;
        public ContentComboBox()
        {
            CommandBindings.Add(new System.Windows.Input.CommandBinding(ControlCommands.Confirm, ButtonConfirm_OnClick));
            CommandBindings.Add(new System.Windows.Input.CommandBinding(ControlCommands.Clear, (s, e) =>
            {
                DisplayText = "";
                //SelectedAllItems = null;
            }));
        }
        private void ButtonConfirm_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedAllItems != null)
            {
                if (SelectedAllItems is IEnumerable Selected)
                {
                    List<string> result = new List<string>();
                    foreach (var selectitem in Selected)
                    {
                        if (DisplayMemberPath != null)
                        {
                            var display = selectitem?.GetType().GetProperty(DisplayMemberPath);
                            if (display != null)
                            {
                                var dismember = display.GetValue(selectitem);
                                result.Add(dismember.ToString());
                            }
                            else
                            {
                                result.Add(selectitem.ToString());
                            }

                        }
                        else
                        {
                            result.Add(selectitem.ToString());
                        }
                    }
                    DisplayText = string.Join(Separator, result.ToArray());
                }
                else
                {
                    DisplayText = SelectedAllItems.ToString();
                }

            }

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _textBlock = GetTemplateChild(ElementTextBlock) as TextBlock;
            Dispatcher.BeginInvoke(new Action(() =>
            {
                IsDropDownOpen = true;
                IsDropDownOpen = false;
            }), System.Windows.Threading.DispatcherPriority.DataBind);
        }


        //public static readonly RoutedEvent ClearEvent =
        //    EventManager.RegisterRoutedEvent("Clear", RoutingStrategy.Bubble,
        //        typeof(EventHandler), typeof(ContentComboBox));
        //public event EventHandler Clear
        //{

        //}



        public static readonly DependencyProperty MaxDropDownHeightProperty =
      System.Windows.Controls.ComboBox.MaxDropDownHeightProperty.AddOwner(typeof(ContentComboBox),
          new FrameworkPropertyMetadata(SystemParameters.PrimaryScreenHeight / 3));

        [Bindable(true), Category("Layout")]
        [TypeConverter(typeof(LengthConverter))]
        public double MaxDropDownHeight
        {
            get => (double)GetValue(MaxDropDownHeightProperty);
            set => SetValue(MaxDropDownHeightProperty, value);
        }

        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register(
            "IsDropDownOpen", typeof(bool), typeof(ContentComboBox), new PropertyMetadata(false));

        public bool IsDropDownOpen
        {
            get => (bool)GetValue(IsDropDownOpenProperty);
            set => SetValue(IsDropDownOpenProperty, value);
        }



        public object SelectedAllItems
        {
            get { return (object)GetValue(SelectedAllItemsProperty); }
            set { SetValue(SelectedAllItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedAllItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedAllItemsProperty =
            DependencyProperty.Register("SelectedAllItems", typeof(object), typeof(ContentComboBox), new PropertyMetadata(default(object), (o, args) =>
            {

            }));





        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayMemberPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(ContentComboBox), new PropertyMetadata(default(string)));



        public bool ShowClearButton
        {
            get { return (bool)GetValue(ShowClearButtonProperty); }
            set { SetValue(ShowClearButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowClearButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowClearButtonProperty =
            DependencyProperty.Register("ShowClearButton", typeof(bool), typeof(ContentComboBox), new PropertyMetadata(true));





        internal bool ShowPlaceholder
        {
            get { return (bool)GetValue(ShowPlaceholderProperty); }
            set { SetValue(ShowPlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowPlace.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty ShowPlaceholderProperty =
            DependencyProperty.Register("ShowPlaceholder", typeof(bool), typeof(ContentComboBox), new PropertyMetadata(true));


        internal string DisplayText
        {
            get { return (string)GetValue(DisplayTextProperty); }
            set { SetValue(DisplayTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register("DisplayText", typeof(string), typeof(ContentComboBox), new PropertyMetadata(default(string)));




        /// <summary>
        /// 分割符
        /// </summary>
        public string Separator
        {
            get { return (string)GetValue(SeparatorProperty); }
            set { SetValue(SeparatorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Separator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeparatorProperty =
            DependencyProperty.Register("Separator", typeof(string), typeof(ContentComboBox), new PropertyMetadata(","));

        
    }
}
