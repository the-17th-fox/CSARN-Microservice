using CSARNCore.AccountsMsvc.Models;
using System.ComponentModel.DataAnnotations;

namespace CSARNCore.MessagingMicroservice.Models
{
    public class Reply : BaseMessage
    {
        [Required]
        public string Organization { get; set; } = null!;

        [Required]
        public Guid ReportId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        // Nav. fields
        public Report Report { get; set; } = null!;
        public Account Account { get; set; } = null!;
    }
}
