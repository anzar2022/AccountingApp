using AccountDatabase.Entities;
using AutoMapper;
using System.Linq.Expressions;
using TransactionApi.Clients;
using TransactionApi.Dtos;
using TransactionApi.Repositories;

namespace TransactionApi.Services
{
    public class DashboardService : IDashboardService
    {
        private IAccountTransactionRepository _accountTransactionRepository;
        private IInterestTransactionRepository _interestTransactionRepository;
        private ILogger<DashboardService> _logger;
        private IMapper _mapper;
        private readonly AccountClient accountClient;
        public DashboardService(IAccountTransactionRepository accountTransactionRepository, IInterestTransactionRepository interestTransactionRepository, ILogger<DashboardService> logger, IMapper mapper, AccountClient accountClient)
        {
            _accountTransactionRepository = accountTransactionRepository;
            _interestTransactionRepository = interestTransactionRepository;
            _logger = logger;
            _mapper = mapper;
            this.accountClient = accountClient;
        }
        public async Task<IEnumerable<InterestSummaryDetailDto>> GetInterestTransactionsDetailAsync(string emiMonth)
        {
            try
            {
                // Fetch all interest transactions
                var interestEntities = await _interestTransactionRepository.GetAllAsync();

                if (interestEntities != null && interestEntities.Any())
                {
                    // Filter interest transactions for the current month
                    //var emiMonth = DateTime.Now.Month.ToString(); // Change this to get the current month as needed
                    Expression<Func<InterestEMI, bool>> monthEMI = e => e.EmiMonth == emiMonth;
                    var interestEMIs = await _interestTransactionRepository.GetAllAsync(monthEMI);

                    // Calculate total interest, paid interest, and balance interest
                    double totalInterestAmount = interestEMIs.Sum(e => e.InterestAmount);
                    double paidInterestAmount = interestEMIs.Sum(e => e.PaidInterestAmount);
                    double balanceInterestAmount = interestEMIs.Sum(e => e.BalanceInterestAmount);

                    // Create a DTO and return it
                    var resultDto = new InterestSummaryDetailDto(
                        TotalInterestAmount: totalInterestAmount,
                        PaidInterestAmount: paidInterestAmount,
                        BalanceInterestAmount: balanceInterestAmount,
                        EmiMonth: emiMonth
                    );

                    return new List<InterestSummaryDetailDto> { resultDto };
                }
                else
                {
                    return Enumerable.Empty<InterestSummaryDetailDto>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
