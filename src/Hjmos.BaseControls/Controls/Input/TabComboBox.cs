using Hjmos.BaseControls.Interactivity;
using Hjmos.BaseControls.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Hjmos.BaseControls.Controls
{
    [TemplatePart(Name = ElementPanel, Type = typeof(Panel))]
    [TemplatePart(Name = ElementTabControl, Type = typeof(TabControl))]
    public class TabComboBox : Control
    {
        private const string ElementPanel = "PART_Panel";
        private const string ElementTabControl = "PART_TabPresenter";
        private Panel _panel;
        private TabControl _tabControl;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _panel = GetTemplateChild(ElementPanel) as Panel;
            _tabControl = GetTemplateChild(ElementTabControl) as TabControl;
            CreateItem();
            Dispatcher.BeginInvoke(new Action(() =>
            {
                IsDropDownOpen = true;
                IsDropDownOpen = false;
            }), DispatcherPriority.DataBind);
        }

        public TabComboBox()
        {
            AddHandler(Controls.Tag.ClosedEvent, new RoutedEventHandler(Tags_OnClosed));
        }

        private void ListBoxItemSelected(object sender, ExecutedRoutedEventArgs e)
        {
            var item = e.OriginalSource;
            var header = e.Parameter;

            AddTags(item, header);
        }

        private void Tags_OnClosed(object sender, RoutedEventArgs e)
        {
            RemoveTags(e.OriginalSource);
        }



        public static readonly DependencyProperty MaxDropDownHeightProperty =
        System.Windows.Controls.ComboBox.MaxDropDownHeightProperty.AddOwner(typeof(TabComboBox),
            new FrameworkPropertyMetadata(SystemParameters.PrimaryScreenHeight / 3));

        [Bindable(true), Category("Layout")]
        [TypeConverter(typeof(LengthConverter))]
        public double MaxDropDownHeight
        {
            get => (double)GetValue(MaxDropDownHeightProperty);
            set => SetValue(MaxDropDownHeightProperty, value);
        }

        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register(
            "IsDropDownOpen", typeof(bool), typeof(TabComboBox), new PropertyMetadata(false));

        public bool IsDropDownOpen
        {
            get => (bool)GetValue(IsDropDownOpenProperty);
            set => SetValue(IsDropDownOpenProperty, value);
        }


        public static readonly DependencyProperty TagStyleProperty = DependencyProperty.Register(
            "TagStyle", typeof(Style), typeof(TabComboBox), new PropertyMetadata(default(Style)));

        public Style TagStyle
        {
            get => (Style)GetValue(TagStyleProperty);
            set => SetValue(TagStyleProperty, value);
        }


        [Bindable(true)]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<string> SelectedItems
        {
            get { return (List<string>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(List<string>), typeof(TabComboBox), new FrameworkPropertyMetadata(default(List<string>), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        /// <summary>
        /// 内容头名
        /// </summary>
        public List<string> Headers
        {
            get { return (List<string>)GetValue(HeadersProperty); }
            set { SetValue(HeadersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Headers.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeadersProperty =
            DependencyProperty.Register("Headers", typeof(List<string>), typeof(TabComboBox), new PropertyMetadata(default(List<string>), (o, args) =>
            {
                if (o is TabComboBox ct1)
                {
                    ct1.CreateItem();
                }
            }));




        internal bool ShowPlaceholder
        {
            get { return (bool)GetValue(ShowPlaceholderProperty); }
            set { SetValue(ShowPlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowPlace.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty ShowPlaceholderProperty =
            DependencyProperty.Register("ShowPlaceholder", typeof(bool), typeof(TabComboBox), new PropertyMetadata(true));





        private void CreateItem()
        {
            if (Headers == null || Contents == null || _tabControl == null)
                return;

            var num = Headers.Count;

            for (int i = 0; i < num; i++)
            {
                TabItem tabItem = new TabItem();
                tabItem.Header = Headers[i];


                ListBox listBox = new ListBox
                {
                    Style = ResourceHelper.GetResource<Style>("TabItemListBox"),
                    ItemsSource = Contents[i],
                    Tag = Headers[i],
                    Background = Brushes.Transparent,
                    Foreground = Foreground,
                    BorderThickness = new Thickness(0)
                };

                listBox.SelectionChanged += ListBox_SelectionChanged;

                tabItem.Content = listBox;

                _tabControl.Items.Add(tabItem);
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;
            var listbox = sender as ListBox;

            var item = e.AddedItems[0];

            AddTags(item, listbox);
        }

        /// <summary>
        /// 内容
        /// </summary>
        public List<List<string>> Contents
        {
            get { return (List<List<string>>)GetValue(ContentsProperty); }
            set { SetValue(ContentsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contents.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentsProperty =
            DependencyProperty.Register("Contents", typeof(List<List<string>>), typeof(TabComboBox), new PropertyMetadata(default(List<string>), (o, args) =>
            {

                if (o is TabComboBox ct1)
                {
                    ct1.CreateItem();
                }
            }));




        private void AddTags(object item, object listbox)
        {

            if (_panel == null) return;
            if (listbox is ListBox listBox)
            {
                var header = listBox.Tag;
                foreach (var children in _panel.Children)
                {
                    var childrentag = children as Tag;
                    if (childrentag.Header == header)
                    {
                        childrentag.Content = item;
                        var index = Headers.IndexOf(header.ToString());
                        SelectedItems[index] = header.ToString();
                        return;
                    }
                }
                var tag = new Tag
                {
                    Style = TagStyle,
                    Header = header,
                    Content = item,
                    Tag = listbox,
                    Foreground = this.Foreground
                };

                _panel.Children.Add(tag);

                if (SelectedItems == null)
                {
                    SelectedItems = new List<string>();
                    for (int i = 0; i < Headers.Count; i++)
                    {
                        SelectedItems.Add(null);
                    }
                }

                var num = Headers.IndexOf(header.ToString());
                SelectedItems[num] = header.ToString();
                ShowPlaceholder = false;
            }
        }


        private void RemoveTags(object tag)
        {
            var taga = (Tag)tag;
            var header = taga.Header;
            var num = Headers.IndexOf(header.ToString());
            SelectedItems[num] = null;
            _panel.Children.Remove(taga);
            var listbox = (ListBox)taga.Tag;
            listbox.UnselectAll();

            var n = SelectedItems.FindIndex(p => p != null);

            if (n < 0)
                ShowPlaceholder = true;
            //var listboxItem = (ListBoxItem)taga.Tag;

            //listboxItem.SetCurrentValue(Selector.IsSelectedProperty, false);

        }
    }
}
