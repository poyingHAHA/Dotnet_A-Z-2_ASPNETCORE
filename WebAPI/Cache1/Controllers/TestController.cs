using Cache1.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Cache1.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : Controller
    {
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<TestController> logger;
        private readonly IDistributedCache distCache;

        public TestController(ILogger<TestController> logger, IMemoryCache memoryCache, IDistributedCache distCache)
        {
            this.logger = logger;
            this.memoryCache = memoryCache;
            this.distCache = distCache;
        }

        [HttpGet]
        public async Task<ActionResult<Book>> GetBookByIdAsync(long id)
        {
            Console.WriteLine("開始執行");

            // 1) 從緩存取數據 2)沒有的話到資料源取數據
            Book? b = await memoryCache.GetOrCreateAsync("Book"+id, async(e) =>{
                Console.WriteLine("緩存沒有，到數據庫查");
                // e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10); // 緩存10秒
                
                Book? d = await MyDbContext.GetByIdAsync(id);
                Console.WriteLine("從數據庫中查詢的結果是"+(d==null?"null":d));

                return d;
            });

            Console.WriteLine($"緩存結果{b}");

            if(b == null)
            {
                return NotFound($"{id} not found");
            }
            return Ok(b);
        }

        [HttpGet]
        public async Task<ActionResult<Book?>> Test2(long id)
        {
            Book? book;
            string? s = await distCache.GetStringAsync("Book"+id);

            Console.WriteLine("開始執行");
            if (s == null)
            {
                Console.WriteLine("緩存沒有，到數據庫查");
                book = await MyDbContext.GetByIdAsync(id);
                // 將object序列化保存
                await distCache.SetStringAsync("Book" + id, JsonSerializer.Serialize(book));
                Console.WriteLine("從數據庫中查詢的結果是" + (book == null ? "null" : book));
            }
            else
            {
                book = JsonSerializer.Deserialize<Book?>(s);
            }

            Console.WriteLine($"緩存結果{book}");

            if (book == null)
            {
                return NotFound($"{id} not found");
            }
            return Ok(book);
        }
    }
}
