using LondonStockExchange.DAL.Data;
using LondonStockExchange.DAL.DataContextModels;
using Microsoft.EntityFrameworkCore;

namespace LondonStockExchange.BusinessLogic.Repositories
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocks();
        Task<Stock> GetStockByTicker(string ticker);
        Task<List<Stock>> GetTopStocks(int count);
        Task<List<Stock>> GetMultipleStocks(List<string> tickers);
        Task UpdateStockPrice(Guid stockId, decimal newStockValue);
    }


    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllStocks()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock> GetStockByTicker(string ticker)
        {
            return await _context.Stocks.SingleOrDefaultAsync(s => s.TickerSymbol == ticker);
        }

        public async Task<List<Stock>> GetTopStocks(int count)
        {
            return await _context.Stocks.OrderByDescending(s => s.CurrentPrice).Take(count).ToListAsync();
        }

        public async Task<List<Stock>> GetMultipleStocks(List<string> tickers)
        {
            return await _context.Stocks.Where(s => tickers.Contains(s.TickerSymbol)).ToListAsync();
        }

        public async Task UpdateStockPrice(Guid stockId, decimal newStockValue)
        {
            var stock = await _context.Stocks.FindAsync(stockId);

            if (stock != null)
            {
                stock.CurrentPrice = newStockValue;
                await _context.SaveChangesAsync();
            }
        }

    }
}
