using CSARN.SharedLib.ViewModels.Pagination;
using SharedLib.AccountsMsvc.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Accounts
{
    public class AccPaginationViewModel : PageParametersViewModel
    {
        public bool ShowDeleted { get; set; } = false;
        public bool ShowBlocked { get; set; } = false;
        public RegionCodes ShowForRegion { get; set; } = RegionCodes.Undefined;
    }
}
