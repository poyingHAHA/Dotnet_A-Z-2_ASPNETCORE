using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo
{
    public class LogExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            return File.AppendAllTextAsync("d:/error.log", context.Exception.ToString());
        }
    }
}
