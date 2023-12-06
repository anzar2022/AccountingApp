using AccountApi.Dtos;

namespace AccountApi.Services
{
    public interface IAccountService
    {
        Task<CreateAccountDto> CreateAccountAsync(CreateAccountDto account);
        Task<bool> DeleteAccountAsync(Guid Id);
        Task<GetAccountDto> GetAccountByIdAsync(Guid Id);
        Task<IEnumerable<GetAccountDto>> GetAccountsAsync();
        Task<UpdateAccountDto> UpdateAccountAsync(Guid Id, UpdateAccountDto account);
    }
}