using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace theBigElephant.Portfolio
{
    class StockPortfolioUpdater
    {
        List<PortfolioStock> stockPortfolioList = new List<PortfolioStock>();
        PortfolioLoader portfolioLoader = new PortfolioLoader();

        public void stockBuy(PortfolioStock stock)
        {
            stockPortfolioList = portfolioLoader.LoadPortfolioStocks();
            stockPortfolioList.Add(stock);
            XmlSerializer xs = new XmlSerializer(typeof(List<Portfolio.PortfolioStock>));
            TextWriter tw = new StreamWriter(@"stockPortfolio.xml");
            xs.Serialize(tw, stockPortfolioList);
            tw.Close();
        }

        public void stockSell(string label, int quantity)
        {
            stockPortfolioList = portfolioLoader.LoadPortfolioStocks();
            foreach (Portfolio.PortfolioStock stock in stockPortfolioList)
            {
                if (stock.symbol == label)
                {
                    stock.quantity -= quantity;
                }

                XmlSerializer xs = new XmlSerializer(typeof(List<Portfolio.PortfolioStock>));
                TextWriter tw = new StreamWriter(@"stockPortfolio.xml");
                xs.Serialize(tw, stockPortfolioList);
                tw.Close();
            }
        }
    }
}
