using AccountDatabase.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionApi.Dtos;
using TransactionApi.Repositories;
using TransactionApi.Services;

namespace TransactionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private IDashboardService _dashboardservice;
        private IAccountTransactionService _accountTransactionService;
        private ILogger<InterestEMI> _logger;
        private readonly IMapper _mapper;
        public DashboardController(IDashboardService dashboardservice, IAccountTransactionService accountTransactionService, IAccountTransactionRepository accountTransactionRepository, ILogger<InterestEMI> logger, IMapper mapper)
        {
            _dashboardservice = dashboardservice;
            _accountTransactionService = accountTransactionService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("GetInterestTransactionsForAllAccounts")]
        public async Task<ActionResult> GetAccountTransactionsWithInterestAsync()
        {
            try
            {
                // Get the current month and year
                var currentMonthYear = DateTime.Now.ToString("MM/yyyy");

                // Get the previous month and year
                var previousMonthYear = DateTime.Now.AddMonths(-1).ToString("MM/yyyy");

                // Get the month before the previous month and year
                var monthBeforePreviousMonthYear = GetMonthYearWithOffset(-2);

                var currentTransactions = await _dashboardservice.GetInterestTransactionsDetailAsync(currentMonthYear);
                var previousTransactions = await _dashboardservice.GetInterestTransactionsDetailAsync(previousMonthYear);
                var monthBeforePreviousTransactions = await _dashboardservice.GetInterestTransactionsDetailAsync(monthBeforePreviousMonthYear);

                var result = new
                {
                    CurrentMonthYear = currentMonthYear,
                    PreviousMonthYear = previousMonthYear,
                    MonthBeforePreviousMonthYear = monthBeforePreviousMonthYear,
                    CurrentTransactions = currentTransactions,
                    PreviousTransactions = previousTransactions,
                    MonthBeforePreviousTransactions = monthBeforePreviousTransactions
                };

                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetMonthYearWithOffset(int offset)
        {
            return DateTime.Now.AddMonths(offset).ToString("MM/yyyy");
        }


        [HttpGet("GetAccountsAndUnpaidInterestAsync")]
        public async Task<ActionResult> GetAccountsAndUnpaidInterestAsync()
        {
            try
            {
                var pendingInterestEMIs = await _dashboardservice.GetAccountsAndUnpaidInterestAsync();
                return Ok(pendingInterestEMIs);


            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}
