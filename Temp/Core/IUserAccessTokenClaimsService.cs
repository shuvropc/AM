using AM.DM.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserAccessTokenClaim.Core
{
    public interface IUserAccessTokenClaimsService
    {
        public UserAccessTokenClaimModel GetCurrentLoggedUserInfo();
    }
}
