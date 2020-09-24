using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Hjmos.BaseControls.Interactivity
{
    public class AdornerContainer : Adorner
    {
      
        public AdornerContainer(UIElement adornedElement) : base(adornedElement)
        {
            
        }

        private UIElement _child;

        public UIElement Child
        {
            get { return _child; }
            set
            { 
               if(value==null)
                {
                    RemoveVisualChild(_child);
                    _child = value;
                    return;
                }
                AddVisualChild(value);
                _child = value;
            }
        }

        protected override int VisualChildrenCount => _child != null ? 1 : 0;

        protected override Size ArrangeOverride(Size finalSize)
        {
            _child?.Arrange(new Rect(finalSize));
            return finalSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index == 0 && _child != null) return _child;
            return base.GetVisualChild(index);
        }
    }
}
