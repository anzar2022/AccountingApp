using TransactionApi.Dtos;

namespace TransactionApi.Services
{
    public interface IDashboardService
    {
        Task<IEnumerable<InterestSummaryDetailDto>> GetInterestTransactionsDetailAsync(string emiMonth);

        Task<List<UnPaidInterestAmount>> GetAccountsAndUnpaidInterestAsync(string emiMonth);
        //Task<IEnumerable<PrincipalSummaryDetailDto>> GetPrincipalTransactionsDetailAsync(string emiMonth);

        Task<PrincipalSummaryDetailDto> GetPrincipalTransactionsSummaryAsync(string emiMonth);

        Task<List<UnPaidPrincipalAmount>> GetUnpaidPrincipalAmountAsync(string emiMonth);
    }
}
