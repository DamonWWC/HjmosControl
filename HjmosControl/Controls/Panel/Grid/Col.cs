using HjmosControl.Data;
using HjmosControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HjmosControl.Controls
{
    public class Col : ContentControl
    {
        public static readonly DependencyProperty LayoutProperty = DependencyProperty.Register(
            "Layout", typeof(ColLayout), typeof(Col), new PropertyMetadata(default(ColLayout)));

        public ColLayout Layout
        {
            get => (ColLayout)GetValue(LayoutProperty);
            set => SetValue(LayoutProperty, value);
        }

        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
            "Offset", typeof(int), typeof(Col), new PropertyMetadata(0));

        public int Offset
        {
            get => (int)GetValue(OffsetProperty);
            set => SetValue(OffsetProperty, value);
        }

        public static readonly DependencyProperty SpanProperty = DependencyProperty.Register(
            "Span", typeof(int), typeof(Col), new PropertyMetadata(24), OnSpanValidate);

        private static bool OnSpanValidate(object value)
        {
            var v = (int)value;
            return v >= 1 || v <= 24;
        }

        public int Span
        {
            get => (int)GetValue(SpanProperty);
            set => SetValue(SpanProperty, value);
        }

        public static readonly DependencyProperty IsFixedProperty = DependencyProperty.Register(
            "IsFixed", typeof(bool), typeof(Col), new PropertyMetadata(false));

        public bool IsFixed
        {
            get => (bool)GetValue(IsFixedProperty);
            set => SetValue(IsFixedProperty, value);
        }

        internal int GetLayoutCellCount(ColLayoutStatus status)
        {
            var result = 0;

            if (Layout != null)
            {
                if (!IsFixed)
                {
                    switch (status)
                    {
                        case ColLayoutStatus.Xs:
                            result = Layout.Xs;
                            break;
                        case ColLayoutStatus.Sm:
                            result = Layout.Sm;
                            break;
                        case ColLayoutStatus.Md:
                            result = Layout.Md;
                            break;
                        case ColLayoutStatus.Lg:
                            result = Layout.Lg;
                            break;
                        case ColLayoutStatus.Xl:
                            result = Layout.Xl;
                            break;
                        case ColLayoutStatus.Xxl:
                            result = Layout.Xxl;
                            break;
                        case ColLayoutStatus.Auto:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(status), status, null);
                    }
                }
            }
            else
            {
                result = Span;
            }

            return result;
        }
    }
}
