using CSARNCore.AccountsMsvc.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CSARNCore.MessagingMicroservice.Models
{
    public class Notification : BaseMessage
    {
        [Required]
        public string Organization { get; set; } = string.Empty;

        [Required]
        public Guid AccountId { get; set; }

        // Nav. fields
        public List<Classification> Classifications { get; set; } = new();
        public Account Account { get; set; } = null!;
    }
}