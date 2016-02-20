using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using SignalR.SLAB.Events;

namespace SignalR.SLAB.Loggers
{
    public class HubServerLogger 
    {
        public void RegisterLogger(Dictionary<int, Action<string>> exectueLogDict)
        {
            exectueLogDict.Add(HubServerType.HubServerCritical, Critical);
            exectueLogDict.Add(HubServerType.HubServerError, Error);
            exectueLogDict.Add(HubServerType.HubServerInformational, Informational);
            exectueLogDict.Add(HubServerType.HubServerLogAlways, LogAlways);
            exectueLogDict.Add(HubServerType.HubServerVerbose, Verbose);
            exectueLogDict.Add(HubServerType.HubServerWarning, Warning);
        }

        public void Critical(string message)
        {
            HubServerEvents.Log.Critical(message);
        }

        public void Error(string message)
        {
            HubServerEvents.Log.Error(message);
        }

        public void Informational(string message)
        {
            HubServerEvents.Log.Informational(message);
        }

        public void LogAlways(string message)
        {
            HubServerEvents.Log.LogAlways(message);
        }

        public void Verbose(string message)
        {
            HubServerEvents.Log.Verbose(message);
        }

        public void Warning(string message)
        {
            HubServerEvents.Log.Warning(message);
        }
    }
}