using HotelManagementSystem.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Services
{
    public class PermissionService
    {
        private readonly AppDbContext _context;
        public PermissionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> HasPermissionAsync(int accountId, string permissionKey)
        {
            return await _context.AccountPermissions
                .Include(ap => ap.Permission)
                .AnyAsync(ap => ap.AccountId == accountId && ap.Permission.Method + ":" + ap.Permission.Path == permissionKey);
        }
    }
}
