﻿using AccountDatabase.Entities;
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

        //[HttpGet("GetInterestTransactionsForAllAccounts")]
        //public async Task<ActionResult> GetAccountTransactionsWithInterestAsync()
        //{
        //    try
        //    {
        //        var emiMonth = DateTime.Now.Month.ToString();
        //        int numberOfPreviousMonths = 2;  
        //        Dictionary<string, object> transactionsByMonth = new Dictionary<string, object>();
        //        for (int i = 0; i <= numberOfPreviousMonths; i++)
        //        {
        //            var targetMonth = (int.Parse(emiMonth) - i).ToString(); 
        //            var transactions = await _dashboardservice.GetInterestTransactionsDetailAsync(targetMonth);
        //            transactionsByMonth.Add($"Month{i}", new
        //            {
        //                Month = targetMonth,
        //                Transactions = transactions
        //            });
        //        }

        //          return Ok(transactionsByMonth);
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
                // Get the current month and year
                var currentMonthYear = DateTime.Now.ToString("MM/yyyy");

                // Get the previous month and year
                var previousMonthYear = DateTime.Now.AddMonths(-1).ToString("MM/yyyy");

                var currentTransactions = await _dashboardservice.GetInterestTransactionsDetailAsync(currentMonthYear);
                var previousTransactions = await _dashboardservice.GetInterestTransactionsDetailAsync(previousMonthYear);

                var result = new
                {
                    CurrentMonthYear = currentMonthYear,
                    PreviousMonthYear = previousMonthYear,
                    CurrentTransactions = currentTransactions,
                    PreviousTransactions = previousTransactions
                };

                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }





    }
}