using System.Windows;
using System.Windows.Data;

namespace Hjmos.CustomCharts.Controls.Tools.Converter
{
    /// <summary>
    /// 布尔值→可见性转换器（false时不占用空间）
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public BooleanToVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed) { }
    }
}
