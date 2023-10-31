using LondonStockExchange.BusinessLogic.Models.Requests;
using LondonStockExchange.BusinessLogic.Repositories;

namespace LondonStockExchange.BusinessLogic.Services
{
    public interface ITransactionService
    {
        Task ProcessTransaction(TransactionModel transaction);
    }

    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IStockService _stockService;

        public TransactionService(ITransactionRepository transactionRepository, IStockService stockService)
        {
            _transactionRepository = transactionRepository;
            _stockService = stockService;
        }

        public async Task ProcessTransaction(TransactionModel transaction)
        {    
           await _transactionRepository.AddTransaction(transaction);

            // Calculate the new stock value based on weighted average price
            //no clue if this is how its done, just making a guess for the sake of a demo :D 
            decimal newStockValue = await CalculateWeightedAveragePrice(transaction.StockId);

            // Update the stock's current price
            await _stockService.UpdateStockPrice(transaction.StockId, newStockValue);
        }

        public async Task<decimal> CalculateWeightedAveragePrice(Guid stockId)
        {
            var recentTransactions = await _transactionRepository.GetRecentTransactionsForStock(stockId);

            decimal totalValue = 0;
            decimal totalShares = 0;

            foreach (var transaction in recentTransactions)
            {
                totalValue += transaction.Price * transaction.SharesExchanged;
                totalShares += transaction.SharesExchanged;
            }

            if (totalShares != 0)
            {
                // Calculate the new weighted average price
                decimal newPrice = totalValue / totalShares;
                return newPrice;
            }
            else
            {
                // Handle the case where totalShares is zero (to avoid division by zero)s
                return 0;
            }
        }


    }


}

