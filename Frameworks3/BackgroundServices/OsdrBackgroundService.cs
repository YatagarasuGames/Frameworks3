using Frameworks3.Models.Options;
using Frameworks3.Services.Abstractions;
using Microsoft.Extensions.Options;

namespace Frameworks3.BackgroundServices
{
    public class OsdrBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly int _intervalSeconds;

        public OsdrBackgroundService(IServiceScopeFactory scopeFactory, IOptions<FetchTimes> times)
        {
            this._scopeFactory = scopeFactory;
            _intervalSeconds = times.Value.Osdr;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var service = scope.ServiceProvider.GetRequiredService<IOsdrService>();
                    var written = await service.FetchAndStoreAsync(stoppingToken);
                    Console.WriteLine($"[OSDR] Updated {written} items.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[OSDR] Error: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(_intervalSeconds), stoppingToken);
            }
        }
    }
}
