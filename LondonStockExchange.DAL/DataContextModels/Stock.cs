using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonStockExchange.DAL.DataContextModels
{
    public class Stock
    {
        public Guid StockId { get; set; }
        public string TickerSymbol { get; set; }
        public string CompanyName { get; set; }
        public decimal CurrentPrice { get; set; }

    }

}
