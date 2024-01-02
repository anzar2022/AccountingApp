using AccountDatabase.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Globalization;
using System.Linq.Expressions;
using TransactionApi.Dtos;
using TransactionApi.Repositories;
using static TransactionApi.Dtos.InterestEMIDto;

namespace TransactionApi.Services
{
    public class InterestTransactionService : IInterestTransactionService
    {
        private IInterestTransactionRepository _interestTransactionRepository;
        private IAccountTransactionRepository _accountTransactionRepository;
        private IAccountTransactionService _accountTransactionService;
        private ILogger<InterestEMI> _logger;
        private readonly IMapper _mapper;
        public InterestTransactionService(IInterestTransactionRepository interestTransactionRepository, IAccountTransactionService accountTransactionService , IAccountTransactionRepository accountTransactionRepository,ILogger<InterestEMI> logger, IMapper mapper)
        {
            _interestTransactionRepository = interestTransactionRepository;
            _accountTransactionService = accountTransactionService;
            _accountTransactionRepository = accountTransactionRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetInterestEMIDto>> GetInterestTransactionAsync()
        {
            try
            {
                var interestEntities = await _interestTransactionRepository.GetAllAsync();
                if (interestEntities != null && interestEntities.Any())
                {

                    var accountDtos = _mapper.Map<IEnumerable<GetInterestEMIDto>>(interestEntities);

                    return accountDtos;
                }
                else { return Enumerable.Empty<GetInterestEMIDto>(); }

                // Map List<Account> to IEnumerable<GetAccountDto>

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetInterestEMIDto> GetInterestTransactionByIdAsync(Guid transactionId)
        {
            try
            {
            

     
                var interestTransaction = await _interestTransactionRepository.GetInterestTransactionByTransactionId(transactionId);

                var interestTransactionDto = _mapper.Map<GetInterestEMIDto>(interestTransaction);

                return interestTransactionDto;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<IEnumerable<GetInterestEMIDto>> GetInterestTransactionsByIdAsync(Guid transactionId)
        {
            try
            {
                Expression<Func<InterestEMI, bool>> filterExpression = x => x.TransactionId == transactionId;

                // Use the GetAllAsync method with the defined filter expression
                var interestTransactions = await _interestTransactionRepository.GetAllAsync(filterExpression);

                // Map the entities to DTOs
                var interestTransactionDtos = _mapper.Map<IEnumerable<GetInterestEMIDto>>(interestTransactions);

                return interestTransactionDtos;



            }
            catch (Exception)
            {

                throw;
            }


        }





        public async Task<List<InterestEMI>> GetInterestTransactionByAccountIdAsync(Guid accountId)
        {
            try
            {
                var accountTransactionsByAccountId = await _accountTransactionService.GetAccountTransactionByAccountIdAsync(accountId);
                List<InterestEMI> createdInterestEMIs = new List<InterestEMI>();

                foreach (var transaction in accountTransactionsByAccountId)
                {
                    var existingInterestEMITransaction = await _interestTransactionRepository.GetByIdAsync(transaction.AccountId);
                    if (existingInterestEMITransaction == null)
                    {
                        double interestAmount = CalculateMonthlyInterest(transaction.PrincipalAmount, transaction.InterestRate);
                        var interestEMI = new InterestEMI
                        {
                            TransactionId = transaction.Id,
                            PrincipalAmount = transaction.PrincipalAmount,
                            InterestRate = transaction.InterestRate,
                            InterestAmount = interestAmount,

                        };

                        var createdInterestEMI = await _interestTransactionRepository.CreateAsync(interestEMI);
                        createdInterestEMIs.Add(createdInterestEMI);
                    }
                }
             

                return createdInterestEMIs;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<InterestEMI> GetInterestEMIByTransactionIdAsync([FromBody] GenerateInterestEMIDto interestEmi)
        {
            try
            {
                var transaction = await _accountTransactionService.GetAccountTransactionByIdAsync(interestEmi.TransactionId);
               
                if (transaction == null)
                {
                    return null;
                }
                var existedInterestTransactions = await _interestTransactionRepository.GetAllAsync();

                double balanceInterestAmount = 0;
                if (existedInterestTransactions != null)
                {
                    balanceInterestAmount = existedInterestTransactions.Sum(e => e.BalanceInterestAmount);
                }

     
                double interestAmount = CalculateMonthlyInterest(transaction.BalanceAmount + balanceInterestAmount, transaction.InterestRate);
                DateOnly generatedDate = DateOnly.FromDateTime(DateTime.Now);
              


                if (generatedDate < transaction.StartDate)
                {
                    return null;
                }
 
                var interestEMI = new InterestEMI
                {
                    TransactionId = transaction.Id,
                    PrincipalAmount = transaction.PrincipalAmount,
                    InterestRate = transaction.InterestRate,
                    InterestAmount = interestAmount,
                    BalanceInterestAmount = interestAmount,
                    PaidInterestAmount = 0,
                    GeneratedDate = generatedDate,
                    EmiMonth = interestEmi.EmiMonth


                };
             
                var createdInterestEMI = await _interestTransactionRepository.CreateAsync(interestEMI);

                return createdInterestEMI;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //generateInterest
        public async Task<List<InterestEMI>> GetInterestEMIForTransactionsAsync(string emiMonth)
        {
            var interestEMIs = new List<InterestEMI>();

            try
            {
                var transactions = await _accountTransactionRepository.GetAllAsync();

                if (transactions == null)
                {
                    throw new Exception("No transactions found to generate interest EMI.");
                }

                foreach (var transaction in transactions)
                {
                    if (transaction == null)
                    {
                        continue;
                    }

                    var existedInterestTransactions = await _interestTransactionRepository.GetAllAsync();
                    double balanceInterestAmount = existedInterestTransactions?.Sum(e => e.BalanceInterestAmount) ?? 0;
                    double interestAmount = CalculateMonthlyInterest(transaction.BalanceAmount + balanceInterestAmount, transaction.InterestRate);
                    DateOnly generatedDate = DateOnly.FromDateTime(DateTime.Now);

                    if (generatedDate < transaction.StartDate)
                    {
                        throw new Exception("Generated date is before transaction start date. Cannot generate interest EMI.");
                    }

                    var interestEMI = new InterestEMI
                    {
                        TransactionId = transaction.Id,
                        PrincipalAmount = transaction.PrincipalAmount,
                        InterestRate = transaction.InterestRate,
                        InterestAmount = interestAmount,
                        BalanceInterestAmount = interestAmount,
                        PaidInterestAmount = 0,
                        GeneratedDate = generatedDate,
                        EmiMonth = emiMonth
                    };

                    var createdInterestEMI = await _interestTransactionRepository.CreateAsync(interestEMI);
                    interestEMIs.Add(createdInterestEMI);
                }

                return interestEMIs;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<InterestEMI> UpdateInterestTransactionPaymentAsync(UpdateInterestEMIDto updateDto)
        {
            try
            {
                var interestEMI = await _interestTransactionRepository.GetByIdAsync(updateDto.Id);
                //check
                if (interestEMI == null)
                {
                    // Handle the case where the interest transaction with the specified Id is not found.
                    return null;
                }

                // Update the paid interest amount and subtract from the balance interest amount.
                interestEMI.PaidInterestAmount += updateDto.PaidInterestAmount;
                //round it here to 2 decimal 
                interestEMI.BalanceInterestAmount = Math.Round(interestEMI.BalanceInterestAmount - updateDto.PaidInterestAmount, 2);
                //interestEMI.BalanceInterestAmount -= updateDto.PaidInterestAmount;

                // Update other fields if needed.

                // Use your existing update method to save changes to the database.
                await _interestTransactionRepository.UpdateAsync(interestEMI);

                return interestEMI;
            }
            catch (Exception ex)
            {
                // Handle the exception here, you can log it or perform any other error handling.
                // You can also throw a custom exception if needed.
                throw new ApplicationException("An error occurred while updating the interest transaction payment.", ex);
            }
        }

        //deleteFunctionality

        public async Task<bool> DeleteInterestEMIAsync(Guid Id)
        {
            try
            {
                var interestEmi = await _interestTransactionRepository.GetByIdAsync(Id);
                await _interestTransactionRepository.DeleteAsync(interestEmi);

                return true;
            }
            catch (Exception)
            {

                throw;
                return false;
            }


        }



        private double CalculateMonthlyInterest(double BalanceAmount, double annualInterestRate)
        {
            // Convert annual interest rate to monthly interest rate
            double monthlyInterestRate = annualInterestRate / 12 / 100; // Assuming interest rate is in percentage

            // Assuming 1 month as the time period
            int numberOfMonths = 1;

            // Calculate interest using the formula: Interest = Principal * Rate * Time
            double interestAmount = BalanceAmount * monthlyInterestRate * numberOfMonths;
            interestAmount = Math.Round(interestAmount, 2);
            return interestAmount;
        }

    }
}
