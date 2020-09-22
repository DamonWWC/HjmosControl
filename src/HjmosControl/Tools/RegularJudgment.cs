using HjmosControl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HjmosControl.Tools
{
    public static class RegularJudgment
    {
        private static readonly RegularPatterns RegularPatterns = new RegularPatterns();
        /// <summary>
        /// 判断字符串是否满足指定格式
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textType"></param>
        /// <returns></returns>
        public static bool IsKindOf(this string text,TextType textType)
        {
            if (textType == TextType.Common) return true;
            return Regex.IsMatch(text, RegularPatterns.GetValue(Enum.GetName(typeof(TextType), textType) + "Pattern").ToString());
        }
    }
}
