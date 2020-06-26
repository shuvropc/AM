using AM.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.DAL.User.Core
{
    public interface IProfessionRepository
    {
        public List<Profession> GetAllProfessions();
    }
}
