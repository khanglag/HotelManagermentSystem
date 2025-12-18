using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Entities;
using HotelManagermentSystem.Api.Repositories.IRepsiories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagermentSystem.Api.Repositories
{
    public class GuestProfileRepository : IGuestProfileRepository
    {
        private readonly AppDbContext _context;

        public GuestProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GuestProfile>> GetAllAsync() =>
            await _context.GuestProfiles.ToListAsync();

        public async Task<GuestProfile?> GetByIdAsync(int id) =>
            await _context.GuestProfiles.FindAsync(id);

        public async Task<GuestProfile> AddAsync(GuestProfile guestProfile)
        {
            await _context.GuestProfiles.AddAsync(guestProfile);
            await _context.SaveChangesAsync();
            return guestProfile;
        }

        public async Task UpdateAsync(GuestProfile guestProfile)
        {
            _context.Entry(guestProfile).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var guest = await _context.GuestProfiles.FindAsync(id);
            if (guest != null)
            {
                _context.GuestProfiles.Remove(guest);
                await _context.SaveChangesAsync();
            }
        }
    }
}
