using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Entities;
using HotelManagermentSystem.Api.Repositories.IRepsiories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagermentSystem.Api.Repositories
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly AppDbContext _context;

        public RoomTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomType>> GetAllAsync() =>
            await _context.RoomTypes.AsNoTracking().ToListAsync();

        public async Task<RoomType?> GetByIdAsync(int id) =>
            await _context.RoomTypes.FindAsync(id);

        public async Task<RoomType> AddAsync(RoomType roomType)
        {
            await _context.RoomTypes.AddAsync(roomType);
            await _context.SaveChangesAsync();
            return roomType;
        }

        public async Task UpdateAsync(RoomType roomType)
        {
            _context.Entry(roomType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.RoomTypes.FindAsync(id);
            if (entity != null)
            {
                _context.RoomTypes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
