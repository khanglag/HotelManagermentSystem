using HotelManagermentSystem.Api.Dtos;

namespace HotelManagermentSystem.Api.Services.IServices
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetAllAsync();
        Task<ServiceDto?> GetByIdAsync(int id);
        Task<ServiceDto> CreateAsync(ServiceDto dto);
        Task<bool> UpdateAsync(int id, ServiceDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
