using MassTransit;
using Microsoft.Extensions.Logging;

namespace CSARN.SharedLib.MessageBroker.Logging
{
    public static class PublisherExtenstions
    {
        public async static Task LogAsync (this IPublishEndpoint publishEndpoint, string producerSvc, LogLevel level, string message, params string[] args)
        {
            await publishEndpoint.Publish<LogRecordSent>(new
            {
                ProducerService = producerSvc,
                Level = level,
                Message = message,
                Args = args
            });
        }

        public async static Task LogInformationAsync(this IPublishEndpoint publishEndpoint, string producerSvc, string message, params string[] args)
        {
            await LogAsync(publishEndpoint, producerSvc, LogLevel.Information, message, args);
        }
    }
}
