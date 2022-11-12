using SharedLib.Generics.Models;

namespace SharedLib.MessagingMsvc.Models
{
    public abstract class BaseMessage : BaseModel
    {
        public string Header { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
