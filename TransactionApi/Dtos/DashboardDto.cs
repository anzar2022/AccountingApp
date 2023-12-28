namespace TransactionApi.Dtos
{
    public record InterestSummaryDetailDto(double TotalInterestAmount, double BalanceInterestAmount, double PaidInterestAmount, string EmiMonth);
    public record UnpaidInterestEMIDto(Guid? AccountId,Guid? InterestEMIId, string? AccountName, double? interestAmount);
}
