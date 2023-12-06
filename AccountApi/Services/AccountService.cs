using AccountApi.Dtos;
using AccountApi.Repositories;
using AccountDBUtilities.Entities;
using AutoMapper;
using System.Collections;

namespace AccountApi.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;
        private ILogger<AccountService> _logger;
        private readonly IMapper _mapper;



        public AccountService(IAccountRepository accountRepository, ILogger<AccountService> logger, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _logger = logger;
            _mapper = mapper;
        }

        //public async Task<IEnumerable<GetAccountDto>> GetAccountsAsync()
        //{
        //    try
        //    {
        //        _logger.LogInformation($"{nameof(GetAccountsAsync)}");
        //        var accounts = await _accountRepository.GetAccountsAsync();


        //        var accountDtos = _mapper.Map<IEnumerable<GetAccountDto>>( accounts );

        //        return accountDtos;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }


        //}
        public async Task<IEnumerable<GetAccountDto>> GetAccountsAsync()
        {
            try
            {
                var accountEntities = await _accountRepository.GetAllAsync();

                // Map List<Account> to IEnumerable<GetAccountDto>
                var accountDtos = _mapper.Map<IEnumerable<GetAccountDto>>(accountEntities);

                return accountDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<GetAccountDto> GetAccountByIdAsync(Guid Id)
        {
            try
            {

                var accounts = await _accountRepository.GetByIdAsync(Id);

                var accountDto = _mapper.Map<GetAccountDto>(accounts);

                return accountDto;
            }
            catch (Exception)
            {

                throw;
            }


        }
        public async Task<CreateAccountDto> CreateAccountAsync(CreateAccountDto accountDto)
        {
            try
            {
                var createdAccount = _mapper.Map<Account>(accountDto);

                var accounts = await _accountRepository.CreateAsync(createdAccount);

                var createdAccountDto = _mapper.Map<CreateAccountDto>(accounts);

                return createdAccountDto;
            }
            catch (Exception)
            {

                throw;
            }


        }
        public async Task<UpdateAccountDto> UpdateAccountAsync(Guid Id, UpdateAccountDto account)
        {
            try
            {
                var existingAccount = await _accountRepository.GetByIdAsync(Id);

                if (existingAccount == null)
                {
                    throw new Exception("Account not found for update.");
                }

                _mapper.Map(account, existingAccount);


                var accounts = await _accountRepository.UpdateAsync(existingAccount);

                var updatedAccountDto = _mapper.Map<UpdateAccountDto>(accounts);

                return updatedAccountDto;
            }
            catch (Exception)
            {

                throw;
            }


        }
        public async Task<bool> DeleteAccountAsync(Guid Id)
        {
            try
            {

                // return await _accountRepository.DeleteAsync(Id);
                return true;
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
