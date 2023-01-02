using SharedLib.Generics.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SharedLib.AccountsMsvc.Models
{
    [Index(nameof(AccountId), IsUnique = true)]
    [Index(nameof(FirstName), nameof(LastName), nameof(Patronymic))]
    public class Passport : BaseModel
    {
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Patronymic { get; set; } = string.Empty;

        // Passport number
        [Required]
        public string Region { get; set; } = string.Empty;
        [Required]
        public string Number { get; set; } = string.Empty;

    }
}
