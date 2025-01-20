namespace WebAPI.Middleware
{
    public class LoggingMiddleware
    {
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly RequestDelegate _request;
        public LoggingMiddleware(ILogger<LoggingMiddleware> logger,RequestDelegate request)
        {
            _logger = logger;
            _request = request;
        }
        public async Task InvokeAsync(HttpContext context) {
            _logger.LogInformation("This Middleware is calling");
            await _request(context);
        }
    }
}
