using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBigElephant.EventModule
{
    class Brent
    {
        int crudeOilBrentPrice;

        public Brent()
        {

        }

        public event EventHandler<BrentPriceChangeEventArgs> BrentPriceChanged;

        protected virtual void OnBrentPriceChanged(BrentPriceChangeEventArgs e)
        {
            BrentPriceChanged?.Invoke(this, e);
        }

        public int CrudeOilBrentPrice
        {
            get { return crudeOilBrentPrice; }
            set
            {
                if(crudeOilBrentPrice == value)
                    return;
                else
                {
                    int oldPrice = crudeOilBrentPrice;
                    crudeOilBrentPrice = value;
                    OnBrentPriceChanged(new BrentPriceChangeEventArgs(oldPrice, crudeOilBrentPrice));
                }
            }
        } 
    }
}
