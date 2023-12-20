using InterestMasterApi.Entities;

namespace InterestMasterApi.Repositories
{
    public interface IInterestMasterRepository
    {
        Task<IEnumerable<InterestMaster>> GetAccountInterestsAsync();
    }
}
