using HotelManagementSystem.Api.Entities;

namespace HotelManagementSystem.Api.Repositories.IRepsiories
{
    public interface IAccountRepository
    {
        Task RegisterAsync(Account account);
        Task<Account?> GetByUserNameAsync(string userName);
        Task<bool> ExistsAsync(string userName);
        Task UpdateAsync(Account account);
    }
}
