using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Entities;
using HotelManagermentSystem.Api.Repositories.IRepsiories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagermentSystem.Api.Repositories
{
    public class AmenityRepository : IAmenityRepository
    {
        private readonly AppDbContext _context;

        public AmenityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Amenity>> GetAllAsync() =>
            await _context.Amenities.AsNoTracking().ToListAsync();

        public async Task<Amenity?> GetByIdAsync(int id) =>
            await _context.Amenities.FindAsync(id);

        public async Task<Amenity> AddAsync(Amenity amenity)
        {
            await _context.Amenities.AddAsync(amenity);
            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task UpdateAsync(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);
            if (amenity != null)
            {
                _context.Amenities.Remove(amenity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
