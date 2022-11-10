using CSARN.SharedLib.Generics.Repositories;
using SharedLib.MessagingMsvc.Models;

namespace DomainServiceAbstractions
{
    public interface IReportsRepository : IBaseRepository<Report, Guid>
    {
        public Task<List<Report>> GetByAccountAsync(Guid accountId, int pageNum, int pageSize);
        public Task AddClassificationAsync(Guid reportId, Guid classificationId);
        public Task RemoveClassificationAsync(Guid reportId, Guid classificationId);
    }
}
