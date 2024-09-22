using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }
        public string StockSymbol { get; set; }
        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }

        public double TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj.GetType() != typeof(SellOrderResponse)) return false;

            SellOrderResponse other = (SellOrderResponse)obj;

            return (SellOrderID == other.SellOrderID &&
                    StockSymbol == other.StockSymbol &&
                    StockName == other.StockName &&
                    DateAndTimeOfOrder == other.DateAndTimeOfOrder &&
                    Quantity == other.Quantity &&
                    Price == other.Price &&
                    TradeAmount == other.TradeAmount
                );

        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }

    public static class SellOrderExtensions
    {
        public static SellOrderResponse TosellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse()
            {
                SellOrderID = sellOrder.SellOrderID,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Quantity = sellOrder.Quantity,
                Price = sellOrder.Price,
                TradeAmount = Math.Round(sellOrder.Price * sellOrder.Quantity, 2)
            };
        }
    }
}
