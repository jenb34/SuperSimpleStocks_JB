using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks
{
    public class Trade
    {
        public DateTime Timestamp { get; set; }
        public int QuantityOfShares { get; set; }
        public TradeType TradeType;
        public Stock TradeStock;
        public double TradePrice { get; set; }

        /// <summary>
        /// Create a trade recording the quantity of shares bought/sold, 
        /// the type of trade (BUY/SELL) and the stock sold.
        /// Also record the timestamp and total trade price.
        /// </summary>
        /// <param name="quantityOfShares"></param>
        /// <param name="tradeType"></param>
        /// <param name="tradeStock"></param>
        public Trade(int quantityOfShares, TradeType tradeType, Stock tradeStock)
        {
            Timestamp = DateTime.Now;
            QuantityOfShares = quantityOfShares;
            TradeType = tradeType;
            TradeStock = tradeStock;
            TradePrice = QuantityOfShares * TradeStock.MarketPrice;
        }
    }
}
