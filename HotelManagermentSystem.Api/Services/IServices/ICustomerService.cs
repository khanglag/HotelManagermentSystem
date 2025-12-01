using HotelManagementSystem.Api.Dtos;
using HotelManagementSystem.Api.Entities;

namespace HotelManagementSystem.Api.Services.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer?> GetByEmailAsync(string email);
        Task<Customer?> GetByPhoneNumberAsync(string phoneNumber);
        Task AddAsync(CustomerDto customer);
        Task UpdateAsync(CustomerDto customer);
    }
}
