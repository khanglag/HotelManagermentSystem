using HotelManagementSystem.Api.Entities;
using HotelManagermentSystem.Api.Dtos;
using HotelManagermentSystem.Api.Repositories.IRepsiories;
using HotelManagermentSystem.Api.Services.IServices;

namespace HotelManagermentSystem.Api.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _repository;

        public RoomTypeService(IRoomTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RoomTypeDto>> GetAllAsync()
        {
            var types = await _repository.GetAllAsync();
            return types.Select(MapToDto);
        }

        public async Task<RoomTypeDto?> GetByIdAsync(int id)
        {
            var type = await _repository.GetByIdAsync(id);
            return type == null ? null : MapToDto(type);
        }

        public async Task<RoomTypeDto> CreateAsync(RoomTypeDto dto)
        {
            var entity = new RoomType
            {
                Name = dto.Name,
                Description = dto.Description,
                DailyPrice = dto.DailyPrice,
                HourlyPrice = dto.HourlyPrice,
                ExtraHourPrice = dto.ExtraHourPrice,
                MaxHourlyDuration = dto.MaxHourlyDuration
            };
            var result = await _repository.AddAsync(entity);
            return MapToDto(result);
        }

        public async Task<bool> UpdateAsync(int id, RoomTypeDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.DailyPrice = dto.DailyPrice;
            existing.HourlyPrice = dto.HourlyPrice;
            existing.ExtraHourPrice = dto.ExtraHourPrice;
            existing.MaxHourlyDuration = dto.MaxHourlyDuration;

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

        private static RoomTypeDto MapToDto(RoomType rt) => new RoomTypeDto
        {
            Id = rt.Id,
            Name = rt.Name,
            Description = rt.Description,
            DailyPrice = rt.DailyPrice,
            HourlyPrice = rt.HourlyPrice,
            ExtraHourPrice = rt.ExtraHourPrice,
            MaxHourlyDuration = rt.MaxHourlyDuration
        };
    }
}
