using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalR.SLAB.Events;

namespace SignalR.SLAB.Loggers
{
    public class HubClientLogger
    {

        public void RegisterLogger(Dictionary<int, Action<string>> exectueLogDict)
        {
            exectueLogDict.Add(HubClientType.HubClientCritical, Critical);
            exectueLogDict.Add(HubClientType.HubClientError, Error);
            exectueLogDict.Add(HubClientType.HubClientInformational, Informational);
            exectueLogDict.Add(HubClientType.HubClientLogAlways, LogAlways);
            exectueLogDict.Add(HubClientType.HubClientVerbose, Verbose);
            exectueLogDict.Add(HubClientType.HubClientWarning, Warning);
        }

        public void Critical(string message)
        {
            HubClientEvents.Log.Critical(message);
        }

        public void Error(string message)
        {
            HubClientEvents.Log.Error(message);
        }

        public void Informational(string message)
        {
            HubClientEvents.Log.Informational(message);
        }

        public void LogAlways(string message)
        {
            HubClientEvents.Log.LogAlways(message);
        }

        public void Verbose(string message)
        {
            HubClientEvents.Log.Verbose(message);
        }

        public void Warning(string message)
        {
            HubClientEvents.Log.Warning(message);
        }
    }
}
