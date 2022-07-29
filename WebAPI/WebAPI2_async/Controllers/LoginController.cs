using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebAPI2_async.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public LoginResponse Login(LoginRequest req)
        {
            if(req.UserName == "admin" && req.Password == "123456")
            {
                // 獲得當前電腦所有進程訊息
                var items = Process.GetProcesses().Select(p => 
                    new ProcessInfo(p.Id, p.ProcessName, p.WorkingSet64)).ToArray();
                return new LoginResponse(true, items);
            }
            else
            {
                return new LoginResponse(false, null);
            }
        }
    }

    public record LoginRequest(string UserName, string Password);
    public record ProcessInfo(long Id, string Name, long WorkingSet);
    public record LoginResponse(bool Ok, ProcessInfo[]? ProcessInfos);
}
