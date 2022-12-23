using Core.Constants;
using CSARN.SharedLib.Constants.CustomExceptions;
using System.Net;
using System.Text.Json;

namespace Web.Middlewares
{
    public class GlobalErrorsHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorsHandler> _logger;

        public GlobalErrorsHandler(RequestDelegate next, ILogger<GlobalErrorsHandler> logger)
        {
            _next = next;
            _logger = logger;
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
                        _logger.Log(LogLevel.Critical, LogEvents.ExceptionForm, exception.GetType().Name, exception.Message);
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
