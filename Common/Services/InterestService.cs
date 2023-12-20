using AccountDatabase.Entities;
using AutoMapper;
using Common.Repositories;
using static Common.Dtos.Interest;

namespace Common.Services
{
    public class InterestService : IInterestService
    {
        private IInterestRepository _interestRepository;
        private ILogger<InterestService> _logger;
        private readonly IMapper _mapper;

        public InterestService(IInterestRepository interestRepository, ILogger<InterestService> logger, IMapper mapper)
        {
            _interestRepository = interestRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetInterestDto>> GetInterestsAsync()
        {
            try
            {
                var interestEntities = await _interestRepository.GetAllAsync();
                if (interestEntities != null && interestEntities.Any())
                {

                    var accountDtos = _mapper.Map<IEnumerable<GetInterestDto>>(interestEntities);

                    return accountDtos;
                }
                else { return Enumerable.Empty<GetInterestDto>(); }

                // Map List<Account> to IEnumerable<GetAccountDto>

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetInterestDto> GetInterestByIdAsync(Guid Id)
        {
            try
            {

                var interest = await _interestRepository.GetByIdAsync(Id);

                var interestDto = _mapper.Map<GetInterestDto>(interest);

                return interestDto;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<CreateInterestDto> CreateInterestAsync(CreateInterestDto interestDto)
        {
            try
            {
                var createdInterest = _mapper.Map<InterestMaster>(interestDto);

                var interest = await _interestRepository.CreateAsync(createdInterest);

                var createdInterestDto = _mapper.Map<CreateInterestDto>(interest);

                return createdInterestDto;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<UpdateInterestDto> UpdateInterestAsync(Guid Id, UpdateInterestDto interest)
        {
            try
            {
                var existingInterest = await _interestRepository.GetByIdAsync(Id);

                if (existingInterest == null)
                {
                    throw new Exception("Interest not found for update.");
                }

                _mapper.Map(interest, existingInterest);


                var updatedInterest = await _interestRepository.UpdateAsync(existingInterest);

                var updatedInterestDto = _mapper.Map<UpdateInterestDto>(updatedInterest);

                return updatedInterestDto;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<bool> DeleteInterestAsync(Guid Id)
        {
            try
            {
                var interestItem = await _interestRepository.GetByIdAsync(Id);

                await _interestRepository.DeleteAsync(interestItem);
                return true;
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
