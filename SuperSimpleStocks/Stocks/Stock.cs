using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks
{
    public class Stock
    {
        public string Symbol { get; set; }        
        public double ParValue { get; set; }
        public double MarketPrice { get; set; }
        private double LastDividend;

        /// <summary>
        /// Create a stock with the Common stock properties of
        /// symbol, last dividend and par value.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="lastDividend"></param>
        /// <param name="parValue"></param>
        public Stock(string symbol, double lastDividend, double parValue)
        {
            Symbol = symbol;
            LastDividend = lastDividend;
            ParValue = parValue;
        }

        /// <summary>
        /// Calculate the dividend yield of the stock.
        /// </summary>
        /// <returns></returns>
        public virtual double CalculateDividendYield()
        {
            return LastDividend / MarketPrice;
        }

        /// <summary>
        /// Calculate the P/E Ratio of the stock.
        /// </summary>
        /// <returns></returns>
        public double CalculatePERatio()
        {
            return MarketPrice / LastDividend;
        }
    }
}
