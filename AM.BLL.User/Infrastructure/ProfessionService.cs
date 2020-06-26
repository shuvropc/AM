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
    public class ProfessionService : IProfessionService
    {
        private readonly IMapper _IMapper;
        private readonly IProfessionRepository _IProfessionRepository;

        public ProfessionService(IMapper mapper, IProfessionRepository professionRepository)
        {
            _IMapper = mapper;
            _IProfessionRepository = professionRepository;
        }

        public List<ProfessionModel> GetAllProfessions()
        {
            var profession = _IProfessionRepository.GetAllProfessions();
            var professionListModel = _IMapper.Map<List<Profession>, List<ProfessionModel>>(profession);
            return professionListModel;

        }
    }
}
