using Core.Constants;
using Core.DomainServicesAbstractions;
using Core.Models;
using Core.Utilities;
using Core.ViewModels;
using CSARN.SharedLib.Constants.CustomExceptions;
using CSARN.SharedLib.Utilities;
using CSARN.SharedLib.ViewModels.Pagination;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text;

namespace Infrastructure
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly IMongoCollection<LoggingRecord> _logsColl;
        private readonly LogsHandler<LoggingRepository> _logsHandler = null!;

        public LoggingRepository(IOptions<LoggingDbConfiguration> options, ILogger<LoggingRepository> logger)
        {
            _logsHandler = new(logger);
            var config = options.Value;

            CheckDbConfiguration(config.ConnectionString, config.CollectionName);

            MongoUrl mongoUrl = new(config.ConnectionString);
            MongoClient mongoClient = new(mongoUrl);

            var logsDb = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _logsColl = logsDb.GetCollection<LoggingRecord>(config.CollectionName);
            _logsHandler.Log(LogLevel.Information, LogEvents.DbConnectionEstablished, config.CollectionName);
        }

        private void CheckDbConfiguration(string connectionString, string collectionName)
        {
            bool isCorrect = true;
            var sb = new StringBuilder();
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                isCorrect = false;
                sb.Append("ConnectionString;");
            }

            if (string.IsNullOrWhiteSpace(collectionName))
            {
                isCorrect = false;
                sb.Append("CollectionName;");
            }

            if(!isCorrect)
            {
                var argument = sb.ToString();
                _logsHandler.Log(LogLevel.Error, LogEvents.RequiredParamsMissed, argument);
                throw new InvalidParamsException($"Required parameters haven't been provided: [{argument}]");
            }
        }

        public async Task<PagedList<LoggingRecord>> GetAllAsync(
            SearchParametersViewModel searchParams, PageParametersViewModel pageParams)
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

            return await PagedList<LoggingRecord>.ToPagedListAsync(logs, pageParams.PageNumber, pageParams.PageSize);
        }
    }
}
