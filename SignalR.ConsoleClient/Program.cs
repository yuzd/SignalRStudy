using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using SignalR.HubClient.Entity;
using SignalR.SLAB.Events;

namespace SignalR.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var myHubClient = new MyHubClient();
            while (true)
            {
                string key = Console.ReadLine();
                if (key.ToUpper() == "A")
                {
                    if (myHubClient.State == ConnectionState.Connected)
                    {
                        myHubClient.AddMessage("damien client", "hello all");
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
                        var hello = new HelloModel { Age = 10, Molly = "clientMessage" };
                        myHubClient.SendHelloObject(hello);
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
