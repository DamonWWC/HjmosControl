using System.ComponentModel;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;

namespace Hjmos.BaseControls.Controls
{
    public class Card:ContentControl
    {

        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(Card), new PropertyMetadata(default(object)));

        [Bindable(true),Category("Content")]
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(Card), new PropertyMetadata(default(DataTemplate)));


        [Bindable(true),Category("Content")]
        public DataTemplateSelector HeaderTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(HeaderTemplateSelectorProperty); }
            set { SetValue(HeaderTemplateSelectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderTemplateSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderTemplateSelectorProperty =
            DependencyProperty.Register("HeaderTemplateSelector", typeof(DataTemplateSelector), typeof(Card), new PropertyMetadata(default(DataTemplateSelector)));


        [Bindable(true),Category("Content")]
        public string HeaderStringFormat
        {
            get { return (string)GetValue(HeaderStringFormatProperty); }
            set { SetValue(HeaderStringFormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderStringFormat.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderStringFormatProperty =
            DependencyProperty.Register("HeaderStringFormat", typeof(string), typeof(Card), new PropertyMetadata(default(string)));



        public object Footer
        {
            get { return (object)GetValue(FooterProperty); }
            set { SetValue(FooterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Footer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FooterProperty =
            DependencyProperty.Register("Footer", typeof(object), typeof(Card), new PropertyMetadata(default(object)));


        [Bindable(true),Category("Content")]
        public DataTemplate FooterTemplate
        {
            get { return (DataTemplate)GetValue(FooterTemplateProperty); }
            set { SetValue(FooterTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FooterTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FooterTemplateProperty =
            DependencyProperty.Register("FooterTemplate", typeof(DataTemplate), typeof(Card), new PropertyMetadata(default(DataTemplate)));


        [Bindable(true),Category("Content")]
        public DataTemplateSelector FooterTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(FooterTemplateSelectorProperty); }
            set { SetValue(FooterTemplateSelectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FooterTemplateSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FooterTemplateSelectorProperty =
            DependencyProperty.Register("FooterTemplateSelector", typeof(DataTemplateSelector), typeof(Card), new PropertyMetadata(default(DataTemplateSelector)));



        [Bindable(true),Category("Content")]
        public string FooterStringFormat
        {
            get { return (string)GetValue(FooterStringFormatProperty); }
            set { SetValue(FooterStringFormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FooterStringFormat.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FooterStringFormatProperty =
            DependencyProperty.Register("FooterStringFormat", typeof(string), typeof(Card), new PropertyMetadata(default(string)));








    }
}
