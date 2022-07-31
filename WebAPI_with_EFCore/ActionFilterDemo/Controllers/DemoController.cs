using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace ActionFilterDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DemoController : Controller
    {
        private readonly MyDbContext _context;

        public DemoController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [NotTransaction]
        public string Test1()
        {
            //_context.Books.Add(new Book { Name = "jj", Price = 100 });
            //_context.SaveChanges(); // 一個tranction
            //_context.Persons.Add(new Person { Name = "popopo", Age=18});
            //_context.SaveChanges(); // 一個tranction，一個插入失敗就全部rollback

            // 只要有錯不管有無savechange直接rollback，如果使用Async就要加TransactionScopeAsyncFlowOption.Enabled
            using (TransactionScope tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _context.Books.Add(new Book { Name = "bb", Price = 100 });
                _context.SaveChangesAsync(); // 一個tranction
                _context.Persons.Add(new Person { Name = "po", Age = 18 });
                _context.SaveChangesAsync(); // 一個tranction
                tx.Complete();
                return "Ok";
            }

        }

        [HttpGet]
        public async Task<string> Test2()
        {
            _context.Books.Add(new Book { Name = "bb", Price = 100 });
            _context.SaveChangesAsync(); // 一個tranction
            _context.Persons.Add(new Person { Name = "po", Age = 18 });
            _context.SaveChangesAsync(); // 一個tranction
            return "Ok";
        }
    }
}
