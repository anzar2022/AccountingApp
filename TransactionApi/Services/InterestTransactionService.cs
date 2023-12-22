using AccountDatabase.Entities;
using AutoMapper;
using Microsoft.Identity.Client;
using System.Linq.Expressions;
using TransactionApi.Dtos;
using TransactionApi.Repositories;
using static TransactionApi.Dtos.InterestEMIDto;

namespace TransactionApi.Services
{
    public class InterestTransactionService : IInterestTransactionService
    {
        private IInterestTransactionRepository _interestTransactionRepository;
        private IAccountTransactionService _accountTransactionService;
        private ILogger<InterestEMI> _logger;
        private readonly IMapper _mapper;
        public InterestTransactionService(IInterestTransactionRepository interestTransactionRepository, IAccountTransactionService accountTransactionService ,ILogger<InterestEMI> logger, IMapper mapper)
        {
            _interestTransactionRepository = interestTransactionRepository;
            _accountTransactionService = accountTransactionService;
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
        public async Task<InterestEMI> GetInterestEMIByTransactionIdAsync(Guid transactionId)
        {
            try
            {
                var transaction = await _accountTransactionService.GetAccountTransactionByIdAsync(transactionId);
                if (transaction == null)
                {
                    return null;
                }
                var existingInterestEMI = await _interestTransactionRepository.GetByIdAsync(transactionId);
                if (existingInterestEMI != null)
                {
                    return existingInterestEMI;
                }
                double interestAmount = CalculateMonthlyInterest(transaction.PrincipalAmount, transaction.InterestRate);
                DateOnly generatedDate = DateOnly.FromDateTime(DateTime.Now);
                string emiMonth = $"{DateOnly.FromDateTime(DateTime.Now):MMMM yyyy}";


                var interestEMI = new InterestEMI
                {
                    TransactionId = transaction.Id,
                    PrincipalAmount = transaction.PrincipalAmount,
                    InterestRate = transaction.InterestRate,
                    InterestAmount = interestAmount,
                    BalanceInterestAmount = 0,
                    PaidInterestAmount = 0,
                    GeneratedDate = generatedDate,
                    EmiMonth = emiMonth


                };
                var existingCreatedInterestEMI = await _interestTransactionRepository.GetByIdAsync(interestEMI.TransactionId);
                if (existingCreatedInterestEMI != null)
                {
                    // If the interestEMI already exists, return a message or handle it as per your requirement.
                    return existingCreatedInterestEMI;
                }
                var createdInterestEMI = await _interestTransactionRepository.CreateAsync(interestEMI);

                return createdInterestEMI;
            }
            catch (Exception)
            {

                throw;
            }
        }


        private double CalculateMonthlyInterest(double principalAmount, double annualInterestRate)
        {
            // Convert annual interest rate to monthly interest rate
            double monthlyInterestRate = annualInterestRate / 12 / 100; // Assuming interest rate is in percentage

            // Assuming 1 month as the time period
            int numberOfMonths = 1;

            // Calculate interest using the formula: Interest = Principal * Rate * Time
            double interestAmount = principalAmount * monthlyInterestRate * numberOfMonths;

            return interestAmount;
        }

    }
}
