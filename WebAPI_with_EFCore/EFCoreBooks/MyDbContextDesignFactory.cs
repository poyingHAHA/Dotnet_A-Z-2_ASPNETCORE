using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBooks
{
    public class MyDbContextDesignFactory : IDesignTimeDbContextFactory<MyDbContext>
    {

        // 開發時(Add-Migration、Update-Database)運行，項目運行時不會有這個類的事
        public MyDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MyDbContext> builder = new DbContextOptionsBuilder<MyDbContext>();

            //builder.UseSqlServer(Environment.GetEnvironmentVariable("ConnStr"));

            string connStr = "Server=localhost;Database=demo666;User Id=sa;Password=PaSSword12!;Trusted_Connection=False;MultipleActiveResultSets=true;";
            builder.UseSqlServer(connStr);

            MyDbContext ctx = new MyDbContext(builder.Options);

            return ctx;
        }
    }
}
