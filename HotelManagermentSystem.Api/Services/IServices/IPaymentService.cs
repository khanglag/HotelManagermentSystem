using HotelManagermentSystem.Api.Dtos;

namespace HotelManagementSystem.Api.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllAsync();
        Task<IEnumerable<PaymentDto>> GetByInvoiceIdAsync(int invoiceId);
        Task<PaymentDto?> GetByIdAsync(int id);
        Task<PaymentDto> ProcessPaymentAsync(PaymentDto dto);
    }
}