using TransactionApi.Dtos;

namespace TransactionApi.Clients
{
    public class AccountClient
    {
        private readonly HttpClient httpClient;
        public AccountClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IReadOnlyCollection<GetAccountDto>> GetAccount()
        {
            try
            {
                var account = await httpClient.GetFromJsonAsync<IReadOnlyCollection<GetAccountDto>>("/api/account/");
                return account;
            }
            catch (Exception ex)
            {
                // Handle the exception or rethrow it based on your application's requirements
                throw ex;
            }
        }
    }
}
