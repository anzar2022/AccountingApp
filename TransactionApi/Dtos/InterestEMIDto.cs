namespace TransactionApi.Dtos
{
    public class InterestEMIDto
    {
        public record GetInterestEMIDto(Guid Id, Guid TransactionId, double PrincipalAmount, double InterestRate, double InterestAmount, double BalanceInterestAmount, double PaidInterestAmount, string emiMonth);
        public record UpdateInterestEMIDto(Guid Id, double PaidInterestAmount);
        public record GenerateInterestEMIDto(Guid TransactionId, string EmiMonth);
        public record GenerateInterestEMIsDto(Guid Id, string EmiMonth);

    }
}
