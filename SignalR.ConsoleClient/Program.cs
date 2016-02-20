﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using SignalR.HubClient.Entity;
using SignalR.SLAB.Base;
using SignalR.SLAB.Events;

namespace SignalR.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            #region SLAB
            var ObservableEventListener = new ObservableEventListener();
            ObservableEventListener.EnableEvents(ControllerEvents.Log, EventLevel.LogAlways, Keywords.All);
            ObservableEventListener.EnableEvents(GlobalEvents.Log, EventLevel.LogAlways, Keywords.All);
            ObservableEventListener.EnableEvents(HubClientEvents.Log, EventLevel.LogAlways, Keywords.All);
            var formatter = new PrefixEventTextFormatter("-----------", null, "# ", @"yyyy-MM-dd hh\:mm\:ss\.fff");
            ObservableEventListener.LogToConsole(formatter);
            #endregion


            var myHubClient = new MyHubClient();
            while (true)
            {
                string key = Console.ReadLine();
                if (key.ToUpper() == "A")
                {
                    if (myHubClient.State == ConnectionState.Connected)
                    {
                        myHubClient.AddMessage("client", "hello all");
                    }
                    else
                    {
                        HubClientEvents.Log.Warning("Can't send message, connectionState= " + myHubClient.State);
                    }

                }
                if (key.ToUpper() == "H")
                {
                    if (myHubClient.State == ConnectionState.Connected)
                    {
                        myHubClient.Heartbeat();
                    }
                    else
                    {
                        HubClientEvents.Log.Warning("Can't send message, connectionState= " + myHubClient.State);
                    }

                }
                if (key.ToUpper() == "O")
                {
                    if (myHubClient.State == ConnectionState.Connected)
                    {
                        
                    }
                    else
                    {
                        HubClientEvents.Log.Warning("Can't send message, connectionState= " + myHubClient.State);
                    }


                }
                if (key.ToUpper() == "C")
                {
                    myHubClient.CloseHub();
                    HubClientEvents.Log.Informational("Closed Hub");
                }
                if (key.ToUpper() == "S")//重新开始
                {
                    myHubClient.StartHub();
                    HubClientEvents.Log.Informational("Started the Hub");
                }
                if (key.ToUpper() == "Q")
                {
                    break;
                }
            }
        }
    }
}
