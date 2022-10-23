namespace CSARNCore.MessagingMicroservice.Models
{
    public class Response : BaseMessage
    {
        public string Organization { get; set; } = null!;
        public Guid ReportId { get; set; }

        // Nav. fields
        public Report Report { get; set; } = null!;
    }
}
