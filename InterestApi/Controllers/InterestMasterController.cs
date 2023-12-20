using InterestMasterApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterestMasterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestMasterController : ControllerBase
    {
        private ILogger<InterestMasterController> _logger;
        private IInterestMasterService _interestMasterService;
        public InterestMasterController(ILogger<InterestMasterController> logger, IInterestMasterService interestMasterService)
        {
            _logger = logger;
            _interestMasterService = interestMasterService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAccountInterestsAsync()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetAccountInterestsAsync)}");

                var entities = await _interestMasterService.GetAccountInterestsAsync();
                return Ok(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
