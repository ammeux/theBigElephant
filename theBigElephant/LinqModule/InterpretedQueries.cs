using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theBigElephant.LinqModule;

namespace theBigElephant.LinqModule
{
    class InterpretedQueries
    {
        DataContext dataContext;
 
        public IEnumerable<LinqModule.Stock> GetSqlElements()
        {
            dataContext = new DataContext(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = the_big_elephant_3; Integrated Security = True; Pooling = False");
            Table<LinqModule.Stock> stocks = dataContext.GetTable<Stock>();

            IQueryable<Stock> query = stocks
                .OrderBy(n => n.Stock_name.Length)
                .Select (n => n);

            return query.AsEnumerable();
        }
    }
}
