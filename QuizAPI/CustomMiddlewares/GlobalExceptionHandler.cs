using Microsoft.Extensions.Logging;
using QuizAPI.Exceptions;
using System.Net;

namespace QuizAPI.CustomMiddlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next ,ILogger<GlobalExceptionHandler> logger)
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
            catch (Exception ex)
            {
                // Handle the exception and generate a response
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "application/json";
            var response = context.Response;
            switch (exception)
            {
                case ResourceNotFoundException ex:
                    response.StatusCode = ex.StatusCode;
                    await response.WriteAsJsonAsync(ex.Message);
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await response.WriteAsJsonAsync("Internal server error!");
                    break;
            }
            var exceptionMessage = exception.Message;
            _logger.LogError(
                "Error Message: {exceptionMessage}, Time of occurrence {time}",
                exception.Message, DateTime.UtcNow);
        }
    }
}

