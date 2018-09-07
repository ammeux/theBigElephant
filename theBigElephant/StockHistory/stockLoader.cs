using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace theBigElephant.StockHistory
{
    class StockLoader
    {
        public List<Stock> LoadStocks()
        {
            List<Stock> stockList = new List<Stock>();
            var doc = new XmlDocument();
            if (File.Exists("stockHistory.xml"))
            {
                var serializer = new XmlSerializer(typeof(List<Stock>));
                using (var s = File.OpenRead("stockHistory.xml"))
                {
                    stockList = (List<Stock>)serializer.Deserialize(s);
                }
            }
            return stockList;

            /* SEE BELOW MY PREVIOUS METHOD FOR COLLECTING ELEMENT FROM XML FILE
             
                doc.Load("stocksPortfolio.xml");
                XmlElement stocks = doc["ArrayOfStock"];
                XmlNodeList items = stocks.GetElementsByTagName("Stock");

                foreach (XmlNode item in items)
                    stockList.Add(new Stock()
                    {
                        Name = item["Name"].InnerText,
                        Symbol = item["Symbol"].InnerText
                    });
            }
               SEE ABOVE MY PREVIOUS METHOD FOR COLLECTING ELEMENT FROM XML FILE*/
        }
    }
}
