using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks
{
    public class PreferredStock : Stock
    {
        private double FixedDividend { get; set; }

        /// <summary>
        /// Create a Preferred stock, an extension of the base type Stock, which has
        /// an extra property of Fixed Dividend.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="lastDividend"></param>
        /// <param name="parValue"></param>
        /// <param name="fixedDividend"></param>
        public PreferredStock(string symbol, double lastDividend, double parValue, double fixedDividend) : base(symbol, lastDividend, parValue)
        {
            FixedDividend = fixedDividend;
        }

        /// <summary>
        /// Override the base class method to calculate dividend yield
        /// specifically for a Preferred stock.
        /// </summary>
        /// <returns></returns>
        public override double CalculateDividendYield()
        {
            return (FixedDividend * ParValue) / MarketPrice;
        }
    }
}
