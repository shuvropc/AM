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
        public UserInformationModel GetUserProfile(string Email);
        public UserInformationModel GetUserForAuth(string pEmail, string pPassword);
        public void CreateProfessionalProfile(ProfessionalProfileModel pProfessionalProfile);
        public ProfessionalProfileModel GetProfessionalProfileByUserId();
        public void ChangePassword(string pPassword, string pNewPassword);
        public void SendResetPasswordCode(string pEmail);
        public void ResetPassword(string Email, string VerificationCode, string NewPassword);

    }
}
