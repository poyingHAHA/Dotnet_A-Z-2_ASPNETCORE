using EFCoreBooks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_with_EFCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Test1Controller : ControllerBase
    {
        private readonly MyDbContext dbCtx;

        public Test1Controller(MyDbContext dbCtx)
        {
            this.dbCtx = dbCtx;
        }


        [HttpGet]
        public string Demo1()
        {
            int c = dbCtx.Books.Count();
            return $"c={c}";
        }
    }
}
