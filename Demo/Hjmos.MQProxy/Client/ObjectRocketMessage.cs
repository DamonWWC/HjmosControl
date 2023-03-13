using Newtonsoft.Json.Linq;
using System;

namespace Hjmos.MQProxy
{
    internal class ObjectRocketMessage : ObjectMessage
    {   
        /// <summary>
        /// Json类型的消息体
        /// </summary>
        public JObject JBody { get ; set ; }

        /// <summary>
        /// 消息类型转换
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>转换后的实体</returns>
        public override T Body<T>() where T : class
        {
            try
            {               
                return JBody == null ? default : JBody.ToObject<T>();
            }
            catch (Exception ex)
            {               
                return default;
            }
        }

    }
}
