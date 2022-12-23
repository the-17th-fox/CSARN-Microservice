using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSARN.SharedLib.ViewModels.Pagination
{
    public class PageMapperProfile<TElement> : Profile
    {
        public PageMapperProfile()
        {
            CreateMap<TElement, PageViewModel<TElement>>();
        }
    }
}
