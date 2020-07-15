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

            container.RegisterType<IAuthenticationManager>(
       new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
            new InjectionConstructor(typeof(ApplicationDbContext)));
            var entity = new GreenLandProjectEntities();
           
            container.RegisterType<IRoomReposity, RoomRepository>(new InjectionConstructor(entity));
            container.RegisterType<IReservationRepository, ReservationRepository>(new InjectionConstructor(entity));
            container.RegisterType<ICategoriesRepository, CategoryRepository>(new InjectionConstructor(entity));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

        }
    }
}