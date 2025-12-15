using DotNetEnv;
using Frameworks3.BackgroundServices;
using Frameworks3.Extensions;
using Frameworks3.Helpers;
using Frameworks3.Models;
using Frameworks3.Models.Options;
using Frameworks3.Repositories;
using Frameworks3.Repositories.Abstractions;
using Frameworks3.Services;
using Frameworks3.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text.Json;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

var dbHost = builder.Configuration["db_host"];
var dbPort = builder.Configuration["db_port"];
var dbName = builder.Configuration["db_name"];
var dbUser = builder.Configuration["db_user"];
var dbPassword = builder.Configuration["db_password"];
var connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword}";
builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connectionString));

var redisHost = builder.Configuration["redis_host"];
var redisPort = builder.Configuration["redis_port"];

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("http://localhost:5001")
            .WithOrigins("http://localhost:5002")
            .WithOrigins("http://localhost:5000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = false;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Frame3",
        Version = "v1"
    }));



builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
    ConnectionMultiplexer.Connect($"{redisHost}:{redisPort}"));

builder.Services.Configure<ApiUrls>(builder.Configuration.GetSection("Urls"));
builder.Services.Configure<FetchTimes>(builder.Configuration.GetSection("Time"));

builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IIssService, IssService>();
builder.Services.AddHttpClient<IAstronomyService, AstronomyService>();
builder.Services.AddScoped<IIssRepository, IssRepository>();
builder.Services.AddScoped<ISpaceCacheRepository, SpaceCacheRepository>();
builder.Services.AddHostedService<IssBackgroundService>();
builder.Services.AddHostedService<OsdrBackgroundService>();
builder.Services.AddScoped<IOsdrService, OsdrService>();
builder.Services.AddScoped<IOsdrRepository, OsdrRepository>();
builder.Services.AddScoped<ISpaceCacheService, SpaceCacheService>();
builder.Services.AddScoped<ITelemetryRepository, TelemetryRepository>();
builder.Services.AddScoped<IAstronomyService, AstronomyService>();
builder.Services.AddHttpClient<IssService>();
builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();
builder.Services.AddSingleton(sp =>
{
    var redis = sp.GetRequiredService<IConnectionMultiplexer>();
    return new RedisRateLimiter(redis, maxRequests: 2, window: TimeSpan.FromSeconds(1));
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Frame3 v1");
    c.RoutePrefix = string.Empty;
});

app.ApplyMigrations();
app.UseMiddleware<RateLimiterMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
