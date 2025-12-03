using HotelManagementSystem.Api.Entities;

namespace HotelManagermentSystem.Api.Repositories.IRepsiories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<Employee>> GetEmployeeByNameAsync(string name);
        Task<IEnumerable<Employee>> GetEmployeeByBranchAsync(int branch);
        Task<IEnumerable<Employee>> GetEmployeesByNameAndBranchAsync (string name, int branch);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee?> GetByEmailAsync(string email);
        Task<Employee?> GetByPhoneNumberAsync(string phoneNumber);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
    }
}
