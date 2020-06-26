using AM.DAL.Core;
using AM.DAL.Core.Entities;
using AM.DAL.User.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AM.DAL.User.Infrastructure
{
    public class OrganizationRepository : IOrganizationRepository
    {
        public List<Organization> GetAllOrganizations()
        {
            using AMDBContext aMDBContext = new AMDBContext();
            var OrgList = aMDBContext.Organization.ToList();
            return OrgList;
        }
    }
}
