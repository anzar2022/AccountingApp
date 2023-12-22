using TransactionApi.Dtos;

namespace TransactionApi.Services
{
    public interface IAccountTransactionService
    {
        Task<CreateAccountTransactionDto> CreateAccountTransactionAsync(CreateAccountTransactionDto account);
        Task<bool> DeleteAccountTransactionAsync(Guid Id);
        Task<GetAccountTransactionDto> GetAccountTransactionByIdAsync(Guid Id);
        Task<IEnumerable<GetAccountTransactionDto>> GetAccountTransactionsAsync();
        Task<List<GetAccountTransactionDto>> GetAccountTransactionByAccountIdAsync(Guid accountId);

        Task<List<GetAccountTransactionWithIntDto>> GetAccountTransactionWithInterestAsync(Guid accountId, string emiMonth);
        Task<UpdateAccountTransactionDto> UpdateAccountTransactionAsync(Guid Id, UpdateAccountTransactionDto account);
    }
}
