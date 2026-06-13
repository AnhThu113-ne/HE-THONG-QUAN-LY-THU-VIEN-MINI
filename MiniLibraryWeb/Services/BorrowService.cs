using Microsoft.EntityFrameworkCore;
using MiniLibraryWeb.Data;
using MiniLibraryWeb.Models.Entities;

namespace MiniLibraryWeb.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly ApplicationDbContext _context;

        public BorrowService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BorrowTicket>> GetAllTicketsAsync()
        {
            return await _context.BorrowTickets
                .Include(t => t.Reader)
                .Include(t => t.Book)
                .OrderByDescending(t => t.BorrowDate)
                .ToListAsync();
        }

        public async Task<BorrowTicket?> GetTicketByIdAsync(int id)
        {
            return await _context.BorrowTickets
                .Include(t => t.Reader)
                .Include(t => t.Book)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> BorrowBookAsync(int readerId, int bookId, DateTime dueDate)
        {
            // Bắt đầu Transaction để đảm bảo tính toàn vẹn dữ liệu
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var reader = await _context.Readers.FindAsync(readerId);
                if (reader == null)
                {
                    throw new ArgumentException("Độc giả không tồn tại trong hệ thống.");
                }

                var book = await _context.Books.FindAsync(bookId);
                if (book == null)
                {
                    throw new ArgumentException("Sách không tồn tại trong hệ thống.");
                }

                // Kiểm tra số lượng tồn kho (Yêu cầu nghiệp vụ cốt lõi)
                if (book.Stock <= 0)
                {
                    throw new InvalidOperationException("Sách này đã hết trong thư viện.");
                }

                // Giảm số lượng tồn kho đi 1
                book.Stock--;
                _context.Books.Update(book);

                // Tạo phiếu mượn mới
                var ticket = new BorrowTicket
                {
                    ReaderId = readerId,
                    BookId = bookId,
                    BorrowDate = DateTime.Today,
                    DueDate = dueDate.Date,
                    Status = "Borrowed",
                    FineAmount = 0
                };

                _context.BorrowTickets.Add(ticket);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ReturnBookAsync(int ticketId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var ticket = await _context.BorrowTickets
                    .Include(t => t.Book)
                    .FirstOrDefaultAsync(t => t.Id == ticketId);

                if (ticket == null || ticket.ReturnDate != null)
                {
                    return false;
                }

                // Cập nhật ngày trả thực tế là hôm nay
                ticket.ReturnDate = DateTime.Today;
                ticket.Status = "Returned";

                // Tính tiền phạt trễ hạn nếu có (Phạt 5.000 VND / ngày quá hạn)
                var daysLate = (ticket.ReturnDate.Value.Date - ticket.DueDate.Date).Days;
                if (daysLate > 0)
                {
                    ticket.FineAmount = daysLate * 5000;
                }
                else
                {
                    ticket.FineAmount = 0;
                }

                // Hoàn trả số lượng sách tồn kho tăng thêm 1
                if (ticket.Book != null)
                {
                    ticket.Book.Stock++;
                    _context.Books.Update(ticket.Book);
                }

                _context.BorrowTickets.Update(ticket);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> GetActiveBorrowsCountAsync()
        {
            return await _context.BorrowTickets
                .CountAsync(t => t.ReturnDate == null);
        }

        public async Task<int> GetOverdueBorrowsCountAsync()
        {
            return await _context.BorrowTickets
                .CountAsync(t => t.ReturnDate == null && t.DueDate < DateTime.Today);
        }

        public async Task<decimal> GetTotalFinesAsync()
        {
            return await _context.BorrowTickets
                .SumAsync(t => t.FineAmount);
        }
    }
}
