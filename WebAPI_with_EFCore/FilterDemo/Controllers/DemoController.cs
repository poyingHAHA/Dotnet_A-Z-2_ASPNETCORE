using Microsoft.AspNetCore.Mvc;

namespace FilterDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DemoController : Controller
    {
        [HttpGet]
        public string Test1()
        {
            string s = System.IO.File.ReadAllText("E:/test.txt");
            return s;
        }
    }
}
