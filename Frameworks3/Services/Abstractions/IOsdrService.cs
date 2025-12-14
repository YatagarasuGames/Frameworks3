namespace Frameworks3.Services.Abstractions
{
    public interface IOsdrService
    {
        Task<int> FetchAndStoreAsync(CancellationToken ct = default);
    }
}