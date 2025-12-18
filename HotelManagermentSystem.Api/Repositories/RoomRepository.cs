using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Entities;
using HotelManagermentSystem.Api.Repositories.IRepsiories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagermentSystem.Api.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _context;

        public RoomRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllAsync() =>
            await _context.Rooms.AsNoTracking().ToListAsync();

        public async Task<Room?> GetByIdAsync(int id) =>
            await _context.Rooms.FindAsync(id);

        public async Task<Room> AddAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task UpdateAsync(Room room)
        {
            // EF Core tự động so sánh RowVersion khi SaveChanges
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }
    }
}
