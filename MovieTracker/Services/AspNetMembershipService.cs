using System.Web.Security;
using MovieTracker.Controllers;

namespace MovieTracker.Services
{
    public class AspNetMembershipService : IMembershipService
    {
        public MembershipUser GetUser(string userName)
        {
            return Membership.GetUser(userName);
        }
    }
}