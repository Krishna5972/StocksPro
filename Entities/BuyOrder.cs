using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;

namespace Entities
{
    public class BuyOrder
    {
        [Required(ErrorMessage = "Buy Order ID is missing")]
        public Guid BuyOrderID { get; set; }

        [Required(ErrorMessage = "Stock Symbol is missing")]
        public string StockSymbol { get; set; }
        [Required(ErrorMessage = "Stock Name is missing")]
        public string StockName { get; set;}

        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1,100000,ErrorMessage ="Quantity must be in range of 1 and 100000")]
        public uint Quantity { get; set; }
        [Range(1, 100000, ErrorMessage = "Quantity must be in range of 1 and 100000")]
        public double Price { get; set; }

    }
}