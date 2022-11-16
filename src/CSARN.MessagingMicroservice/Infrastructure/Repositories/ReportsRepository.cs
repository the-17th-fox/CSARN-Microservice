using Core.Domain;
using Core.RepositoriesAbstractions;
using Infrastructure.Utilities;
using Microsoft.Extensions.Options;
using SharedLib.MessagingMsvc.Misc;
using SharedLib.MessagingMsvc.Models;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class ReportsRepository : BaseRepository<Report, Guid>, IReportsRepository
    {
        public ReportsRepository(IOptions<RepositoryConfiguration> configuration)
            : base(configuration, TableName.Reports.ToString()) {}

        public async override Task CreateAsync(Report entity)
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
                    new("@AccountId", entity.AccountId),
                    new("@StatusId", entity.Status)
                });

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<Report>> GetAllByAccountAsync(Guid accountId, int pageNum, int pageSize)
        {
            var procedure = $"proc_{_tableName}_GetAllByAccount";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new("@AccountId", accountId));
                cmd.Parameters.Add(new("@PageNum", pageNum));
                cmd.Parameters.Add(new("@PageSize", pageSize));

                await connection.OpenAsync();
                var response = new List<Report>();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        response.Add(MapToResult(reader));
                }

                return response;
            }
        }

        public async Task AddClassificationAsync(Guid reportId, Guid classificationId)
        {
            var tableName = TableName.NotificationsClassifications.ToString();
            var procedure = $"proc_{tableName}_{StoredProcedureMethod.Create.ToString()}";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new("@ReportId", reportId));
                cmd.Parameters.Add(new("@ClassificationId", classificationId));

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task RemoveClassificationAsync(Guid reportId, Guid classificationId)
        {
            var tableName = TableName.ReportsClassifications.ToString();
            var procedure = $"proc_{tableName}_{StoredProcedureMethod.Delete}";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new("@ReportId", reportId));
                cmd.Parameters.Add(new("@ClassificationId", classificationId));

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async override Task UpdateAsync(Report entity)
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
                    new("@AccountId", entity.AccountId),
                    new("@StatusId", entity.Status)
                });

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        protected override Report MapToResult(SqlDataReader reader, params string[] fields)
        {
            return new()
            {
                Id = (Guid)reader[$"{nameof(Report.Id)}"],
                Header = reader[$"{nameof(Report.Header)}"].ToString() ?? string.Empty,
                Body = reader[$"{nameof(Report.Body)}"].ToString() ?? string.Empty,
                AccountId = (Guid)reader[$"{nameof(Report.AccountId)}"],
                Status = (ReportStatuses)reader[$"Id"],
                UpdatedAt = (DateTime)reader[$"{nameof(Report.UpdatedAt)}"],
                CreatedAt = (DateTime)reader[$"{nameof(Report.CreatedAt)}"],
            };
        }
    }
}
