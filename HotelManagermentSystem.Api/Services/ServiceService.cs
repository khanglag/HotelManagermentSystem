using HotelManagementSystem.Api.Entities;
using HotelManagermentSystem.Api.Dtos;
using HotelManagermentSystem.Api.Repositories.IRepsiories;
using HotelManagermentSystem.Api.Services.IServices;

namespace HotelManagermentSystem.Api.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repository;

        public ServiceService(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ServiceDto>> GetAllAsync()
        {
            var services = await _repository.GetAllAsync();
            return services.Select(MapToDto);
        }

        public async Task<ServiceDto?> GetByIdAsync(int id)
        {
            var service = await _repository.GetByIdAsync(id);
            return service == null ? null : MapToDto(service);
        }

        public async Task<ServiceDto> CreateAsync(ServiceDto dto)
        {
            var entity = new Service
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                IsActive = dto.IsActive
            };
            var result = await _repository.AddAsync(entity);
            return MapToDto(result);
        }

        public async Task<bool> UpdateAsync(int id, ServiceDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.IsActive = dto.IsActive;

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

        private static ServiceDto MapToDto(Service s) => new ServiceDto
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description,
            Price = s.Price,
            IsActive = s.IsActive
        };
    }
}
