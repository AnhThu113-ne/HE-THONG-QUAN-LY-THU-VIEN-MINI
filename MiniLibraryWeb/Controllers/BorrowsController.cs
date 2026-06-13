using Microsoft.AspNetCore.Mvc;
using MiniLibraryWeb.Models.ViewModels;
using MiniLibraryWeb.Services;

namespace MiniLibraryWeb.Controllers
{
    public class BorrowsController : Controller
    {
        private readonly IBorrowService _borrowService;
        private readonly IBookService _bookService;
        private readonly IReaderService _readerService;

        public BorrowsController(IBorrowService borrowService, IBookService bookService, IReaderService readerService)
        {
            _borrowService = borrowService;
            _bookService = bookService;
            _readerService = readerService;
        }

        // GET: Borrows
        public async Task<IActionResult> Index()
        {
            var tickets = await _borrowService.GetAllTicketsAsync();
            return View(tickets);
        }

        // GET: Borrows/Create
        public async Task<IActionResult> Create()
        {
            var vm = new BorrowRequestVM
            {
                Books = await _bookService.GetAllBooksAsync(),
                Readers = await _readerService.GetAllReadersAsync()
            };
            return View(vm);
        }

        // POST: Borrows/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BorrowRequestVM vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _borrowService.BorrowBookAsync(vm.ReaderId, vm.BookId, vm.DueDate);
                    TempData["SuccessMessage"] = "Lập phiếu mượn sách thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    // Lỗi hết sách (yêu cầu nghiệp vụ: thông báo sách đã hết)
                    TempData["ErrorMessage"] = ex.Message;
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Đã xảy ra lỗi: " + ex.Message;
                }
            }

            // Nạp lại danh sách khi có lỗi xảy ra
            vm.Books = await _bookService.GetAllBooksAsync();
            vm.Readers = await _readerService.GetAllReadersAsync();
            return View(vm);
        }

        // POST: Borrows/Return/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id)
        {
            try
            {
                var success = await _borrowService.ReturnBookAsync(id);
                if (success)
                {
                    var ticket = await _borrowService.GetTicketByIdAsync(id);
                    if (ticket != null && ticket.FineAmount > 0)
                    {
                        var daysLate = ((ticket.ReturnDate ?? DateTime.Today) - ticket.DueDate).Days;
                        TempData["SuccessMessage"] = $"Trả sách thành công! Phát sinh tiền phạt trễ hạn: {ticket.FineAmount:N0} VND ({daysLate} ngày quá hạn).";
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Trả sách thành công!";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Phiếu mượn không tồn tại hoặc sách đã được trả rồi.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
