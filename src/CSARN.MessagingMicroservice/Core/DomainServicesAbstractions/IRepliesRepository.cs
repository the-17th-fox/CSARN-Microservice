using SharedLib.Generics.Repositories;
using SharedLib.MessagingMsvc.Models;

namespace Core.RepositoriesAbstractions
{
    public interface IRepliesRepository : IBaseRepository<Reply, Guid>
    {
        public Task<List<Reply>> GetAllForReportAsync(Guid reportId, int pageNum, int pageSize);
        public Task<List<Reply>> GetAllUnreadForAccountAsync(Guid accountId, int pageNum, int pageSize);
    }
}
