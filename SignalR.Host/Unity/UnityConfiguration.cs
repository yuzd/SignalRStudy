using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Practices.Unity;
using SignalR.Host.Hubs;
using SignalR.HubClient;
using SignalR.SLAB.Services;

namespace SignalR.Host.Unity
{
    public class UnityConfiguration
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion
     

        public static void RegisterTypes(IUnityContainer containerObj)
        {

            containerObj.RegisterType<IHubLogger, HubServerLogger>(new ContainerControlledLifetimeManager());
            containerObj.RegisterType<SendHubSync, Service.SendHubSync>(new ContainerControlledLifetimeManager());

            // Hub must be transient see signalr docs
            containerObj.RegisterType<HubSync, HubSync>(new TransientLifetimeManager());
            containerObj.RegisterType<Hub, Hub>(new TransientLifetimeManager());
            containerObj.RegisterType<IHubActivator, UnityHubActivator>(new ContainerControlledLifetimeManager());
        }
    }
}
