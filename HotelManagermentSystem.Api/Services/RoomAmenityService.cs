using HotelManagementSystem.Api.Entities;
using HotelManagermentSystem.Api.Dtos;
using HotelManagermentSystem.Api.Repositories.IRepsiories;
using HotelManagermentSystem.Api.Services.IServices;

namespace HotelManagermentSystem.Api.Services
{
    public class RoomAmenityService : IRoomAmenityService
    {
        private readonly IRoomAmenityRepository _repository;

        public RoomAmenityService(IRoomAmenityRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AmentityDto>> GetAmenitiesByRoomAsync(int roomId)
        {
            var roomAmenities = await _repository.GetByRoomIdAsync(roomId);
            return roomAmenities.Select(ra => new AmentityDto
            {
                Id = ra.Amenity.Id,
                Name = ra.Amenity.Name,
                Description = ra.Amenity.Description
            });
        }

        public async Task<bool> AddAmenityToRoomAsync(int roomId, int amenityId)
        {
            if (await _repository.ExistsAsync(roomId, amenityId)) return false;

            await _repository.AddAsync(new RoomAmenity
            {
                RoomId = roomId,
                AmenityId = amenityId
            });
            return true;
        }

        public async Task<bool> RemoveAmenityFromRoomAsync(int roomId, int amenityId)
        {
            if (!await _repository.ExistsAsync(roomId, amenityId)) return false;

            await _repository.DeleteAsync(roomId, amenityId);
            return true;
        }
    }
}
