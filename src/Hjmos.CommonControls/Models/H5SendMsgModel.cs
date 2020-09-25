
namespace Hjmos.CommonControls
{
    /// <summary>
    /// WPF发送H5的请求类
    /// </summary>
   public class H5SendMsgModel
    {
        /// <summary>
        /// 用于请求Js的方法名（例如wpfToH5）
        /// </summary>
        public string JavaScripMethodName
        {
            get;set;
        }

        /// <summary>
        /// 请求发送的字符串
        /// </summary>
        public string SendJsonMsg
        {
            get;set;
        }
    }
}
