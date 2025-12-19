using HotelManagementSystem.Api.Entities;
using HotelManagementSystem.Api.Repositories;
using HotelManagermentSystem.Api.Dtos;

namespace HotelManagementSystem.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;

        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllAsync()
        {
            var payments = await _repository.GetAllAsync();
            return payments.Select(MapToDto);
        }

        public async Task<IEnumerable<PaymentDto>> GetByInvoiceIdAsync(int invoiceId)
        {
            var payments = await _repository.GetByInvoiceIdAsync(invoiceId);
            return payments.Select(MapToDto);
        }

        public async Task<PaymentDto?> GetByIdAsync(int id)
        {
            var payment = await _repository.GetByIdAsync(id);
            return payment == null ? null : MapToDto(payment);
        }

        public async Task<PaymentDto> ProcessPaymentAsync(PaymentDto dto)
        {
            var entity = new Payment
            {
                InvoiceId = dto.InvoiceId,
                PaymentDate = DateTime.Now,
                Amount = dto.Amount,
                Method = dto.Method,
                Status = dto.Status
            };

            var result = await _repository.AddAsync(entity);
            return MapToDto(result);
        }

        private static PaymentDto MapToDto(Payment p) => new PaymentDto
        {
            Id = p.Id,
            InvoiceId = p.InvoiceId,
            PaymentDate = p.PaymentDate,
            Amount = p.Amount,
            Method = p.Method,
            Status = p.Status
        };
    }
}