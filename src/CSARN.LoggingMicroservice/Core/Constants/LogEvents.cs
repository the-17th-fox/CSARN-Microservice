using CSARN.SharedLib.Constants;

namespace Core.Constants
{
    public abstract class LogEvents : CommonLoggingForms
    {
        public const string LogsRetrievingAttempt = "The user [{userId}] is trying to retrieve logs from the database";
        public const string LogsRetrieved = "The user [{userId}] has successfully retrieved logs from the database";
        public const string DbConnectionEstablished = "Connection to the database [{params}] has been successfully established";

        public const string MessageConsumed = "Message [{messageId}] consumed from [{producer}]: [{logLevel}] | [{message}] | [{args}].";
        public const string MessageProcessed = "Message [{messageId}] has been successfully logged.";
    }
}
