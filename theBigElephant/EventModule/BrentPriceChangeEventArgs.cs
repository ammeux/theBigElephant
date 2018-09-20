using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBigElephant.EventModule
{
    class BrentPriceChangeEventArgs : EventArgs
    {
        public readonly int LastBrentPrice;
        public readonly int NewBrentPrice;

        public BrentPriceChangeEventArgs(int lastBrentPrice, int newBrentPrice)
        {
            LastBrentPrice = lastBrentPrice;
            NewBrentPrice = newBrentPrice;
        }
    }
}
