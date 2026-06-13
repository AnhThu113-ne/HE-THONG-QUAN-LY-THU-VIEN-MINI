using System.ComponentModel.DataAnnotations;

namespace MiniLibraryWeb.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sách không được để trống")]
        [Display(Name = "Tên sách")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên tác giả không được để trống")]
        [Display(Name = "Tác giả")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 0")]
        [Display(Name = "Số lượng")]
        public int Stock { get; set; }

        [Display(Name = "Thể loại")]
        public string? Category { get; set; }

        [Display(Name = "Năm xuất bản")]
        public int? PublishYear { get; set; }

        // Navigation property
        public ICollection<BorrowTicket>? BorrowTickets { get; set; }
    }
}
