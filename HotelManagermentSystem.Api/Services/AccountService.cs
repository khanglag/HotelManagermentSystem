using HotelManagementSystem.Api.Dtos;
using HotelManagementSystem.Api.Entities;
using HotelManagementSystem.Api.Repositories.IRepsiories;
using HotelManagementSystem.Api.Services.IService;

namespace HotelManagementSystem.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<AccountDto?> GetByUserNameAsync(string userName)
        {
            var account = await _accountRepository.GetByUserNameAsync(userName);
            if( account == null)
            {
                return null;
            }
            return new AccountDto
            {
                Id = account.Id,
                Username = account.Username,
                Role = account.Role,
                Exist = account.Status.ToString()
            };
        }

        public async Task<Account?> LoginAsync(string userName, string passWord)
        {
            var account = await _accountRepository.GetByUserNameAsync(userName);
            if (account == null)
                return null;
            
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(passWord, account.Password);
            return isPasswordValid ? account : null;
        }

        public async Task RegisterAsync(Account account)
        {
            var accountExist = await _accountRepository.GetByUserNameAsync(account.Username);
            if (accountExist != null)
                throw new Exception("Username is alreder exists");
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            await _accountRepository.RegisterAsync(account);
        }

        public Task UpdateAsync(Account account)
        {
            return _accountRepository.UpdateAsync(account);
        }

        
    }
}
