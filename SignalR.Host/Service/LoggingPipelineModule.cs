﻿using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Practices.Unity;
using SignalR.Host.Unity;
using SignalR.SLAB;
using SignalR.SLAB.Services;

namespace SignalR.Host.Service
{
    public class LoggingPipelineModule : HubPipelineModule 
    {
        private readonly IHubLogger _slabLogger;

        public LoggingPipelineModule()
        {
            _slabLogger = UnityConfiguration.GetConfiguredContainer().Resolve<IHubLogger>();
        }

        protected override bool OnBeforeIncoming(IHubIncomingInvokerContext context)
        {
            _slabLogger.Log(HubServerType.HubServerVerbose, "=> Invoking " + context.MethodDescriptor.Name + " on hub " + context.MethodDescriptor.Hub.Name);
            return base.OnBeforeIncoming(context);
        }
        protected override bool OnBeforeOutgoing(IHubOutgoingInvokerContext context)
        {
            _slabLogger.Log(HubServerType.HubServerVerbose, "<= Invoking " + context.Invocation.Method + " on client hub " + context.Invocation.Hub);
            return base.OnBeforeOutgoing(context);
        } 
    }
}
