using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace theBigElephant.StockHistory
{
    class StockListUpdater
    {
        List<Stock> stockList = new List<Stock>();
        StockLoader stockLoader = new StockLoader();
        Stock stockToBeRemoved;

        public void Save(Stock stockToAddOrRemove) {
            stockList = stockLoader.LoadStocks();
            foreach (var stock in stockList)
            {
                if (stock.Symbol == stockToAddOrRemove.Symbol)
                {
                    stockToBeRemoved = stock;
                }
            }
            if (stockToBeRemoved == null)
                stockList.Add(stockToAddOrRemove);
            else
                stockList.Remove(stockToBeRemoved);
            XmlSerializer xs = new XmlSerializer(typeof(List<Stock>));
            TextWriter tw = new StreamWriter(@"stockHistory.xml");
            xs.Serialize(tw, stockList);
            tw.Close();
        }
    }
}
