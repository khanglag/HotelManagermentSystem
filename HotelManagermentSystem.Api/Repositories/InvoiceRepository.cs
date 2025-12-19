using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync() =>
            await _context.Invoices.AsNoTracking().ToListAsync();

        public async Task<Invoice?> GetByIdAsync(int id) =>
            await _context.Invoices
                .Include(i => i.Payments)
                .FirstOrDefaultAsync(i => i.Id == id);

        public async Task<Invoice?> GetByReservationIdAsync(int reservationId) =>
            await _context.Invoices
                .FirstOrDefaultAsync(i => i.ReservationId == reservationId);

        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            _context.Entry(invoice).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Invoices.FindAsync(id);
            if (entity != null)
            {
                _context.Invoices.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}