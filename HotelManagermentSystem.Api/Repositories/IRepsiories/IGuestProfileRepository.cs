using HotelManagementSystem.Api.Entities;

namespace HotelManagermentSystem.Api.Repositories.IRepsiories
{
    public interface IGuestProfileRepository
    {
        Task<IEnumerable<GuestProfile>> GetAllAsync();
        Task<GuestProfile?> GetByIdAsync(int id);
        Task<GuestProfile> AddAsync(GuestProfile guestProfile);
        Task UpdateAsync(GuestProfile guestProfile);
        Task DeleteAsync(int id);
    }
}
