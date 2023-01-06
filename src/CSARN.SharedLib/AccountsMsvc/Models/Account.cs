using SharedLib.Generics.Models;

namespace SharedLib.AccountsMsvc.Models
{
    public class Account : BaseIdentityModel
    {
        public Passport? Passport { get; set; }

        public Guid? RefreshTokenId { get; set; }
        public RefreshToken? RefreshToken { get; set; }

        //public List<Notification> Notifications { get; set; } = new();
        //public List<Report> Reports { get; set; } = new();
        //public List<Reply> Responses { get; set; } = new();

        // Account management
        public bool IsDeleted { get; set; }
        public bool IsBlocked { get; set; }
    }
}
