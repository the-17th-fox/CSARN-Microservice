using Core.Constants;
using Core.Utilities;
using CSARN.SharedLib.Constants.CustomExceptions;
using System.Net;
using System.Text.Json;

namespace Web.Middlewares
{
    public class GlobalErrorsHandler
    {
        private readonly RequestDelegate _next;
        private readonly LogsHandler<GlobalErrorsHandler> _logsHandler;

        public GlobalErrorsHandler(RequestDelegate next, ILogger<GlobalErrorsHandler> logger)
        {
            _next = next;
            _logsHandler = new(logger);
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (exception)
                {
                    case NotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case InvalidParamsException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        _logsHandler.Log(LogLevel.Critical, LogEvents.ExceptionForm, exception.GetType().Name, exception.Message);
                        break;
                }

                var exceptionResponse = new
                {
                    exceptionType = exception.GetType().Name,
                    statusCode = response.StatusCode,
                    message = exception.Message,
                };

                var result = JsonSerializer.Serialize(exceptionResponse);
                await response.WriteAsync(result);
            }
        }
    }
}
