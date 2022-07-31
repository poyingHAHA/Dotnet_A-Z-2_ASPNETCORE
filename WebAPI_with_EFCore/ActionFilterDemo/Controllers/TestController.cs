using Microsoft.AspNetCore.Mvc;

namespace ActionFilterDemo.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : Controller
    {
        [HttpGet]
        public string test1()
        {
            Console.WriteLine("Action 執行中");
            return "HAHA";
        }
    }
}
