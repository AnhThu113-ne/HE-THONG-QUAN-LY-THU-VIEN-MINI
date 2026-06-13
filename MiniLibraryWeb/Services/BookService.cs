using Microsoft.EntityFrameworkCore;
using MiniLibraryWeb.Data;
using MiniLibraryWeb.Models.Entities;

namespace MiniLibraryWeb.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return await _context.Books.ToListAsync();
            }

            search = search.ToLower().Trim();
            return await _context.Books
                .Where(b => b.Title.ToLower().Contains(search) || 
                            b.Author.ToLower().Contains(search) || 
                            (b.Category != null && b.Category.ToLower().Contains(search)))
                .ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
