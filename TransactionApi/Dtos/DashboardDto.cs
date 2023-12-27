namespace TransactionApi.Dtos
{
    public record InterestSummaryDetailDto(double TotalInterestAmount, double BalanceInterestAmount, double PaidInterestAmount, string EmiMonth);
    public record GenerateInterestEMIsDto(Guid Id, string EmiMonth);
}
