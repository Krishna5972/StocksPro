using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public interface IOrderResponse
    {
        string StockSymbol { get; set; }
        string StockName { get; set; }

        OrderType OrderType { get; set; }
        DateTime DateAndTimeOfOrder { get; set; }
        uint Quantity { get; set; }
        double Price { get; set; }
        double TradeAmount { get; set; }
    }

    public enum  OrderType{

        [Description("Buy Order")]
        Buy,

        [Description("Sell Order")]
        Sell 
        }
}
