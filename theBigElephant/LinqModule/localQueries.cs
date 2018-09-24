using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBigElephant.LinqModule
{
    class LocalQueries
    {
        InterpretedQueries interpretedQueries = new InterpretedQueries();

        public IEnumerable<Stock> getStocksContaining(string searchString)
        {
            IEnumerable<Stock> myIEnumerable = from stock in interpretedQueries.GetSqlElements()
                where stock.Stock_name.Contains(searchString)
                select stock;

            return myIEnumerable;
        }

        public IEnumerable<Stock> orderStocksAlphabetically()
        {
            IEnumerable<Stock> myIEnumerable = interpretedQueries.GetSqlElements()
                .OrderBy(n => n)
                .Select(n => n);

            return myIEnumerable;
        }
    }
}
