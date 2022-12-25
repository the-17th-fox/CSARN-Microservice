using AutoMapper;
using Core.ViewModels.Accounts;
using SharedLib.AccountsMsvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
