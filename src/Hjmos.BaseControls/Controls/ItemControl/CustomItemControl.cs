using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Hjmos.BaseControls.Controls
{
    [TemplatePart(Name = ElementRectangle, Type = typeof(Rectangle))]
    public class CustomItemControl : ItemsControl
    {
        private Rectangle _rectangle;
        private const string ElementRectangle = "PART_Rectangle";

        public CustomItemControl()
        {
            this.SizeChanged += CustomItemControl_SizeChanged;
            this.Loaded += CustomItemControl_Loaded;
        }

        private void CustomItemControl_Loaded(object sender, RoutedEventArgs e)
        {
            var colCount = this.Items.Count;
            if (colCount == 0) return;
            _rectangle.Width = (colCount - 1) * (ActualWidth / colCount);
        }

        private void CustomItemControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _rectangle = GetTemplateChild(ElementRectangle) as Rectangle;
        }
    }
}