using Core.Constants;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.ViewModels;
using CSARN.SharedLib.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly IMongoCollection<LoggingRecord> _logsColl;
        private readonly ILogger<LoggingRepository> _logger = null!;

        public LoggingRepository(IOptions<LoggingDbConfiguration> options, ILogger<LoggingRepository> logger)
        {
            _logger = logger;
            var config = options.Value;

            CheckDbConfiguration(config.ConnectionString, config.CollectionName);

            MongoUrl mongoUrl = new(config.ConnectionString);
            MongoClient mongoClient = new(mongoUrl);

            var logsDb = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _logsColl = logsDb.GetCollection<LoggingRecord>(config.CollectionName);
            _logger.LogInformation(LogEvents.DbConnectionEstablished, config.CollectionName);
        }

        private static void CheckDbConfiguration(string connectionString, string collectionName)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }
        }

        public async Task<PagedList<LoggingRecord>> GetAllAsync(SearchParametersViewModel searchParams)
        {
            var fromDate = new DateTime(
                year: searchParams.FromYear,
                month: searchParams.FromMonth,
                day: searchParams.FromDay);

            var logLevelFilter = Builders<LoggingRecord>.Filter
                .Eq(nameof(LoggingRecord.LogLevel), Enum.GetName<LogLevel>(searchParams.LowestLoggingLevel));

            var dateFilter = Builders<LoggingRecord>.Filter
                .Gte(nameof(LoggingRecord.UtcTimestamp), $"{fromDate:u}");

            var sort = Builders<LoggingRecord>.Sort.Descending(nameof(LoggingRecord.UtcTimestamp));

            var logs = _logsColl
                .Find(Builders<LoggingRecord>.Filter.And(logLevelFilter, dateFilter))
                .Sort(sort);

            return await PagedList<LoggingRecord>.ToPagedListAsync(logs, searchParams.PageNumber, searchParams.PageSize);
        }
    }
}
