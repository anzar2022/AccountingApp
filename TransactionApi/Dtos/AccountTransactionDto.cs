//namespace TransactionApi.Dtos
//{
//    public record GetAccountTransactionDto(Guid Id, string AccountTransactionName, string MobileNo, string EmailAddress, bool IsActive);
//    public record CreateAccountTransactionDto(string AccountTransactionName, string MobileNo, string EmailAddress, bool IsActive);

//    public record UpdateAccountTransactionDto(string AccountTransactionName, string MobileNo, string EmailAddress, bool IsActive);
//}
namespace TransactionApi.Dtos
{
    public record GetAccountTransactionDto(Guid Id, Guid AccountId,string AccountName, double PrincipalAmount, double PaidAmount, double BalanceAmount, DateOnly CreatedDate, DateOnly UpdatedDate, Guid CreatedUserId, Guid UpdatedUserId, DateOnly StartDate, DateOnly CloseDate, double InterestRate);
    public record CreateAccountTransactionDto(Guid AccountId, double PrincipalAmount, double PaidAmount, double BalanceAmount, DateOnly CreatedDate, DateOnly UpdatedDate, Guid CreatedUserId, Guid UpdatedUserId, DateOnly StartDate, DateOnly CloseDate, double InterestRate);

    public record UpdateAccountTransactionDto(double PrincipalAmount, double PaidAmount, double BalanceAmount, DateOnly CreatedDate, DateOnly UpdatedDate, Guid CreatedUserId, Guid UpdatedUserId, DateOnly StartDate, DateOnly CloseDate, double InterestRate);

    public record GetAccountDto(Guid Id, string AccountName, string MobileNo, string EmailAddress, string PanCard, string AdharCard, string Adderss, bool IsActive, DateTime CreatedDate);

    //public record GetAccountTransactionWithIntDto(Guid Id, Guid AccountId, string AccountName, double PrincipalAmount,  double InterestRate, double InterestAmount);
    public record GetAccountTransactionWithIntDto(Guid Id, double InterestRate, double PrincipalAmount, double InterestAmount,  string EmiMonth);

    public record AccountTransactionRequestDto(Guid accountId, string? EmiMonth);

    public record TransactionsRequestDto(Guid Id, string? EmiMonth);

    public record PayPrincipalTransaction(Guid Id, double PaidAmount);
}
