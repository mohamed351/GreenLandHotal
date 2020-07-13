using IdentityLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Repositories;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Testing.App_Start;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using EntityDataLayer;

namespace GreenLandHotal
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            //    container.RegisterType<DbContext, MyDbContext>(new PerRequestLifetimeManager());
            //    container.RegisterType<IUserStore<ApplicationUser>,
            //        UserStore<ApplicationUser>>(new PerRequestLifetimeManager());
            //    container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            //    container.RegisterType<IAuthenticationManager>(
            //        new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication))
            //container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());

            //container.RegisterType<DbContext, ApplicationDbContext>();
            //container.RegisterType<IUser, ApplicationUser>();
            //container.RegisterType<ApplicationUserManager>();
            //container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new UserStore<ApplicationUser>(new ApplicationDbContext));
            //container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IAuthenticationManager>(
       new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
            new InjectionConstructor(typeof(ApplicationDbContext)));
            container.RegisterType<IRoomReposity, RoomRepository>(new InjectionConstructor(new GreenLandProjectEntities()));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

        }
    }
}