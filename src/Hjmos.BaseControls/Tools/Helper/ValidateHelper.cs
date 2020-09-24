

namespace HjmosControl.Tools
{
    /// <summary>
    /// 验证帮助类
    /// </summary>
    public class ValidateHelper
    {
        /// <summary>
        /// 是否在浮点数范围内
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInRangeOfDouble(object value)
        {
            var v = (double)value;
            return !(double.IsNaN(v) || double.IsInfinity(v));
        }
        /// <summary>
        /// 是否在正浮点数范围类（包括0）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInRangeOfPosDoubleIncludeZero(object value)
        {
            var v = (double)value;
            return !(double.IsNaN(v) || double.IsInfinity(v)) && v >= 0;
        }
    }
}
