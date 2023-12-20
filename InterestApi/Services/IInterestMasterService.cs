using static InterestMasterApi.Dtos.InterestMasterDto;

namespace InterestMasterApi.Services
{
    public interface IInterestMasterService
    {
        Task<IEnumerable<GetInterestDto>> GetAccountInterestsAsync();
    }
}
