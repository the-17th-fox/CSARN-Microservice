using SharedLib.Generics.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SharedLib.AccountsMsvc.Models
{
    [Index(nameof(EncodedNumber), IsUnique = true)]
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
        public string EncodedRegion { get; set; } = string.Empty;
        [Required]
        public string EncodedNumber { get; set; } = string.Empty;

    }
}
