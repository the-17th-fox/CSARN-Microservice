using SharedLib.AccountsMsvc.Misc;

namespace Core.Domain.ViewModels.Accounts
{
    /// <summary>
    /// TODO: Add validation attributes
    /// </summary>

    public class RegistrationViewModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        
        public RegionCodes PassRegionCode { get; set; } 
        public string PassNumber { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
