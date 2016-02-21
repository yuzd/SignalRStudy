using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using SignalR.Host.Entity;
using SignalR.HubClient;
using SignalR.HubClient.Entity;
using SignalR.SLAB;
using SignalR.SLAB.Services;

namespace SignalR.Host.Hubs
{
    /// <summary>
    /// 客户端 建立连接实例
    /// </summary>
    public class HubSync : Hub
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

        /// <summary>
        /// 心跳
        /// </summary>
        public void Heartbeat()
        {
            _slabLogger.Log(HubServerType.HubServerVerbose, "HubSync Sending Heartbeat");
            Clients.Caller.Heartbeat();
        }


        /// <summary>
        /// 广播信息到所有clinet
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void SendAllClient(string name, string message)
        {
            Clients.All.SendMessage(name, message);
        }

        /// <summary>
        /// 所有人除了触发者
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void SendAllClientExceptSelf(string name, string message)
        {
            Clients.Others.SendMessage(name, message);
        }

        /// <summary>
        /// 触发者
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void SendToSelf(string name, string message)
        {
            Clients.Caller.SendMessage(name, message);
        }

        /// <summary>
        /// 发送信息给单一client的人 
        /// </summary>
        /// <param name="toConnectionID"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void SendToSingle(string toConnectionID, string name, string message)
        {
            Clients.Client(toConnectionID).SendMessage(name, message);
            Clients.Client(Context.ConnectionId).SendMessage( name, message);
        }

        /// <summary>
        /// 发送信息给多个Client
        /// </summary>
        /// <param name="connectionIds"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void SendToMany(IList<string> connectionIds, string name, string message)
        {
            Clients.Clients(connectionIds).SendMessage(name, message);
        }

        /// <summary>
        /// 除了个别的广播信息到所有clinet
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="connectionIds"></param>
        public void SendAllClientExcept(string name, string message, params string[] connectionIds)
        {
            Clients.AllExcept(connectionIds).SendMessage(name, message);
        }

        /// <summary>
        /// 发送信息给一组的人 
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void SendToGroup(string groupName, string name, string message)
        {
            Clients.Group(groupName).SendMessage(name, message);
        }

        /// <summary>
        /// 一组可见，用于小组聊天，除了指定的connectionId1，connectionId2
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="connectionIds"></param>
        public void SendToGroupExcept(string groupName, string name, string message, params string[] connectionIds)
        {
            Clients.Group(groupName, connectionIds).SendMessage(name, message);
        }

        /// <summary>
        /// 发送信息给多个组的人
        /// </summary>
        /// <param name="groupNameList"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void SendToGroups(IList<string> groupNameList, string name, string message)
        {
            Clients.Groups(groupNameList).SendMessage(name, message);
        }

        /// <summary>
        /// 除了当前组其他可见
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void SendToOtherGroups(string groupName, string name, string message)
        {
            Clients.OthersInGroup(groupName).SendMessage(name, message);
        }


        /// <summary>
        /// 除了当前的多个组其他可见
        /// </summary>
        /// <param name="groupNameList"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void SendToOtherManyGroups(IList<string> groupNameList, string name, string message)
        {
            Clients.OthersInGroups(groupNameList).SendMessage(name, message);
        }


        /// <summary>
        /// 获取所有用户
        /// </summary>
        public void RefreshAllClientList()
        {
            Clients.All.GetAllClientList(GlobalData.userInfoList);
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="connectionID"></param>
        public void GetCurrentUserInfo(string connectionID)
        {
            UserInfo userInfo;
            GlobalData.userInfoList.TryGetValue(connectionID, out userInfo);
            Clients.Caller.GetUserInfo(userInfo);
        }

        /// <summary>
        /// 加入组
        /// </summary>
        /// <param name="groupName"></param>
        public void SubscribeGroup(string groupName)
        {
            Groups.Add(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// 离开组
        /// </summary>
        /// <param name="groupName"></param>
        public void UnsubscribeGroup(string groupName)
        {
            Groups.Remove(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// 加入会话
        /// </summary>
        /// <param name="name"></param>
        public void Subscribe(string name)
        {
            //TODO 容错
            if (!GlobalData.userInfoList.ContainsKey(name))
            {
                GlobalData.userInfoList.TryAdd(name, new UserInfo {ConnectionId = Context.ConnectionId, UserName = name});
                ////Clients 通知所有用户刷新用户列表
                RefreshAllClientList();
                //系统通知，新用户进入聊天室 
                SendAllClientExceptSelf(name,"加入聊天");
            }
        }

        /// <summary>
        /// 离开会话
        /// </summary>
        /// <param name="name"></param>
        public void Unsubscribe(string name)
        {
            if (GlobalData.userInfoList.ContainsKey(name))
            {
                UserInfo userInfo = new UserInfo();
                GlobalData.userInfoList.TryRemove(name,out userInfo);
                ////Clients 通知所有用户刷新用户列表
                RefreshAllClientList();
                //系统通知，用户离开聊天室 
                SendAllClientExceptSelf(name, "离开聊天");
            }
        }

        #endregion

       
        public override Task OnConnected()
        {
            _slabLogger.Log(HubServerType.HubServerVerbose, "HubSync OnConnected" + Context.ConnectionId);
            return (base.OnConnected());
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _slabLogger.Log(HubServerType.HubServerVerbose, "HubSync OnDisconnected" + Context.ConnectionId);
            return (base.OnDisconnected(stopCalled));
        }

        public override Task OnReconnected()
        {
            _slabLogger.Log(HubServerType.HubServerVerbose, "HubSync OnReconnected" + Context.ConnectionId);
            return (base.OnReconnected());
        }
    }
}
