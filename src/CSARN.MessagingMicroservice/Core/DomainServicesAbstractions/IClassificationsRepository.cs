using SharedLib.Generics.Repositories;
using SharedLib.MessagingMsvc.Models;

namespace DomainServiceAbstractions
{
    public interface IClassificationsRepository : IBaseRepository<Classification, Guid>
    {
        public Task<List<Classification>> GetAllForNotificationAsync(Guid notificationId, int pageNum, int pageSize);
        public Task<List<Classification>> GetAllForReportAsync(Guid reportId, int pageNum, int pageSize);
    }
}