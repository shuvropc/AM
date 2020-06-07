using AM.DM.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.BLL.Users.Core
{
    public interface IUserService
    {
        public void Create(UserModel pUser);
    }
}
