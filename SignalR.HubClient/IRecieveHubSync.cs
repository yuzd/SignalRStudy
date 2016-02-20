using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalR.HubClient.Entity;

namespace SignalR.HubClient
{
    public interface IRecieveHubSync
    {

        /// <summary>
        /// 接收心跳
        /// </summary>
        void Recieve_Heartbeat();

        /// <summary>
        /// 接收信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        void Recieve_SendMessage(string name, string message);

        /// <summary>
        /// 接收 获取所有用户
        /// </summary>
        /// <param name="_userInfoList"></param>
        void Recieve_RefreshAllClientList(ConcurrentDictionary<string, UserInfo> _userInfoList);

        /// <summary>
        /// 接收 获取用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        void Recieve_GetCurrentUserInfo(UserInfo userInfo);
    }
}
