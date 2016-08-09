using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Data.Entity;
using IoTShop.Common.Logic.Context;
using IoTShop.Common.Logic.Models;
using IoTShop.Server.WebApp.Controllers;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using IoTShop.Common.Logic.Services;

namespace IoTShop.Server.WebApp
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
            container.RegisterType<IAuthenticationManager>();

            container.RegisterType<IOSService, OSService>();
            container.RegisterType<IFrameworkService, FrameworkService>();
            container.RegisterType<IDeviceService, DeviceService>();
            container.RegisterType<IOrderService, OrderService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}