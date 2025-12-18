using HotelManagementSystem.Api.Entities;

namespace HotelManagermentSystem.Api.Repositories.IRepsiories
{
    public interface IRoomAmenityRepository
    {
        Task<IEnumerable<RoomAmenity>> GetByRoomIdAsync(int roomId);
        Task AddAsync(RoomAmenity roomAmenity);
        Task DeleteAsync(int roomId, int amenityId);
        Task<bool> ExistsAsync(int roomId, int amenityId);
    }
}
