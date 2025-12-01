using HotelManagementSystem.Api.Dtos;
using HotelManagementSystem.Api.Entities;

namespace HotelManagementSystem.Api.Services.IService
{
    public interface IAccountService
    {
        Task<Account?> LoginAsync(string userName, string passWord);
        Task RegisterAsync(Account account);
        Task<AccountDto?> GetByUserNameAsync(string userName);
        Task UpdateAsync(Account account);
    }
}
