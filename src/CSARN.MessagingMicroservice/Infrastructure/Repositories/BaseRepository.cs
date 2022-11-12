using SharedLib.Generics.Repositories;
using Domain;
using Infrastructure.Utilities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class
        where TKey : struct
    {
        protected readonly string _connectionString;
        protected readonly string _tableName;

        internal BaseRepository(IOptions<RepositoryConfiguration> configuration, string tableName)
        {
            _connectionString = configuration.Value.DatabaseConnection;
            _tableName = tableName;
        }

        protected abstract TEntity MapToResult(SqlDataReader reader, params string[] fields);
        public abstract Task CreateAsync(TEntity entity);
        public abstract Task UpdateAsync(TEntity entity);

        public async Task DeleteAsync(TKey id)
        {
            var procedure = $"proc_{_tableName}_{StoredProcedureMethod.Delete}";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new("@Id", id));

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<TEntity>> GetAllAsync(int pageNum, int pageSize)
        {
            var procedure = $"proc_{_tableName}_{StoredProcedureMethod.GetAll}";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new("@PageNum", pageNum));
                cmd.Parameters.Add(new("@PageSize", pageSize));

                await connection.OpenAsync();
                var response = new List<TEntity>();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        response.Add(MapToResult(reader));
                }

                return response;
            }
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            var procedure = $"proc_{_tableName}_{StoredProcedureMethod.GetById}";

            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new("@Id", id));

                await connection.OpenAsync();
                TEntity? response = null;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        response = MapToResult(reader);
                }

                return response;
            }
        }
    }
}
