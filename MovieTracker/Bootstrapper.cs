using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MovieTracker.Data;
using MovieTracker.Services;
using Unity.Mvc3;

namespace MovieTracker
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            IUnityContainer container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IRepository<Movie>, MovieRepository>()
                    .RegisterType<IRepository<Genre>, GenreRepository>()
                    .RegisterType<IMembershipService, AspNetMembershipService>();


            container.RegisterControllers();

            return container;
        }
    }
}