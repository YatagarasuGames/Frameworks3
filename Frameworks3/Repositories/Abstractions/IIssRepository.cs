using Frameworks3.Models.Entities;

namespace Frameworks3.Repositories.Abstractions
{
    public interface IIssRepository
    {
        Task<IssFetchLog> AddAsync(IssFetchLog log);
        Task<IssFetchLog?> GetLastAsync();
        Task<List<IssFetchLog>> GetLastNAsync(int n);
    }
}