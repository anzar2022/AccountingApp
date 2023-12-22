using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using TransactionApi.Services;

namespace TransactionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestTransactionController : ControllerBase
    {
        private ILogger<InterestTransactionController> _logger = null;
        private IInterestTransactionService _interestTransactionService = null;

        public InterestTransactionController(ILogger<InterestTransactionController> logger, IInterestTransactionService interestTransactionService)
        {
            _logger = logger;
            _interestTransactionService = interestTransactionService;
        }
        [HttpGet]
        public async Task<ActionResult> GetInterestTransactionAsync()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetInterestTransactionAsync)}");

                var interestTransactions = await _interestTransactionService.GetInterestTransactionAsync();
                return Ok(interestTransactions);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        //generateEmiByTransactionId
        [HttpGet("GenerateInterestEMI/{transactionId}")]
        public async Task<ActionResult> GetInterestTransactionByAccountIdAsync(Guid transactionId)
        {
            try
            {
                
                _logger.LogInformation($"{nameof(GetInterestTransactionByAccountIdAsync)}");

                var interestTransactions = await _interestTransactionService.GetInterestEMIByTransactionIdAsync(transactionId);


                return Ok(interestTransactions);
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
