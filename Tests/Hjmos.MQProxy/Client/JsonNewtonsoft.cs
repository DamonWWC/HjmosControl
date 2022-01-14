using Newtonsoft.Json;
using System;

namespace Hjmos.MQProxy
{
    /// <summary>
    /// Json与对象实体转换类
    /// </summary>
    public static class JsonNewtonsoft
    {
        /// <summary>
        /// 把对象转换为JSON字符串
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>JSON字符串</returns>
        public static string ToJSON(object o)
        {
            if (o == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(o);
        }
        /// <summary>
        /// 把Json文本转为实体
        /// </summary>
        /// <param name="input">JSON字符串</param>
        /// <param name="o">对象类型</param>
        /// <returns>对象实体</returns>
        public static object FromJSON(string input, Type o)
        {
            try
            {
                return JsonConvert.DeserializeObject(input, o);
            }
            catch (Exception ex)
            {
                return o;
            }
        }
    }
}
