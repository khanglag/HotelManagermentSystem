using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Entities;
using HotelManagermentSystem.Api.Repositories.IRepsiories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagermentSystem.Api.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.AsNoTracking().ToListAsync();
        }

        public async Task<Employee?> GetByEmailAsync(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Employees.FirstOrDefaultAsync(x =>x.PhoneNumber == phoneNumber);
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByBranchAsync(int branch)
        {
            return await _context.Employees.Where(x => x.BranchId == branch).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByNameAsync(string name)
        {
            return await _context.Employees.Where(x => x.Name.Contains(name)).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByNameAndBranchAsync(string name, int branch)
        {
            Task<IEnumerable<Employee>> taskByName = GetEmployeeByNameAsync(name);
            Task<IEnumerable<Employee>> taskByBranch = GetEmployeeByBranchAsync(branch);

            await Task.WhenAll(taskByName, taskByBranch);

            var employeesByName = taskByName.Result;
            var employeeByBranch = taskByBranch.Result;

            return employeesByName.Intersect(employeeByBranch);
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
