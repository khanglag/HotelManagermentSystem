using HotelManagementSystem.Api.Entities;

namespace HotelManagementSystem.Api.Repositories
{
    public interface IServiceUsageRepository
    {
        Task<IEnumerable<ServiceUsage>> GetAllAsync();
        Task<IEnumerable<ServiceUsage>> GetByReservationDetailIdAsync(int detailId);
        Task<ServiceUsage?> GetByIdAsync(int id);
        Task<ServiceUsage> AddAsync(ServiceUsage usage);
        Task UpdateAsync(ServiceUsage usage);
        Task DeleteAsync(int id);
    }
}