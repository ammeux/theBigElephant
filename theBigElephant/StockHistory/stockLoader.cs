using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace theBigElephant.StockHistory
{
    class StockLoader
    {
        public List<Stock> LoadStocks()
        {
            List<Stock> stockList = new List<Stock>();
            var doc = new XmlDocument();
            doc.Load("stocksPortfolio.xml");
            XmlElement stocks = doc["stocks"];
            XmlNodeList items = stocks.GetElementsByTagName("stock");
            foreach (XmlNode item in items)
                stockList.Add(new Stock()
                {
                    Name = item["name"].InnerText,
                    Symbol = item["symbol"].InnerText
                });
            return stockList;
        }
    }
}
