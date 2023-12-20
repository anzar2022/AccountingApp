using Interest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Interest.Dtos.InterestDto;

namespace Interest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestController : ControllerBase
    {
        private ILogger<InterestController> _logger = null;
        private IInterestService _interestService = null;

        public InterestController(ILogger<InterestController> logger, IInterestService interestService)
        {
            _logger = logger;
            _interestService = interestService;
        }
        [HttpGet]
        public async Task<ActionResult> GetInterestsAsync()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetInterestsAsync)}");

                var accounts = await _interestService.GetInterestsAsync();
                return Ok(accounts);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult> GetInterestByIdAsync(Guid Id)
        {
            try
            {
                var accounts = await _interestService.GetInterestByIdAsync(Id);
                return Ok(accounts);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateInterestAsync(CreateInterestDto interest)
        {
            try
            {
                var interests = await _interestService.CreateInterestAsync(interest);
                return Ok(interests);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateInterestAsync(Guid Id, UpdateInterestDto interest)
        {
            try
            {
                var accounts = await _interestService.UpdateInterestAsync(Id, interest);
                return Ok(accounts);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteInterestAsync(Guid Id)
        {
            try
            {
                var accounts = await _interestService.DeleteInterestAsync(Id);
                return Ok(accounts);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
