using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;

namespace Presentation.Attributes
{
    public class CacheAttribute(int durationInSeconds) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().cacheService;

            var CacheKey = GenerateCacheKey(context.HttpContext.Request);

            var result = await CacheService.GetCacheValueAsync(CacheKey);

            if (!string.IsNullOrEmpty(result))
            {
                context.Result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                    Content = result
                };
                return;
            }
            var ContextResult = await next.Invoke();

            if (ContextResult.Result is OkObjectResult okObject)
            {
                var serializedValue = JsonSerializer.Serialize(okObject.Value);
                await CacheService.SetCacheValueAsync(CacheKey, serializedValue, TimeSpan.FromSeconds(durationInSeconds));
            }
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var Key = new StringBuilder();
            Key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(q => q.Key))
            {
                Key.Append($"|{item.Key}-{item.Value}");
            }
            return Key.ToString();
        }
    }
}
