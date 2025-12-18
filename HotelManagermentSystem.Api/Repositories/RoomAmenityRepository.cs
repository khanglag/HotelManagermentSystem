using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Entities;
using HotelManagermentSystem.Api.Repositories.IRepsiories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagermentSystem.Api.Repositories
{
    public class RoomAmenityRepository : IRoomAmenityRepository
    {
        private readonly AppDbContext _context;

        public RoomAmenityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomAmenity>> GetByRoomIdAsync(int roomId)
        {
            return await _context.RoomAmenities
                .Include(ra => ra.Amenity)
                .Where(ra => ra.RoomId == roomId)
                .ToListAsync();
        }

        public async Task AddAsync(RoomAmenity roomAmenity)
        {
            await _context.RoomAmenities.AddAsync(roomAmenity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int roomId, int amenityId)
        {
            var entity = await _context.RoomAmenities
                .FirstOrDefaultAsync(ra => ra.RoomId == roomId && ra.AmenityId == amenityId);
            if (entity != null)
            {
                _context.RoomAmenities.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int roomId, int amenityId) =>
            await _context.RoomAmenities.AnyAsync(ra => ra.RoomId == roomId && ra.AmenityId == amenityId);
    }
}
