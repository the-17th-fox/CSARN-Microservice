using CSARNCore.MessagingMicroservice.Misc;
using System.ComponentModel.DataAnnotations;

namespace CSARNCore.MessagingMicroservice.Models
{
    public class Report : BaseMessage
    {
        public ReportStatuses Status { get; set; } = ReportStatuses.Pending;
        public Guid? ResponseId { get; set; }

        // Nav. fields
        public Response? Response { get; set; }
        public List<Tag> Tags { get; set; } = new();
    }
}
