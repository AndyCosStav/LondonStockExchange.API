using LondonStockExchange.DAL.Data;
using LondonStockExchange.DAL.DataContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonStockExchange.BusinessLogic.Services.SeedData
{
    public interface ISeedDataService
    {
        void SeedStockData();
    }

    public class SeedDataService : ISeedDataService
    {
        private readonly AppDbContext _context;

        public SeedDataService(AppDbContext context)
        {
            _context = context;
        }

        public void SeedStockData()
        {
            if (!_context.Stocks.Any())
            {
                // Add sample stocks to the database
                var sampleStocks = new List<Stock>
            {
                new Stock
                {
                    TickerSymbol = "AAPL",
                    CompanyName = "Apple Inc.",
                    CurrentPrice = 150.0m,
                },
                new Stock
                {
                    TickerSymbol = "GOOGL",
                    CompanyName = "Alphabet Inc.",
                    CurrentPrice = 2700.0m,
                },
                new Stock
                {
                    TickerSymbol = "AMZN",
                    CompanyName = "Amazon.com Inc.",
                    CurrentPrice = 3300.0m,
                },
                new Stock
                {
                    TickerSymbol = "MSFT",
                    CompanyName = "Microsoft Corporation",
                    CurrentPrice = 300.0m,
                },
                // Add more sample stocks as needed
            };

                _context.Stocks.AddRange(sampleStocks);
                _context.SaveChanges();
            }
        }
    }

}
