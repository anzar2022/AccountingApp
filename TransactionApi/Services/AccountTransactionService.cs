using AutoMapper;
using TransactionApi.Dtos;
using TransactionApi.Entities;
using TransactionApi.Repositories;

namespace TransactionApi.Services
{
    public class AccountTransactionService : IAccountTransactionService
    {
        private IAccountTransactionRepository  _accountTransactionRepository;
        private ILogger<AccountTransactionService> _logger;
        private IMapper _mapper;
        public AccountTransactionService(IAccountTransactionRepository  accountTransactionRepository , ILogger<AccountTransactionService> logger, IMapper mapper) { 
            _accountTransactionRepository = accountTransactionRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CreateAccountTransactionDto> CreateAccountTransactionAsync(CreateAccountTransactionDto account)
        {
            try
            {
                var createdAccount = _mapper.Map<AccountTransaction>(account);

                var accounts = await _accountTransactionRepository.CreateAccountTransactionAsync(createdAccount);

                var createdAccountDto = _mapper.Map<CreateAccountTransactionDto>(accounts);

                return createdAccountDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<bool> DeleteAccountTransactionAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAccountTransactionDto> GetAccountTransactionByIdAsync(Guid Id)
        {
            try
            {

                var accounts = await _accountTransactionRepository.GetAccountTransactionByIdAsync(Id);

                var accountDto = _mapper.Map<GetAccountTransactionDto>(accounts);

                return accountDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<GetAccountTransactionDto>> GetAccountTransactionsAsync()
        {
            try
            {
                var accountEntities = await _accountTransactionRepository.GetAccountTransactionsAsync();

              
                var accountDtos = _mapper.Map<IEnumerable<GetAccountTransactionDto>>(accountEntities);

                return accountDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UpdateAccountTransactionDto> UpdateAccountTransactionAsync(Guid Id, UpdateAccountTransactionDto account)
        {
            try
            {
                var existingAccount = await _accountTransactionRepository.GetAccountTransactionByIdAsync(Id);

                if (existingAccount == null)
                {
                    throw new Exception("Account not found for update.");
                }

                _mapper.Map(account, existingAccount);


                var accounts = await _accountTransactionRepository.UpdateAccountTransactionAsync(existingAccount);

                var updatedAccountDto = _mapper.Map<UpdateAccountTransactionDto>(accounts);

                return updatedAccountDto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
