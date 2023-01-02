using AutoMapper;
using Core.ViewModels.Accounts;
using Core.ViewModels.Passports;
using SharedLib.AccountsMsvc.Models;

namespace Core.Domain.ViewModels
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Account, AccountViewModel>();
            CreateMap<Passport, PassportViewModel>();
        }
    }
}
