using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Practices.Unity;
using SignalR.Host.Unity;
using SignalR.SLAB;
using SignalR.SLAB.Services;

namespace SignalR.Host.Service
{
    public class ErrorHandlingPipelineModule : HubPipelineModule
    {
        private readonly IHubLogger _slabLogger;

        public ErrorHandlingPipelineModule()
        {
            _slabLogger = UnityConfiguration.GetConfiguredContainer().Resolve<IHubLogger>();
        }

        protected override void OnIncomingError(ExceptionContext ex, IHubIncomingInvokerContext context)
        {
            _slabLogger.Log(HubType.HubServerVerbose, "=> Exception " + ex.Error + " " + ex.Result);
            if (ex.Error.InnerException != null)
            {
                _slabLogger.Log(HubType.HubServerVerbose, "=> Inner Exception " + ex.Error.InnerException.Message);
            }
            base.OnIncomingError(ex, context);
        }
    }
}
