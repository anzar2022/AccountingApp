using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using TransactionApi.Services;
using static TransactionApi.Dtos.InterestEMIDto;

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

        [HttpGet("GetInterestEMI/{transactionId}")]
        public async Task<ActionResult> GetInterestTransactionByIdAsync(Guid transactionId)
        {
            try
            {
                var entity = await _interestTransactionService.GetInterestTransactionByIdAsync(transactionId);
                return Ok(entity);
            }
            catch (Exception)
            {


                throw;
            }
        }

        //generateEmiByTransactionId
        [HttpPost("GenerateInterestEMI")]
        public async Task<ActionResult> GetInterestTransactionByAccountIdAsync([FromBody] GenerateInterestEMIDto interestEmi)
        {
            try
            {
                
                _logger.LogInformation($"{nameof(GetInterestTransactionByAccountIdAsync)}");

                var interestTransactions = await _interestTransactionService.GetInterestEMIByTransactionIdAsync(interestEmi);


                return Ok(interestTransactions);
            }
            catch (Exception)
            {

                throw;
            }
        }


            [HttpPost("PayInterestAmount")]
            public async Task<ActionResult> PayInterestTransactionAsync([FromBody] UpdateInterestEMIDto interestEmi)
            {
                try
                {

                    _logger.LogInformation($"{nameof(GetInterestTransactionByAccountIdAsync)}");

                    var interestTransactions = await _interestTransactionService.UpdateInterestTransactionPaymentAsync(interestEmi);


                    return Ok(interestTransactions);
                }
                catch (Exception)
                {

                    throw;
                }
            }


    }
}
