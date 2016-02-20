using System.Diagnostics.Tracing;

namespace SignalR.SLAB.Events
{
    [EventSource(Name = "HubServerEvents")]
    public class HubServerEvents : EventSource
    {
        public static readonly HubServerEvents Log = new HubServerEvents();

        [Event(HubServerType.HubServerCritical, Message = "HubServerLogger Critical: {0}", Level = EventLevel.Critical)]
        public void Critical(string message)
        {
            if (IsEnabled()) WriteEvent(HubServerType.HubServerCritical, message);
        }

        [Event(HubServerType.HubServerError, Message = "HubServerLogger Error {0}", Level = EventLevel.Error)]
        public void Error(string message)
        {
            if (IsEnabled()) WriteEvent(HubServerType.HubServerError, message);
        }

        [Event(HubServerType.HubServerInformational, Message = "HubServerLogger Informational {0}", Level = EventLevel.Informational)]
        public void Informational(string message)
        {
            if (IsEnabled()) WriteEvent(HubServerType.HubServerInformational, message);
        }

        [Event(HubServerType.HubServerLogAlways, Message = "HubServerLogger LogAlways {0}", Level = EventLevel.LogAlways)]
        public void LogAlways(string message)
        {
            if (IsEnabled()) WriteEvent(HubServerType.HubServerLogAlways, message);
        }

        [Event(HubServerType.HubServerVerbose, Message = "HubServerLogger Verbose {0}", Level = EventLevel.Verbose)]
        public void Verbose(string message)
        {
            if (IsEnabled()) WriteEvent(HubServerType.HubServerVerbose, message);
        }

        [Event(HubServerType.HubServerWarning, Message = "HubServerLogger Warning {0}", Level = EventLevel.Warning)]
        public void Warning(string message)
        {
            if (IsEnabled()) WriteEvent(HubServerType.HubServerWarning, message);
        }
    }
}