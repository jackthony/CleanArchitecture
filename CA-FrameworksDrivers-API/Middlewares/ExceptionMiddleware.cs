using CA_ApplicationLayer.Exceptions;
using System.Net;
using System.Text.Json;

namespace CA_FrameworksDrivers_API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        /*private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //_logger.LogError(exception, "An unhandled exception occurred while processing the request.");

            var response = context.Response;
            response.ContentType = "application/json";
            
            var statusCode = HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new
            {
                StatusCode = (int)statusCode,
                Message = "Internal Server Error. Please try again later."
            });
            
            response.StatusCode = (int)statusCode;
            await response.WriteAsync(result);

        }*/

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var errorMessage = $"[{DateTime.Now}] {exception.Message} {Environment.NewLine}{exception.StackTrace}{Environment.NewLine}";

            var logFilePath = Path.Combine(AppContext.BaseDirectory, "Logs", "exceptions.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)!);
            await File.AppendAllTextAsync(logFilePath, errorMessage);

            var result = JsonSerializer.Serialize(new
            {
                StatusCode = (int)statusCode,
                Message = "Internal Server Error. Please try again later."
            });

            response.StatusCode = (int)statusCode;
            await response.WriteAsync(result);
        }

    }
}
