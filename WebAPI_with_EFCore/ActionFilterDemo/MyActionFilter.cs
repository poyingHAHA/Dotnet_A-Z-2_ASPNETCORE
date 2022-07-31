using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilterDemo
{
    public class MyActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, 
            ActionExecutionDelegate next)
        {
            Console.WriteLine("MyActionFilter1 開始執行");
            ActionExecutedContext result = await next();

            if(result.Exception != null)
            {
                Console.WriteLine("MyActionFilter1: 發生異常");
            }
            else
            {
                Console.WriteLine("MyActionFilter1: 執行成功");
            }
        }
    }
}
