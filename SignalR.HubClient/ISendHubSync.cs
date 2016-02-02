using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalR.HubClient.Entity;

namespace SignalR.HubClient
{
    public interface ISendHubSync
    {
        /// <summary>
        /// 发送文本
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        void AddMessage(string name, string message);

        /// <summary>
        /// 心跳
        /// </summary>
        void Heartbeat();

        /// <summary>
        /// 发送对象
        /// </summary>
        /// <param name="hello"></param>
        void SendHelloObject(HelloModel hello);

    }
}
