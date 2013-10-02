using System.Web.Mvc;

namespace MovieTracker.Tests.Unit.Controllers
{
    public static class RedirectToRouteResultExtensions
    {
        public static string Action(this RedirectToRouteResult redirectToRouteResult)
        {
            return redirectToRouteResult.RouteValues["action"].ToString();
        }
        public static string Controller(this RedirectToRouteResult redirectToRouteResult)
        {
            return redirectToRouteResult.RouteValues["controller"].ToString();
        }
    }
}