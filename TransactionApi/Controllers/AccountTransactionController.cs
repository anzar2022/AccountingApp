using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionApi.Dtos;
using TransactionApi.Services;

namespace TransactionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTransactionController : ControllerBase
    {
        private ILogger<AccountTransactionController> _logger ;
        private IAccountTransactionService _accountTransactionService ;
        public AccountTransactionController(ILogger<AccountTransactionController> logger, IAccountTransactionService accountTransactionService)
        {
            _logger = logger;
            _accountTransactionService = accountTransactionService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAccountTransactionsAsync()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetAccountTransactionsAsync)}");

                var entities = await _accountTransactionService.GetAccountTransactionsAsync();
                return Ok(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult> GetAccountTransactionByIdAsync(Guid Id)
        {
            try
            {
                var entity = await _accountTransactionService.GetAccountTransactionByIdAsync(Id);
                return Ok(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("GetTransactionsByAccountId")]
        public async Task<ActionResult> GetTransactionsByAccountIdAsync([FromBody] AccountTransactionRequestDto requestDto)
        {
            try
            {
                if (string.IsNullOrEmpty(requestDto.EmiMonth))
                {
                    var accountTransactions = await _accountTransactionService.GetAccountTransactionByAccountIdAsync(requestDto.accountId);
                    return Ok(accountTransactions);
                }
                else
                {
                    var accountTransactions = await _accountTransactionService.GetAccountTransactionWithInterestAsync(requestDto.accountId, requestDto.EmiMonth);
                    return Ok(accountTransactions);
                }
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("GetTransactionsForAllAccounts")]
        public async Task<ActionResult> GetAccountTransactionsWithInterestAsync([FromBody] TransactionsRequestDto requestDto)
        {
            try
            {
                    var accountTransactions = await _accountTransactionService.GetAccountTransactionsWithInterestAsync(requestDto.EmiMonth);
                    return Ok(accountTransactions);
            

            }
            catch (Exception)
            {

                throw;
            }
        }
        //[HttpGet("GetTransactionsByAccountId/{accountId}")]
        //public async Task<ActionResult> GetTransactionsByAccountIdAsync(Guid accountId)
        //{
        //    try
        //    {
        //        var accountTransactions = await _accountTransactionService.GetAccountTransactionByAccountIdAsync(accountId);
        //        return Ok(accountTransactions);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}




        [HttpPost]
        public async Task<ActionResult> CreateAccountAsync(CreateAccountTransactionDto account)
        {
            try
            {
                var entity = await _accountTransactionService.CreateAccountTransactionAsync(account);
                return Ok(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateAccountTransactionAsync(Guid Id, UpdateAccountTransactionDto account)
        {
            try
            {

                var accounts = await _accountTransactionService.UpdateAccountTransactionAsync(Id, account);
                return Ok(accounts);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteAccountTransactionAsync(Guid Id)
        {
            try
            {
                var accounts = await _accountTransactionService.DeleteAccountTransactionAsync(Id);
                return Ok(accounts);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
