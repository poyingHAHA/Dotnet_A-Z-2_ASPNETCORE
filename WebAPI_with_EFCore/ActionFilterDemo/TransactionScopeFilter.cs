using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Transactions;

namespace ActionFilterDemo
{
    public class TransactionScopeFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // context.ActionDescriptor中是當前被執行的Action方法的描述信息
            // context.ActionArguments當前被執行的Action方法的參數信息
            ControllerActionDescriptor ctrlActionDesc = context.ActionDescriptor as ControllerActionDescriptor;

            // if(ctrlActionDesc != null) // 不是一個MVC的Action

            bool isTX = false; // 是否進行Transaction控制
            if(ctrlActionDesc != null)
            {
                // ctrlActionDesc.MethodInfo 當前的Action方法
                bool hasNotTransactionAttribute = ctrlActionDesc.MethodInfo.GetCustomAttributes(typeof(NotTransactionAttribute), false).Any();
                isTX = !hasNotTransactionAttribute;
            }

            if(isTX)
            {
                using(TransactionScope tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var r = await next();
                    if(r.Exception == null)
                    {
                        tx.Complete();
                    }
                }
            }
            else
            {
                await next();
            }
        }
    }
}
