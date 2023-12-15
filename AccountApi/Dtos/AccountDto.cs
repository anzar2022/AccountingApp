using System.ComponentModel.DataAnnotations;

namespace AccountApi.Dtos
{
    public record GetAccountDto(Guid Id, string AccountName, string MobileNo, string EmailAddress, bool IsActive);
    public record CreateAccountDto([Required(ErrorMessage ="Account name is required")] string AccountName, [Required(ErrorMessage = "Mobile No is required")] string MobileNo, string EmailAddress, bool IsActive);
    // public record UpdateAccountDto(Guid Id , string AccountName, string MobileNo, string EmailAddress, bool IsActive);

    public record UpdateAccountDto(string AccountName, string MobileNo, string EmailAddress, bool IsActive);
}
