using HotelManagementSystem.Api.Entities;
using HotelManagementSystem.Api.Repositories;
using HotelManagermentSystem.Api.Dtos;

namespace HotelManagementSystem.Api.Services
{
    public class ServiceUsageService : IServiceUsageService
    {
        private readonly IServiceUsageRepository _repository;

        public ServiceUsageService(IServiceUsageRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ServiceUsageDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(MapToDto);
        }

        public async Task<IEnumerable<ServiceUsageDto>> GetByReservationDetailAsync(int detailId)
        {
            var items = await _repository.GetByReservationDetailIdAsync(detailId);
            return items.Select(MapToDto);
        }

        public async Task<ServiceUsageDto?> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item == null ? null : MapToDto(item);
        }

        public async Task<ServiceUsageDto> CreateAsync(ServiceUsageDto dto)
        {
            var entity = new ServiceUsage
            {
                ReservationDetailId = dto.ReservationDetailId,
                ServiceId = dto.ServiceId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                TotalPrice = dto.Quantity * dto.UnitPrice, // Tự động tính tổng tiền
                UsedAt = DateTime.Now
            };
            var result = await _repository.AddAsync(entity);
            return MapToDto(result);
        }

        public async Task<bool> UpdateAsync(int id, ServiceUsageDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Quantity = dto.Quantity;
            existing.UnitPrice = dto.UnitPrice;
            existing.TotalPrice = dto.Quantity * dto.UnitPrice; // Cập nhật lại tổng tiền

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

        private static ServiceUsageDto MapToDto(ServiceUsage su) => new ServiceUsageDto
        {
            Id = su.Id,
            ReservationDetailId = su.ReservationDetailId,
            ServiceId = su.ServiceId,
            Quantity = su.Quantity,
            UnitPrice = su.UnitPrice,
            TotalPrice = su.TotalPrice,
            UsedAt = su.UsedAt,
            ServiceName = su.Service?.Name // Lấy tên dịch vụ từ Navigation Property
        };
    }
}