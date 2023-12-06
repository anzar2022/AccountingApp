using AccountApi.Dtos;
using AccountApi.Repositories;
using AccountApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ILogger<AccountController> _logger = null;
        private IAccountService _accountService =  null;
        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAccountsAsync()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetAccountsAsync)}");

                var accounts = await _accountService.GetAccountsAsync();
                return Ok(accounts);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult> GetAccountByIdAsync(Guid Id)
        {
            try
            {
                var accounts = await _accountService.GetAccountByIdAsync(Id);
                return Ok(accounts);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateAccountAsync(CreateAccountDto account)
        {
            try
            {
                var accounts = await _accountService.CreateAccountAsync(account);
                return Ok(accounts);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateAccountAsync(Guid Id, UpdateAccountDto account)
        {
            try
            {
                var accounts = await _accountService.UpdateAccountAsync(Id,account);
                return Ok(accounts);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteAccountAsync(Guid Id)
        {
            try
            {
                var accounts = await _accountService.DeleteAccountAsync(Id);
                return Ok(accounts);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
