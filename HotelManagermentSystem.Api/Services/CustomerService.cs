using HotelManagementSystem.Api.Dtos;
using HotelManagementSystem.Api.Entities;
using HotelManagementSystem.Api.Repositories.IRepsiories;
using HotelManagementSystem.Api.Services.IServices;

namespace HotelManagementSystem.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(CustomerDto customer)
        {
            var checkEmail = await _repository.GetByEmailAsync(customer.Email);
            if (checkEmail != null) throw new KeyNotFoundException($"Email has been used");
            var checkPhone = await _repository.GetByPhoneNumberAsync(customer.Phone);
            if (checkPhone != null) throw new KeyNotFoundException($"Phone number has been used");

            var newCustomer = new Customer
            {
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.Phone
            };
            await _repository.AddAsync(newCustomer);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Customer?> GetByPhoneNumberAsync(string phoneNumber)
        {
           return await _repository.GetByPhoneNumberAsync(phoneNumber);
        }

        public async Task UpdateAsync(CustomerDto customer)
        {
            var existingCustomer = await _repository.GetByIdAsync(customer.ID);
            if (existingCustomer == null) throw new KeyNotFoundException("Not found customer");
            
            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNumber = customer.Phone;
            
            await _repository.UpdateAsync(existingCustomer);
        }
    }
}
