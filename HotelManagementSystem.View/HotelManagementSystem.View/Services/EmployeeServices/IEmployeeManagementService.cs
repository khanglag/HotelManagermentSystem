using HotelManagermentSystem.View.Models.Requests;

namespace HotelManagermentSystem.View.Services.EmployeeServices
{
    public interface IEmployeeManagementService
    {
        Task<List<EmployeeDto>> GetAllAsync();
        Task<List<EmployeeDto>> GetByNameAsync(string name);
        Task<List<EmployeeDto>> GetByBranchAsync(int branchId);
        Task<List<EmployeeDto>> GetByNameAndBranchAsync(string name, int branchId);
        Task<EmployeeDto?> GetByIdAsync(int id);
        Task<EmployeeDto?> GetMyDetailAsync();
        Task<string> CreateAsync(EmployeeDto employee);
        Task<string> UpdateAsync(int id, EmployeeDto employee);
    }
}
