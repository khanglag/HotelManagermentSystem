using HotelManagementSystem.Api.Entities;
using HotelManagementSystem.Api.Repositories;
using HotelManagermentSystem.Api.Dtos;

namespace HotelManagementSystem.Api.Services
{
    public class ReservationDetailService : IReservationDetailService
    {
        private readonly IReservationDetailRepository _repository;

        public ReservationDetailService(IReservationDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReservationDetailDto>> GetByReservationIdAsync(int reservationId)
        {
            var details = await _repository.GetByReservationIdAsync(reservationId);
            return details.Select(MapToDto);
        }

        public async Task<ReservationDetailDto?> GetByIdAsync(int id)
        {
            var detail = await _repository.GetByIdAsync(id);
            return detail == null ? null : MapToDto(detail);
        }

        public async Task<ReservationDetailDto> CreateAsync(ReservationDetailDto dto)
        {
            var entity = new ReservationDetail
            {
                ReservationId = dto.ReservationId,
                RoomId = dto.RoomId,
                CheckInDateTime = dto.CheckInDateTime,
                CheckOutDateTime = dto.CheckOutDateTime,
                IsHourlyRate = dto.IsHourlyRate,
                AgreedHourlyRate = dto.AgreedHourlyRate,
                AgreedBaseDailyPrice = dto.AgreedBaseDailyPrice,
                AgreedExtraHourRate = dto.AgreedExtraHourRate,
                Status = dto.Status,
                // Trong thực tế, bạn sẽ gọi một hàm CalculatePrice ở đây
                SubTotalPrice = dto.SubTotalPrice
            };

            var result = await _repository.AddAsync(entity);
            return MapToDto(result);
        }

        public async Task<bool> UpdateAsync(int id, ReservationDetailDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.RoomId = dto.RoomId;
            existing.CheckInDateTime = dto.CheckInDateTime;
            existing.CheckOutDateTime = dto.CheckOutDateTime;
            existing.IsHourlyRate = dto.IsHourlyRate;
            existing.Status = dto.Status;
            existing.SubTotalPrice = dto.SubTotalPrice;

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

        private static ReservationDetailDto MapToDto(ReservationDetail rd) => new ReservationDetailDto
        {
            Id = rd.Id,
            ReservationId = rd.ReservationId,
            RoomId = rd.RoomId,
            RoomNumber = rd.Room?.RoomNumber,
            CheckInDateTime = rd.CheckInDateTime,
            CheckOutDateTime = rd.CheckOutDateTime,
            IsHourlyRate = rd.IsHourlyRate,
            AgreedHourlyRate = rd.AgreedHourlyRate,
            AgreedBaseDailyPrice = rd.AgreedBaseDailyPrice,
            AgreedExtraHourRate = rd.AgreedExtraHourRate,
            SubTotalPrice = rd.SubTotalPrice,
            Status = rd.Status
        };
    }
}