using System;
using System.Data;

namespace InstinetTicketer
{
    static class Helper
    {
        public static string GetUntilOrEmpty(this string text, string stopAt = ".")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }
            return text;
        }

        public static int isBuyOrSell(this int shares, string side)
        {
            if (side.ToLower().Contains("sell"))
            {
                return Math.Abs(shares) * -1;
            }
            else
            {
                return Math.Abs(shares);
            }
        }


        public static bool CompareVals<T1, T2> (T1 Val1, T2 Val2)
        {
            return Val1.Equals(Val2);
        }


        public static string getFutures(this string text)
        {
            foreach(DataRow row in Futures.FuturesTable.Rows)
            {
                if (row["Reuters"].Equals(text))
                {
                    return row["Pwatch"].ToString();
                }
            }
            return text;
        }

        public static string removeShort(this string text)
        {
            if (text.ToLower().Contains("short"))
            {
                return "Sell";
            }
            else
            {
                return text;
            }
        }


        public static string requiresShort(this string text)
        {
            if (text.ToLower().Contains("short"))
            {
                return "SHORT SELL";
            }
            else
            {
                return text;
            }
        }

    }
}
