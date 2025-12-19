using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Repositories
{
    public class ReservationDetailRepository : IReservationDetailRepository
    {
        private readonly AppDbContext _context;

        public ReservationDetailRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReservationDetail>> GetByReservationIdAsync(int reservationId) =>
            await _context.ReservationDetails
                .Include(rd => rd.Room)
                .Where(rd => rd.ReservationId == reservationId)
                .AsNoTracking().ToListAsync();

        public async Task<ReservationDetail?> GetByIdAsync(int id) =>
            await _context.ReservationDetails
                .Include(rd => rd.Room)
                .Include(rd => rd.GuestProfiles)
                .FirstOrDefaultAsync(rd => rd.Id == id);

        public async Task<ReservationDetail> AddAsync(ReservationDetail detail)
        {
            await _context.ReservationDetails.AddAsync(detail);
            await _context.SaveChangesAsync();
            return detail;
        }

        public async Task UpdateAsync(ReservationDetail detail)
        {
            _context.Entry(detail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ReservationDetails.FindAsync(id);
            if (entity != null)
            {
                _context.ReservationDetails.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}