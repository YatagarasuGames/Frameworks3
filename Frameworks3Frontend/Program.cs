using DotNetEnv;
using Frameworks3.Models;
using Frameworks3.Models.Options;
using Frameworks3.Repositories;
using Frameworks3.Repositories.Abstractions;
using Frameworks3.Services;
using Frameworks3.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

var dbHost = builder.Configuration["db_host"] ?? "";
var dbPort = builder.Configuration["db_port"] ?? "";
var dbName = builder.Configuration["db_name"] ?? "";
var dbUser = builder.Configuration["db_user"] ?? "";
var dbPassword = builder.Configuration["db_password"] ?? "";
var connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword}";

var redisHost = builder.Configuration["redis_host"] ?? "";
var redisPort = builder.Configuration["redis_port"] ?? "";

builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
    ConnectionMultiplexer.Connect($"{redisHost}:{redisPort}"));

builder.Services.Configure<ApiUrls>(builder.Configuration.GetSection("Urls"));
builder.Services.Configure<FetchTimes>(builder.Configuration.GetSection("Time"));
builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connectionString));
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IAstronomyService, AstronomyService>();
builder.Services.AddScoped<IIssRepository, IssRepository>();
builder.Services.AddScoped<ISpaceCacheRepository, SpaceCacheRepository>();
builder.Services.AddScoped<IOsdrRepository, OsdrRepository>();
builder.Services.AddScoped<IIssService, IssService>();
builder.Services.AddScoped<IOsdrService, OsdrService>();
builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();
builder.Services.AddScoped<ITelemetryRepository, TelemetryRepository>();
builder.Services.AddScoped<IAstronomyService, AstronomyService>();


builder.Services.AddRazorPages();
builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();