using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTO
{
    public class BuyOrderResponse
    {

        public Guid BuyOrderID { get; set; }
        public string StockSymbol { get; set; }
        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }

        public double TradeAmount { get; set; }


        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj.GetType() != typeof(BuyOrderResponse)) return false;

            BuyOrderResponse other = (BuyOrderResponse)obj;

            return (BuyOrderID == other.BuyOrderID &&
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

    public static class BuyOrderExtensions
    {
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse()
            {
                BuyOrderID = buyOrder.BuyOrderID,
                StockSymbol = buyOrder.StockSymbol,
                StockName = buyOrder.StockName,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                Quantity = buyOrder.Quantity,
                Price = buyOrder.Price,
                TradeAmount = Math.Round(buyOrder.Price * buyOrder.Quantity,2)
            };
        }
    }
}
