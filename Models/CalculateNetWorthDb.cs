using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CalculateNetWorth.Models
{
    public class CalculateNetWorthDb : ICalculateNetWorth
    {
        public static List<StockTransactionDetails> stdList = new List<StockTransactionDetails>
       {
           new StockTransactionDetails{TStockId=1,PortfolioId=1,StockName="HDFC",StockCount=10,saleStatus=true},
           new StockTransactionDetails{TStockId=2,PortfolioId=1,StockName="CDSL",StockCount=10,saleStatus=true},
           new StockTransactionDetails{TStockId=3,PortfolioId=3,StockName="Pidilite",StockCount=10,saleStatus=true},
           new StockTransactionDetails{TStockId=4,PortfolioId=2,StockName="SBI",StockCount=10,saleStatus=true},
           new StockTransactionDetails{TStockId=5,PortfolioId=3,StockName="SBI",StockCount=10,saleStatus=true},
           new StockTransactionDetails{TStockId=6,PortfolioId=2,StockName="HDFC",StockCount=10,saleStatus=true},
           new StockTransactionDetails{TStockId=7,PortfolioId=1,StockName="SBI",StockCount=10,saleStatus=true},
           new StockTransactionDetails{TStockId=8,PortfolioId=2,StockName="CDSL",StockCount=10,saleStatus=true}
       };

        public static List<MutualFundTransactionDetails> mtdList = new List<MutualFundTransactionDetails>
        {
           new MutualFundTransactionDetails{TMutualId=1,PortfolioId=1,MutualFundName="Vanguard",MutualFundCount=10,saleStatus=true},
           new MutualFundTransactionDetails{TMutualId=2,PortfolioId=1,MutualFundName="Fidelity",MutualFundCount=10,saleStatus=true},
           new MutualFundTransactionDetails{TMutualId=3,PortfolioId=3,MutualFundName="ICICI",MutualFundCount=10,saleStatus=true},
           new MutualFundTransactionDetails{TMutualId=4,PortfolioId=2,MutualFundName="Axis",MutualFundCount=10,saleStatus=true},
           new MutualFundTransactionDetails{TMutualId=5,PortfolioId=2,MutualFundName="ICICI",MutualFundCount=10,saleStatus=true},
           new MutualFundTransactionDetails{TMutualId=6,PortfolioId=3,MutualFundName="Vanguard",MutualFundCount=10,saleStatus=true},
           new MutualFundTransactionDetails{TMutualId=7,PortfolioId=3,MutualFundName="Fidelity",MutualFundCount=10,saleStatus=true},
           new MutualFundTransactionDetails{TMutualId=8,PortfolioId=1,MutualFundName="Axis",MutualFundCount=10,saleStatus=true}


        };
        public async Task<List<StockTransactionDetails>> GetStockTransactionDetails(PortfolioDetail customer)
        {
            var StockList = stdList.Where(x => x.PortfolioId == customer.PortfolioId && x.saleStatus == true).ToList();
            foreach(var item in StockList)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://13.89.113.158/api/DailySharePrice/" + item.StockName))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var stockObj = JsonConvert.DeserializeObject<DailyStockDetails>(apiResponse);
                        item.TotalPrice = item.StockCount * stockObj.StockValue;
                    }
                }
            }
            return StockList;
        }
        public async Task<List<MutualFundTransactionDetails>> GetMutualFundTransactionDetails(PortfolioDetail customer)
        {
            var MutualFundList = mtdList.Where(x => x.PortfolioId == customer.PortfolioId && x.saleStatus == true).ToList();
            foreach (var item in MutualFundList)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://52.154.50.148/api/DailyMutualFundNav/" + item.MutualFundName))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var mutualFundObj = JsonConvert.DeserializeObject<MutualFundDetail>(apiResponse);
                        item.TotalPrice = item.MutualFundCount * mutualFundObj.MutualFundValue;
                    }
                }
            }
            return MutualFundList;
        }

        public void SellStock(int id)
        {
            var obj=(stdList.FirstOrDefault(x => x.TStockId == id));
            obj.saleStatus = false;
        }

        public void SellMutualFund(int id)
        {
            var obj = (mtdList.FirstOrDefault(x => x.TMutualId == id));
            obj.saleStatus = false;
        }
    }
}
