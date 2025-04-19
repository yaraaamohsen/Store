using Shared.ErrotModels;

namespace Store.Owner.Middlewares
{
    // Must Ends With "Middleware"
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2. Set Status Code For response
                // 3. Set Content Type Code For response
                // 4. Response Object
                // 5. Return Response

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = ex.Message
                };
                
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
