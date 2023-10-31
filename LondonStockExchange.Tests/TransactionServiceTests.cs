using LondonStockExchange.BusinessLogic.Models.Requests;
using LondonStockExchange.BusinessLogic.Repositories;
using LondonStockExchange.BusinessLogic.Services;
using LondonStockExchange.DAL.DataContextModels;
using Moq;

namespace LondonStockExchange.Tests
{
    public class TransactionServiceTests
    {
        [Fact]
        public async Task ProcessTransaction_Should_AddTransactionAndUpdateStockPrice()
        {
            // Arrange
            var transactionModel = new TransactionModel
            {
                StockId = Guid.NewGuid(),
                Price = 100,
                SharesExchanged = 10,
            };

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var stockServiceMock = new Mock<IStockService>();

            transactionRepositoryMock
                .Setup(repo => repo.AddTransaction(transactionModel))
                .Verifiable();

            transactionRepositoryMock
                .Setup(repo => repo.GetRecentTransactionsForStock(transactionModel.StockId))
                .ReturnsAsync(new List<Transaction>())
                .Verifiable();

            stockServiceMock
                .Setup(service => service.UpdateStockPrice(transactionModel.StockId, It.IsAny<decimal>()))
                .Verifiable();

            var transactionService = new TransactionService(transactionRepositoryMock.Object, stockServiceMock.Object);

            // Act
            await transactionService.ProcessTransaction(transactionModel);

            // Assert
            transactionRepositoryMock.Verify();
            stockServiceMock.Verify();
        }

        [Fact]
        public async Task CalculateWeightedAveragePrice_Should_CalculateCorrectly()
        {
            // Arrange
            var stockId = Guid.NewGuid();
            var recentTransactions = new List<Transaction>
            {
                new Transaction { Price = 100, SharesExchanged = 10 },
                new Transaction { Price = 110, SharesExchanged = 5 },
            };

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            transactionRepositoryMock
                .Setup(repo => repo.GetRecentTransactionsForStock(stockId))
                .ReturnsAsync(recentTransactions);

            var stockServiceMock = new Mock<IStockService>();

            var transactionService = new TransactionService(transactionRepositoryMock.Object, stockServiceMock.Object);

            // Act
            var weightedAveragePrice = await transactionService.CalculateWeightedAveragePrice(stockId);

            // Assert
            Assert.Equal(103.33m, weightedAveragePrice, 2);
        }
    }
}
