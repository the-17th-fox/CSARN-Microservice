using SharedLib.Generics.Repositories;
using SharedLib.MessagingMsvc.Models;

namespace DomainServiceAbstractions
{
    public interface INotificationsRepository : IBaseRepository<Notification, Guid>
    {
        public Task<List<Notification>> GetAllForAccountAsync(Guid accountId, int pageNum, int pageSize);
        public Task AddClassificationAsync(Guid notificationId, Guid classificationId);
        public Task RemoveClassificationAsync(Guid notificationId, Guid classificationId);
    }
}
