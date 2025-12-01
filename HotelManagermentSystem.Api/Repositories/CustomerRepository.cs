using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Dtos;
using HotelManagementSystem.Api.Entities;
using HotelManagementSystem.Api.Repositories.IRepsiories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Customers.FirstOrDefaultAsync(x =>x.PhoneNumber == phoneNumber);
        }

        public async Task UpdateAsync(Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(customer.Id);
            if (existingCustomer == null) throw new KeyNotFoundException($"Customer with ID {customer.Id} not found."); ;
            _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
            await _context.SaveChangesAsync();
        }
    }
}
