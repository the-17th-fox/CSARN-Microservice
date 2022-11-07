using SharedLib.AccountsMsvc.Models;
using System.ComponentModel.DataAnnotations;

namespace SharedLib.MessagingMsvc.Models
{
    public class Notification : BaseMessage
    {
        [Required]
        public string Organization { get; set; } = string.Empty;

        [Required]
        public Guid AccountId { get; set; }

        public Guid? TargetAccountId { get; set; }

        // Nav. fields
        public List<Classification> Classifications { get; set; } = new();
        public Account Account { get; set; } = null!;
        public Account? TargetAccount { get; set; }
    }
}