using SharedLib.AccountsMsvc.Models;
using SharedLib.MessagingMsvc.Misc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLib.MessagingMsvc.Models
{
    public class Report : BaseMessage
    {
        public ReportStatuses Status { get; set; } = ReportStatuses.Pending;
        public Guid? ReplyId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        // Nav. fields
        [ForeignKey(nameof(ReplyId))]
        public Reply? Reply { get; set; }
        public List<Classification> Classifications { get; set; } = new();
        public Account Account { get; set; } = null!;
    }
}
