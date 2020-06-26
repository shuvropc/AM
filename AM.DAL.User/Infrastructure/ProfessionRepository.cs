using AM.DAL.Core;
using AM.DAL.Core.Entities;
using AM.DAL.User.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AM.DAL.User.Infrastructure
{
    public class ProfessionRepository : IProfessionRepository
    {
        public List<Profession> GetAllProfessions()
        {
            using AMDBContext aMDBContext = new AMDBContext();
            var ProList = aMDBContext.Profession.ToList();
            return ProList;
        }
    }
}
