using AM.BLL.Common;
using AM.BLL.Common.Core;
using AM.BLL.Users.Core;
using AM.DAL.Core.Entities;
using AM.DAL.Users.Core;
using AM.DM.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UserAccessTokenClaim.Core;

namespace AM.BLL.Users.Infrastructure
{
    public class UserService : IUserService
    {
        private readonly IMapper _IMapper;
        private readonly IUserRepository _IUserRepository;
        private readonly IUserAccessTokenClaimsService _IUserAccessTokenClaims;
        private readonly IEmailHandlerService _IEmailHandlerService;

        public UserService(IMapper mapper, IUserRepository userRepository, IUserAccessTokenClaimsService userAccessTokenClaims, IEmailHandlerService emailHandlerService)
        {
            _IMapper = mapper;
            _IUserRepository = userRepository;
            _IUserAccessTokenClaims = userAccessTokenClaims;
            _IEmailHandlerService = emailHandlerService;
        }

        public void ChangePassword(string pPassword, string pNewPassword)
        {
            var loggedUser = _IUserAccessTokenClaims.GetCurrentLoggedUserInfo();
            string curPass = Convert.ToBase64String(Encoding.Unicode.GetBytes(pPassword));
            if(curPass == loggedUser.Password)
            {
                var userProfile = _IUserRepository.GetUserProfileByUserId(1);
                userProfile.Password= Convert.ToBase64String(Encoding.Unicode.GetBytes(pNewPassword));
                userProfile.DateModified = DateTime.Now;
                _IUserRepository.UpdateProfile(userProfile);
            }
            else
            {
                throw new Exception("Current password doesn't match");
            }
        }

        public void Create(UserInformationModel pUser)
        {

            if (pUser.Password != pUser.ConfirmPassword)
            {
                throw new Exception("Password & ConfirmPassword don't match!");
            }

            var userToSave = _IMapper.Map<UserInformationModel, UserInformation>(pUser);
            userToSave.Password = Convert.ToBase64String(Encoding.Unicode.GetBytes(pUser.Password));

            Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(pUser.Password));
            userToSave.DateCreated = DateTime.Now;
            userToSave.IsArticleUser = true;
            _IUserRepository.Create(userToSave);
        }

        public void CreateProfessionalProfile(ProfessionalProfileModel pProfessionalProfile)
        {
            var loggedUser = _IUserAccessTokenClaims.GetCurrentLoggedUserInfo();

            pProfessionalProfile.UserId = loggedUser.Id;
            var userProProfileToSave = _IMapper.Map<ProfessionalProfileModel, ProfessionalProfile>(pProfessionalProfile);
            _IUserRepository.CreateProfessionalProfile(userProProfileToSave);
        }

        public ProfessionalProfileModel GetProfessionalProfileByUserId()
        {
            var loggedUser = _IUserAccessTokenClaims.GetCurrentLoggedUserInfo();

            var userProfile = _IUserRepository.GetProfessionalProfileByUserId(loggedUser.Id);
            var userProfileModel = _IMapper.Map<ProfessionalProfile, ProfessionalProfileModel>(userProfile);
            return userProfileModel;
        }

        public UserInformationModel GetUserForAuth(string pEmail, string pPassword)
        {
            var userProfile = _IUserRepository.GetUserForAuth(pEmail,pPassword);
            var userProfileModel = _IMapper.Map<UserInformation, UserInformationModel>(userProfile);
            return userProfileModel;
        }

        public UserInformationModel GetUserProfile()
        {
            var loggedUser = _IUserAccessTokenClaims.GetCurrentLoggedUserInfo();

            var userProfile = _IUserRepository.GetUserProfileByUserId(loggedUser.Id);
            var userProfileModel = _IMapper.Map<UserInformation, UserInformationModel>(userProfile);
            return userProfileModel;
        }

        public UserInformationModel GetUserProfile(string Email)
        {
            var userProfile = _IUserRepository.GetUserProfile(Email);
            var userProfileModel = _IMapper.Map<UserInformation, UserInformationModel>(userProfile);
            return userProfileModel;
        }

        public void ResetPassword(string Email, string VerificationCode, string NewPassword)
        {
            var userProfile = _IUserRepository.GetUserProfile(Email);
            if (userProfile.PassChangeVerifyCode == VerificationCode && !string.IsNullOrEmpty(VerificationCode))
            {
                userProfile.Password = Convert.ToBase64String(Encoding.Unicode.GetBytes(NewPassword));
                userProfile.PassChangeVerifyCode = null;
                _IUserRepository.UpdateProfile(userProfile);
            }
            else
            {
                throw new Exception("Verification Code doesn't match");
            }
        }

        public void SendResetPasswordCode(string pEmail)
        {

            var userProfile = _IUserRepository.GetUserProfile(pEmail);
            if (userProfile != null)
            {
                String verificationCode = (new Random()).Next(0, 999999).ToString("D6");
                string toEmail = pEmail;
                string subject = "Password Reset Code";
                string body = "Your password reset code is "+ verificationCode;
                _IEmailHandlerService.SendEmail(toEmail, subject, body);

                userProfile.PassChangeVerifyCode = verificationCode;
                _IUserRepository.UpdateProfile(userProfile);
            }
            else
            {
                throw new Exception("Email Not found!");
            }
        }

        public void UpdateProfile(UserInformationModel pUser)
        {
            var loggedUser = _IUserAccessTokenClaims.GetCurrentLoggedUserInfo();

            var userProfile = _IUserRepository.GetUserProfileByUserId(1);
            userProfile.Profession = pUser.Profession;
            userProfile.Organization = pUser.Organization;
            userProfile.Country = pUser.Country;
            userProfile.MobileNumber = pUser.MobileNumber;
            userProfile.DateModified = DateTime.Now;
            userProfile.ModifiedBy = loggedUser.Id;
            userProfile.Id = loggedUser.Id;
            _IUserRepository.UpdateProfile(userProfile);
        }
    }
}
