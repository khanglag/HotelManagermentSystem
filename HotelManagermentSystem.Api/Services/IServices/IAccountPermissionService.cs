using HotelManagermentSystem.Api.Dtos;

namespace HotelManagementSystem.Api.Services
{
    public interface IAccountPermissionService
    {
        Task<IEnumerable<PermissionDto>> GetPermissionsByAccountAsync(int accountId);
        Task<bool> AssignPermissionAsync(int accountId, int permissionId);
        Task<bool> RevokePermissionAsync(int accountId, int permissionId);
    }
}