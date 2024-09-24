using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
    public class StocksService : IStocksService
    {
        private readonly StocksProDbContext _db;
        public StocksService(StocksProDbContext stockMarketDbContext) 
        {
            _db = stockMarketDbContext;
        }
        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest is null) throw new ArgumentNullException(nameof(buyOrderRequest));
            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            buyOrder.BuyOrderID = Guid.NewGuid();

            _db.Add(buyOrder);
            await _db.SaveChangesAsync();

            return buyOrder.ToBuyOrderResponse();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest is null) throw new ArgumentNullException(nameof(sellOrderRequest));
            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            sellOrder.SellOrderID = Guid.NewGuid();

            _db.Add(sellOrder);
            await _db.SaveChangesAsync();

            return sellOrder.TosellOrderResponse();
        }

        public async Task<List<BuyOrderResponse>> GetAllBuyOrders()
        {
            return await _db.BuyOrders.OrderByDescending(x=>x.DateAndTimeOfOrder)
                                 .Select(y=>y.ToBuyOrderResponse())
                                 .ToListAsync();
        }

        public async Task<List<SellOrderResponse>> GetAllSellOrders()
        {
            return await _db.SellOrders.OrderByDescending(x => x.DateAndTimeOfOrder)
                                 .Select(y => y.TosellOrderResponse())
                                 .ToListAsync();
        }
    }

    
        

}
