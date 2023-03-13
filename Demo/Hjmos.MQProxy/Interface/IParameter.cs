
namespace Hjmos.MQProxy
{
    /// <summary>
    /// 访问参数接口
    /// </summary>
    public interface IParameter
    {
        /// <summary>
        /// 主题
        /// </summary>
        string Topic { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        string NameServerAddress { get; set; }

        /// <summary>访问令牌</summary>
        string AccessKey { get; set; }

        /// <summary>访问密钥</summary>
        string SecretKey { get; set; }
    }
}
