
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
    public class SendHubSync : HubClient.SendHubSync
    {
        private readonly IHubLogger _slabLogger;
        private readonly IHubContext _hubContext;

        public SendHubSync(IHubLogger slabLogger)
        {
            _slabLogger = slabLogger;
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<HubSync>(); 
        }


        public override void Heartbeat()
        {
            throw new System.NotImplementedException();
        }

        public override void RefreshAllClientList()
        {
            throw new System.NotImplementedException();
        }

        public override void GetCurrentUserInfo()
        {
            throw new System.NotImplementedException();
        }

        public override void SendAllClient(string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public override void SendAllClientExceptSelf(string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public override void SendToSelf(string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public override void SendToSingle(string toConnectionID, string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public override void SendToMany(IList<string> connectionIds, string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public override void SendAllClientExcept(string name, string message, params string[] connectionIds)
        {
            throw new System.NotImplementedException();
        }

        public override void SendToGroup(string groupName, string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public override void SendToGroupExcept(string groupName, string name, string message, params string[] connectionIds)
        {
            throw new System.NotImplementedException();
        }

        public override void SendToGroups(IList<string> groupNameList, string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public override void SendToOtherGroups(string groupName, string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public override void SendToOtherManyGroups(IList<string> groupNameList, string name, string message)
        {
            throw new System.NotImplementedException();
        }

        public override void SubscribeGroup(string groupName)
        {
            throw new System.NotImplementedException();
        }

        public override void UnsubscribeGroup(string groupName)
        {
            throw new System.NotImplementedException();
        }

        public override void Subscribe(string name)
        {
            throw new System.NotImplementedException();
        }

        public override void Unsubscribe(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
