using Frameworks3.DTO;
using Frameworks3.Helpers;
using Frameworks3.Models.Entities;
using Frameworks3.Repositories.Abstractions;
using Frameworks3.Services.Abstractions;
using System.Text.Json;

namespace Frameworks3.Services
{
    public class IssService : IIssService
    {
        private readonly HttpClient _httpClient;
        private readonly IIssRepository _repository;
        private const string _IssApiUrl = "https://api.wheretheiss.at/v1/satellites/25544";

        public IssService(HttpClient httpClient, IIssRepository repository)
        {
            _httpClient = httpClient;
            _repository = repository;
        }

        public async Task<string?> FetchCurrentAsync()
        {
            var response = await _httpClient.GetStringAsync(_IssApiUrl);

            if (!string.IsNullOrWhiteSpace(response))
            {
                await _repository.AddAsync(new IssFetchLog
                {
                    SourceUrl = _IssApiUrl,
                    Payload = response,
                    FetchedAt = DateTime.UtcNow
                });
            }

            return response;
        }

        public async Task<IssFetchLog?> GetLastAsync()
        {
            return await _repository.GetLastAsync();
        }

        public async Task<object> GetTrendAsync()
        {
            var logs = await _repository.GetLastNAsync(2);
            if (logs.Count < 2) return new { Movement = false };

            var from = JsonSerializer.Deserialize<IssPosition>(logs[1].Payload)!;
            var to = JsonSerializer.Deserialize<IssPosition>(logs[0].Payload)!;

            var deltaKm = MathHelper.Haversine(from.Latitude, from.Longitude, to.Latitude, to.Longitude);
            var dtSec = (logs[0].FetchedAt - logs[1].FetchedAt).TotalSeconds;

            return new
            {
                Movement = deltaKm > 0.1,
                DeltaKm = deltaKm,
                DtSec = dtSec,
                VelocityKmh = to.Velocity,
                FromTime = logs[1].FetchedAt,
                ToTime = logs[0].FetchedAt,
                FromLat = from.Latitude,
                FromLon = from.Longitude,
                ToLat = to.Latitude,
                ToLon = to.Longitude
            };
        }
    }
}
