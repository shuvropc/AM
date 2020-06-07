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

        public void Create(UserModel pUser)
        {
            var userToSave = _IMapper.Map<UserModel, User>(pUser);
            userToSave.CreatedBy = 1;
            userToSave.DateCreated = DateTime.Now;
            _IUserRepository.Create(userToSave);
        }
    }
}
