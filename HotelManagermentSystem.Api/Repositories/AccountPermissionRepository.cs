using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Repositories
{
    public class AccountPermissionRepository : IAccountPermissionRepository
    {
        private readonly AppDbContext _context;

        public AccountPermissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountPermission>> GetByAccountIdAsync(int accountId)
        {
            return await _context.AccountPermissions
                .Include(ap => ap.Permission)
                .Where(ap => ap.AccountId == accountId)
                .ToListAsync();
        }

        public async Task AddAsync(AccountPermission accountPermission)
        {
            await _context.AccountPermissions.AddAsync(accountPermission);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int accountId, int permissionId)
        {
            var entity = await _context.AccountPermissions
                .FirstOrDefaultAsync(ap => ap.AccountId == accountId && ap.PermissionId == permissionId);
            if (entity != null)
            {
                _context.AccountPermissions.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int accountId, int permissionId) =>
            await _context.AccountPermissions.AnyAsync(ap => ap.AccountId == accountId && ap.PermissionId == permissionId);
    }
}