
using System.Collections.Concurrent;
using System.Collections.Generic;
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


        public void SendMessage(string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public void Heartbeat()
        {
            _hubContext.Clients.All.heartbeat();
            _slabLogger.Log(HubServerType.HubServerVerbose, "MyHub Sending heartbeat");
        }

        public void RefreshAllClientList(ConcurrentDictionary<string, UserInfo> _userInfoList)
        {
            throw new System.NotImplementedException();
        }

        public void GetCurrentUserInfo(UserInfo userInfo)
        {
            throw new System.NotImplementedException();
        }

        public void SendAllClient(string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public void SendAllClientExceptSelf(string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public void SendToSelf(string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public void SendToSingle(string toConnectionID, string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public void SendToMany(IList<string> connectionIds, string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public void SendAllClientExcept(string name, string message, params string[] connectionIds)
        {
            throw new System.NotImplementedException();
        }

        public void SendToGroup(string groupName, string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public void SendToGroupExcept(string groupName, string name, string message, params string[] connectionIds)
        {
            throw new System.NotImplementedException();
        }

        public void SendToGroups(IList<string> groupNameList, string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public void SendToOtherGroups(string groupName, string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public void SendToOtherManyGroups(IList<string> groupNameList, string name, string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
