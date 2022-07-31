using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBooks
{
    public class MyDbContext:DbContext
    {
        public DbSet<Book> Books { get; set; }

        // 直接將options傳給父類的構造函數
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {
        }

        /* 在WebAPI_with_EFCore中配置 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // 這樣直接寫死要連接的數據庫不好，因為一個項目可能用不只一個數據庫
            // 盡量讓用什麼數據庫推遲到ASP.NET webAPI中再決定。
            //optionsBuilder.UseSqlServer(......)
        }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Assembly.GetEntryAssembly()獲取入口程序集，也就是WebAPI_with_EFCore這個程序集
            // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetEntryAssembly());

            // this.GetType().Assembly獲取當前程序集
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
