using HotelManagermentSystem.Api.Dtos;

namespace HotelManagermentSystem.Api.Services.IServices
{
    public interface IGuestProfileService
    {
        Task<IEnumerable<GuestProfileDto>> GetAllAsync();
        Task<GuestProfileDto?> GetByIdAsync(int id);
        Task<GuestProfileDto> CreateAsync(GuestProfileDto dto);
        Task<bool> UpdateAsync(int id, GuestProfileDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
