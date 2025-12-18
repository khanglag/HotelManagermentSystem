using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Repositories
{
    public class ServiceUsageRepository : IServiceUsageRepository
    {
        private readonly AppDbContext _context;

        public ServiceUsageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceUsage>> GetAllAsync() =>
            await _context.ServiceUsages.Include(su => su.Service).AsNoTracking().ToListAsync();

        public async Task<IEnumerable<ServiceUsage>> GetByReservationDetailIdAsync(int detailId) =>
            await _context.ServiceUsages
                .Include(su => su.Service)
                .Where(su => su.ReservationDetailId == detailId)
                .ToListAsync();

        public async Task<ServiceUsage?> GetByIdAsync(int id) =>
            await _context.ServiceUsages.Include(su => su.Service).FirstOrDefaultAsync(su => su.Id == id);

        public async Task<ServiceUsage> AddAsync(ServiceUsage usage)
        {
            await _context.ServiceUsages.AddAsync(usage);
            await _context.SaveChangesAsync();
            return usage;
        }

        public async Task UpdateAsync(ServiceUsage usage)
        {
            _context.Entry(usage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ServiceUsages.FindAsync(id);
            if (entity != null)
            {
                _context.ServiceUsages.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}