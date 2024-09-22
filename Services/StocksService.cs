using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
    public class StocksService : IStocksService
    {
        private List<BuyOrder> _buyOrdersList;
        private List<SellOrder> _sellOrdersList;
        public StocksService() 
        {
            _buyOrdersList = new List<BuyOrder>();
            _sellOrdersList = new List<SellOrder>();
        }
        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest is null) throw new ArgumentNullException(nameof(buyOrderRequest));
            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            buyOrder.BuyOrderID = Guid.NewGuid();
            _buyOrdersList.Add(buyOrder);

            return buyOrder.ToBuyOrderResponse();
        }

        public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest is null) throw new ArgumentNullException(nameof(sellOrderRequest));
            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            sellOrder.SellOrderID = Guid.NewGuid();
            _sellOrdersList.Add(sellOrder);

            return sellOrder.TosellOrderResponse();
        }

        public List<BuyOrderResponse> GetAllBuyOrders()
        {


            return _buyOrdersList.OrderByDescending(x=>x.DateAndTimeOfOrder)
                                 .Select(y=>y.ToBuyOrderResponse())
                                 .ToList();
        }

        public List<SellOrderResponse> GetAllSellOrders()
        {

            return _sellOrdersList.OrderByDescending(x => x.DateAndTimeOfOrder)
                                 .Select(y => y.TosellOrderResponse())
                                 .ToList();
        }


        
    }

    
        

}
