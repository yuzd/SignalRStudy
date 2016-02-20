using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using SignalR.HubClient;
using SignalR.HubClient.Entity;
using SignalR.SLAB;

namespace SignalR.ConsoleClient
{
    public class MyHubClient : BaseHubClient, ISendHubSync, IRecieveHubSync
    {
        public static ConcurrentDictionary<string, UserInfo> userInfoList = new ConcurrentDictionary<string, UserInfo>();
        public MyHubClient()
        {
            Init();
        }

        public new void Init()
        {
            HubConnectionUrl = "http://localhost:8089/";
            HubProxyName = "Hubsync";
            HubTraceLevel = TraceLevels.None;
            HubTraceWriter = Console.Out;

            base.Init();

            #region Recieve事件注册
            _myHubProxy.On<string, string>("SendMessage", Recieve_SendMessage);
            _myHubProxy.On("Heartbeat", Recieve_Heartbeat);
            _myHubProxy.On<ConcurrentDictionary<string, UserInfo>>("RefreshAllClientList", Recieve_RefreshAllClientList);
            _myHubProxy.On<UserInfo>("GetCurrentUserInfo", Recieve_GetCurrentUserInfo); 
            #endregion

            StartHubInternal();
        }
        public override void StartHub()
        {
            _hubConnection.Dispose();
            Init();
        }

        public void AddMessage(string name, string message)
        {
            _myHubProxy.Invoke("addMessage", "client message", " sent from console client").ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
            SlabClientLogger.Log(HubClientType.HubClientInformational, "Client Sending addMessage to server");
        }

        void ISendHubSync.SendMessage(string name, string message)
        {
            throw new NotImplementedException();
        }

      

        public void Heartbeat()
        {
            _myHubProxy.Invoke("Heartbeat").ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
            SlabClientLogger.Log(HubClientType.HubClientInformational, "Client heartbeat sent to server");
        }

       

        void ISendHubSync.RefreshAllClientList(ConcurrentDictionary<string, UserInfo> _userInfoList)
        {
            throw new NotImplementedException();
        }

        void ISendHubSync.GetCurrentUserInfo(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public void SendAllClient(string name, string message)
        {
            throw new NotImplementedException();
        }

        public void SendAllClientExceptSelf(string name, string message)
        {
            throw new NotImplementedException();
        }

        public void SendToSelf(string name, string message)
        {
            throw new NotImplementedException();
        }

        public void SendToSingle(string toConnectionID, string name, string message)
        {
            throw new NotImplementedException();
        }

        public void SendToMany(IList<string> connectionIds, string name, string message)
        {
            throw new NotImplementedException();
        }

        public void SendAllClientExcept(string name, string message, params string[] connectionIds)
        {
            throw new NotImplementedException();
        }

        public void SendToGroup(string groupName, string name, string message)
        {
            throw new NotImplementedException();
        }

        public void SendToGroupExcept(string groupName, string name, string message, params string[] connectionIds)
        {
            throw new NotImplementedException();
        }

        public void SendToGroups(IList<string> groupNameList, string name, string message)
        {
            throw new NotImplementedException();
        }

        public void SendToOtherGroups(string groupName, string name, string message)
        {
            throw new NotImplementedException();
        }

        public void SendToOtherManyGroups(IList<string> groupNameList, string name, string message)
        {
            throw new NotImplementedException();
        }

  

     

        public void Recieve_Heartbeat()
        {
            SlabClientLogger.Log(HubClientType.HubClientInformational, "Recieved heartbeat ");
        }

        public void Recieve_SendMessage(string name, string message)
        {
            throw new NotImplementedException();
        }

        public void Recieve_RefreshAllClientList(ConcurrentDictionary<string, UserInfo> _userInfoList)
        {
            throw new NotImplementedException();
        }

        public void Recieve_GetCurrentUserInfo(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
