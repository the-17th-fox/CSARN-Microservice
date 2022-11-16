using Core.Domain;
using Core.RepositoriesAbstractions;
using Infrastructure.Utilities;
using Microsoft.Extensions.Options;
using SharedLib.MessagingMsvc.Models;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class ClassificationsRepository : BaseRepository<Classification, Guid>, IClassificationsRepository
    {
        public ClassificationsRepository(IOptions<RepositoryConfiguration> configuration) 
            : base(configuration, TableName.Classifications.ToString()) {}

        public async override Task CreateAsync(Classification entity)
        {
            var procedure = $"proc_{_tableName}_{StoredProcedureMethod.Create.ToString()}";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new("@Title", entity.Title));

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<Classification>> GetAllForNotificationAsync(Guid notificationId, int pageNum, int pageSize)
        {
            var procedure = $"proc_{_tableName}_GetAllForNotification";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new("@NotificationId", notificationId));
                cmd.Parameters.Add(new("@PageNum", pageNum));
                cmd.Parameters.Add(new("@PageSize", pageSize));

                await connection.OpenAsync();

                var response = new List<Classification>();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        response.Add(MapToResult(reader));
                }

                return response;
            }
        }

        public async Task<List<Classification>> GetAllForReportAsync(Guid reportId, int pageNum, int pageSize)
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
                var response = new List<Classification>();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        response.Add(MapToResult(reader));
                }

                return response;
            }
        }

        public async override Task UpdateAsync(Classification entity)
        {
            var procedure = $"proc_{_tableName}_{StoredProcedureMethod.Update.ToString()}";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new("@Id", entity.Id));
                cmd.Parameters.Add(new("@Title", entity.Title));

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        protected override Classification MapToResult(SqlDataReader reader, params string[] fields)
        {
            return new()
            {
                Id = (Guid)reader[$"{nameof(Classification.Id)}"],
                Title = reader[$"{nameof(Classification.Title)}"].ToString() ?? string.Empty,
                UpdatedAt = (DateTime)reader[$"{nameof(Classification.UpdatedAt)}"],
                CreatedAt = (DateTime)reader[$"{nameof(Classification.CreatedAt)}"],
            };
        }
    }
}