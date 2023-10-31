namespace LondonStockExchange.DAL.DataContextModels
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }

        public Guid StockId { get; set; } 

        public decimal Price { get; set; }
        public decimal SharesExchanged { get; set; }
        public string BrokerId { get; set; }
        public DateTime TransactionDateTime { get; set; }

    }

}
