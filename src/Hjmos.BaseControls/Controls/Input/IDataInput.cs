using HjmosControl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HjmosControl.Controls
{
    public interface IDataInput
    {
        /// <summary>
        ///     验证数据
        /// </summary>
        /// <returns></returns>
        bool VerifyData();

        /// <summary>
        ///     数据验证委托
        /// </summary>
        Func<string, OperationResult<bool>> VerifyFunc { get; set; }

        /// <summary>
        ///     数据是否错误
        /// </summary>
        bool IsError { get; set; }

        /// <summary>
        ///     错误提示
        /// </summary>
        string ErrorStr { get; set; }

        /// <summary>
        ///     文本类型
        /// </summary>
        TextType TextType { get; set; }

        /// <summary>
        ///     是否显示清除按钮
        /// </summary>
        bool ShowClearButton { get; set; }
    }
}
