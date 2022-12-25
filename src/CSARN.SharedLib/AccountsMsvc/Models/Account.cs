using SharedLib.Generics.Models;
using SharedLib.MessagingMsvc.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLib.AccountsMsvc.Models
{
    public class Account : BaseIdentityModel
    {
        [Required]
        public Guid PassportId { get; set; }
        [ForeignKey(nameof(PassportId))]
        public Passport Passport { get; set; } = null!;

        //public List<Notification> Notifications { get; set; } = new();
        //public List<Report> Reports { get; set; } = new();
        //public List<Reply> Responses { get; set; } = new();

        // Account management
        public bool IsDeleted { get; set; }
        public bool IsBlocked { get; set; }

    }
}
