using System.ComponentModel.DataAnnotations;

namespace MiniLibraryWeb.Models.Entities
{
    public class BorrowTicket
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Độc giả")]
        public int ReaderId { get; set; }
        public Reader? Reader { get; set; }

        [Required]
        [Display(Name = "Sách")]
        public int BookId { get; set; }
        public Book? Book { get; set; }

        [Required]
        [Display(Name = "Ngày mượn")]
        public DateTime BorrowDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Hạn trả")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Ngày trả thực tế")]
        public DateTime? ReturnDate { get; set; }

        [Required]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Borrowed"; // "Borrowed", "Returned", "Overdue"

        [Display(Name = "Tiền phạt (VND)")]
        [Range(0, double.MaxValue)]
        public decimal FineAmount { get; set; } = 0;
    }
}
