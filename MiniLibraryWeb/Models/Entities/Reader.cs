using System.ComponentModel.DataAnnotations;

namespace MiniLibraryWeb.Models.Entities
{
    public class Reader
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [Display(Name = "Họ tên")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; } = string.Empty;

        [Display(Name = "Số thẻ thư viện")]
        public string? CardNumber { get; set; }

        // Navigation property
        public ICollection<BorrowTicket>? BorrowTickets { get; set; }
    }
}
