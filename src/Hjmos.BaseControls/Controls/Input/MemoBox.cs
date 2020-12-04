using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Hjmos.BaseControls.Controls
{
    public class MemoBox : ComboBox
    {

        public MemoBox()
        {

        }




        public string MemoText
        {
            get { return (string)GetValue(MemoTextProperty); }
            set { SetValue(MemoTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MemoTextProperty =
            DependencyProperty.Register("MemoText", typeof(string), typeof(MemoBox), new PropertyMetadata(default(string),(o,args)=>
            {
                var memoBox = o as MemoBox;
                var value = args.NewValue as string;
                var list = new List<string> { value};

                memoBox.ItemsSource = list;

            }));


    }
}
