using System.ComponentModel.DataAnnotations;
using MovieTracker.Models;
using System.Web.Security;
using System;

namespace MovieTracker.Data
{
    [MetadataType(typeof(MovieMetaData))]
    public partial class Movie
    {
        public void ClearRating()
        {
            Rating = 0;
        }

        public bool IsOwnedBy(MembershipUser user)
        {
            if (user == null)
            {
                return false;
            }

            var userKey = (Guid)user.ProviderUserKey;
            return this.aspnet_UsersUserId == userKey;
        }
    }
}