using HotelManagementSystem.Api.Entities;

namespace HotelManagementSystem.Api.Repositories
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<Permission?> GetByIdAsync(int id);
        Task<Permission> AddAsync(Permission permission);
        Task UpdateAsync(Permission permission);
        Task DeleteAsync(int id);
        Task<bool> HasPermissionAsync(int accountId, string permissionKey);
    }
}