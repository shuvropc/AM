using AM.DM.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.BLL.User.Core
{
    public interface IProfessionService
    {
        public List<ProfessionModel> GetAllProfessions();

    }
}
