
using Microsoft.AspNet.SignalR;
using SignalR.Host.Hubs;
using SignalR.HubClient;
using SignalR.HubClient.Entity;
using SignalR.SLAB;
using SignalR.SLAB.Services;

namespace SignalR.Host.Service
{
    public class SendHubSync : ISendHubSync
    {
        private readonly IHubLogger _slabLogger;
        private readonly IHubContext _hubContext;

        public SendHubSync(IHubLogger slabLogger)
        {
            _slabLogger = slabLogger;
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<HubSync>(); 
        }
        
        public void AddMessage(string name, string message)
        {
            _hubContext.Clients.All.addMessage("MyHub", "ServerMessage");
            _slabLogger.Log(HubType.HubServerVerbose, "MyHub Sending addMessage");
        }

        public void Heartbeat()
        {
            _hubContext.Clients.All.heartbeat();
            _slabLogger.Log(HubType.HubServerVerbose, "MyHub Sending heartbeat");
        }

        public void SendHelloObject(HelloModel hello)
        {
            _hubContext.Clients.All.sendHelloObject(hello);
            _slabLogger.Log(HubType.HubServerVerbose, "MyHub Sending sendHelloObject");
        }
    }
}
