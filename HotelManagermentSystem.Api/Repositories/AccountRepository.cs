using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Entities;
using HotelManagementSystem.Api.Repositories.IRepsiories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task RegisterAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task<Account?> GetByUserNameAsync(string username)
        {
            return await _context.Accounts.Include(a => a.AccountPermissions).ThenInclude(ap => ap.Permission).FirstOrDefaultAsync(a => a.Username == username);
        }

        public async Task<bool> ExistsAsync(string username)
        {
            return await _context.Accounts.AnyAsync(a => a.Username == username);
        }

        public async Task UpdateAsync(Account account)
        {
            var existingAccount = await _context.Accounts.FindAsync(account.Id);
            if (existingAccount == null) throw new KeyNotFoundException($"Account with ID {account.Id} not found."); ;
            _context.Entry(existingAccount).CurrentValues.SetValues(account);
            await _context.SaveChangesAsync();
        }
    }
}
