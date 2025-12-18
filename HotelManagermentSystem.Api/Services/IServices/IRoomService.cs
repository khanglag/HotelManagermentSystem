using HotelManagermentSystem.Api.Dtos;

namespace HotelManagermentSystem.Api.Services.IServices
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllAsync();
        Task<RoomDto?> GetByIdAsync(int id);
        Task<RoomDto> CreateAsync(RoomDto dto);
        Task<bool> UpdateAsync(int id, RoomDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
