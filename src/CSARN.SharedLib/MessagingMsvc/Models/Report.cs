using SharedLib.AccountsMsvc.Models;
using SharedLib.MessagingMsvc.Misc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLib.MessagingMsvc.Models
{
    public class Report : BaseMessage
    {
        public ReportStatuses Status { get; set; } = ReportStatuses.Pending;

        [Required]
        public Guid AccountId { get; set; }

        // Nav. fields
        public List<Reply> Reply { get; set; } = new();
        public List<Classification> Classifications { get; set; } = new();
        public Account Account { get; set; } = null!;
    }
}
