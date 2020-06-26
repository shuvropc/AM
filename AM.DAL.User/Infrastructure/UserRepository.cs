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

        public void CreateProfessionalProfile(ProfessionalProfile pProfessionalProfile)
        {
            using AMDBContext aMDBContext = new AMDBContext();
            aMDBContext.ProfessionalProfile.Add(pProfessionalProfile);
            aMDBContext.SaveChanges();
        }

        public ProfessionalProfile GetProfessionalProfileByUserId(long pUserId)
        {
            using AMDBContext aMDBContext = new AMDBContext();
            var userProfile = aMDBContext.ProfessionalProfile.FirstOrDefault(u => u.UserId == pUserId);
            return userProfile;
        }

        public UserInformation GetUserProfile(string Email)
        {
            using AMDBContext aMDBContext = new AMDBContext();
            var result = aMDBContext.UserInformation.FirstOrDefault(x => x.Email == Email);
            return result;
        }

        public UserInformation GetUserForAuth(string pEmail, string pPassword)
        {
            using AMDBContext aMDBContext = new AMDBContext();
            var result = aMDBContext.UserInformation.FirstOrDefault(x => x.Email == pEmail && x.Password==pPassword);
            return result;
        }
    }
}
