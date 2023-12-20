using AccountDatabase.Entities;
using AutoMapper;
using Master.Repositories;
using static Master.Dtos.MasterDto;

namespace Master.Services
{
    public class MasterService : IMasterService
    {
        private IMasterRepository _masterRepository;
        private ILogger<MasterService> _logger;
        private readonly IMapper _mapper;
        //constructor

        public MasterService(IMasterRepository masterRepository, ILogger<MasterService> logger, IMapper mapper)
        {
            _masterRepository = masterRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetInterestDto>> GetInterestsAsync()
        {
            try
            {
                var interestEntities = await _masterRepository.GetAllAsync();
                if(interestEntities != null && interestEntities.Any()) {

                    var accountDtos = _mapper.Map<IEnumerable<GetInterestDto>>(interestEntities);

                    return accountDtos;
                }
                else { return Enumerable.Empty<GetInterestDto>();}

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

                var interest = await _masterRepository.GetByIdAsync(Id);

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

                var interest = await _masterRepository.CreateAsync(createdInterest);

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
                var existingInterest = await _masterRepository.GetByIdAsync(Id);

                if (existingInterest == null)
                {
                    throw new Exception("Interest not found for update.");
                }

                _mapper.Map(interest, existingInterest);


                var updatedInterest = await _masterRepository.UpdateAsync(existingInterest);

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
                var interestItem = await _masterRepository.GetByIdAsync(Id);

                await _masterRepository.DeleteAsync(interestItem);
                return true;
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
    
}
