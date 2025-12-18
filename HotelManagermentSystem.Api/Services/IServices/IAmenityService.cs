using HotelManagermentSystem.Api.Dtos;

namespace HotelManagermentSystem.Api.Services.IServices
{
    public interface IAmenityService
    {
        Task<IEnumerable<AmentityDto>> GetAllAsync();
        Task<AmentityDto?> GetByIdAsync(int id);
        Task<AmentityDto> CreateAsync(AmentityDto dto);
        Task<bool> UpdateAsync(int id, AmentityDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
