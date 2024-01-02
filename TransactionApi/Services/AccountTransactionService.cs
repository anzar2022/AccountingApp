﻿using AccountDatabase.Entities;
using AutoMapper;
using System.Linq.Expressions;
using TransactionApi.Clients;
using TransactionApi.Dtos;
using TransactionApi.Repositories;
using static TransactionApi.Dtos.InterestEMIDto;

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
                var interestEMIs = await _interestTransactionRepository.GetAllAsync(monthEMI);

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
                        //interestEMI != null ? interestEMI.PaidInterestAmount : 0,
                        interestEMI != null ? interestEMI.InterestAmount : 0,
                        interestEMI != null ? interestEMI.PaidInterestAmount : 0,
                        interestEMI != null ? interestEMI.EmiMonth : string.Empty
                        //paid amount pn pathav paidInterestAMount
                    );

                return updatedAccountTransactions.ToList();
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<GetAccountTransactionWithIntDto>> GetAccountTransactionsWithInterestAsync(string emiMonth)
        {
            try
            {
                // Get data from repository
                var accountTransactionsByAccountId = await _accountTransactionRepository.GetAllAsync();

                // Get interestEMIs data from repository
                // you can get emis based on month
                Expression<Func<InterestEMI, bool>> monthEMI = e => e.EmiMonth == emiMonth;
                var interestEMIs = await _interestTransactionRepository.GetAllAsync(monthEMI);

                var updatedAccountTransactions = new List<GetAccountTransactionWithIntDto>();

                foreach (var transaction in accountTransactionsByAccountId)
                {
                    // Join Transactions and InterestEMIs tables and project the result into the DTO
                    var matchingInterestEMI = interestEMIs.FirstOrDefault(e => e.TransactionId == transaction.Id);

                    updatedAccountTransactions.Add(new GetAccountTransactionWithIntDto(
                        transaction.Id,
                        transaction.InterestRate,
                        transaction.PrincipalAmount,
                        matchingInterestEMI != null ? matchingInterestEMI.InterestAmount : 0,
                        matchingInterestEMI != null ? matchingInterestEMI.EmiMonth : string.Empty
                    ));
                }

                return updatedAccountTransactions;
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

        public async Task<AccountTransaction> PayPrincipalTransactionAsync(PayPrincipalTransaction principalTransaction)
        {
            try
            {
                var transaction = await _accountTransactionRepository.GetByIdAsync(principalTransaction.Id);
                //check
                if (transaction == null)
                {
                    // Handle the case where the interest transaction with the specified Id is not found.
                    return null;
                }

                // Update the paid interest amount and subtract from the balance interest amount.
                transaction.PaidAmount += principalTransaction.paidAmount;
                //round it here to 2 decimal 
                transaction.BalanceAmount = Math.Round(transaction.BalanceAmount - principalTransaction.paidAmount, 2);
                //interestEMI.BalanceInterestAmount -= updateDto.PaidInterestAmount;

                // Update other fields if needed.

                // Use your existing update method to save changes to the database.
                await _accountTransactionRepository.UpdateAsync(transaction);

                return transaction;
            }
            catch (Exception ex)
            {
                // Handle the exception here, you can log it or perform any other error handling.
                // You can also throw a custom exception if needed.
                throw new ApplicationException("An error occurred while updating the interest transaction payment.", ex);
            }
        }
    }
}
