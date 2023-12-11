using Microsoft.Extensions.Logging;
using QuizAPI.Exceptions;
using System.Net;

namespace QuizAPI.CustomMiddlewares
{
    public class GlobalExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;   
        }
        public ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";
            var response = httpContext.Response;
            switch (exception)
            {
                case ResourceNotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    response.WriteAsync(ex.Message);
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.WriteAsync("Internal server error!");
                    break;
            }
            var exceptionMessage = exception.Message;
            _logger.LogError(
                "Error Message: {exceptionMessage}, Time of occurrence {time}",
                exceptionMessage, DateTime.UtcNow);
            // Return false to continue with the default behavior
            // - or - return true to signal that this exception is handled
            return ValueTask.FromResult(false);
        }
    }
}

