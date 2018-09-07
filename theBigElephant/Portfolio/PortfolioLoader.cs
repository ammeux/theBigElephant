using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace theBigElephant.Portfolio
{
    class PortfolioLoader
    {
        public List<PortfolioStock> LoadPortfolioStocks()
        {
            List<PortfolioStock> PortfolioStockList = new List<PortfolioStock>();
            var doc = new XmlDocument();
            if (File.Exists("stockPortfolio.xml"))
            {
                var serializer = new XmlSerializer(typeof(List<PortfolioStock>));
                using (var s = File.OpenRead("stockPortfolio.xml"))
                {
                    PortfolioStockList = (List<PortfolioStock>)serializer.Deserialize(s);
                }
            }
            return PortfolioStockList;
        }
    }
}
