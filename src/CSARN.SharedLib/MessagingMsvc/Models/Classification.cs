using SharedLib.Generics.Models;

namespace SharedLib.MessagingMsvc.Models
{
    public class Classification : BaseModel
    {
        public string Title { get; set; } = string.Empty;

        // Nav. fields
        public List<Report> Reports { get; set; } = new();
        public List<Notification> Notifications { get; set; } = new();
    }
}
