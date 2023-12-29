using AccountApi.Repositories;
using AccountApi.Services;
using AccountDatabase.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Globalization;
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
        private IAccountTransactionService _accountTransactionService;
        private IAccountRepository _accountRepository;
        private ILogger<DashboardService> _logger;
        private IMapper _mapper;
        private readonly AccountClient accountClient;
        public DashboardService(IAccountTransactionRepository accountTransactionRepository, IAccountTransactionService accountTransactionService , IInterestTransactionRepository interestTransactionRepository, IAccountRepository accountRepository, ILogger<DashboardService> logger, IMapper mapper, AccountClient accountClient)
        {
            _accountTransactionRepository = accountTransactionRepository;
            _interestTransactionRepository = interestTransactionRepository;
            _accountTransactionService = accountTransactionService;
            _accountRepository = accountRepository;
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
        public async Task<IEnumerable<PrincipalSummaryDetailDto>> GetPrincipalTransactionsDetailAsync(string emiMonth)
        {
            try
            {
                // Parse the month and year from the emiMonth parameter
                if (DateTime.TryParseExact(emiMonth, "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                {
                    int selectedMonth = parsedDate.Month;
                    int selectedYear = parsedDate.Year;

                    // Get data from GetAccountTransactionsAsync function
                    var accountTransactions = await _accountTransactionService.GetAccountTransactionsAsync();

                    // Filter transactions for the selected month and year
                    var filteredTransactions = accountTransactions
                        .Where(t => t.CreatedDate.Month == selectedMonth && t.CreatedDate.Year == selectedYear);

                    if (filteredTransactions.Any())
                    {
                        // Group by AccountId and InterestRate and calculate sums
                        var principalSummary = filteredTransactions
                            .GroupBy(t => new { t.AccountId, t.InterestRate })
                            .Select(g => new PrincipalSummaryDetailDto(
                                AccountId: g.Key.AccountId,
                                AccountName: g.First().AccountName,
                                TotalPrincipalAmount: g.Sum(t => t.PrincipalAmount),
                                TotalPaidAmount: g.Sum(t => t.PaidAmount),
                                TotalBalanceAmount: g.Sum(t => t.BalanceAmount),
                                InterestRate: g.Key.InterestRate
                            ));

                        return principalSummary;
                    }
                    else
                    {
                        return Enumerable.Empty<PrincipalSummaryDetailDto>();
                    }
                }
                else
                {
                    // Handle invalid emiMonth format
                    throw new ArgumentException("Invalid emiMonth format. Use the format 'MM/yyyy'.", nameof(emiMonth));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }










        //public async Task<List<UnPaidInterestAmount>> GetAccountsAndUnpaidInterestAsync()
        //{
        //    try
        //    {
        //        // Ensure you await the repository methods to get the actual data
        //        var accounts = await _accountRepository.GetAllAsync();
        //        var transactions = await _accountTransactionRepository.GetAllAsync();
        //        var interestEMIs = await _interestTransactionRepository.GetAllAsync();

        //        var result = (
        //            from account in accounts
        //            join transaction in transactions on account.Id equals transaction.AccountId into accountTransactions
        //            from transaction in accountTransactions.DefaultIfEmpty()
        //            join interestEMI in interestEMIs on transaction.Id equals interestEMI.TransactionId into interestEMIsGroup
        //            from interestEMI in interestEMIsGroup.DefaultIfEmpty()
        //            where interestEMI == null || interestEMI.PaidInterestAmount == 0
        //            select new UnPaidInterestAmount(
        //                AccountId: account.Id,
        //                AccountName: account.AccountName,
        //                InterestAmount: interestEMI != null ? interestEMI.InterestAmount : 0,
        //                InterestId: interestEMI != null ? interestEMI.Id : Guid.Empty
        //            )).ToList();


        //        return result;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        public async Task<List<UnPaidInterestAmount>> GetAccountsAndUnpaidInterestAsync(string emiMonth)
        {
            try
            {
                var accounts = await _accountRepository.GetAllAsync();

                var transactions = await _accountTransactionRepository.GetAllAsync();
                Expression<Func<InterestEMI, bool>> monthEMI = e => e.EmiMonth == emiMonth;
                var interestEMIs = await _interestTransactionRepository.GetAllAsync(monthEMI);


                var result = (
                    from account in accounts
                    join transaction in transactions on account.Id equals transaction.AccountId into accountTransactions
                    from transaction in accountTransactions.DefaultIfEmpty()
                    join interestEMI in interestEMIs on transaction?.Id equals interestEMI?.TransactionId into interestEMIsGroup
                    from interestEMI in interestEMIsGroup.DefaultIfEmpty()
                    where interestEMI == null || interestEMI.PaidInterestAmount == 0
                    select new UnPaidInterestAmount(
                        AccountId: account.Id,
                        AccountName: account.AccountName,
                        InterestAmount: interestEMI?.InterestAmount ?? 0,
                        InterestId: interestEMI?.Id ?? Guid.Empty
                    )).ToList();

                return result;
            }
            catch
            {
                throw;
            }
        }




    }



}




