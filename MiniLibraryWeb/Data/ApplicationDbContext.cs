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
        }
    }
}
