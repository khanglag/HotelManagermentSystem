using HotelManagementSystem.Api.Entities;
using HotelManagermentSystem.Api.Dtos;

namespace HotelManagermentSystem.Api.Services.IServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<Employee>> GetEmployeeByNameAsync(string name);
        Task<IEnumerable<Employee>> GetEmployeeByBranchAsync(int branch);
        Task<IEnumerable<Employee>> GetEmployeesByNameAndBranchAsync(string name, int branch);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee?> GetByEmailAsync(string email);
        Task<Employee?> GetByPhoneNumberAsync(string phoneNumber);
        Task AddAsync(EmployeeDto employee);
        Task UpdateAsync(EmployeeDto employee);
    }
}
