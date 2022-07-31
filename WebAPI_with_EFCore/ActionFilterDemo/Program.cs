using ActionFilterDemo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.Configure<MvcOptions>(opt =>
{
    opt.Filters.Add<RateLimitActionFilter>(); // 這個放前面一點先執行，一旦攔截後面就不用再做了
    opt.Filters.Add<MyActionFilter2>();
    opt.Filters.Add<MyActionFilter>();
    opt.Filters.Add<TransactionScopeFilter>();
});

builder.Services.AddDbContext<MyDbContext>(opt => {
    opt.UseSqlServer("Server=localhost;Database=demo7;User Id=sa;Password=PaSSword12!;Trusted_Connection=False;MultipleActiveResultSets=true;");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
