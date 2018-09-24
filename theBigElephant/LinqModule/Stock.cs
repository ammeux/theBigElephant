using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.Data.Linq;
using System.Data.Linq.Mapping;


namespace theBigElephant.LinqModule
{
    [Table(Name = "dbo.Stocks")]
    public class Stock : IComparable<Stock>
    {
        [Column(IsPrimaryKey = true)] public int id;
        [Column] public string Stock_name;
        [Column] public int Stock_price;
        [Column] public string Stock_country;

        public int CompareTo(Stock stock)
        {
            return this.Stock_name.CompareTo(stock.Stock_name);
        }
    }
}
