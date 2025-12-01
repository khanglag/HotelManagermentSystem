using BCrypt.Net;
using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Entities;
using HotelManagementSystem.Api.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace HotelManagementSystem.Api.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // Đảm bảo cơ sở dữ liệu và schema được tạo
            context.Database.EnsureCreated();

            // Nếu đã có dữ liệu rồi thì không seed nữa
            if (context.Accounts.Any())
            {
                return;
            }

            // --- Hàm hỗ trợ mã hóa mật khẩu đơn giản (chỉ cho mục đích seed) ---
            //static string HashPassword(string Password)
            //{
            //    using (var sha256 = SHA256.Create())
            //    {
            //        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));
            //        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            //    }
            //}
            // ------------------------------------------------------------------

            // 1. Seed Permissions (Quyền hạn)
            var permissions = new List<Permission>
            {
                new Permission { Path = "/api/accounts", Method = "GET", Description = "Xem danh sách tài khoản" },
                new Permission { Path = "/api/accounts", Method = "POST", Description = "Tạo tài khoản mới" },
                new Permission { Path = "/api/rooms", Method = "GET", Description = "Xem danh sách phòng" },
                new Permission { Path = "/api/rooms", Method = "POST", Description = "Tạo phòng mới" },
                new Permission { Path = "/api/reservations", Method = "POST", Description = "Tạo đặt phòng" },
                new Permission { Path = "/api/reports", Method = "GET", Description = "Xem báo cáo" },
                new Permission { Path = "/api/customer", Method = "POST", Description = "Thêm khách hàng" },
                new Permission { Path = "/api/customer", Method = "GET", Description = "Xem danh sách khách hàng" },
                new Permission { Path = "/api/customer", Method = "PUT", Description = "Cập nhật khách hàng" },
                new Permission { Path = "/api/customer/by-email", Method = "GET", Description = "Tìm kiếm khách hàng theo email" },
                new Permission { Path = "/api/customer/by-phone", Method = "GET", Description = "Tìm khách hàng theo số điện thoại" },

            };
            context.Permissions.AddRange(permissions);
            context.SaveChanges();

            // 2. Seed Accounts (Tài khoản)
            // Mật khẩu mặc định: "P@ssword123"
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("pass123");

            var adminAccount = new Account
            {
                Username = "admin",
                Password = hashedPassword,
                Role = Role.ADMIN,
                Status = AccountStatus.ACTIVE,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(-1) // Token hết hạn
            };

            var employeeAccount = new Account
            {
                Username = "nhanvien01",
                Password = hashedPassword,
                Role = Role.EMPLOYEE,
                Status = AccountStatus.ACTIVE,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(-1)
            };

            var customerAccount = new Account
            {
                Username = "khachhang01",
                Password = hashedPassword,
                Role = Role.CUSTOMER,
                Status = AccountStatus.ACTIVE,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(-1)
            };

            context.Accounts.AddRange(adminAccount, employeeAccount, customerAccount);
            context.SaveChanges();

            // 3. Seed AccountPermissions (Phân quyền)
            // Admin có tất cả các quyền
            var adminPermissions = permissions.Select(p => new AccountPermission
            {
                AccountId = adminAccount.Id,
                PermissionId = p.Id
            }).ToList();
            context.AccountPermissions.AddRange(adminPermissions);

            // Nhân viên chỉ có quyền xem phòng và tạo đặt phòng
            var employeePermissions = new List<AccountPermission>
            {
                new AccountPermission { AccountId = employeeAccount.Id, PermissionId = permissions.First(p => p.Path == "/api/rooms" && p.Method == "GET").Id },
                new AccountPermission { AccountId = employeeAccount.Id, PermissionId = permissions.First(p => p.Path == "/api/reservations" && p.Method == "POST").Id }
            };
            context.AccountPermissions.AddRange(employeePermissions);
            context.SaveChanges();

            // 4. Seed Branches (Chi nhánh)
            var branch1 = new Branch { Name = "Khách sạn Sài Gòn", Location = "Quận 1, TP. Hồ Chí Minh" };
            var branch2 = new Branch { Name = "Khách sạn Hà Nội", Location = "Quận Hoàn Kiếm, Hà Nội" };
            context.Branches.AddRange(branch1, branch2);
            context.SaveChanges();

            // 5. Seed Employees (Nhân viên)
            var employee1 = new Employee
            {
                Name = "Nguyễn Văn A",
                Email = "nguyenvana@hms.com",
                PhoneNumber = "0901112222",
                BranchId = branch1.Id,
                AccountId = employeeAccount.Id,
                Status = EmployeeStatus.WORKING
            };
            context.Employees.Add(employee1);
            context.SaveChanges();

            // 6. Seed RoomTypes (Loại phòng)
            var roomTypeStandard = new RoomType
            {
                Name = "Standard",
                Description = "Phòng tiêu chuẩn, diện tích 20m2",
                DailyPrice = 500000m,
                HourlyPrice = 100000m,
                ExtraHourPrice = 50000m,
                MaxHourlyDuration = 4
            };
            var roomTypeDeluxe = new RoomType
            {
                Name = "Deluxe",
                Description = "Phòng cao cấp, diện tích 30m2, có ban công",
                DailyPrice = 1000000m,
                HourlyPrice = 200000m,
                ExtraHourPrice = 100000m,
                MaxHourlyDuration = 4
            };
            context.RoomTypes.AddRange(roomTypeStandard, roomTypeDeluxe);
            context.SaveChanges();

            // 7. Seed Amenities (Tiện nghi)
            var wifi = new Amenity { Name = "Wi-Fi miễn phí", Description = "Mạng internet tốc độ cao" };
            var tv = new Amenity { Name = "Smart TV", Description = "TV màn hình phẳng" };
            var minibar = new Amenity { Name = "Minibar", Description = "Tủ lạnh nhỏ với đồ uống" };
            context.Amenities.AddRange(wifi, tv, minibar);
            context.SaveChanges();

            // 8. Seed Rooms (Phòng)
            var room101 = new Room { RoomNumber = "101", RoomTypeId = roomTypeStandard.Id, BranchId = branch1.Id, Status = RoomStatus.VACANT_CLEAN, RowVersion = new byte[8] };
            var room102 = new Room { RoomNumber = "102", RoomTypeId = roomTypeStandard.Id, BranchId = branch1.Id, Status = RoomStatus.VACANT_CLEAN, RowVersion = new byte[8] };
            var room201 = new Room { RoomNumber = "201", RoomTypeId = roomTypeDeluxe.Id, BranchId = branch1.Id, Status = RoomStatus.VACANT_CLEAN, RowVersion = new byte[8] };
            var room301 = new Room { RoomNumber = "301", RoomTypeId = roomTypeDeluxe.Id, BranchId = branch2.Id, Status = RoomStatus.VACANT_CLEAN, RowVersion = new byte[8] };
            context.Rooms.AddRange(room101, room102, room201, room301);
            context.SaveChanges();

            // 9. Seed RoomAmenities (Tiện nghi phòng)
            var roomAmenities = new List<RoomAmenity>
            {
                new RoomAmenity { RoomId = room101.Id, AmenityId = wifi.Id },
                new RoomAmenity { RoomId = room101.Id, AmenityId = tv.Id },
                new RoomAmenity { RoomId = room201.Id, AmenityId = wifi.Id },
                new RoomAmenity { RoomId = room201.Id, AmenityId = tv.Id },
                new RoomAmenity { RoomId = room201.Id, AmenityId = minibar.Id },
                new RoomAmenity { RoomId = room301.Id, AmenityId = wifi.Id },
                new RoomAmenity { RoomId = room301.Id, AmenityId = tv.Id },
                new RoomAmenity { RoomId = room301.Id, AmenityId = minibar.Id },
            };
            context.RoomAmenities.AddRange(roomAmenities);
            context.SaveChanges();

            // 10. Seed Customers (Khách hàng)
            var customer1 = new Customer
            {
                Name = "Trần Thị B",
                Email = "tranthib@gmail.com",
                PhoneNumber = "0987654321",
                AccountId = customerAccount.Id
            };
            var customer2 = new Customer
            {
                Name = "Phạm Văn C",
                Email = "phamvanc@yahoo.com",
                PhoneNumber = "0912345678",
                AccountId = null // Khách vãng lai chưa có tài khoản
            };
            context.Customers.AddRange(customer1, customer2);
            context.SaveChanges();

            // *Lưu ý: Các bảng Invoice, Payment, Reservation, ReservationDetail, Service, ServiceUsage*
            // *thường được tạo sau khi có đặt phòng/check-in/sử dụng dịch vụ thực tế.*
            // *Để tránh phức tạp, tôi không seed dữ liệu cho các bảng này, nhưng nếu cần,*
            // *bạn có thể thêm tương tự như các bước trên, đảm bảo khóa ngoại hợp lệ.*

            context.SaveChanges();
        }
    }
}