using HotelManagementSystem.Api.Dtos;
using HotelManagementSystem.Api.Entities;

namespace HotelManagementSystem.Api.Repositories.IRepsiories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer?> GetByEmailAsync(string email);
        Task<Customer?> GetByPhoneNumberAsync(string phoneNumber);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        //Task DeleteAsyc(int id);
    }
}
