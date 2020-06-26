using AM.BLL.User.Core;
using AM.DAL.Core.Entities;
using AM.DAL.User.Core;
using AM.DM.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.BLL.User.Infrastructure
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IMapper _IMapper;
        private readonly IOrganizationRepository _IOrganizationRepository;

        public OrganizationService(IMapper mapper, IOrganizationRepository organizationRepository)
        {
            _IMapper = mapper;
            _IOrganizationRepository = organizationRepository;
        }

        public List<OrganizationModel> GetAllOrganizations()
        {
            var organization = _IOrganizationRepository.GetAllOrganizations();
            var organizationListModel = _IMapper.Map<List<Organization>, List<OrganizationModel>>(organization);
            return organizationListModel;
        }
    }
}
