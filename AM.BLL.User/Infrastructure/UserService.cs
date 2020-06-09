using AM.BLL.Users.Core;
using AM.DAL.Core.Entities;
using AM.DAL.Users.Core;
using AM.DM.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.BLL.Users.Infrastructure
{
    public class UserService : IUserService
    {
        private readonly IMapper _IMapper;
        private readonly IUserRepository _IUserRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _IMapper = mapper;
            _IUserRepository = userRepository;
        }

        public void Create(UserInformationModel pUser)
        {
            if (pUser.Password != pUser.ConfirmPassword)
            {
                throw new Exception("Password & ConfirmPassword don't match!");
            }

            var userToSave = _IMapper.Map<UserInformationModel, UserInformation>(pUser);
            userToSave.Password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(pUser.Password));
            userToSave.CreatedBy = 1;
            userToSave.DateCreated = DateTime.Now;
            _IUserRepository.Create(userToSave);
        }

        public UserInformationModel GetUserProfile()
        {
            var userProfile = _IUserRepository.GetUserProfileByUserId(1);
            var userProfileModel = _IMapper.Map<UserInformation, UserInformationModel>(userProfile);
            return userProfileModel;
        }

        public void UpdateProfile(UserInformationModel pUser)
        {
            var userProfile = _IUserRepository.GetUserProfileByUserId(1);
            userProfile.Profession = pUser.Profession;
            userProfile.Organization = pUser.Organization;
            userProfile.Country = pUser.Country;
            userProfile.MobileNumber = pUser.MobileNumber;
            userProfile.DateModified = DateTime.Now;
            userProfile.ModifiedBy = 1;
            userProfile.Id = 1;
            _IUserRepository.UpdateProfile(userProfile);
        }
    }
}
