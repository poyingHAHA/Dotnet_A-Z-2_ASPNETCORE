using Microsoft.AspNetCore.Mvc;
using WebAPI2_async.Services;

namespace WebAPI2_async.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : Controller
    {
        public TestController()
        {
        }

        [HttpGet]
        public int FileCount([FromServices]BigService bigService )
        {
            return bigService.Coutnt;
        }

        [HttpGet]
        public int TestCount()
        {
            return 5;
        }
    }
}
