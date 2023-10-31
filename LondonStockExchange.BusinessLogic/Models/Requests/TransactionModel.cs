namespace LondonStockExchange.BusinessLogic.Models.Requests
{
    public class TransactionModel
    {
        public Guid StockId { get; set; }
        public string BrokerId { get; set; } 
        public string TickerSymbol { get; set; } 
        public decimal Price { get; set; } 
        public decimal SharesExchanged { get; set; }
        public DateTime TransactionDateTime { get; set; }
    }
}
