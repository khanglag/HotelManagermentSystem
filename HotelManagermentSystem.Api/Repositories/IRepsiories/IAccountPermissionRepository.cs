using HotelManagementSystem.Api.Entities;

namespace HotelManagementSystem.Api.Repositories
{
    public interface IAccountPermissionRepository
    {
        Task<IEnumerable<AccountPermission>> GetByAccountIdAsync(int accountId);
        Task AddAsync(AccountPermission accountPermission);
        Task DeleteAsync(int accountId, int permissionId);
        Task<bool> ExistsAsync(int accountId, int permissionId);
    }
}