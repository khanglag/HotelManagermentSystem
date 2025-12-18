using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _context;

        public PermissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permission>> GetAllAsync() =>
            await _context.Permissions.AsNoTracking().ToListAsync();

        public async Task<Permission?> GetByIdAsync(int id) =>
            await _context.Permissions.FindAsync(id);

        public async Task<Permission> AddAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task UpdateAsync(Permission permission)
        {
            _context.Entry(permission).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Permissions.FindAsync(id);
            if (entity != null)
            {
                _context.Permissions.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> HasPermissionAsync(int accountId, string permissionKey)
        {
            return await _context.AccountPermissions
                .Include(ap => ap.Permission)
                .AnyAsync(ap => ap.AccountId == accountId && ap.Permission.Method + ":" + ap.Permission.Path == permissionKey);
        }
    }
}