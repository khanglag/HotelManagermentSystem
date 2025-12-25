using HotelManagermentSystem.View.Models.Requests;

namespace HotelManagermentSystem.View.Services.EmployeeServices
{
    public class EmployeeManagementService : IEmployeeManagementService
    {
        private readonly HttpClient _httpClient;

        public EmployeeManagementService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Lấy toàn bộ danh sách nhân viên
        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/employee");

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                // Log lỗi hoặc thông báo cho người dùng
                Console.WriteLine("Lỗi: Bạn không có quyền truy cập (403)");
                return new List<EmployeeDto>();
            }

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<EmployeeDto>>() ?? new();
            }

            return new List<EmployeeDto>();
        }

        // Tìm kiếm theo tên
        public async Task<List<EmployeeDto>> GetByNameAsync(string name)
        {
            return await _httpClient.GetFromJsonAsync<List<EmployeeDto>>($"api/employee/by-name/{name}") ?? new();
        }

        // Tìm kiếm theo chi nhánh
        public async Task<List<EmployeeDto>> GetByBranchAsync(int branchId)
        {
            return await _httpClient.GetFromJsonAsync<List<EmployeeDto>>($"api/employee/by-branch/{branchId}") ?? new();
        }

        // Tìm kiếm kết hợp tên và chi nhánh (Sử dụng Query String)
        public async Task<List<EmployeeDto>> GetByNameAndBranchAsync(string name, int branchId)
        {
            var url = $"api/employee/by-name-and-branch?name={name}&branchId={branchId}";
            return await _httpClient.GetFromJsonAsync<List<EmployeeDto>>(url) ?? new();
        }

        // Lấy chi tiết nhân viên theo ID
        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/employee/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EmployeeDto>();
            }
            return null;
        }

        // Lấy thông tin cá nhân của nhân viên đang đăng nhập (Sử dụng JWT token trong header)
        public async Task<EmployeeDto?> GetMyDetailAsync()
        {
            var response = await _httpClient.GetAsync("api/employee/me");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EmployeeDto>();
            }
            return null;
        }

        // Thêm mới nhân viên
        public async Task<string> CreateAsync(EmployeeDto employee)
        {
            var response = await _httpClient.PostAsJsonAsync("api/employee", employee);
            if (response.IsSuccessStatusCode) return "Add Successful";
            return "Error creating employee";
        }

        // Cập nhật thông tin nhân viên
        public async Task<string> UpdateAsync(int id, EmployeeDto employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/employee/{id}", employee);
            if (response.IsSuccessStatusCode) return "Update Successful";
            return "Error updating employee";
        }
    }
}
