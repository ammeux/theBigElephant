using System;
using System.Collections.Generic;
using System.IO;
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
            if (File.Exists("stocksPortfolio.xml"))
            {
                doc.Load("stocksPortfolio.xml");
                XmlElement stocks = doc["stocks"];
                XmlNodeList items = stocks.GetElementsByTagName("stock");
                foreach (XmlNode item in items)
                    stockList.Add(new Stock()
                    {
                        Name = item["name"].InnerText,
                        Symbol = item["symbol"].InnerText
                    });
            }
            return stockList;
        }
    }
}
