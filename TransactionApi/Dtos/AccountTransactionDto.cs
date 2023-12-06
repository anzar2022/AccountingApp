namespace TransactionApi.Dtos
{
    public record GetAccountTransactionDto(Guid Id, string AccountTransactionName, string MobileNo, string EmailAddress, bool IsActive);
    public record CreateAccountTransactionDto(string AccountTransactionName, string MobileNo, string EmailAddress, bool IsActive);
    
    public record UpdateAccountTransactionDto(string AccountTransactionName, string MobileNo, string EmailAddress, bool IsActive);
}
