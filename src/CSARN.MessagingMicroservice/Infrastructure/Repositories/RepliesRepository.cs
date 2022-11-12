using Domain;
using DomainServiceAbstractions;
using Infrastructure.Utilities;
using Microsoft.Extensions.Options;
using SharedLib.MessagingMsvc.Models;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class RepliesRepository : BaseRepository<Reply, Guid>, IRepliesRepository
    {
        public RepliesRepository(IOptions<RepositoryConfiguration> configuration) 
            : base(configuration, TableName.Replies.ToString()) {}

        public async override Task CreateAsync(Reply entity)
        {
            var procedure = $"proc_{_tableName}_{StoredProcedureMethod.Create.ToString()}";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddRange(new SqlParameter[]
                {
                    new("@Header", entity.Header),
                    new("@Body", entity.Body),
                    new("@AuthorId", entity.AccountId),
                    new("@ReportId", entity.ReportId),
                    new("@WasRead", entity.WasRead)
                });

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async override Task UpdateAsync(Reply entity)
        {
            var procedure = $"proc_{_tableName}_{StoredProcedureMethod.Update.ToString()}";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddRange(new SqlParameter[]
                {
                    new("@Id", entity.Id),
                    new("@Header", entity.Header),
                    new("@Body", entity.Body),
                    new("@AuthorId", entity.AccountId),
                    new("@ReportId", entity.ReportId),
                    new("@WasRead", entity.WasRead)
                });

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<Reply>> GetAllForReportAsync(Guid reportId, int pageNum, int pageSize)
        {
            var procedure = $"proc_{_tableName}_GetAllForReport";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new("@ReportId", reportId));
                cmd.Parameters.Add(new("@PageNum", pageNum));
                cmd.Parameters.Add(new("@PageSize", pageSize));

                await connection.OpenAsync();
                var response = new List<Reply>();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        response.Add(MapToResult(reader));
                }

                return response;
            }
        }

        public async Task<List<Reply>> GetAllUnreadForAccountAsync(Guid accountId, int pageNum, int pageSize)
        {
            var procedure = $"proc_{_tableName}_GetAllUnreadForAccount";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new("@AccountId", accountId));
                cmd.Parameters.Add(new("@PageNum", pageNum));
                cmd.Parameters.Add(new("@PageSize", pageSize));

                await connection.OpenAsync();
                var response = new List<Reply>();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        response.Add(MapToResult(reader));
                }

                return response;
            }
        }

        protected override Reply MapToResult(SqlDataReader reader, params string[] fields)
        {
            return new()
            {
                Id = (Guid)reader[$"{nameof(Reply.Id)}"],
                Header = reader[$"{nameof(Reply.Header)}"].ToString() ?? string.Empty,
                Body = reader[$"{nameof(Reply.Body)}"].ToString() ?? string.Empty,
                Organization = reader[$"{nameof(Reply.Organization)}"].ToString() ?? string.Empty,
                AccountId = (Guid)reader[$"{nameof(Reply.AccountId)}"],
                ReportId = (Guid)reader[$"{nameof(Reply.ReportId)}"],
                UpdatedAt = (DateTime)reader[$"{nameof(Reply.UpdatedAt)}"],
                CreatedAt = (DateTime)reader[$"{nameof(Reply.CreatedAt)}"],
            };
        }
    }
}
