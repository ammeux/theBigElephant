using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBigElephant.UnitTests
{   
    [TestFixture]
    public class UnitTestExample
    {
        [Test]
        public void CreateStockObject()
        {
            Stock stock = new Stock()
            {
                Name = "Total",
                Symbol = "TT"
            };
            Assert.AreEqual("Total", stock.Name);
        }
    }
}
