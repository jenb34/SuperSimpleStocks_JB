using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks
{
    class SuperSimpleStocks
    {
        static void Main(string[] args)
        {
            // Initialise GBCE and the available stocks
            GBCE GBCE = new GBCE();
            Stock tea = new Stock("TEA", 0, 100);
            Stock pop = new Stock("POP", 8, 100);
            Stock ale = new Stock("ALE", 23, 60);
            Stock joe = new Stock("JOE", 13, 250);
            Stock gin = new PreferredStock("GIN", 8, 100, 0.02);
            GBCE.AddStock(tea);            
            GBCE.AddStock(pop);
            GBCE.AddStock(ale);
            GBCE.AddStock(joe);
            GBCE.AddStock(gin);

            // Initialise some commonly used strings 
            string enterMarketPrice = "Enter the market price for ";
            string performStockOperation = "Perform a Stock Operation (DY | PER | BUY | SELL | VWSP) or EXIT to main menu:";
            string enterStockSymbol = "Enter a stock symbol or another operation to begin...";

            // Show the user the HELP guide
            ShowUserGuide(GBCE.stocks);

            // Run the program...
            string input;
            Boolean exit = false;
            while (!exit)
            {
                input = Console.ReadLine().ToUpper().Trim();
                switch (input)
                {
                    case "TEA": 
                    case "POP": 
                    case "ALE": 
                    case "GIN": 
                    case "JOE":
                        Stock currentStock = GBCE.GetStockBySymbol(input);
                        Console.WriteLine(enterMarketPrice + input + ":");    
                        String stockInput;
                        double marketPrice = 0.0;
                        Boolean validMarketPrice = false;
                        while (!validMarketPrice)
                        {
                            stockInput = Console.ReadLine().ToUpper().Trim();
                            validMarketPrice = Double.TryParse(stockInput, out marketPrice);
                            if (marketPrice <= 0) validMarketPrice = false;
                            if (!validMarketPrice)
                            {
                                Console.WriteLine("ERROR: Invalid market price \"" + stockInput + "\"");
                                Console.WriteLine(enterMarketPrice + input + ":"); 
                            }
                        }
                        currentStock.MarketPrice = marketPrice;
                        Console.WriteLine("");
                        Console.WriteLine(performStockOperation);
                        Boolean back = false;
                        while (!back)
                        {
                            stockInput = Console.ReadLine().ToUpper().Trim();
                            switch (stockInput)
                            {
                                case "DY":
                                    double dividendYield = currentStock.CalculateDividendYield();
                                    Console.WriteLine("DY = " + dividendYield.ToString());
                                    Console.WriteLine("");
                                    Console.WriteLine(performStockOperation);
                                    break;

                                case "PER":
                                    double PERatio = currentStock.CalculatePERatio();
                                    Console.WriteLine("PER = " + PERatio.ToString());
                                    Console.WriteLine("");
                                    Console.WriteLine(performStockOperation);
                                    break;

                                case "BUY":
                                case "SELL":
                                    Console.WriteLine("Enter the number of shares of " + input + " to " + stockInput + ":");
                                    String quantityInput;
                                    Boolean validQuantity = false;
                                    int shares = 0;
                                    while (!validQuantity)
                                    {
                                        quantityInput = Console.ReadLine().ToUpper().Trim();
                                        validQuantity = Int32.TryParse(quantityInput, out shares);
                                        if (shares <= 0) validQuantity = false;
                                        if (!validQuantity)
                                        {
                                            Console.WriteLine("ERROR: Invalid number of shares \"" + quantityInput + "\"");
                                            Console.WriteLine("Enter the number of shares of " + input + " to " + stockInput + ":");
                                        }
                                    }
                                    TradeType type;
                                    type = (stockInput == "BUY") ? TradeType.BUY : TradeType.SELL;
                                    Trade trade = new Trade(shares, TradeType.SELL, currentStock);
                                    GBCE.RecordTrade(trade);
                                    Console.WriteLine("Successful " + stockInput + " trade: " + shares.ToString() + " " + currentStock.Symbol + ", Trade Price: " + trade.TradePrice);
                                    Console.WriteLine("");
                                    Console.WriteLine(performStockOperation);
                                    break;

                                case "VWSP":
                                    double VWSP = GBCE.CalculateVolumeWeightedStockPrice(currentStock);
                                    Console.WriteLine("VWSP = " + VWSP.ToString());
                                    Console.WriteLine("");
                                    Console.WriteLine(performStockOperation);
                                    break;

                                case "EXIT":
                                    back = true;
                                    Console.WriteLine("");
                                    Console.WriteLine(enterStockSymbol);
                                    break;

                                default:
                                    Console.WriteLine("Unknown Stock Operation: " + stockInput);
                                    Console.WriteLine(performStockOperation);
                                    break;
                            }
                        }
                        break;

                    case "ASI":
                        if ((GBCE.stocks.Count(stock => stock.MarketPrice > 0)) > 0)
                        {
                            double allShareIndex = GBCE.CalculateAllShareIndex();
                            Console.WriteLine("ASI = " + allShareIndex.ToString());                            
                        }
                        else
                        {
                            Console.WriteLine("ERROR: No stock prices have been entered");
                        }
                        Console.WriteLine("");
                        Console.WriteLine(enterStockSymbol);
                        break;

                    case "HELP":
                        Console.WriteLine("");
                        ShowUserGuide(GBCE.stocks);
                        break;

                    case "EXIT":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("ERROR: Unknown command \"" + input + "\"");
                        Console.WriteLine(enterStockSymbol);
                        break;
                }
            }
        }

        /// <summary>
        /// Output the HELP to the console for the end user.
        /// </summary>
        /// <param name="stocks"></param>
        private static void ShowUserGuide(List<Stock> stocks)
        {
            Console.WriteLine("GBCE");
            Console.WriteLine("----");

            string availableStocksString = "Stock Symbols: ";
            foreach (var stock in stocks)
            {
                availableStocksString += stock.Symbol + " ";
            }

            Console.WriteLine(availableStocksString);
            Console.WriteLine("");
            Console.WriteLine("Stock Operations:");
            Console.WriteLine("DY | Calculate divivend yield");
            Console.WriteLine("PER | Calculate the P/E Ratio");
            Console.WriteLine("BUY | Buy shares");
            Console.WriteLine("SELL | Sell shares");
            Console.WriteLine("VWSP | Volume Weighted Stock Price (15 minute period)");
            Console.WriteLine("");
            Console.WriteLine("Other Operations:");
            Console.WriteLine("ASI | Calculate GBCE All Share Index");
            Console.WriteLine("HELP | Show operations");
            Console.WriteLine("EXIT | Exit the program");
            Console.WriteLine("");
            Console.WriteLine("Enter a stock symbol or another operation to begin...");
        }
    }
}
