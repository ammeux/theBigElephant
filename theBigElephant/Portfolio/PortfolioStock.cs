using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBigElephant.Portfolio
{
    public class PortfolioStock
    {
        public string symbol { get; set; }
        public DateTime purchaseDate { get; set; }
        public int quantity { get; set; }
        public float purchasePrice { get; set; }
    }
}
