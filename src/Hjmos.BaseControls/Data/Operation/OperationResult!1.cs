using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjmos.BaseControls.Data
{
    public class OperationResult<T>:OperationResult
    {
        /// <summary>
        /// 操作结果
        /// </summary>
        public ResultType ResultType { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 操作信息
        /// </summary>
        public string Message { get; set; }
    }
}
