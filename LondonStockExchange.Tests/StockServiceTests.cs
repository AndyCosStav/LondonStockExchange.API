using LondonStockExchange.BusinessLogic.Repositories;
using LondonStockExchange.BusinessLogic.Services;
using LondonStockExchange.DAL.DataContextModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonStockExchange.Tests
{
    public class StockServiceTests
    {
        [Fact]
        public async Task GetAllStocks_ReturnsAllStocks()
        {
            // Arrange
            var mockStockRepository = new Mock<IStockRepository>();
            var stocks = new List<Stock>
        {
            new Stock { StockId = Guid.NewGuid(), TickerSymbol = "AAPL", CurrentPrice = 150.0m },
            new Stock { StockId = Guid.NewGuid(), TickerSymbol = "GOOG", CurrentPrice = 2500.0m },
            new Stock { StockId = Guid.NewGuid(), TickerSymbol = "TSLA", CurrentPrice = 700.0m },
        };
            mockStockRepository.Setup(repo => repo.GetAllStocks()).ReturnsAsync(stocks);
            var stockService = new StockService(mockStockRepository.Object);

            // Act
            var result = await stockService.GetAllStocks();

            // Assert
            Assert.Equal(stocks, result);
        }

        [Fact]
        public async Task UpdateStockPrice_UpdatesStockPrice()
        {
            // Arrange
            var stockId = Guid.NewGuid();
            var newStockValue = 160.0m;
            var mockStockRepository = new Mock<IStockRepository>();
            mockStockRepository.Setup(repo => repo.UpdateStockPrice(stockId, newStockValue)).Returns(Task.CompletedTask);
            var stockService = new StockService(mockStockRepository.Object);

            // Act
            await stockService.UpdateStockPrice(stockId, newStockValue);

            // Assert
            mockStockRepository.Verify(repo => repo.UpdateStockPrice(stockId, newStockValue), Times.Once);
        }
    }
}
