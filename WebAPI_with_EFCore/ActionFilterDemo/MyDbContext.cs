using Microsoft.EntityFrameworkCore;

namespace ActionFilterDemo
{
    public class MyDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> Persons { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> opt)
            : base(opt) 
        {

        }
    }
}
