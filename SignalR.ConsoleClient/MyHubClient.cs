using System;
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

            _myHubProxy.On<string, string>("AddMessage", Recieve_AddMessage);
            _myHubProxy.On("Heartbeat", Recieve_Heartbeat);
            _myHubProxy.On<HelloModel>("SendHelloObject", Recieve_SendHelloObject);

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

        public void SendHelloObject(HelloModel hello)
        {
            _myHubProxy.Invoke<HelloModel>("SendHelloObject", hello).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null)
                        SlabClientLogger.Log(HubClientType.HubClientError, "There was an error opening the connection:" + task.Exception.GetBaseException());
                }

            }).Wait();
            SlabClientLogger.Log(HubClientType.HubClientInformational, "Client sendHelloObject sent to server");
        }

        public void Recieve_AddMessage(string name, string message)
        {
            SlabClientLogger.Log(HubClientType.HubClientInformational, "Recieved addMessage: " + name + ": " + message);
        }

        public void Recieve_Heartbeat()
        {
            SlabClientLogger.Log(HubClientType.HubClientInformational, "Recieved heartbeat ");
        }

        public void Recieve_SendHelloObject(HelloModel hello)
        {
            SlabClientLogger.Log(HubClientType.HubClientInformational, "Recieved sendHelloObject " + hello.Molly + ", " + hello.Age);
        }
    }
}
