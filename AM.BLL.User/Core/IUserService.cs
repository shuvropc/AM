using AM.DM.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.BLL.Users.Core
{
    public interface IUserService
    {
        public void Create(UserInformationModel pUser);
        public void UpdateProfile(UserInformationModel pUser);
        public UserInformationModel GetUserProfile();
    }
}
