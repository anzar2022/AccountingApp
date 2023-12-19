namespace TransactionApi.Dtos
{
    public record GetAccountTransactionDto(Guid Id, Guid AccountId, double PrincipalAmount, double PaidAmount, double BalanceAmount, DateOnly CreatedDate, DateOnly UpdatedDate, Guid CreatedUserId, Guid UpdatedUserId, DateOnly StartDate, DateOnly CloseDate, double InterestRate);
    public record CreateAccountTransactionDto(Guid AccountId, double PrincipalAmount, double PaidAmount, double BalanceAmount, DateOnly CreatedDate, DateOnly UpdatedDate, Guid CreatedUserId, Guid UpdatedUserId, DateOnly StartDate, DateOnly CloseDate, double InterestRate);
    
    public record UpdateAccountTransactionDto(double PrincipalAmount, double PaidAmount, double BalanceAmount, DateOnly CreatedDate, DateOnly UpdatedDate, Guid CreatedUserId, Guid UpdatedUserId, DateOnly StartDate, DateOnly CloseDate, double InterestRate);
}
