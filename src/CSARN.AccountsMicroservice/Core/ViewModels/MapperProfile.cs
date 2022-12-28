using AutoMapper;
using Core.ViewModels.Accounts;
using SharedLib.AccountsMsvc.Models;

namespace Core.Domain.ViewModels
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Account, AccountViewModel>();
        }
    }
}
