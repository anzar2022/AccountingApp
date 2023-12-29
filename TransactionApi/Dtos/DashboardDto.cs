namespace TransactionApi.Dtos
{
    public record InterestSummaryDetailDto(double TotalInterestAmount, double BalanceInterestAmount, double PaidInterestAmount, string EmiMonth);

    //public record PrincipalSummaryDetailDto(Guid AccountId, string AccountName,  double TotalPrincipalAmount, double TotalBalanceAmount, double TotalPaidAmount, double InterestRate);
    public record PrincipalSummaryDetailDto( double TotalPrincipalAmount, double TotalBalanceAmount, double TotalPaidAmount);

    public record UnPaidInterestAmount(Guid AccountId, string AccountName, double InterestAmount, Guid InterestId); 
}
