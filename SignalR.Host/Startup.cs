using System;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Owin;
using SignalR.Host.Service;
using SignalR.Host.Unity;
using SignalR.Host.Utils;
using SignalR.SLAB.Base;
using SignalR.SLAB.Events;

[assembly: OwinStartup(typeof(SignalR.Host.Startup))]

namespace SignalR.Host
{

    public class Startup
    {

        public static void Start()
        {
            #region SLAB
            var ObservableEventListener = new ObservableEventListener();
            ObservableEventListener.EnableEvents(ControllerEvents.Log, EventLevel.LogAlways, Keywords.All);
            ObservableEventListener.EnableEvents(GlobalEvents.Log, EventLevel.LogAlways, Keywords.All);
            ObservableEventListener.EnableEvents(HubServerEvents.Log, EventLevel.LogAlways, Keywords.All);
            var formatter = new PrefixEventTextFormatter("-----------", null, "# ", @"yyyy-MM-dd hh\:mm\:ss\.fff");
            ObservableEventListener.LogToConsole(formatter);
            #endregion

            //Hub孵化器
            GlobalHost.DependencyResolver.Register(typeof (IHubActivator),
                () => new UnityHubActivator(UnityConfiguration.GetConfiguredContainer()));
            GlobalHost.HubPipeline.AddModule(new LoggingPipelineModule());
            GlobalHost.HubPipeline.AddModule(new ErrorHandlingPipelineModule());
            var url = ConfigUtils.GetConfig<string>("HubServiceUrl");
            using (WebApp.Start(url))
            {
                Console.WriteLine("Server running on {0}", url);
                while (true)
                {
                    var key = Console.ReadLine();
                    if (key != null && key.ToUpper() == "Q")
                    {
                        break;
                    }
                }
                Console.ReadLine();
            }

        }

        public void Configuration(IAppBuilder app)
        {
            // Branch the pipeline here for requests that start with "/signalr"
            app.Map("/signalr", map =>
            {


                // Setup the CORS middleware to run before SignalR.
                // By default this will allow all origins. You can 
                // configure the set of origins and/or http verbs by
                // providing a cors options with a different policy.
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    // You can enable JSONP by uncommenting line below.
                    // JSONP requests are insecure but some older browsers (and some
                    // versions of IE) require JSONP to work cross domain
                    // EnableJSONP = true
                };
                // Run the SignalR pipeline. We're not using MapSignalR
                // since this branch already runs under the "/signalr"
                // path.

                hubConfiguration.EnableDetailedErrors = true;
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}
