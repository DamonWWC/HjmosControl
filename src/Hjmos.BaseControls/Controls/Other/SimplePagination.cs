using Hjmos.BaseControls.Data;
using Hjmos.BaseControls.Interactivity;
using Hjmos.BaseControls.Tools;
using Hjmos.BaseControls.Tools.Extension;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hjmos.BaseControls.Controls
{
    [TemplatePart(Name = ElementButtonLeft, Type = typeof(Button))]
    [TemplatePart(Name = ElementButtonRight, Type = typeof(Button))]
    [TemplatePart(Name = ElementButtonFirst, Type = typeof(RadioButton))]
    [TemplatePart(Name = ElementMoreLeft, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ElementPanelMain, Type = typeof(Panel))]
    [TemplatePart(Name = ElementMoreRight, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ElementButtonLast, Type = typeof(RadioButton))]
    public class SimplePagination : Control
    {
        #region
        private const string ElementButtonLeft = "PART_ButtonLeft";
        private const string ElementButtonRight = "PART_ButtonRight";
        private const string ElementButtonFirst = "PART_ButtonFirst";
        private const string ElementMoreLeft = "PART_MoreLeft";
        private const string ElementPanelMain = "PART_PanelMain";
        private const string ElementMoreRight = "PART_MoreRight";
        private const string ElementButtonLast = "PART_ButtonLast";
        #endregion Constants

        #region Data
        private Button _buttonLeft;
        private Button _buttonRight;
        private RadioButton _buttonFirst;
        private FrameworkElement _moreLeft;
        private Panel _panelMain;
        private FrameworkElement _moreRight;
        private RadioButton _buttonLast;

        private bool _appliedTemplate;
        #endregion

        #region Event

        /// <summary>
        /// 页面更新事件
        /// </summary>
        public static readonly RoutedEvent PageUpdateEvent =
            EventManager.RegisterRoutedEvent("PageUpdated", RoutingStrategy.Bubble,
                typeof(EventHandler<FunctionEventArgs<int>>), typeof(SimplePagination));

        /// <summary>
        /// 页面更新事件
        /// </summary>
        public event EventHandler<FunctionEventArgs<int>> PageUpdated
        {
            add => AddHandler(PageUpdateEvent, value);
            remove => RemoveHandler(PageUpdateEvent, value);
        }

        #endregion

        public SimplePagination()
        {
            CommandBindings.Add(new System.Windows.Input.CommandBinding(ControlCommands.Prev, ButtonPrev_OnClick));
            CommandBindings.Add(new System.Windows.Input.CommandBinding(ControlCommands.Next, ButtonNext_OnClick));
            CommandBindings.Add(new System.Windows.Input.CommandBinding(ControlCommands.Selected, ToggleButton_OnChecked));
            CommandBindings.Add(new System.Windows.Input.CommandBinding(ControlCommands.Jump, (s, e) => PageIndex = JumpNum));
            this.Show(MaxPageCount > 0);
            MaxPageRange = new ObservableCollection<int>() { 10, 20, 30 };
        }

        /// <summary>
        /// 总共页数
        /// </summary>
        internal int MaxPageCount
        {
            get { return (int)GetValue(MaxPageCountProperty); }
            set { SetValue(MaxPageCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxPageCount.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty MaxPageCountProperty =
            DependencyProperty.Register("MaxPageCount", typeof(int), typeof(SimplePagination), new PropertyMetadata(1, (o, args) =>
            {
                if (o is SimplePagination pagination && args.NewValue is int value)
                {
                    if (pagination.PageIndex > pagination.MaxPageCount)
                    {
                        pagination.PageIndex = pagination.MaxPageCount;
                    }

                    pagination.Show(value > 0);
                    pagination.Update();
                }
            }, (o, value) =>
            {
                if (!(o is SimplePagination)) return 1;
                var intValue = (int)value;
                if (intValue < 1)
                {
                    return 1;
                }
                return intValue;
            }));

        /// <summary>
        /// 总共数据量
        /// </summary>
        public int MaxNum
        {
            get { return (int)GetValue(MaxNumProperty); }
            set { SetValue(MaxNumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxNum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxNumProperty =
            DependencyProperty.Register("MaxNum", typeof(int), typeof(SimplePagination), new PropertyMetadata(default(int), (o, args) =>
            {
                if (o is SimplePagination pagination && args.NewValue is int value)
                {
                    pagination.UpdatePage();
                }
            }));

        /// <summary>
        /// 每页最大数量
        /// </summary>
        public int DataCountPerPage
        {
            get { return (int)GetValue(DataCountPerPageProperty); }
            set { SetValue(DataCountPerPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataCountPerPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataCountPerPageProperty =
            DependencyProperty.Register("DataCountPerPage", typeof(int), typeof(SimplePagination), new PropertyMetadata(10, (o, args) =>
            {
                if (o is SimplePagination pagination && args.NewValue is int value)
                {
                    pagination.UpdatePage();
                }
            }));

        /// <summary>
        /// 设置每页可允许的数量
        /// </summary>
        internal ObservableCollection<int> MaxPageRange
        {
            get { return (ObservableCollection<int>)GetValue(MaxPageRangeProperty); }
            set { SetValue(MaxPageRangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty MaxPageRangeProperty =
            DependencyProperty.Register("MaxPageRange", typeof(ObservableCollection<int>), typeof(SimplePagination), new PropertyMetadata(default(ObservableCollection<int>)));

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("PageIndex", typeof(int), typeof(SimplePagination), new PropertyMetadata(1, (o, args) =>
            {
                if (o is SimplePagination pagination && args.NewValue is int value)
                {
                    pagination.Update();
                    pagination.RaiseEvent(new FunctionEventArgs<int>(PageUpdateEvent, pagination)
                    {
                        Info = value
                    });
                }
            }, (o, value) =>
            {
                if (!(o is SimplePagination pagination)) return 1;
                var intValue = (int)value;
                if (intValue <= 0)
                {
                    return 1;
                }
                if (intValue > pagination.MaxPageCount) return pagination.MaxPageCount;
                return intValue;
            }));

        /// <summary>
        ///     表示当前选中的按钮距离左右两个方向按钮的最大间隔（4表示间隔4个按钮，如果超过则用省略号表示）
        /// </summary>
        public int MaxPageInterval
        {
            get { return (int)GetValue(MaxPageIntervalProperty); }
            set { SetValue(MaxPageIntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxPageInterval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxPageIntervalProperty =
            DependencyProperty.Register("MaxPageInterval", typeof(int), typeof(SimplePagination), new PropertyMetadata(2, (o, args) =>
            {
                if (o is SimplePagination pagination)
                {
                    pagination.Update();
                }
            }), value =>
            {
                var intValue = (int)value;
                return intValue >= 0;
            });

        /// <summary>
        /// 跳转页数
        /// </summary>
        public int JumpNum
        {
            get { return (int)GetValue(JumpNumProperty); }
            set { SetValue(JumpNumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for JumpNum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty JumpNumProperty =
            DependencyProperty.Register("JumpNum", typeof(int), typeof(SimplePagination), new PropertyMetadata(1, (o, args) =>
            {
                if (o is SimplePagination pagination && args.NewValue is int value)
                {
                    pagination.PageIndex = value;
                }
            }, (o, value) =>
            {
                if (!(o is SimplePagination pagination)) return 1;
                var intValue = (int)value;
                if (intValue <= 0)
                    return 1;
                if (intValue > pagination.MaxPageCount)
                    return pagination.MaxPageCount;
                return intValue;
            }));

        public override void OnApplyTemplate()
        {
            _appliedTemplate = false;
            base.OnApplyTemplate();

            _buttonLeft = GetTemplateChild(ElementButtonLeft) as Button;
            _buttonRight = GetTemplateChild(ElementButtonRight) as Button;
            _buttonFirst = GetTemplateChild(ElementButtonFirst) as RadioButton;
            _moreLeft = GetTemplateChild(ElementMoreLeft) as FrameworkElement;
            _panelMain = GetTemplateChild(ElementPanelMain) as Panel;
            _moreRight = GetTemplateChild(ElementMoreRight) as FrameworkElement;
            _buttonLast = GetTemplateChild(ElementButtonLast) as RadioButton;

            CheckNull();
            _appliedTemplate = true;

            Update();
        }

        private void CheckNull()
        {
            if (_buttonLeft == null || _buttonRight == null || _buttonFirst == null ||
               _moreLeft == null || _panelMain == null || _moreRight == null ||
               _buttonLast == null) throw new Exception();
        }

        private void Update()
        {
            if (!_appliedTemplate) return;
            _buttonLeft.IsEnabled = PageIndex > 1;
            _buttonRight.IsEnabled = PageIndex < MaxPageCount;

            if (MaxPageInterval == 0)
            {
                _buttonFirst.Collapse();
                _buttonLast.Collapse();
                _moreLeft.Collapse();
                _moreRight.Collapse();
                _panelMain.Collapse();
                var selectButton = CreateButton(PageIndex);
                _panelMain.Children.Add(selectButton);
                selectButton.IsChecked = true;
                return;
            }

            _buttonFirst.Show();
            _buttonLast.Show();
            _moreLeft.Show();
            _moreRight.Show();

            if (MaxPageCount == 1)
            {
                _buttonLast.Collapse();
            }
            else
            {
                _buttonLast.Show();
                _buttonLast.Content = MaxPageCount.ToString();
            }

            var right = MaxPageCount - PageIndex;
            var left = PageIndex - 1;
            _moreRight.Show(right > MaxPageInterval);
            _moreLeft.Show(left > MaxPageInterval);

            _panelMain.Children.Clear();

            if (PageIndex > 1 && PageIndex < MaxPageCount)
            {
                var selectenButton = CreateButton(PageIndex);
                _panelMain.Children.Add(selectenButton);
                selectenButton.IsChecked = true;
            }
            else if (PageIndex == 1)
            {
                _buttonFirst.IsChecked = true;
            }
            else
            {
                _buttonLast.IsChecked = true;
            }

            var sub = PageIndex;
            for (var i = 0; i < MaxPageInterval - i; i++)
            {
                if (--sub > 1)
                {
                    _panelMain.Children.Insert(0, CreateButton(sub));
                }
                else
                {
                    break;
                }
            }
            var add = PageIndex;
            for (var i = 0; i < MaxPageInterval - 1; i++)
            {
                if (++add < MaxPageCount)
                {
                    _panelMain.Children.Add(CreateButton(add));
                }
                else
                {
                    break;
                }
            }
        }

        private void UpdatePage()
        {
            if (DataCountPerPage == 0) return;
            MaxPageCount = MaxNum % DataCountPerPage == 0 ? MaxNum / DataCountPerPage : MaxNum / DataCountPerPage + 1;
        }

        private void ButtonPrev_OnClick(object sender, RoutedEventArgs e) => PageIndex--;

        private void ButtonNext_OnClick(object sender, RoutedEventArgs e) => PageIndex++;

        private RadioButton CreateButton(int page)
        {
            return new RadioButton
            {
                Style = ResourceHelper.GetResource<Style>("SimplePaginationButtonStyle"),
                Content = page.ToString(),
            };
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!(e.OriginalSource is RadioButton button)) return;

            if (button.IsChecked == false) return;
            PageIndex = int.Parse(button.Content.ToString());
        }
    }
}