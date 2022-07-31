using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo
{
    public class MyExceptionFilter : IAsyncExceptionFilter
    {
        // 注入服務判斷是否為開發環境，是的才用這個Filter
        private readonly IWebHostEnvironment hostEnv;

        public MyExceptionFilter(IWebHostEnvironment hostEnv)
        {
            this.hostEnv = hostEnv;
        } 

        public Task OnExceptionAsync(ExceptionContext context)
        {
            // - context.Exception 發生異常的異常對象信息
            // - context.ExceptionHandled賦值為true，則其他ExceptionFilter不會再執行
            // - context.Result 的值會被輸出給客戶端
            string msg;
            if(hostEnv.IsDevelopment())
            {
                msg = context.Exception.ToString();
            }
            else
            {
                msg = "未發生異常";
            }

            ObjectResult objResult = new ObjectResult(new {code=500, message=msg}); // objectResult會自動序列化為Json string
            context.Result = objResult;
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
