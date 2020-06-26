using AM.DM.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.BLL.User.Core
{
    public interface IOrganizationService
    {
        public List<OrganizationModel> GetAllOrganizations();

    }
}
