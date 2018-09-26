using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace theBigElephant.LinqModule
{
    class XmlToLinq
    {
        public void createStocksXml()
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "utf-16", "yes"),
                new XElement("root",
                    new XElement("stock",
                        new XElement("name", "Total"),
                        new XElement("price", "82"),
                        new XElement("country", "GE")),
                new XElement("stock",
                    new XElement("name", "BNP"),
                    new XElement("price", "34"),
                    new XElement("country", "FR")),
                new XElement("stock",
                    new XElement("name", "Michelin"),
                    new XElement("price", "123"),
                    new XElement("country", "US"))
                )
           );

            doc.Save("xmlToLinq.xml");
        }

        public Stock getStocksFromXml()
        {
            createStocksXml();

            XElement portfolio = XElement.Load("xmlToLinq.xml");

            Stock stock = new Stock();

            stock.Stock_name = portfolio.Element("stock").Element("name").Value;
            stock.Stock_price = int.Parse(portfolio.Element("stock").Element("price").Value);
            stock.Stock_country = portfolio.Element("stock").Element("country").Value;

            return stock;
        }
    }
}
