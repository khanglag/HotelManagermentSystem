using HotelManagementSystem.Api.Entities;
using HotelManagementSystem.Api.Repositories;
using HotelManagermentSystem.Api.Dtos;

namespace HotelManagementSystem.Api.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;

        public ReservationService(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(MapToDto);
        }

        public async Task<ReservationDto?> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item == null ? null : MapToDto(item);
        }

        public async Task<ReservationDto> CreateAsync(ReservationDto dto)
        {
            var entity = new Reservation
            {
                CustomerId = dto.CustomerId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                TotalAmount = 0, // Sẽ được tính từ ReservationDetails sau
                Status = dto.Status,
                CreatedAt = DateTime.Now
            };
            var result = await _repository.AddAsync(entity);
            return MapToDto(result);
        }

        public async Task<bool> UpdateAsync(int id, ReservationDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.CheckInDate = dto.CheckInDate;
            existing.CheckOutDate = dto.CheckOutDate;
            existing.Status = dto.Status;
            existing.CheckinEmployeeId = dto.CheckinEmployeeId ?? 0;
            existing.CheckoutEmployeeId = dto.CheckoutEmployeeId ?? 0;
            existing.ActualCheckinTime = dto.ActualCheckinTime ?? DateTime.MinValue;
            existing.ActualCheckoutTime = dto.ActualCheckoutTime ?? DateTime.MinValue;

            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;
            await _repository.DeleteAsync(id);
            return true;
        }

        private static ReservationDto MapToDto(Reservation r) => new ReservationDto
        {
            Id = r.Id,
            CustomerId = r.CustomerId,
            CustomerName = r.Customer?.Name,
            CheckInDate = r.CheckInDate,
            CheckOutDate = r.CheckOutDate,
            TotalAmount = r.TotalAmount,
            CheckinEmployeeId = r.CheckinEmployeeId,
            CheckoutEmployeeId = r.CheckoutEmployeeId,
            ActualCheckinTime = r.ActualCheckinTime,
            ActualCheckoutTime = r.ActualCheckoutTime,
            CreatedAt = r.CreatedAt,
            Status = r.Status
        };
    }
}