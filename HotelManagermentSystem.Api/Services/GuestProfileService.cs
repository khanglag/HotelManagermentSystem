using HotelManagementSystem.Api.Entities;
using HotelManagermentSystem.Api.Dtos;
using HotelManagermentSystem.Api.Repositories.IRepsiories;
using HotelManagermentSystem.Api.Services.IServices;

namespace HotelManagermentSystem.Api.Services
{
    public class GuestProfileService : IGuestProfileService
    {
        private readonly IGuestProfileRepository _repository;

        public GuestProfileService(IGuestProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GuestProfileDto>> GetAllAsync()
        {
            var guests = await _repository.GetAllAsync();
            return guests.Select(g => MapToDto(g));
        }

        public async Task<GuestProfileDto?> GetByIdAsync(int id)
        {
            var guest = await _repository.GetByIdAsync(id);
            return guest == null ? null : MapToDto(guest);
        }

        public async Task<GuestProfileDto> CreateAsync(GuestProfileDto dto)
        {
            var entity = new GuestProfile
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                DayOfBirth = dto.DayOfBirth,
                INumber = dto.IdNumber, // Mapping IdNumber -> INumber
                ReservationDetailId = dto.ReservationDetailId
            };

            var result = await _repository.AddAsync(entity);
            dto.Id = result.Id;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, GuestProfileDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = dto.Name;
            existing.Email = dto.Email;
            existing.PhoneNumber = dto.PhoneNumber;
            existing.DayOfBirth = dto.DayOfBirth;
            existing.INumber = dto.IdNumber;
            existing.ReservationDetailId = dto.ReservationDetailId;

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

        // Hàm hỗ trợ ánh xạ thủ công
        private static GuestProfileDto MapToDto(GuestProfile g) => new GuestProfileDto
        {
            Id = g.Id,
            Name = g.Name,
            Email = g.Email,
            PhoneNumber = g.PhoneNumber,
            DayOfBirth = g.DayOfBirth,
            IdNumber = g.INumber, // Mapping INumber -> IdNumber
            ReservationDetailId = g.ReservationDetailId
        };
    }
}

