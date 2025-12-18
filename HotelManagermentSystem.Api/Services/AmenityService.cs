using HotelManagementSystem.Api.Entities;
using HotelManagermentSystem.Api.Dtos;
using HotelManagermentSystem.Api.Repositories.IRepsiories;
using HotelManagermentSystem.Api.Services.IServices;

namespace HotelManagermentSystem.Api.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly IAmenityRepository _repository;

        public AmenityService(IAmenityRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AmentityDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(MapToDto);
        }

        public async Task<AmentityDto?> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item == null ? null : MapToDto(item);
        }

        public async Task<AmentityDto> CreateAsync(AmentityDto dto)
        {
            var entity = new Amenity
            {
                Name = dto.Name,
                Description = dto.Description
            };
            var result = await _repository.AddAsync(entity);
            return MapToDto(result);
        }

        public async Task<bool> UpdateAsync(int id, AmentityDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = dto.Name;
            existing.Description = dto.Description;

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

        private static AmentityDto MapToDto(Amenity a) => new AmentityDto
        {
            Id = a.Id,
            Name = a.Name,
            Description = a.Description
        };
    }
}
