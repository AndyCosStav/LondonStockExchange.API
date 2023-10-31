using LondonStockExchange.BusinessLogic.Models.Requests;
using LondonStockExchange.DAL.Data;
using LondonStockExchange.DAL.DataContextModels;
using Microsoft.EntityFrameworkCore;

namespace LondonStockExchange.BusinessLogic.Repositories
{
    public interface ITransactionRepository
    {
        Task AddTransaction(TransactionModel transaction);
        Task<List<Transaction>> GetRecentTransactionsForStock(Guid stockId);
    }

    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _dbContext;

        public TransactionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddTransaction(TransactionModel request)
        {
            // Add the transaction to the database
            await _dbContext.Transactions.AddAsync(new DAL.DataContextModels.Transaction
            {
                BrokerId = request.BrokerId,
                Price = request.Price,
                TransactionDateTime = DateTime.Now,
                SharesExchanged = request.SharesExchanged,
                StockId = request.StockId
            });

            // Save changes to the database
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetRecentTransactionsForStock(Guid stockId)
        {
            DateTime oneHourAgo = DateTime.Now.AddHours(-1);

            return await _dbContext.Transactions
                .Where(t => t.StockId == stockId && t.TransactionDateTime >= oneHourAgo)
                .ToListAsync();
        }

    }
}
