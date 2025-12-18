using HotelManagermentSystem.Api.Dtos;

namespace HotelManagermentSystem.Api.Services.IServices
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchDto>> GetAllBranchesAsync();
        Task<BranchDto?> GetBranchByIdAsync(int id);
        Task<BranchDto> CreateBranchAsync(BranchDto branchDto);
        Task<bool> UpdateBranchAsync(int id, BranchDto branchDto);
        Task<bool> DeleteBranchAsync(int id);
    }
}
