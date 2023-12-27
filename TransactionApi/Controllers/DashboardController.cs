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

        //[HttpGet("GetInterestTransactionsForAllAccounts")]
        //public async Task<ActionResult> GetAccountTransactionsWithInterestAsync()
        //{
        //    try
        //    {
        //        var emiMonth = DateTime.Now.Month.ToString();
        //        var accountDetailTransactions = await _dashboardservice.GetInterestTransactionsDetailAsync(emiMonth);
        //        return Ok(accountDetailTransactions);


        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        [HttpGet("GetInterestTransactionsForAllAccounts")]
        public async Task<ActionResult> GetAccountTransactionsWithInterestAsync()
        {
            try
            {
                // Get the current month as a string
                var emiMonth = DateTime.Now.Month.ToString();

                // Number of previous months to fetch transactions for
                int numberOfPreviousMonths = 2;  // Change this value based on your requirement

                // Create a dictionary to store transactions for each month
                Dictionary<string, object> transactionsByMonth = new Dictionary<string, object>();

                // Loop through the current month and the preceding months
                for (int i = 0; i <= numberOfPreviousMonths; i++)
                {
                    // Calculate the month for which to fetch transactions
                    var targetMonth = (int.Parse(emiMonth) - i).ToString();

                    // Fetch transactions for the target month
                    var transactions = await _dashboardservice.GetInterestTransactionsDetailAsync(targetMonth);

                    // Add the transactions to the dictionary using a key indicating the month
                    transactionsByMonth.Add($"Month{i}", new
                    {
                        Month = targetMonth,
                        Transactions = transactions
                    });
                }

                // You can customize this logic based on how your service handles month-based transactions

                return Ok(transactionsByMonth);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
