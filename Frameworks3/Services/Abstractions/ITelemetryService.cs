namespace Frameworks3.Services.Abstractions
{
    public interface ITelemetryService
    {
        Task GenerateAndStoreAsync(CancellationToken ct = default);
    }
}