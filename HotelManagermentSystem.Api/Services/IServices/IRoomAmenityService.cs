using HotelManagermentSystem.Api.Dtos;

namespace HotelManagermentSystem.Api.Services.IServices
{
    public interface IRoomAmenityService
    {
        Task<IEnumerable<AmentityDto>> GetAmenitiesByRoomAsync(int roomId);
        Task<bool> AddAmenityToRoomAsync(int roomId, int amenityId);
        Task<bool> RemoveAmenityFromRoomAsync(int roomId, int amenityId);
    }
}
