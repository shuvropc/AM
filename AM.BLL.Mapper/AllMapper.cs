using AM.DAL.Core.Entities;
using AM.DM.Article;
using AM.DM.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.BLL.Mapper
{
    public class AllMapper : Profile
    {
        public AllMapper()
        {
            CreateMap<UserInformation, UserInformationModel>().ReverseMap();
            CreateMap<Profession, ProfessionModel>().ReverseMap();
            CreateMap<Organization, OrganizationModel>().ReverseMap();
            CreateMap<ProfessionalProfile, ProfessionalProfileModel>().ReverseMap();
            CreateMap<Article, ArticleModel>().ReverseMap();
        }
    }
}
