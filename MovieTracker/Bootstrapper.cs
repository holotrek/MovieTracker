using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using MovieTracker.Data;
using MovieTracker.Services;

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

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();            

            container.RegisterType<IRepository<Movie>, MovieRepository>()
                    .RegisterType<IRepository<Genre>, GenreRepository>()
                    .RegisterType<IMembershipService, AspNetMembershipService>();

            return container;
        }
    }
}
