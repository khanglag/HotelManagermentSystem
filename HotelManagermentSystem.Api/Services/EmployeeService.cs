using HotelManagementSystem.Api.Entities;
using HotelManagermentSystem.Api.Dtos;
using HotelManagermentSystem.Api.Repositories.IRepsiories;
using HotelManagermentSystem.Api.Services.IServices;

namespace HotelManagermentSystem.Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        public IEmployeeRepository _repository;
        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(EmployeeDto employee)
        {
            var checkEmail = await _repository.GetByEmailAsync(employee.Email);
            if (checkEmail != null) throw new KeyNotFoundException($"Email has been used");
            var checkPhone = await _repository.GetByPhoneNumberAsync(employee.Phone);
            if (checkPhone != null) throw new KeyNotFoundException($"Phone number has been used");

            var newEmployee = new Employee
            {
                Name = employee.Name,
                Email = employee.Email,
                BranchId = employee.BranchId,
                AccountId = employee.AccountId,
                PhoneNumber = employee.Phone,
                Status = employee.Status
            };
            await _repository.AddAsync(newEmployee);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Employee?> GetByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Employee?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _repository.GetByPhoneNumberAsync(phoneNumber);
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByBranchAsync(int branch)
        {
            return await _repository.GetEmployeeByBranchAsync(branch);
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByNameAsync(string name)
        {
            return await _repository.GetEmployeeByNameAsync(name);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByNameAndBranchAsync(string name, int branch)
        {
            return await _repository.GetEmployeesByNameAndBranchAsync($"{name}", branch);
        }

        public async Task UpdateAsync(EmployeeDto employee)
        {
            var existingEmployees = await _repository.GetByIdAsync(employee.Id);
            if (existingEmployees == null) throw new KeyNotFoundException("Not found Employee");
            
            existingEmployees.Name = employee.Name;
            existingEmployees.PhoneNumber = employee.Phone;
            existingEmployees.Email = employee.Email;
            existingEmployees.BranchId = employee.BranchId;
            existingEmployees.Status = employee.Status;

            
            await _repository.UpdateAsync(existingEmployees);
        }
    }
}
