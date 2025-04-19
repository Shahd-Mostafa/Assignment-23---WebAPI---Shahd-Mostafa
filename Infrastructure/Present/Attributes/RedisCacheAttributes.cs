using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Present.Attributes
{
    public class RedisCacheAttributes : ActionFilterAttribute
    {
        private readonly TimeSpan duration;
        public RedisCacheAttributes(int seconds, int minutes=0, int days = 0)
        {
            duration = TimeSpan.FromSeconds(seconds);
            duration = minutes !=0 ? duration.Add(TimeSpan.FromMinutes(minutes)): duration;
            duration=  days != 0 ? duration.Add(TimeSpan.FromDays(days)):duration;

        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().cacheServices;
            var cacheKey = GenerateCacheKey(context.HttpContext.Request);
            var cachedResponse = await cacheService.GetCachedItem(cacheKey);
            if (!string.IsNullOrEmpty(cachedResponse))
            {
                context.Result = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }
            var contextResult = await next.Invoke();
            if (contextResult.Result is OkObjectResult OkObject)
            {
                await cacheService.SetCacheValue(cacheKey, OkObject, TimeSpan.FromMinutes(5));
            }
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(request.Path);
            foreach(var query in request.Query.OrderBy(q=>q.Key))
            {
                stringBuilder.Append($"|{query.Key}-{query.Value}");
            }
            return stringBuilder.ToString();

        }
    }
}
