using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Hjmos.CustomCharts.Controls.Tools.Converter
{
    /// <summary>
    /// 布尔值转换器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BooleanConverter<T> : IValueConverter
    {
        protected BooleanConverter(T trueResult, T falseResult)
        {
            TrueResult = trueResult;
            FalseResult = falseResult;
        }

        /// <summary>
        /// value为true时返回的值
        /// </summary>
        public T TrueResult { get; set; }

        /// <summary>
        /// value为false时返回的值
        /// </summary>
        public T FalseResult { get; set; }


        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool flag && flag ? TrueResult : FalseResult;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T flag && EqualityComparer<T>.Default.Equals(flag, TrueResult);
        }
    }
}
