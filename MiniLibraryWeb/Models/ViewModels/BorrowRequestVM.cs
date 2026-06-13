using System.ComponentModel.DataAnnotations;
using MiniLibraryWeb.Models.Entities;

namespace MiniLibraryWeb.Models.ViewModels
{
    public class BorrowRequestVM
    {
        [Required(ErrorMessage = "Vui lòng chọn độc giả")]
        [Display(Name = "Độc giả")]
        public int ReaderId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn sách")]
        [Display(Name = "Sách")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn hạn trả")]
        [DataType(DataType.Date)]
        [Display(Name = "Hạn trả")]
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(14); // Mặc định 14 ngày

        public IEnumerable<Reader>? Readers { get; set; }
        public IEnumerable<Book>? Books { get; set; }
    }
}
