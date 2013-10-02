using System.Web.Security;

namespace MovieTracker.Services
{
    public interface IMembershipService
    {
        MembershipUser GetUser(string userName);
    }
}