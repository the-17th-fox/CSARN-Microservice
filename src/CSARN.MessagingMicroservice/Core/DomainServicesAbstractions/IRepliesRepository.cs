using CSARN.SharedLib.Generics.Repositories;
using SharedLib.MessagingMsvc.Models;

namespace DomainServiceAbstractions
{
    public interface IRepliesRepository : IBaseRepository<Reply, Guid>
    {
        public Task<List<Reply>> GetForReportAsync(Guid reportId, int pageNum, int pageSize);
        public Task<List<Reply>> GetUnreadForAccountAsync(Guid accountId, int pageNum, int pageSize);
    }
}
