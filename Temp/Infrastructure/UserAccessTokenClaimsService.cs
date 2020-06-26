using AM.DM.User;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserAccessTokenClaim.Core;

namespace UserAccessTokenClaim.Infrastructure
{
    public class UserAccessTokenClaimsService : IUserAccessTokenClaimsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessTokenClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public UserAccessTokenClaimModel GetCurrentLoggedUserInfo()
        {

            var res = _httpContextAccessor.HttpContext.User.Claims.Where(s => s.Type == "userAccessClaim").FirstOrDefault().Value;
            var userclaim = JsonConvert.DeserializeObject<UserAccessTokenClaimModel>(res);
            return userclaim;
        }
    }
}
