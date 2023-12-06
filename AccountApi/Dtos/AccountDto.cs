namespace AccountApi.Dtos
{
    public record GetAccountDto(Guid Id, string AccountName, string MobileNo, string EmailAddress, bool IsActive);
    public record CreateAccountDto(string AccountName, string MobileNo, string EmailAddress, bool IsActive);
    // public record UpdateAccountDto(Guid Id , string AccountName, string MobileNo, string EmailAddress, bool IsActive);

    public record UpdateAccountDto(string AccountName, string MobileNo, string EmailAddress, bool IsActive);
}
