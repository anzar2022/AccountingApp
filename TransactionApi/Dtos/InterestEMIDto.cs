namespace TransactionApi.Dtos
{
    public class InterestEMIDto
    {
        public record GetInterestEMIDto(Guid Id, Guid TransactionId, double PrincipalAmount, double InterestRate, double InterestAmount, double PaidInterestAmount);
        public record UpdateInterestEMIDto(Guid TransactionId, double PaidInterestAmount);
        public record GenerateInterestEMIDto(Guid TransactionId, string EmiMonth);

    }
}
