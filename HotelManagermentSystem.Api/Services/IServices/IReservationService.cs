using HotelManagermentSystem.Api.Dtos;

namespace HotelManagementSystem.Api.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllAsync();
        Task<ReservationDto?> GetByIdAsync(int id);
        Task<ReservationDto> CreateAsync(ReservationDto dto);
        Task<bool> UpdateAsync(int id, ReservationDto dto);
        Task<bool> DeleteAsync(int id);
    }
}