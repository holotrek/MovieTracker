using System;
using System.Web.Security;

namespace MovieTracker.Tests.Unit.Controllers
{
    public class TestMembershipUser : MembershipUser
    {
        public TestMembershipUser(string userName, Guid key)
            : base(
                "AspNetSqlMembershipProvider", userName, key, "test@test.com", "", "", true, false, DateTime.Now, DateTime.Now,
                DateTime.Now, DateTime.Now, DateTime.Now)
        {
        }
    }
}