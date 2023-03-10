using AutoMapper;
using BankingApp.DTOLayer.DTOs.AppUserDTOs;
using BankingApp.DTOLayer.DTOs.CustomerDTOs;
using BankingApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Configuration.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SignInDTO, AppUser>().ReverseMap();
            CreateMap<SignUpDTO, AppUser>().ReverseMap();
            CreateMap<CustomerUpdateDTO, AppUser>().ReverseMap();
            CreateMap<CustomerAddDTO, AppUser>().ReverseMap();
            CreateMap<ProfileUpdateDTO, AppUser>().ReverseMap();
        }
    }
}
