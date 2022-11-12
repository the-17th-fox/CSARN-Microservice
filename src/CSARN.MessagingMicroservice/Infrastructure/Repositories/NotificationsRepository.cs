using Domain;
using DomainServiceAbstractions;
using Infrastructure.Utilities;
using Microsoft.Extensions.Options;
using SharedLib.MessagingMsvc.Models;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class NotificationsRepository : BaseRepository<Notification, Guid>, INotificationsRepository
    {
        public NotificationsRepository(IOptions<RepositoryConfiguration> configuration) 
            : base(configuration, TableName.Notifications.ToString()) {}

        public async override Task CreateAsync(Notification entity)
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
                    new("@Organization", entity.Organization),
                    new("@AccountId", entity.AccountId),
                    new("@TargetAccountId", entity.TargetAccountId)
                });

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<Notification>> GetAllForAccountAsync(Guid accountId, int pageNum, int pageSize)
        {
            var procedure = $"proc_{_tableName}_GetAllForAccount";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new("@AccountId", accountId));
                cmd.Parameters.Add(new("@PageNum", pageNum));
                cmd.Parameters.Add(new("@PageSize", pageSize));

                await connection.OpenAsync();
                var response = new List<Notification>();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        response.Add(MapToResult(reader));
                }

                return response;
            }
        }

        public async Task RemoveClassificationAsync(Guid notificationId, Guid classificationId)
        {
            var tableName = TableName.NotificationsClassifications.ToString();
            var procedure = $"proc_{tableName}_{StoredProcedureMethod.Delete}";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new("@NotificationId", notificationId));
                cmd.Parameters.Add(new("@ClassificationId", classificationId));

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task AddClassificationAsync(Guid notificationId, Guid classificationId)
        {
            var tableName = TableName.NotificationsClassifications.ToString();
            var procedure = $"proc_{tableName}_{StoredProcedureMethod.Create.ToString()}";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new("@NotificationId", notificationId));
                cmd.Parameters.Add(new("@ClassificationId", classificationId));

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async override Task UpdateAsync(Notification entity)
        {
            var procedure = $"proc_{_tableName}_{StoredProcedureMethod.Update.ToString()}";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddRange(new SqlParameter[]
                {
                    new("@Header", entity.Id),
                    new("@Header", entity.Header),
                    new("@Body", entity.Body),
                    new("@Organization", entity.Organization),
                    new("@AccountId", entity.AccountId),
                    new("@TargetAccountId", entity.TargetAccountId)
                });

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        protected override Notification MapToResult(SqlDataReader reader, params string[] fields)
        {
            return new()
            {
                Id = (Guid)reader[$"{nameof(Notification.Id)}"],
                Header = reader[$"{nameof(Notification.Header)}"].ToString() ?? string.Empty,
                Body = reader[$"{nameof(Notification.Body)}"].ToString() ?? string.Empty,
                Organization = reader[$"{nameof(Notification.Organization)}"].ToString() ?? string.Empty,
                AccountId = (Guid)reader[$"{nameof(Notification.AccountId)}"],
                TargetAccountId = (Guid)reader[$"{nameof(Notification.TargetAccountId)}"],
                UpdatedAt = (DateTime)reader[$"{nameof(Notification.UpdatedAt)}"],
                CreatedAt = (DateTime)reader[$"{nameof(Notification.CreatedAt)}"],
            };
        }
    }
}
