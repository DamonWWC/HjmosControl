using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Hjmos.BaseControls.Controls
{
    public class ButtonGroup : ItemsControl
    {
        protected override bool IsItemItsOwnContainerOverride(object item) => item is Button || item is RadioButton || item is ToggleButton;



        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ButtonGroup), new PropertyMetadata(default(Orientation)));


        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);

            var count = Items.Count;
            for(var i=0;i<count;i++)
            {
                var item = (ButtonBase)Items[i];
                item.Style = ItemContainerStyleSelector?.SelectStyle(item, this);
            }
        }
    }
}
