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

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            CreateMap<Account, ExtendedAccountViewModel>()
                .ForMember(m => m.IsRevoked, a => a.MapFrom(src => src.RefreshToken.IsRevoked))
                .ForMember(m => m.ExpiresAt, a => a.MapFrom(src => src.RefreshToken.ExpiresAt));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            CreateMap<Passport, PassportViewModel>();
        }
    }
}
