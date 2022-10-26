using CSARNCore.AccountsMsvc.Models;
using CSARNCore.MessagingMicroservice.Misc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSARNCore.MessagingMicroservice.Models
{
    public class Report : BaseMessage
    {
        public ReportStatuses Status { get; set; } = ReportStatuses.Pending;
        public Guid? ResponseId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        // Nav. fields
        [ForeignKey(nameof(ResponseId))]
        public Response? Response { get; set; }
        public List<Tag> Tags { get; set; } = new();
        public Account Account { get; set; } = null!;
    }
}
