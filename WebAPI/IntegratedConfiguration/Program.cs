using IntegratedConfiguration;
using StackExchange.Redis;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// �t�mMSSQL
var webBuilder = builder.WebHost;
webBuilder.ConfigureAppConfiguration((hostCtx, configBuilder) => {
    string connStr = builder.Configuration["ConnStr"];
    configBuilder.AddDbConfiguration(() => new SqlConnection(connStr),
        reloadOnChange: true, reloadInterval: TimeSpan.FromSeconds(2));
});

// Redis�t�m
builder.Services.AddSingleton<IConnectionMultiplexer>( sp =>
{
    // �bProgram.csŪ���t�m����k
    string connstr = builder.Configuration.GetSection("Redis").Value;
    return ConnectionMultiplexer.Connect(connstr);
});

// smtp�t�m
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));


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
