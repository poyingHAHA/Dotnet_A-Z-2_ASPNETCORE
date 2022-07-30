var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddLogging();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "127.0.0.1:6379";
    options.InstanceName = "test1_";
});

var app = builder.Build();

Console.WriteLine(app.Environment.EnvironmentName);
Console.WriteLine(app.Environment.IsDevelopment());
Console.WriteLine(app.Environment.IsProduction());

string pass = app.Configuration.GetSection("testPassword").Value;
Console.WriteLine(pass);


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
