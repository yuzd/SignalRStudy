using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalR.HubClient.Entity;

namespace SignalR.Host.Entity
{
    public class GlobalData
    {
        //存储在线用户集合
        public static ConcurrentDictionary<string, UserInfo> userInfoList = new ConcurrentDictionary<string, UserInfo>();
    }
}
