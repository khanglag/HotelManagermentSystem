using HotelManagementSystem.Api.Entities;
using HotelManagementSystem.Api.Repositories;
using HotelManagermentSystem.Api.Dtos;

namespace HotelManagementSystem.Api.Services
{
    public class AccountPermissionService : IAccountPermissionService
    {
        private readonly IAccountPermissionRepository _repository;

        public AccountPermissionService(IAccountPermissionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PermissionDto>> GetPermissionsByAccountAsync(int accountId)
        {
            var results = await _repository.GetByAccountIdAsync(accountId);
            return results.Select(ap => new PermissionDto
            {
                Id = ap.Permission.Id,
                Path = ap.Permission.Path,
                Method = ap.Permission.Method,
                Description = ap.Permission.Description
            });
        }

        public async Task<bool> AssignPermissionAsync(int accountId, int permissionId)
        {
            if (await _repository.ExistsAsync(accountId, permissionId)) return false;

            await _repository.AddAsync(new AccountPermission
            {
                AccountId = accountId,
                PermissionId = permissionId
            });
            return true;
        }

        public async Task<bool> RevokePermissionAsync(int accountId, int permissionId)
        {
            if (!await _repository.ExistsAsync(accountId, permissionId)) return false;

            await _repository.DeleteAsync(accountId, permissionId);
            return true;
        }
    }
}