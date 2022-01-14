using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjmos.MQProxy
{
    /// <summary>
    /// 消费队列订阅方式
    /// </summary>
    public enum SubscriptionMode
    {
        /// <summary> 集群订阅 </summary>
        CLUSTERING = 0,
        /// <summary> 广播订阅 </summary>
        BROADCASTING = 1,
    }
}
