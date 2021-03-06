﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalR.HubClient.Entity;

namespace SignalR.HubClient
{
    public abstract  class SendHubSync
    {
        #region 需要接收

     

        /// <summary>
        /// 心跳
        /// </summary>
        public abstract void Heartbeat();


        /// <summary>
        /// 获取所有用户
        /// </summary>
        public abstract void RefreshAllClientList();

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public abstract void GetCurrentUserInfo();


        #endregion

        #region 发送的多种方式

        /// <summary>
        /// 广播信息到所有clinet
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public abstract void SendAllClient(string name, string message);

        /// <summary>
        /// 所有人除了触发者
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public abstract  void SendAllClientExceptSelf(string name, string message);

        /// <summary>
        /// 触发者
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public abstract void SendToSelf(string name, string message);

        /// <summary>
        /// 发送信息给单一client的人 
        /// </summary>
        /// <param name="toConnectionID"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public abstract void SendToSingle(string toConnectionID, string name, string message);

        /// <summary>
        /// 发送信息给多个Client
        /// </summary>
        /// <param name="connectionIds"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public abstract void SendToMany(IList<string> connectionIds, string name, string message);

        /// <summary>
        /// 除了个别的广播信息到所有clinet
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="connectionIds"></param>
        public abstract void SendAllClientExcept(string name, string message, params string[] connectionIds);

        /// <summary>
        /// 发送信息给一组的人 
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public abstract void SendToGroup(string groupName, string name, string message);

        /// <summary>
        /// 一组可见，用于小组聊天，除了指定的connectionId1，connectionId2
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="connectionIds"></param>
        public abstract void SendToGroupExcept(string groupName, string name, string message, params string[] connectionIds);

        /// <summary>
        /// 发送信息给多个组的人
        /// </summary>
        /// <param name="groupNameList"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public abstract void SendToGroups(IList<string> groupNameList, string name, string message);

        /// <summary>
        /// 除了当前组其他可见
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public abstract void SendToOtherGroups(string groupName, string name, string message);

        /// <summary>
        /// 除了当前的多个组其他可见
        /// </summary>
        /// <param name="groupNameList"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public abstract void SendToOtherManyGroups(IList<string> groupNameList, string name, string message);

        /// <summary>
        /// 加入组
        /// </summary>
        /// <param name="groupName"></param>
        public abstract void SubscribeGroup(string groupName);

        /// <summary>
        /// 离开组
        /// </summary>
        /// <param name="groupName"></param>
        public abstract void UnsubscribeGroup(string groupName);

        /// <summary>
        /// 加入会话
        /// </summary>
        /// <param name="name"></param>
        public abstract void Subscribe(string name);

        /// <summary>
        /// 离开会话
        /// </summary>
        /// <param name="name"></param>
        public abstract void Unsubscribe(string name);

        #endregion

    }
}
