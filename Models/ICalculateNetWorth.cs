using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateNetWorth.Models
{
    public interface ICalculateNetWorth
    {

        Task<List<StockTransactionDetails>> GetStockTransactionDetails(PortfolioDetail customer);
        Task<List<MutualFundTransactionDetails>> GetMutualFundTransactionDetails(PortfolioDetail customer);

        void SellStock(int id);
        void SellMutualFund(int id);

        
    }
}
