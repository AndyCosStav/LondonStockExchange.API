using LondonStockExchange.BusinessLogic.Repositories;
using LondonStockExchange.DAL.DataContextModels;

namespace LondonStockExchange.BusinessLogic.Services
{
    public interface IStockService
    {
        Task<List<Stock>> GetAllStocks();
        Task<Stock> GetStockByTicker(string ticker);
        Task<List<Stock>> GetTopStocks(int count);
        Task<List<Stock>> GetMultipleStocks(List<string> tickers);
        Task UpdateStockPrice(Guid stockId, decimal newStockValue);
    }


    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task <List<Stock>> GetAllStocks()
        {
            return await _stockRepository.GetAllStocks();
        }

        public async Task<Stock> GetStockByTicker(string ticker)
        {
            return await _stockRepository.GetStockByTicker(ticker);
        }

        public async Task<List<Stock>> GetTopStocks(int count)
        {
            return await _stockRepository.GetTopStocks(count);
        }

        public async Task<List<Stock>> GetMultipleStocks(List<string> tickers)
        {
            return await _stockRepository.GetMultipleStocks(tickers);
        }

        public async Task UpdateStockPrice(Guid stockId, decimal newStockValue)
        {
            // Call the repository method to update the stock price
            await _stockRepository.UpdateStockPrice(stockId, newStockValue);
        }
    }

}
