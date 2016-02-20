using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using SignalR.SLAB;
using SignalR.SLAB.Services;

namespace SignalR.HubClient
{
    public abstract class BaseHubClient
    {
        protected HubConnection _hubConnection;
        protected IHubProxy _myHubProxy;

        public string HubConnectionUrl { get; set; }
        public string HubProxyName { get; set; }
        public TraceLevels HubTraceLevel { get; set; }
        public System.IO.TextWriter HubTraceWriter { get; set; }
        public IHubLogger SlabClientLogger { get; set; }

        public ConnectionState State
        {
            get { return _hubConnection.State; }
        }

        protected void Init()
        {
            _hubConnection = new HubConnection(HubConnectionUrl)
            {
                TraceLevel = HubTraceLevel,
                TraceWriter = HubTraceWriter
            };
            SlabClientLogger = new HubClientLogger();
            _myHubProxy = _hubConnection.CreateHubProxy(HubProxyName);

            _hubConnection.Received += _hubConnection_Received;
            _hubConnection.Reconnected += _hubConnection_Reconnected;
            _hubConnection.Reconnecting += _hubConnection_Reconnecting;
            _hubConnection.StateChanged += _hubConnection_StateChanged;
            _hubConnection.Error += _hubConnection_Error;
            _hubConnection.ConnectionSlow += _hubConnection_ConnectionSlow;
            _hubConnection.Closed += _hubConnection_Closed;

        }

        public void CloseHub()
        {
            _hubConnection.Stop();
            _hubConnection.Dispose();
        }

        protected void StartHubInternal()
        {
            try
            {
                _hubConnection.Start().Wait();
            }
            catch (Exception ex)
            {
                SlabClientLogger.Log(HubClientType.HubClientError, ex.Message + " " + ex.StackTrace);
            }

        }

        public abstract void StartHub();

        void _hubConnection_Closed()
        {
            SlabClientLogger.Log(HubClientType.HubClientInformational,
                "_hubConnection_Closed New State:" + _hubConnection.State + " " + _hubConnection.ConnectionId);
        }

        void _hubConnection_ConnectionSlow()
        {
            SlabClientLogger.Log(HubClientType.HubClientInformational,
               "_hubConnection_ConnectionSlow New State:" + _hubConnection.State + " " + _hubConnection.ConnectionId);
        }

        void _hubConnection_Error(Exception obj)
        {
            SlabClientLogger.Log(HubClientType.HubClientError,
             "_hubConnection_Error New State:" + _hubConnection.State + " " + _hubConnection.ConnectionId);
        }

        void _hubConnection_StateChanged(StateChange obj)
        {
            SlabClientLogger.Log(HubClientType.HubClientInformational,
            "_hubConnection_StateChanged New State:" + _hubConnection.State + " " + _hubConnection.ConnectionId);
        }

        void _hubConnection_Reconnecting()
        {
            SlabClientLogger.Log(HubClientType.HubClientInformational,
            "_hubConnection_Reconnecting New State:" + _hubConnection.State + " " + _hubConnection.ConnectionId);
        }

        void _hubConnection_Reconnected()
        {
            SlabClientLogger.Log(HubClientType.HubClientInformational,
            "_hubConnection_Reconnected New State:" + _hubConnection.State + " " + _hubConnection.ConnectionId);
        }

        void _hubConnection_Received(string obj)
        {
            SlabClientLogger.Log(HubClientType.HubClientInformational,
            "_hubConnection_Received New State:" + _hubConnection.State + " " + _hubConnection.ConnectionId);
        }
    }
}
