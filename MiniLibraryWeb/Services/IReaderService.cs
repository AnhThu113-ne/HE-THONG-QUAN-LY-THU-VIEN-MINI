using MiniLibraryWeb.Models.Entities;

namespace MiniLibraryWeb.Services
{
    public interface IReaderService
    {
        Task<IEnumerable<Reader>> GetAllReadersAsync();
        Task<Reader?> GetReaderByIdAsync(int id);
        Task<Reader?> GetReaderWithHistoryAsync(int id);
        Task AddReaderAsync(Reader reader);
        Task UpdateReaderAsync(Reader reader);
        Task DeleteReaderAsync(int id);
    }
}
