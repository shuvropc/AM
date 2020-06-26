using AM.BLL.Users.Core;
using AM.DAL.Core.Entities;
using AM.DAL.Users.Core;
using AM.DM.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UserAccessTokenClaim.Core;

namespace AM.BLL.Users.Infrastructure
{
    public class UserService : IUserService
    {
        private readonly IMapper _IMapper;
        private readonly IUserRepository _IUserRepository;
        private readonly IUserAccessTokenClaimsService _IUserAccessTokenClaims;

        public UserService(IMapper mapper, IUserRepository userRepository, IUserAccessTokenClaimsService userAccessTokenClaims)
        {
            _IMapper = mapper;
            _IUserRepository = userRepository;
            _IUserAccessTokenClaims = userAccessTokenClaims;
        }

        public void Create(UserInformationModel pUser)
        {
            var loggedUser = _IUserAccessTokenClaims.GetCurrentLoggedUserInfo();

            if (pUser.Password != pUser.ConfirmPassword)
            {
                throw new Exception("Password & ConfirmPassword don't match!");
            }

            var userToSave = _IMapper.Map<UserInformationModel, UserInformation>(pUser);
            userToSave.Password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(pUser.Password));
            userToSave.CreatedBy = loggedUser.Id;
            userToSave.DateCreated = DateTime.Now;
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
