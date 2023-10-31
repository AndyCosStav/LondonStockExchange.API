using LondonStockExchange.BusinessLogic.Models.Requests;
using LondonStockExchange.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LondonStockExchange.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly ITransactionService _transactionService;

        public StockController(IStockService stockService, ITransactionService transactionService)
        {
            _stockService = stockService;
            _transactionService = transactionService;
        }

        [HttpGet("stocks")]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _stockService.GetAllStocks();
            return Ok(stocks);
        }

        [HttpGet("stocks/{ticker}")]
        public async Task<IActionResult> GetStockByTicker(string ticker)
        {
            var stock = await _stockService.GetStockByTicker(ticker);
            if (stock == null)
            {
                return NotFound("Stock not found");
            }
            return Ok(stock);
        }

        [HttpGet("topstocks")]
        public async Task<IActionResult> GetTopStocks(int count)
        {
            var topStocks = await _stockService.GetTopStocks(count);
            return Ok(topStocks);
        }

        [HttpGet("multiple-stocks")]
        public async Task<IActionResult> GetMultipleStocks([FromQuery] List<string> tickers)
        {
            var stocks = await _stockService.GetMultipleStocks(tickers);
            return Ok(stocks);
        }

        [HttpPost("transactions")]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionModel request)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                request.BrokerId = userId;
                // Call the transaction service to process the transaction and update stock value
                await _transactionService.ProcessTransaction(request);

                return Ok("Transaction added and stock value updated.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return BadRequest($"Failed to process transaction: {ex.Message}");
            }
        }
    }

}
