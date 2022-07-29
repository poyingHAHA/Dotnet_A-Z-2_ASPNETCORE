using Cache1.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Cache1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<TestController> logger;

        public TestController(ILogger<TestController> logger, IMemoryCache memoryCache)
        {
            this.logger = logger;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<ActionResult<Book>> GetBookByIdAsync(long id)
        {
            Console.WriteLine("開始執行");

            // 1) 從緩存取數據 2)沒有的話到資料源取數據
            Book? b = await memoryCache.GetOrCreateAsync("Book"+id, async(e) =>{
                Console.WriteLine("還存沒有，到數據庫查");
                // e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10); // 緩存10秒
                e.SlidingExpiration = TimeSpan.FromSeconds(10);
                return await MyDbContext.GetByIdAsync(id);
            });

            Console.WriteLine($"緩存結果{b}");

            if(b == null)
            {
                return NotFound($"{id} not found");
            }
            return Ok(b);
        }
    }
}
