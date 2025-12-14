using Frameworks3.Models.Entities;

namespace Frameworks3.Services.Abstractions
{
    public interface IIssService
    {
        Task<string?> FetchCurrentAsync();
        Task<IssFetchLog?> GetLastAsync();
        Task<object> GetTrendAsync();
    }
}