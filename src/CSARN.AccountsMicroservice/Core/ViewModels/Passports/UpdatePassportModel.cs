using SharedLib.AccountsMsvc.Misc;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.Passports
{
    /// <summary>
    /// TODO: Add validation attributes
    /// </summary>

    public class UpdatePassportModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Patronymic { get; set; } = string.Empty;

        [Required]
        public RegionCodes RegionCode { get; set; }
    }
}
