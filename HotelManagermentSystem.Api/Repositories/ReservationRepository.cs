using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync() =>
            await _context.Reservations
                .Include(r => r.Customer)
                .AsNoTracking().ToListAsync();

        public async Task<Reservation?> GetByIdAsync(int id) =>
            await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.ReservationDetails)
                .FirstOrDefaultAsync(r => r.Id == id);

        public async Task<Reservation> AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Reservations.FindAsync(id);
            if (entity != null)
            {
                _context.Reservations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}