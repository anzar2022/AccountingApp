using AccountDatabase.Entities;
using AutoMapper;
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

        public async Task<List<InterestEMI>> GetInterestTransactionByAccountIdAsync(Guid accountId)
        {
            try
            {
                var accountTransactionsByAccountId = await _accountTransactionService.GetAccountTransactionByAccountIdAsync(accountId);
                List<InterestEMI> createdInterestEMIs = new List<InterestEMI>();

                foreach (var transaction in accountTransactionsByAccountId)
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
             

                return createdInterestEMIs;
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
