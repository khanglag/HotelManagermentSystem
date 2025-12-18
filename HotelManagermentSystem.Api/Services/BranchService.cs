using HotelManagementSystem.Api.Entities;
using HotelManagermentSystem.Api.Dtos;
using HotelManagermentSystem.Api.Repositories.IRepsiories;
using HotelManagermentSystem.Api.Services.IServices;

namespace HotelManagermentSystem.Api.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _repository;

        public BranchService(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BranchDto>> GetAllBranchesAsync()
        {
            var branches = await _repository.GetAllAsync();
            return branches.Select(b => new BranchDto
            {
                Id = b.Id,
                Name = b.Name,
                Location = b.Location
            });
        }

        public async Task<BranchDto?> GetBranchByIdAsync(int id)
        {
            var b = await _repository.GetByIdAsync(id);
            return b == null ? null : new BranchDto { Id = b.Id, Name = b.Name, Location = b.Location };
        }

        public async Task<BranchDto> CreateBranchAsync(BranchDto branchDto)
        {
            var branch = new Branch { Name = branchDto.Name, Location = branchDto.Location };
            var result = await _repository.AddAsync(branch);
            branchDto.Id = result.Id;
            return branchDto;
        }

        public async Task<bool> UpdateBranchAsync(int id, BranchDto branchDto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = branchDto.Name;
            existing.Location = branchDto.Location;

            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteBranchAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
