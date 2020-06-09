using AM.DAL.Core;
using AM.DAL.Core.Entities;
using AM.DAL.Users.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AM.DAL.Users.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        public void Create(UserInformation pUser)
        {
            using AMDBContext aMDBContext = new AMDBContext();
            aMDBContext.UserInformation.Add(pUser);
            aMDBContext.SaveChanges();

        }

        public void UpdateProfile(UserInformation pUser)
        {
            using AMDBContext aMDBContext = new AMDBContext();
            aMDBContext.UserInformation.Update(pUser);
            aMDBContext.SaveChanges();
        }

        public UserInformation GetUserProfileByUserId(long pUserId)
        {
            using AMDBContext aMDBContext = new AMDBContext();
            var userProfile = aMDBContext.UserInformation.FirstOrDefault(u => u.Id == pUserId);
            return userProfile;
        }
    }
}
