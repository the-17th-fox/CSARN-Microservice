using CSARN.SharedLib.Constants;

namespace Core.Constants
{
    public abstract class LogEvents : CommonLoggingForms
    {
        public const string LogsRetrievingAttempt = "The user [{userId}] is trying to retrieve logs from the database";
        public const string LogsRetrieved = "The user [{userId}] has successfully retrieved logs from the database";
        public const string DbConnectionEstablished = "Connection to the database [{params}] has been successfully established";
    }
}
