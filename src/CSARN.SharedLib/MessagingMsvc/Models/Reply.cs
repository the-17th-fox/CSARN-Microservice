using SharedLib.AccountsMsvc.Models;
using System.ComponentModel.DataAnnotations;

namespace SharedLib.MessagingMsvc.Models
{
    public class Reply : BaseMessage
    {
        [Required]
        public string Organization { get; set; } = null!;

        [Required]
        public Guid ReportId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        public bool WasRead { get; set; }

        // Nav. fields
        public Report Report { get; set; } = null!;
        public Account Account { get; set; } = null!;
    }
}
