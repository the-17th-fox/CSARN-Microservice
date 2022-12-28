using SharedLib.AccountsMsvc.Misc;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.ViewModels.Accounts
{
    /// <summary>
    /// TODO: Add validation attributes
    /// </summary>

    public class UpdatePassportModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;

        [Required]
        public RegionCodes RegionCode { get; set; }
    }
}
