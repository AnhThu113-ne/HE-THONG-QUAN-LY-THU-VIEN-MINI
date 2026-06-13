using Microsoft.AspNetCore.Mvc;
using MiniLibraryWeb.Models;
using MiniLibraryWeb.Services;
using System.Diagnostics;

namespace MiniLibraryWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IReaderService _readerService;
        private readonly IBorrowService _borrowService;

        public HomeController(IBookService bookService, IReaderService readerService, IBorrowService borrowService)
        {
            _bookService = bookService;
            _readerService = readerService;
            _borrowService = borrowService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            var readers = await _readerService.GetAllReadersAsync();
            var activeBorrowsCount = await _borrowService.GetActiveBorrowsCountAsync();
            var overdueBorrowsCount = await _borrowService.GetOverdueBorrowsCountAsync();
            var totalFines = await _borrowService.GetTotalFinesAsync();
            var allTickets = await _borrowService.GetAllTicketsAsync();
            var recentTickets = allTickets.Take(5); // Lấy tối đa 5 phiếu mượn gần đây

            ViewBag.TotalBooks = books.Count();
            ViewBag.TotalReaders = readers.Count();
            ViewBag.ActiveBorrows = activeBorrowsCount;
            ViewBag.OverdueBorrows = overdueBorrowsCount;
            ViewBag.TotalFines = totalFines;

            ViewData["PageTitle"] = "Bảng điều khiển";
            return View(recentTickets);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
