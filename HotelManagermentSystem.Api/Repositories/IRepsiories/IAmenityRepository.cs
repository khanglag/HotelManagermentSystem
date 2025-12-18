using HotelManagementSystem.Api.Entities;

namespace HotelManagermentSystem.Api.Repositories.IRepsiories
{
    public interface IAmenityRepository
    {
        Task<IEnumerable<Amenity>> GetAllAsync();
        Task<Amenity?> GetByIdAsync(int id);
        Task<Amenity> AddAsync(Amenity amenity);
        Task UpdateAsync(Amenity amenity);
        Task DeleteAsync(int id);
    }
}
