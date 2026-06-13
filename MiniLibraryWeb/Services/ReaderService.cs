using Microsoft.EntityFrameworkCore;
using MiniLibraryWeb.Data;
using MiniLibraryWeb.Models.Entities;

namespace MiniLibraryWeb.Services
{
    public class ReaderService : IReaderService
    {
        private readonly ApplicationDbContext _context;

        public ReaderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reader>> GetAllReadersAsync()
        {
            return await _context.Readers.ToListAsync();
        }

        public async Task<Reader?> GetReaderByIdAsync(int id)
        {
            return await _context.Readers.FindAsync(id);
        }

        public async Task<Reader?> GetReaderWithHistoryAsync(int id)
        {
            return await _context.Readers
                .Include(r => r.BorrowTickets!)
                    .ThenInclude(b => b.Book)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddReaderAsync(Reader reader)
        {
            if (string.IsNullOrWhiteSpace(reader.CardNumber))
            {
                reader.CardNumber = "LIB-" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            }
            _context.Readers.Add(reader);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReaderAsync(Reader reader)
        {
            _context.Readers.Update(reader);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReaderAsync(int id)
        {
            var reader = await _context.Readers.FindAsync(id);
            if (reader != null)
            {
                _context.Readers.Remove(reader);
                await _context.SaveChangesAsync();
            }
        }
    }
}
