using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace ActionFilterDemo
{
    public class RateLimitActionFilter : IAsyncActionFilter
    {
        private readonly IMemoryCache memCache;

        public RateLimitActionFilter(IMemoryCache memCache)
        {
            this.memCache = memCache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string ip = context.HttpContext.Connection.RemoteIpAddress.ToString(); // 獲取IP地址
            string? actionName = context.ActionDescriptor.DisplayName;
            string cacheKey = $"{actionName}_lastvisittick_{ip}";
            long? lastVisit = memCache.Get<long?>(cacheKey);
            
            if(lastVisit == null || (Environment.TickCount64 - lastVisit) > 1000) // 一個Action一秒只能訪問一次
            {
                memCache.Set(cacheKey, Environment.TickCount64, TimeSpan.FromSeconds(10)); // 避免長期不訪問的用戶長期占用內存
                await next();
            }
            else
            {
                ObjectResult result = new ObjectResult("訪問太頻繁") { StatusCode = 429 };
                context.Result = result;
            }
        }
    }
}
