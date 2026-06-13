using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniLibraryWeb.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Category", "PublishYear", "Stock", "Title" },
                values: new object[,]
                {
                    { 1, "Dale Carnegie", "Tâm lý học", 1936, 10, "Đắc Nhân Tâm" },
                    { 2, "Paulo Coelho", "Tiểu thuyết", 1988, 5, "Nhà Giả Kim" },
                    { 3, "Nguyễn Nhật Ánh", "Truyện dài", 2010, 8, "Tôi Thấy Hoa Vàng Trên Cỏ Xanh" },
                    { 4, "Jeffrey Archer", "Tiểu thuyết", 1979, 3, "Hai Số Phận" },
                    { 5, "Rosie Nguyễn", "Kỹ năng sống", 2016, 12, "Tuổi Trẻ Đáng Giá Bao Nhiêu" }
                });

            migrationBuilder.InsertData(
                table: "Readers",
                columns: new[] { "Id", "CardNumber", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "LIB-ANHTHU01", "anhthu@gmail.com", "Anh Thư", "0987654321" },
                    { 2, "LIB-MAING01", "mai.nguyen@gmail.com", "Nguyễn Thị Mai", "0901234567" },
                    { 3, "LIB-TUTRAN01", "camtu.tran@gmail.com", "Trần Lê Cẩm Tú", "0912345678" },
                    { 4, "LIB-GIANG01", "hagiang.pham@gmail.com", "Phạm Hà Giang", "0923456789" },
                    { 5, "LIB-HANLE01", "ngochan.le@gmail.com", "Lê Ngọc Hân", "0934567890" }
                });

            migrationBuilder.InsertData(
                table: "BorrowTickets",
                columns: new[] { "Id", "BookId", "BorrowDate", "DueDate", "FineAmount", "ReaderId", "ReturnDate", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 1, null, "Borrowed" },
                    { 2, 2, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 2, new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Returned" },
                    { 3, 3, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 100000m, 3, null, "Overdue" },
                    { 4, 4, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 30000m, 4, new DateTime(2026, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Returned" },
                    { 5, 5, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 5, null, "Borrowed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BorrowTickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BorrowTickets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BorrowTickets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BorrowTickets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BorrowTickets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Readers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Readers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Readers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Readers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Readers",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
