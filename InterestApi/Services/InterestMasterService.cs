using AutoMapper;
using InterestMasterApi.Repositories;
using static InterestMasterApi.Dtos.InterestMasterDto;

namespace InterestMasterApi.Services
{
    public class InterestMasterService : IInterestMasterService
    {
        private IInterestMasterRepository _interestMasterRepository;
        private ILogger<InterestMasterService> _logger;
        private IMapper _mapper;
        public InterestMasterService(IInterestMasterRepository interestMasterRepository, ILogger<InterestMasterService> logger, IMapper mapper)
        {
            _interestMasterRepository = interestMasterRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetInterestDto>> GetAccountInterestsAsync()
        {
            try
            {
                var interests = await _interestMasterRepository.GetAccountInterestsAsync();


                var interestDtos = _mapper.Map<IEnumerable<GetInterestDto>>(interests);

                return interestDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
