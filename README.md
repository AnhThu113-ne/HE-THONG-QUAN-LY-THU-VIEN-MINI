# Hệ Thống Quản Lý Thư Viện Mini (Mini Library Management System)

Ứng dụng quản lý thư viện mini được xây dựng bằng **ASP.NET Core MVC (.NET 10.0)**, sử dụng **Entity Framework Core** và **SQL Server** để lưu trữ và quản lý dữ liệu sách, độc giả, và các phiếu mượn/trả sách. Giao diện được thiết kế hiện đại, tinh tế với **Vanilla CSS** và bố cục responsive.

---

## 🛠️ Công Nghệ Sử Dụng

- **Framework chính**: ASP.NET Core MVC (.NET 10.0)
- **Truy cập cơ sở dữ liệu**: Entity Framework Core
- **Hệ quản trị CSDL**: SQL Server (LocalDB / SQL Express)
- **Giao diện (UI)**: HTML5, Vanilla CSS3, Google Fonts (Outfit), FontAwesome
- **Quản lý mã nguồn**: Git với chiến lược phát triển theo phân nhánh (`Feature Branching`)

---

## 📋 Yêu Cầu Chức Năng

1. **Quản lý Sách**:
   - Thêm sách mới, sửa thông tin, xóa sách.
   - Tìm kiếm sách nhanh theo tiêu đề hoặc tên tác giả.
2. **Quản lý Độc Giả**:
   - Đăng ký và quản lý thông tin độc giả (Họ tên, Email, Số điện thoại, Số thẻ).
   - Xem chi tiết lịch sử mượn/trả sách riêng biệt của từng độc giả.
3. **Quản lý Mượn / Trả Sách**:
   - Tạo phiếu mượn sách mới cho độc giả.
   - **Kiểm tra tồn kho**: Nếu số lượng sách (`Stock`) bằng 0, hệ thống sẽ cảnh báo hết sách và ngăn không cho tạo phiếu mượn.
   - Ghi nhận ngày trả sách, chuyển đổi trạng thái phiếu.
   - **Tính tiền phạt trễ hạn**: Nếu ngày trả thực tế trễ hơn ngày hẹn trả (`DueDate`), hệ thống sẽ tính phạt 5.000 VND / ngày quá hạn.

---

## 🚀 Hướng Dẫn Cài Đặt Và Chạy Ứng Dụng

### 1. Yêu Cầu Hệ Thống
- Đã cài đặt [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).
- Đã cài đặt SQL Server (khuyến nghị LocalDB đi kèm Visual Studio).
- Công cụ dòng lệnh `dotnet ef` (nếu chưa có, chạy lệnh: `dotnet tool install --global dotnet-ef`).

### 2. Cài Đặt CSDL (Chạy Migrations)
Di chuyển vào thư mục dự án và chạy lệnh cập nhật database:
```bash
# Thêm và áp dụng migration để tạo cấu trúc bảng CSDL
dotnet ef database update --project MiniLibraryWeb
```

### 3. Khởi Chạy Ứng Dụng
Chạy lệnh sau ở thư mục gốc để khởi động máy chủ thử nghiệm:
```bash
dotnet run --project MiniLibraryWeb
```
Sau đó truy cập đường dẫn mặc định trên trình duyệt (thường là `https://localhost:5001` hoặc `http://localhost:5000` tùy cấu hình hiển thị trên console).

---

## 📂 Cấu Trúc Dự Án

```text
MiniLibraryManagement/
├── README.md                       # Tài liệu hướng dẫn sử dụng ứng dụng
├── .gitignore                      # File cấu hình bỏ qua của Git
├── MiniLibraryManagement.sln       # File Solution bao quát toàn bộ dự án
└── MiniLibraryWeb/                 # Thư mục chứa project Web chính
    ├── Controllers/                # Xử lý các request từ người dùng
    ├── Models/                     # Định nghĩa Entity và ViewModel
    ├── Data/                       # Cấu hình CSDL (DbContext & Migrations)
    ├── Services/                   # Lớp logic nghiệp vụ (Business Services)
    ├── Views/                      # Giao diện Razor Pages (.cshtml)
    └── wwwroot/                    # File tĩnh (CSS, JS, Libraries)
```
