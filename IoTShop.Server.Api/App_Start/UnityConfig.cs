using IoTShop.Common.Logic.Context;
using IoTShop.Common.Logic.Models;
using IoTShop.Common.Logic.Services;
using IoTShop.Server.Api.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using System.Data.Entity;
using System.Web.Http;
using Unity.WebApi;

namespace IoTShop.Server.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType<IOSService, OSService>();
            container.RegisterType<IFrameworkService, FrameworkService>();
            //container.RegisterType<IDeviceService, DeviceService>();
            container.RegisterType<IDeviceService, ApiDeviceService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}