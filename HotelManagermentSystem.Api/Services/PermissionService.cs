using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Entities;
using HotelManagementSystem.Api.Repositories;
using HotelManagermentSystem.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _repository;
        public PermissionService(IPermissionRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> HasPermissionAsync(int accountId, string permissionKey)
        {
            return await _repository.HasPermissionAsync(accountId, permissionKey);
        }
        public async Task<IEnumerable<PermissionDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(MapToDto);
        }

        public async Task<PermissionDto?> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item == null ? null : MapToDto(item);
        }

        public async Task<PermissionDto> CreateAsync(PermissionDto dto)
        {
            var entity = new Permission
            {
                Path = dto.Path,
                Method = dto.Method,
                Description = dto.Description ?? string.Empty
            };
            var result = await _repository.AddAsync(entity);
            return MapToDto(result);
        }

        public async Task<bool> UpdateAsync(int id, PermissionDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Path = dto.Path;
            existing.Method = dto.Method;
            existing.Description = dto.Description ?? string.Empty;

            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;
            await _repository.DeleteAsync(id);
            return true;
        }

        private static PermissionDto MapToDto(Permission p) => new PermissionDto
        {
            Id = p.Id,
            Path = p.Path,
            Method = p.Method,
            Description = p.Description
        };
    }
}
