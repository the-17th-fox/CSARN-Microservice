using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CSARNCore.MessagingMicroservice.Models
{
    public class Notification : BaseMessage
    {
        [Required]
        public string Organization { get; set; } = string.Empty;

        // Nav. fields
        public List<Tag> Tags { get; set; } = new();
    }
}