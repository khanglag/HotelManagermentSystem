using HotelManagementSystem.Api.Enums;
using HotelManagermentSystem.Api.Dtos;

namespace HotelManagementSystem.Api.Services
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDto>> GetAllAsync();
        Task<InvoiceDto?> GetByIdAsync(int id);
        Task<InvoiceDto> CreateAsync(InvoiceDto dto);
        Task<bool> UpdateStatusAsync(int id, InvoiceStatus status);
    }
}