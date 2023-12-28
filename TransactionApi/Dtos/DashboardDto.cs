namespace TransactionApi.Dtos
{
    public record InterestSummaryDetailDto(double TotalInterestAmount, double BalanceInterestAmount, double PaidInterestAmount, string EmiMonth);
    public record UnPaidInterestAmount(Guid AccountId, string AccountName, double InterestAmount, Guid InterestId); 
}
