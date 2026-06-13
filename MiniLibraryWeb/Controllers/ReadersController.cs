using Microsoft.AspNetCore.Mvc;
using MiniLibraryWeb.Models.Entities;
using MiniLibraryWeb.Services;

namespace MiniLibraryWeb.Controllers
{
    public class ReadersController : Controller
    {
        private readonly IReaderService _readerService;

        public ReadersController(IReaderService readerService)
        {
            _readerService = readerService;
        }

        // GET: Readers
        public async Task<IActionResult> Index()
        {
            var readers = await _readerService.GetAllReadersAsync();
            return View(readers);
        }

        // GET: Readers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var reader = await _readerService.GetReaderWithHistoryAsync(id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        // GET: Readers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Readers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reader reader)
        {
            if (ModelState.IsValid)
            {
                await _readerService.AddReaderAsync(reader);
                TempData["SuccessMessage"] = "Đăng ký độc giả thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(reader);
        }

        // GET: Readers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var reader = await _readerService.GetReaderByIdAsync(id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        // POST: Readers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reader reader)
        {
            if (id != reader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _readerService.UpdateReaderAsync(reader);
                TempData["SuccessMessage"] = "Cập nhật thông tin độc giả thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(reader);
        }

        // POST: Readers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _readerService.DeleteReaderAsync(id);
            TempData["SuccessMessage"] = "Xóa độc giả thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
