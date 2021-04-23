using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateNetWorth.Models
{
    public class StockTransactionDetails
    {
        public int TStockId { get; set; }
        public int PortfolioId { get; set; }
        public string StockName { get; set; }
        public int StockCount { get; set; }
        public int TotalPrice { get; set; }
        public bool saleStatus { get; set; }
    }
}
