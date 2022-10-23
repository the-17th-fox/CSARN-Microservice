using CSARNCore.Generics;
using CSARNCore.MessagingMicroservice.Models;
using System.ComponentModel.DataAnnotations;

namespace CSARNCore.AccountsMsvc.Models
{
    public class Account : BaseIdentityModel
    {
        [Required]
        public Guid PassportId { get; set; }
        public Passport Passport { get; set; } = null!;

        public List<Notification> Notifications { get; set; } = new();
        public List<Report> Reports { get; set; } = new();
        public List<Response> Responses { get; set; } = new();

        // Account management
        public bool IsDeleted { get; set; }
        public bool IsLocked { get; set; }

    }
}
