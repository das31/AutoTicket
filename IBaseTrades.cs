using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstinetTicketer
{
    interface IBaseTrades
    {
        string Symbol { get; set; }
        string Side { get; set; }
        int Shares { get; set; }
        decimal Avg_Price { get; set; }
        string ClientAcct { get; set; }

    }
}
