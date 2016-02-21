using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.SLAB.Events
{

    [EventSource(Name = "HubClientEvents")]
    public class HubClientEvents : EventSource
    {
        public static readonly HubClientEvents Log = new HubClientEvents();

        [Event(HubClientType.HubClientCritical, Message = "{0}", Level = EventLevel.Critical)]
        public void Critical(string message)
        {
            if (IsEnabled()) WriteEvent(HubClientType.HubClientCritical, message);
        }

        [Event(HubClientType.HubClientError, Message = "{0}", Level = EventLevel.Error)]
        public void Error(string message)
        {
            if (IsEnabled()) WriteEvent(HubClientType.HubClientError, message);
        }

        [Event(HubClientType.HubClientInformational, Message = "{0}", Level = EventLevel.Informational)]
        public void Informational(string message)
        {
            if (IsEnabled()) WriteEvent(HubClientType.HubClientInformational, message);
        }

        [Event(HubClientType.HubClientLogAlways, Message = "{0}", Level = EventLevel.LogAlways)]
        public void LogAlways(string message)
        {
            if (IsEnabled()) WriteEvent(HubClientType.HubClientLogAlways, message);
        }

        [Event(HubClientType.HubClientVerbose, Message = "{0}", Level = EventLevel.Verbose)]
        public void Verbose(string message)
        {
            if (IsEnabled()) WriteEvent(HubClientType.HubClientVerbose, message);
        }

        [Event(HubClientType.HubClientWarning, Message = "{0}", Level = EventLevel.Warning)]
        public void Warning(string message)
        {
            if (IsEnabled()) WriteEvent(HubClientType.HubClientWarning, message);
        }


    }
}
