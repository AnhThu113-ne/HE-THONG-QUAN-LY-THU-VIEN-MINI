using Microsoft.EntityFrameworkCore;
using MiniLibraryWeb.Models.Entities;

namespace MiniLibraryWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Reader> Readers { get; set; } = null!;
        public DbSet<BorrowTicket> BorrowTickets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình các ràng buộc khóa ngoại (Restrict để tránh cascade delete vòng tròn)
            modelBuilder.Entity<BorrowTicket>()
                .HasOne(b => b.Reader)
                .WithMany(r => r.BorrowTickets)
                .HasForeignKey(b => b.ReaderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BorrowTicket>()
                .HasOne(b => b.Book)
                .WithMany(bk => bk.BorrowTickets)
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Data (5 bản ghi mỗi bảng)
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Đắc Nhân Tâm", Author = "Dale Carnegie", Stock = 10, Category = "Tâm lý học", PublishYear = 1936 },
                new Book { Id = 2, Title = "Nhà Giả Kim", Author = "Paulo Coelho", Stock = 5, Category = "Tiểu thuyết", PublishYear = 1988 },
                new Book { Id = 3, Title = "Tôi Thấy Hoa Vàng Trên Cỏ Xanh", Author = "Nguyễn Nhật Ánh", Stock = 8, Category = "Truyện dài", PublishYear = 2010 },
                new Book { Id = 4, Title = "Hai Số Phận", Author = "Jeffrey Archer", Stock = 3, Category = "Tiểu thuyết", PublishYear = 1979 },
                new Book { Id = 5, Title = "Tuổi Trẻ Đáng Giá Bao Nhiêu", Author = "Rosie Nguyễn", Stock = 12, Category = "Kỹ năng sống", PublishYear = 2016 }
            );

            modelBuilder.Entity<Reader>().HasData(
                new Reader { Id = 1, Name = "Anh Thư", Email = "anhthu@gmail.com", Phone = "0987654321", CardNumber = "LIB-ANHTHU01" },
                new Reader { Id = 2, Name = "Nguyễn Thị Mai", Email = "mai.nguyen@gmail.com", Phone = "0901234567", CardNumber = "LIB-MAING01" },
                new Reader { Id = 3, Name = "Trần Lê Cẩm Tú", Email = "camtu.tran@gmail.com", Phone = "0912345678", CardNumber = "LIB-TUTRAN01" },
                new Reader { Id = 4, Name = "Phạm Hà Giang", Email = "hagiang.pham@gmail.com", Phone = "0923456789", CardNumber = "LIB-GIANG01" },
                new Reader { Id = 5, Name = "Lê Ngọc Hân", Email = "ngochan.le@gmail.com", Phone = "0934567890", CardNumber = "LIB-HANLE01" }
            );

            modelBuilder.Entity<BorrowTicket>().HasData(
                new BorrowTicket { Id = 1, ReaderId = 1, BookId = 1, BorrowDate = new DateTime(2026, 6, 1), DueDate = new DateTime(2026, 6, 15), Status = "Borrowed", FineAmount = 0 },
                new BorrowTicket { Id = 2, ReaderId = 2, BookId = 2, BorrowDate = new DateTime(2026, 5, 20), DueDate = new DateTime(2026, 6, 3), ReturnDate = new DateTime(2026, 6, 3), Status = "Returned", FineAmount = 0 },
                new BorrowTicket { Id = 3, ReaderId = 3, BookId = 3, BorrowDate = new DateTime(2026, 5, 10), DueDate = new DateTime(2026, 5, 24), Status = "Overdue", FineAmount = 100000 },
                new BorrowTicket { Id = 4, ReaderId = 4, BookId = 4, BorrowDate = new DateTime(2026, 4, 15), DueDate = new DateTime(2026, 4, 29), ReturnDate = new DateTime(2026, 5, 5), Status = "Returned", FineAmount = 30000 },
                new BorrowTicket { Id = 5, ReaderId = 5, BookId = 5, BorrowDate = new DateTime(2026, 6, 10), DueDate = new DateTime(2026, 6, 24), Status = "Borrowed", FineAmount = 0 }
            );
        }
    }
}
