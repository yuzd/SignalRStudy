using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using SignalR.HubClient;
using SignalR.HubClient.Entity;
using SignalR.SLAB;
using SignalR.SLAB.Services;

namespace SignalR.Host.Hubs
{
    /// <summary>
    /// 客户端 建立连接实例
    /// </summary>
    public class HubSync : Hub<ISendHubSync>
    {
        private readonly IHubLogger _slabLogger;

        /// <summary>
        /// Unit注入
        /// </summary>
        /// <param name="slabLogger"></param>
        public HubSync(IHubLogger slabLogger)
        {
            _slabLogger = slabLogger;
        }

        #region ISendHubSync
        public void AddMessage(string name, string message)
        {
            _slabLogger.Log(HubType.HubServerVerbose, "HubSync Sending AddMessage " + name + " " + message);
            Clients.All.AddMessage(name, message);
        }

        public void Heartbeat()
        {
            _slabLogger.Log(HubType.HubServerVerbose, "HubSync Sending Heartbeat");
            Clients.All.Heartbeat();
        }

        public void SendHelloObject(HelloModel hello)
        {
            _slabLogger.Log(HubType.HubServerVerbose, "HubSync Sending SendHelloObject " + hello.Molly + " " + hello.Age);
            Clients.All.SendHelloObject(hello);
        } 
        #endregion

        public override Task OnConnected()
        {
            _slabLogger.Log(HubType.HubServerVerbose, "HubSync OnConnected" + Context.ConnectionId);
            return (base.OnConnected());
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _slabLogger.Log(HubType.HubServerVerbose, "HubSync OnDisconnected" + Context.ConnectionId);
            return (base.OnDisconnected(stopCalled));
        }

        public override Task OnReconnected()
        {
            _slabLogger.Log(HubType.HubServerVerbose, "HubSync OnReconnected" + Context.ConnectionId);
            return (base.OnReconnected());
        }
    }
}
