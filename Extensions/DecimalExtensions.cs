using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class DecimalExtensions
    {
        public static string GetSemiColonPrice(this decimal price)
        {
            if(price.IsNull())
            {
                return "";
            }
            try
            {
                var priceString = price.ToString().Split('.');
                var priceStr = priceString[0];
                string semiColonedReverse = "";
                int index = 0;
                for (int i = priceStr.Length - 1; i >= 0; i--)
                {
                    if (index == 3)
                    {
                        semiColonedReverse += ",";
                        index = 0;
                    }
                    index++;
                    semiColonedReverse += priceStr[i];

                }
                string semiColoned = "";

                for (int i = semiColonedReverse.Length - 1; i >= 0; i--)
                {
                    semiColoned += semiColonedReverse[i];
                }

                return semiColoned;
            }
            catch
            {
                return price + "";
            }
        }
    }
}
