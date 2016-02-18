using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks
{
    public class GBCE
    {
        public List<Stock> stocks { get; set; }
        public List<Trade> trades { get; set; }

        /// <summary>
        /// Initialise GBCE with an empty list of stocks and trades.
        /// </summary>
        public GBCE()
        {
            stocks = new List<Stock> { };
            trades = new List<Trade> { };
        }

        /// <summary>
        /// Add a stock to the stocks list for GBCE.
        /// </summary>
        /// <param name="stock"></param>
        public void AddStock(Stock stock)
        {
            stocks.Add(stock);
        }

        /// <summary>
        /// Return the Stock from the stocks list that matches the symbol.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public Stock GetStockBySymbol(string symbol)
        {
            return stocks.Find(stock => stock.Symbol == symbol);
        }

        /// <summary>
        /// Record a BUY/SELL trade in the trades list for GBCE.
        /// </summary>
        /// <param name="trade"></param>
        public void RecordTrade(Trade trade)
        {
            trades.Add(trade);
        }

        /// <summary>
        /// For a given stock, calculate the Volume Weighted Stock Price
        /// for trades that have taken place in the last 15 minutes.
        /// </summary>
        /// <param name="currentStock"></param>
        /// <returns></returns>
        public double CalculateVolumeWeightedStockPrice(Stock currentStock)
        {
            double sumTradePriceByQuantity = 0.0;
            double sumQuantity = 0.0;
            DateTime now = DateTime.Now;

            foreach (Trade trade in trades)
            {
                if(trade.TradeStock == currentStock && trade.Timestamp > now.AddMinutes(-15))
                {
                    sumTradePriceByQuantity += (trade.TradePrice * trade.QuantityOfShares);
                    sumQuantity += trade.QuantityOfShares;
                }

            }

            double volumeWeightedStockPrice = 0.0;
            if (sumQuantity > 0)
            {
                volumeWeightedStockPrice = sumTradePriceByQuantity / sumQuantity;
            }

            return volumeWeightedStockPrice;
        }

        /// <summary>
        /// Calculate the GBCE All Share Index using the geometric mean 
        /// of prices for all stocks.
        /// </summary>
        /// <returns></returns>
        public double CalculateAllShareIndex()
        {
            double productStockPrices = 1.0;
            int countStocksWithPrice = 0;

            foreach (Stock stock in stocks)
            {
                if (stock.MarketPrice > 0)
                {
                    productStockPrices *= stock.MarketPrice;
                    countStocksWithPrice++;
                }
            }

            if (countStocksWithPrice == 0) return 0.0;
            return NthRoot(productStockPrices, countStocksWithPrice);
        }

        static double NthRoot(double a, int n)
        {
            return Math.Pow(a, 1.0 / n);
        }
    }
}
