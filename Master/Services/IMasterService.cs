using static Master.Dtos.MasterDto;

namespace Master.Services
{
    public interface IMasterService
    {
        Task<IEnumerable<GetInterestDto>> GetInterestsAsync();
        Task<GetInterestDto> GetInterestByIdAsync(Guid Id);
        Task<CreateInterestDto> CreateInterestAsync(CreateInterestDto interestDto);
        Task<UpdateInterestDto> UpdateInterestAsync(Guid Id, UpdateInterestDto interest);
        Task<bool> DeleteInterestAsync(Guid Id);
    }
}
