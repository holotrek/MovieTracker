using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using MovieTracker.Services;

namespace MovieTracker.Tests.Unit.Fakes
{
    public class FakeMembershipService : IMembershipService
    {
        private readonly IEnumerable<MembershipUser> _users;

        public FakeMembershipService(IEnumerable<MembershipUser> users)
        {
            _users = users;
        }

        #region IMembershipService Members

        public MembershipUser GetUser(string userName)
        {
            return _users.FirstOrDefault(u => u.UserName == userName);
        }

        #endregion
    }
}