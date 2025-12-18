using HotelManagementSystem.Api.Entities;
using HotelManagermentSystem.Api.Dtos;
using HotelManagermentSystem.Api.Repositories.IRepsiories;
using HotelManagermentSystem.Api.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace HotelManagermentSystem.Api.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;

        public RoomService(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RoomDto>> GetAllAsync()
        {
            var rooms = await _repository.GetAllAsync();
            return rooms.Select(MapToDto);
        }

        public async Task<RoomDto?> GetByIdAsync(int id)
        {
            var room = await _repository.GetByIdAsync(id);
            return room == null ? null : MapToDto(room);
        }

        public async Task<RoomDto> CreateAsync(RoomDto dto)
        {
            var entity = new Room
            {
                RoomNumber = dto.RoomNumber,
                RoomTypeId = dto.RoomTypeId,
                BranchId = dto.BranchId,
                Status = dto.Status
            };
            await _repository.AddAsync(entity);
            return MapToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, RoomDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            // Cập nhật thông tin
            existing.RoomNumber = dto.RoomNumber;
            existing.RoomTypeId = dto.RoomTypeId;
            existing.BranchId = dto.BranchId;
            existing.Status = dto.Status;

            // Gán RowVersion cũ vào thuộc tính OriginalValue để EF Core thực hiện so sánh (Concurrency Check)
            // Nếu RowVersion trong DB đã khác so với dto.RowVersion, Exception sẽ văng ra.
            existing.RowVersion = dto.RowVersion;

            try
            {
                await _repository.UpdateAsync(existing);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Logic xử lý khi có tranh chấp dữ liệu
                throw new Exception("Dữ liệu đã bị thay đổi bởi người dùng khác. Vui lòng tải lại trang.");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;
            await _repository.DeleteAsync(id);
            return true;
        }

        private static RoomDto MapToDto(Room r) => new RoomDto
        {
            Id = r.Id,
            RoomNumber = r.RoomNumber,
            RoomTypeId = r.RoomTypeId,
            BranchId = r.BranchId,
            Status = r.Status,
            RowVersion = r.RowVersion
        };
    }
}
