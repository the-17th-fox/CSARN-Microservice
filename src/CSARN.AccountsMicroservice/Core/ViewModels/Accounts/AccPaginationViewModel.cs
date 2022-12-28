using CSARN.SharedLib.ViewModels.Pagination;

namespace Core.ViewModels.Accounts
{
    public class AccPaginationViewModel : PageParametersViewModel
    {
        public bool ShowDeleted { get; set; } = false;
        public bool ShowBlocked { get; set; } = false;
    }
}
