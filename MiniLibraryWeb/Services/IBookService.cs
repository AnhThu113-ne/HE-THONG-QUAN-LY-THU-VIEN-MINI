using MiniLibraryWeb.Models.Entities;

namespace MiniLibraryWeb.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(string? search = null);
        Task<Book?> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}
