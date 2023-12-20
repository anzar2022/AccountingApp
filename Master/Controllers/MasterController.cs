using Master.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Master.Dtos.MasterDto;

namespace Master.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private ILogger<MasterController> _logger = null;
        private IMasterService _masterService = null;

        public MasterController(ILogger<MasterController> logger, IMasterService masterService)
        {
            _logger = logger;
            _masterService = masterService;
        }
        [HttpGet]
        public async Task<ActionResult> GetInterestsAsync()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetInterestsAsync)}");

                var accounts = await _masterService.GetInterestsAsync();
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
                var accounts = await _masterService.GetInterestByIdAsync(Id);
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
                var interests = await _masterService.CreateInterestAsync(interest);
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
                var accounts = await _masterService.UpdateInterestAsync(Id, interest);
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
                var accounts = await _masterService.DeleteInterestAsync(Id);
                return Ok(accounts);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
