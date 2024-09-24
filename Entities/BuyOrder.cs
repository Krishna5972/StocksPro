using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;

namespace Entities
{
    public class BuyOrder
    {
        public Guid BuyOrderID { get; set; }

        public string StockSymbol { get; set; }
        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }

    }
}