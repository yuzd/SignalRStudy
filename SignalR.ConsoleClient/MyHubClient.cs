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


        public void SendMessage(string name, string message)
        {
            throw new NotImplementedException();
        }

        public void GetAllClientList(ConcurrentDictionary<string, UserInfo> _userInfoList)
        {
            throw new NotImplementedException();
        }

        public void GetUserInfo(UserInfo userInfo)
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
        }

        public void RefreshAllClientList()
        {
            _myHubProxy.Invoke("RefreshAllClientList").ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
        }

        public void GetCurrentUserInfo()
        {
            _myHubProxy.Invoke("GetCurrentUserInfo").ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
        }


        public void SendAllClient(string name, string message)
        {
            _myHubProxy.Invoke("SendAllClient",name,message).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
        }

        public void SendAllClientExceptSelf(string name, string message)
        {
            _myHubProxy.Invoke("SendAllClientExceptSelf", name, message).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
        }

        public void SendToSelf(string name, string message)
        {
            _myHubProxy.Invoke("SendToSelf", name, message).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
        }

        public void SendToSingle(string toConnectionID, string name, string message)
        {
            _myHubProxy.Invoke("SendToSingle", toConnectionID,name, message).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
        }

        public void SendToMany(IList<string> connectionIds, string name, string message)
        {
            _myHubProxy.Invoke("SendToMany", connectionIds, name, message).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
        }

        public void SendAllClientExcept(string name, string message, params string[] connectionIds)
        {
            _myHubProxy.Invoke("SendAllClientExcept",  name, message, connectionIds).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
        }

        public void SendToGroup(string groupName, string name, string message)
        {
            _myHubProxy.Invoke("SendToGroup", groupName,name, message).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
        }

        public void SendToGroupExcept(string groupName, string name, string message, params string[] connectionIds)
        {
            _myHubProxy.Invoke("SendToGroupExcept", groupName, name, message, connectionIds).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
        }

        public void SendToGroups(IList<string> groupNameList, string name, string message)
        {
            _myHubProxy.Invoke("SendToGroups", groupNameList, name, message).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
        }

        public void SendToOtherGroups(string groupName, string name, string message)
        {
            _myHubProxy.Invoke("SendToOtherGroups", groupName, name, message).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
        }

        public void SendToOtherManyGroups(IList<string> groupNameList, string name, string message)
        {
            _myHubProxy.Invoke("SendToOtherManyGroups", groupNameList, name, message).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
        }

    


        public void Recieve_Heartbeat()
        {
            SlabClientLogger.Log(HubClientType.HubClientInformational, "Recieved heartbeat Success");
        }

        public void Recieve_SendMessage(string name, string message)
        {
            SlabClientLogger.Log(HubClientType.HubClientInformational, $"【{name}】:{message}");
        }

        public void Recieve_RefreshAllClientList(ConcurrentDictionary<string, UserInfo> _userInfoList)
        {
            userInfoList = _userInfoList;
            if (userInfoList!=null && userInfoList.Count>0)
            {
                foreach (var keyValuePair in userInfoList)
                {
                    SlabClientLogger.Log(HubClientType.HubClientInformational, $"【{keyValuePair.Value.ConnectionId}】:{keyValuePair.Value.UserName}");
                }
            }
        }

        public void Recieve_GetCurrentUserInfo(UserInfo userInfo)
        {
            SlabClientLogger.Log(HubClientType.HubClientInformational, $"【{userInfo.ConnectionId}】:{userInfo.UserName}");
        }
    }
}
