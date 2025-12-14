using Frameworks3.Models.Entities;
using Frameworks3.Models.Options;
using Frameworks3.Repositories.Abstractions;
using Microsoft.Extensions.Options;

namespace Frameworks3.BackgroundServices
{
    public class IssBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl;
        private readonly int _fetchIntervalSeconds;

        public IssBackgroundService(IServiceScopeFactory scopeFactory, IHttpClientFactory httpClientFactory, IOptions<ApiUrls> urls, IOptions<FetchTimes> times)
        {
            this._scopeFactory = scopeFactory;
            this._httpClientFactory = httpClientFactory;
            _apiUrl = urls.Value.IssUrl;
            _fetchIntervalSeconds = times.Value.Iss;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<IIssRepository>();
                    var spacerepository = scope.ServiceProvider.GetRequiredService<ISpaceCacheRepository>();
                    var httpClient = _httpClientFactory.CreateClient();

                    var response = await httpClient.GetStringAsync(_apiUrl);

                    if (!string.IsNullOrWhiteSpace(response))
                    {
                        await repository.AddAsync(new IssFetchLog
                        {
                            SourceUrl = _apiUrl,
                            Payload = response,
                            FetchedAt = DateTime.UtcNow
                        });
                        await spacerepository.AddAsync(new SpaceCache
                        {
                            Source = "iss",
                            Payload = response,
                            FetchedAt = DateTime.UtcNow
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении ISS: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(_fetchIntervalSeconds), stoppingToken);
            }
        }
    }
}
