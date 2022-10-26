using CSARNCore.AccountsMsvc.Models;
using CSARNCore.Generics;

namespace CSARNCore.MessagingMicroservice.Models
{
    public abstract class BaseMessage : BaseModel
    {
        public string Header { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
