using HotelManagermentSystem.Api.Dtos;

namespace HotelManagementSystem.Api.Services
{
    public interface IReservationDetailService
    {
        Task<IEnumerable<ReservationDetailDto>> GetByReservationIdAsync(int reservationId);
        Task<ReservationDetailDto?> GetByIdAsync(int id);
        Task<ReservationDetailDto> CreateAsync(ReservationDetailDto dto);
        Task<bool> UpdateAsync(int id, ReservationDetailDto dto);
        Task<bool> DeleteAsync(int id);
    }
}