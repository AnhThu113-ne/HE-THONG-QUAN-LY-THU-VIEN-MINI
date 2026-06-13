using MiniLibraryWeb.Models.Entities;

namespace MiniLibraryWeb.Services
{
    public interface IBorrowService
    {
        Task<IEnumerable<BorrowTicket>> GetAllTicketsAsync();
        Task<BorrowTicket?> GetTicketByIdAsync(int id);
        Task<bool> BorrowBookAsync(int readerId, int bookId, DateTime dueDate);
        Task<bool> ReturnBookAsync(int ticketId);
        Task<int> GetActiveBorrowsCountAsync();
        Task<int> GetOverdueBorrowsCountAsync();
        Task<decimal> GetTotalFinesAsync();
    }
}
