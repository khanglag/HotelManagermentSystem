using HotelManagermentSystem.Api.Dtos;

namespace HotelManagementSystem.Api.Services
{
    public interface IServiceUsageService
    {
        Task<IEnumerable<ServiceUsageDto>> GetAllAsync();
        Task<IEnumerable<ServiceUsageDto>> GetByReservationDetailAsync(int detailId);
        Task<ServiceUsageDto?> GetByIdAsync(int id);
        Task<ServiceUsageDto> CreateAsync(ServiceUsageDto dto);
        Task<bool> UpdateAsync(int id, ServiceUsageDto dto);
        Task<bool> DeleteAsync(int id);
    }
}