using AM.DAL.Core;
using AM.DAL.Core.Entities;
using AM.DAL.Users.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.DAL.Users.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        public void Create(User pUser)
        {
            using AMDBContext aMDBContext = new AMDBContext();
            aMDBContext.User.Add(pUser);
            aMDBContext.SaveChanges();
        }
    }
}
