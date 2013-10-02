using System.Security.Principal;
using System.Web;

namespace MovieTracker.Tests.Unit.Fakes
{
    public class FakeHttpContext : HttpContextBase
    {
        public override IPrincipal User
        {
            get { return new GenericPrincipal(new GenericIdentity("test"), new string[] {}); }
            set { base.User = value; }
        }
    }
}