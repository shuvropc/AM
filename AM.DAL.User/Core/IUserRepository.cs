﻿using AM.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
namespace AM.DAL.Users.Core
{
    public interface IUserRepository
    {
        public void Create(UserInformation pUser);
        public void UpdateProfile(UserInformation pUser);
        public UserInformation GetUserProfileByUserId(long pUserId);
    }
}
