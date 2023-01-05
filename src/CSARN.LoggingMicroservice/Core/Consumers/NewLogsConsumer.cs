using Core.Constants;
using Core.Interfaces.Services;
using Core.ViewModels;
using CSARN.SharedLib.MessageBroker;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Core.Consumers
{
    public class NewLogsConsumer : IConsumer<LogRecordSent>
    {
        private readonly ILogger<NewLogsConsumer> _logger;
        private readonly ILogsService _logsSvc;

        public NewLogsConsumer(ILogger<NewLogsConsumer> logger, ILogsService logsSvc)
        {
            _logger = logger;
            _logsSvc = logsSvc;
        }

        public async Task Consume(ConsumeContext<LogRecordSent> context)
        {
            var logLevel = context.Message.Level;
            var msg = context.Message.Message;
            var args = context.Message.Args;
            var producerSvc = context.Message.ProducerService;

            _logger.LogInformation(LogEvents.MessageConsumed, context.MessageId, producerSvc, logLevel, msg, args);



            //await _logsSvc.AddAsync(new()
            //{
            //    ProducerService = producerSvc ?? "Unknown",
            //    Level= logLevel,
            //    Message = msg,
            //    Args = args
            //});

            _logger.LogInformation(LogEvents.MessageProcessed, context.MessageId);
        }
    }

    public class NewLogsConsumerDefinition : ConsumerDefinition<NewLogsConsumer>
    {
        public NewLogsConsumerDefinition()
        {
            Endpoint(opt =>
            {
                opt.Name = "logging-msvc-new-logs-consumer";
                opt.Temporary = false;
            });
        }
    }
}
