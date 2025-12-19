using HotelManagementSystem.Api.Entities;
using HotelManagementSystem.Api.Repositories;
using HotelManagermentSystem.Api.Dtos;
using HotelManagementSystem.Api.Enums;

namespace HotelManagementSystem.Api.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _repository;

        public InvoiceService(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllAsync()
        {
            var invoices = await _repository.GetAllAsync();
            return invoices.Select(MapToDto);
        }

        public async Task<InvoiceDto?> GetByIdAsync(int id)
        {
            var invoice = await _repository.GetByIdAsync(id);
            return invoice == null ? null : MapToDto(invoice);
        }

        public async Task<InvoiceDto> CreateAsync(InvoiceDto dto)
        {
            var entity = new Invoice
            {
                ReservationId = dto.ReservationId,
                IssuedAt = DateTime.Now,
                DueDate = dto.DueDate,
                Amount = dto.Amount,
                TaxAmount = dto.TaxAmount,
                TotalAmount = dto.Amount + dto.TaxAmount, // Tự động tính tổng
                Status = dto.Status
            };

            var result = await _repository.AddAsync(entity);
            return MapToDto(result);
        }

        public async Task<bool> UpdateStatusAsync(int id, InvoiceStatus status)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Status = status;
            await _repository.UpdateAsync(existing);
            return true;
        }

        private static InvoiceDto MapToDto(Invoice i) => new InvoiceDto
        {
            Id = i.Id,
            ReservationId = i.ReservationId,
            IssuedAt = i.IssuedAt,
            DueDate = i.DueDate,
            Amount = i.Amount,
            TaxAmount = i.TaxAmount,
            TotalAmount = i.TotalAmount,
            Status = i.Status
        };
    }
}