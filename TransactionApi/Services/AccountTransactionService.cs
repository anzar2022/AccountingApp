using AccountDatabase.Entities;
using AutoMapper;
using System.Linq.Expressions;
using TransactionApi.Clients;
using TransactionApi.Dtos;
using TransactionApi.Repositories;

namespace TransactionApi.Services
{
    public class AccountTransactionService : IAccountTransactionService
    {
        private IAccountTransactionRepository  _accountTransactionRepository;
        private IInterestTransactionRepository _interestTransactionRepository;
        private ILogger<AccountTransactionService> _logger;
        private IMapper _mapper;
        private readonly AccountClient accountClient;
        public AccountTransactionService(IAccountTransactionRepository  accountTransactionRepository ,IInterestTransactionRepository interestTransactionRepository, ILogger<AccountTransactionService> logger, IMapper mapper, AccountClient accountClient) { 
            _accountTransactionRepository = accountTransactionRepository;
            _interestTransactionRepository = interestTransactionRepository;
            _logger = logger;
            _mapper = mapper;
            this.accountClient = accountClient;
        }
        public async Task<CreateAccountTransactionDto> CreateAccountTransactionAsync(CreateAccountTransactionDto account)
        {
            try
            {
                var createdAccount = _mapper.Map<AccountTransaction>(account);

                var accounts = await _accountTransactionRepository.CreateAsync(createdAccount);

                var createdAccountDto = _mapper.Map<CreateAccountTransactionDto>(accounts);

                return createdAccountDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public async Task<List<GetAccountTransactionDto>> GetAccountTransactionByAccountIdAsync(Guid accountId)
        //{
        //    try
        //    {
        //        Expression<Func<AccountTransaction, bool>> filter = transaction => transaction.AccountId == accountId;
        //        var accountTransactionsByAccountId = await _accountTransactionRepository.GetAllAsync(filter);

        //        return accountTransactionsByAccountId;
        //    }
        //    catch {
        //        throw;
        //    }
        //}
        public async Task<List<GetAccountTransactionDto>> GetAccountTransactionByAccountIdAsync(Guid accountId)
        {
            try
            {
                // Get data from accountClient and repository
                var accounts = await accountClient.GetAccount();
                Expression<Func<AccountTransaction, bool>> filter = transaction => transaction.AccountId == accountId;
                var accountTransactionsByAccountId = await _accountTransactionRepository.GetAllAsync(filter);

                // Mapping to update AccountName based on AccountId
                var updatedAccountTransactions = accountTransactionsByAccountId.Select(entity =>
                {
                    var correspondingAccount = accounts.FirstOrDefault(acc => acc.Id == entity.AccountId);
                    if (correspondingAccount != null)
                    {
                        return new GetAccountTransactionDto(
                            entity.Id,
                            entity.AccountId,
                            correspondingAccount.AccountName, // Update AccountName from accounts
                            entity.PrincipalAmount,
                            entity.PaidAmount,
                            entity.BalanceAmount,
                            entity.CreatedDate,
                            entity.UpdatedDate,
                            entity.CreatedUserId,
                            entity.UpdatedUserId,
                            entity.StartDate,
                            entity.CloseDate,
                            entity.InterestRate
                        );
                    }
                    else
                    {
                        // If no corresponding account found, return original entity with empty AccountName
                        return new GetAccountTransactionDto(
                            entity.Id,
                            entity.AccountId,
                            "",
                            entity.PrincipalAmount,
                            entity.PaidAmount,
                            entity.BalanceAmount,
                            entity.CreatedDate,
                            entity.UpdatedDate,
                            entity.CreatedUserId,
                            entity.UpdatedUserId,
                            entity.StartDate,
                            entity.CloseDate,
                            entity.InterestRate
                        );
                    }
                });




                return updatedAccountTransactions.ToList();
            }
            catch
            {
                throw;
            }
        }

        //important
        //public async Task<List<GetAccountTransactionWithIntDto>> GetAccountTransactionWithInterestAsync(Guid accountId)
        //{
        //    try
        //    {
        //        // Get data from repository
        //        Expression<Func<AccountTransaction, bool>> filter = transaction => transaction.AccountId == accountId;
        //        var accountTransactionsByAccountId = await _accountTransactionRepository.GetAllAsync(filter);

        //        // Get interestEMIs data from repository
        //        var interestEMIs = await _interestTransactionRepository.GetAllAsync();

        //        // Join Transactions and InterestEMIs tables and project the result into the DTO
        //        var updatedAccountTransactions =
        //            from transaction in accountTransactionsByAccountId
        //            join interestEMI in interestEMIs on transaction.Id equals interestEMI.TransactionId into joinedData
        //            from interestEMI in joinedData.DefaultIfEmpty()
        //            select new GetAccountTransactionWithIntDto(
        //                transaction.Id,
        //                transaction.InterestRate,
        //                transaction.PrincipalAmount,
        //                interestEMI != null ? interestEMI.InterestAmount : 0,
        //                interestEMI != null ? interestEMI.EmiMonth : string.Empty

        //            );

        //        return updatedAccountTransactions.ToList();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public async Task<List<GetAccountTransactionWithIntDto>> GetAccountTransactionWithInterestAsync(Guid accountId, string emiMonth)
        {
            try
            {
                // Get data from repository
                Expression<Func<AccountTransaction, bool>> filter = transaction => transaction.AccountId == accountId;
                var accountTransactionsByAccountId = await _accountTransactionRepository.GetAllAsync(filter);

                // Get interestEMIs data from repository
                //you can get emis based on month
                Expression<Func<InterestEMI, bool>> monthEMI = e => e.EmiMonth == emiMonth;
                var interestEMIs = await _interestTransactionRepository.GetAllAsync();

                // Join Transactions and InterestEMIs tables and project the result into the DTO
                var updatedAccountTransactions =
                    from transaction in accountTransactionsByAccountId
                    join interestEMI in interestEMIs on transaction.Id equals interestEMI.TransactionId into joinedData
                    from interestEMI in joinedData.DefaultIfEmpty()
                    //where (interestEMI == null && emiMonth == null) || (interestEMI != null && interestEMI.EmiMonth == emiMonth)
                    //where interestEMI == null || interestEMI.EmiMonth == emiMonth
                    select new GetAccountTransactionWithIntDto(
                        transaction.Id,
                        transaction.InterestRate,
                        transaction.PrincipalAmount,
                        interestEMI != null ? interestEMI.InterestAmount : 0,
                        interestEMI != null ? interestEMI.EmiMonth : string.Empty
                    );

                return updatedAccountTransactions.ToList();
            }
            catch
            {
                throw;
            }
        }




        public async Task<bool> DeleteAccountTransactionAsync(Guid Id)
        {
            try
            {
                var accounts = await _accountTransactionRepository.GetByIdAsync(Id);
                await _accountTransactionRepository.DeleteAsync(accounts);

                return true;
            }
            catch (Exception)
            {
              
                throw;
                return false;
            }

          
        }

        public async Task<GetAccountTransactionDto> GetAccountTransactionByIdAsync(Guid Id)
        {
            try
            {

                var accounts = await _accountTransactionRepository.GetByIdAsync(Id);

                /// var accountDto = _mapper.Map<GetAccountTransactionDto>(accounts);

                // Map the fetched data to the GetAccountTransactionDto record
                GetAccountTransactionDto accountDto = new GetAccountTransactionDto(
                    Id: accounts.Id,
                    AccountId: accounts.AccountId,
                    AccountName: "",
                    PrincipalAmount: accounts.PrincipalAmount,
                    PaidAmount: accounts.PaidAmount,
                    BalanceAmount: accounts.BalanceAmount,
                    CreatedDate: accounts.CreatedDate,
                    UpdatedDate: accounts.UpdatedDate,
                    CreatedUserId: accounts.CreatedUserId,
                    UpdatedUserId: accounts.UpdatedUserId,
                    StartDate: accounts.StartDate,
                    CloseDate: accounts.CloseDate,
                    InterestRate: accounts.InterestRate
                );

                return accountDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public async Task<IEnumerable<GetAccountTransactionDto>> GetAccountTransactionsAsync()
        //{
        //    try
        //    {
        //        var accounts =  await   accountClient.GetAccount(); 

        //        var accountEntities = await _accountTransactionRepository.GetAllAsync();

        //        var accountDtos = _mapper.Map<IEnumerable<GetAccountTransactionDto>>(accountEntities);

        //        foreach (var account in accounts)
        //        {
        //            var accountIdToUpdate = account.Id;
        //            var accountToUpdate = accountDtos.FirstOrDefault(a => a.AccountId == accountIdToUpdate);
        //            if (accountToUpdate != null)
        //            {
        //                // Update the AccountName for the specified accountId
        //                accountToUpdate.AccountName = account.AccountName; // Assuming 'Name' is the property representing the account name
        //            }
        //        }

        //            return accountDtos;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public async Task<IEnumerable<GetAccountTransactionDto>> GetAccountTransactionsAsync()
        //{
        //    try
        //    {
        //        var accounts = await accountClient.GetAccount();

        //        var accountEntities = await _accountTransactionRepository.GetAllAsync();

        //        var config = new MapperConfiguration(cfg =>
        //        {
        //            cfg.CreateMap<GetAccountDto, GetAccountTransactionDto>()
        //                .ForMember(dest => dest.AccountName, opt => opt.Ignore());
        //        });

        //        var mapper = config.CreateMapper();
        //        var accountDtos = mapper.Map<IEnumerable<GetAccountTransactionDto>>(accountEntities);

        //        foreach (var account in accounts)
        //        {
        //            var accountIdToUpdate = account.Id;
        //            var accountToUpdate = accountDtos.FirstOrDefault(a => a.AccountId == accountIdToUpdate);
        //            if (accountToUpdate != null)
        //            {
        //                accountToUpdate.AccountName = account.AccountName; // Update AccountName
        //            }
        //        }

        //        return accountDtos;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public async Task<IEnumerable<GetAccountTransactionDto>> GetAccountTransactionsAsync()
        //{
        //    try
        //    {
        //        var accounts = await accountClient.GetAccount();
        //        var accountEntities = await _accountTransactionRepository.GetAllAsync();



        //        return accountDtos;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public async Task<IEnumerable<GetAccountTransactionDto>> GetAccountTransactionsAsync()
        {
            try
            {
                // Get data from accountClient and repository
                var accounts = await accountClient.GetAccount();
                var accountEntities = await _accountTransactionRepository.GetAllAsync();

                // Mapping to update AccountName based on AccountId
                var updatedAccountTransactions = accountEntities.Select(entity =>
                {
                    var correspondingAccount = accounts.FirstOrDefault(acc => acc.Id == entity.AccountId);
                    if (correspondingAccount != null)
                    {
                        return new GetAccountTransactionDto(
                            entity.Id,
                            entity.AccountId,
                            correspondingAccount.AccountName, // Update AccountName from accounts
                            entity.PrincipalAmount,
                            entity.PaidAmount,
                            entity.BalanceAmount,
                            entity.CreatedDate,
                            entity.UpdatedDate,
                            entity.CreatedUserId,
                            entity.UpdatedUserId,
                            entity.StartDate,
                            entity.CloseDate,
                            entity.InterestRate
                        );
                    }
                    else
                    {
                        // If no corresponding account found, return original entity
                        return new GetAccountTransactionDto(
                            entity.Id,
                            entity.AccountId,
                           "",
                            entity.PrincipalAmount,
                            entity.PaidAmount,
                            entity.BalanceAmount,
                            entity.CreatedDate,
                            entity.UpdatedDate,
                            entity.CreatedUserId,
                            entity.UpdatedUserId,
                            entity.StartDate,
                            entity.CloseDate,
                            entity.InterestRate
                        );
                    }
                });

                return updatedAccountTransactions;
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
                var existingAccount = await _accountTransactionRepository.GetByIdAsync(Id);

                if (existingAccount == null)
                {
                    throw new Exception("Account not found for update.");
                }

                _mapper.Map(account, existingAccount);


                var accounts = await _accountTransactionRepository.UpdateAsync(existingAccount);

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
