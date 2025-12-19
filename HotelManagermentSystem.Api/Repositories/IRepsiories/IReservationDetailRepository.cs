using HotelManagementSystem.Api.Entities;

namespace HotelManagementSystem.Api.Repositories
{
    public interface IReservationDetailRepository
    {
        Task<IEnumerable<ReservationDetail>> GetByReservationIdAsync(int reservationId);
        Task<ReservationDetail?> GetByIdAsync(int id);
        Task<ReservationDetail> AddAsync(ReservationDetail detail);
        Task UpdateAsync(ReservationDetail detail);
        Task DeleteAsync(int id);
    }
}