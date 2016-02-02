using System;
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
        /// 接收文本
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        void Recieve_AddMessage(string name, string message);

        /// <summary>
        /// 接收心跳
        /// </summary>
        void Recieve_Heartbeat();

        /// <summary>
        /// 接收对象
        /// </summary>
        /// <param name="hello"></param>
        void Recieve_SendHelloObject(HelloModel hello);

    }
}
