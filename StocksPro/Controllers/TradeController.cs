using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Models;
using ServiceContracts.DTO;
using Rotativa.AspNetCore;

namespace StocksPro.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stocksService;
        private readonly IConfiguration _configuration;

        public TradeController(IFinnhubService finnhubService, IStocksService stocksService, IConfiguration configuration)
        {
            _finnhubService = finnhubService;
            _stocksService = stocksService;
            _configuration = configuration;
        }


        [Route("/")]
        [Route("/Trade")]
        [Route("/Trade/Index")]
        public async Task<IActionResult> Index()
        {
            string symbol = _configuration.GetSection("TradingOptions")["DefaultStockSymbol"];
            Dictionary<string, object> quoteData = await _finnhubService.GetStockPriceQuote(symbol);
            Dictionary<string, object> profileData = await _finnhubService.GetCompanyProfile(symbol);

            StockTrade stockTrade = new StockTrade()
            {
                StockName = profileData["name"].ToString(),
                StockSymbol = symbol,
                Price = Convert.ToDouble(quoteData["c"].ToString())
            };

            ViewBag.FinnhubToken = _configuration["FinHubApiKey"];
            return View(stockTrade);
        }

        [Route("/Trade/Buy")]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            ModelState.Clear();
            TryValidateModel(buyOrderRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade()
                {
                    StockName = buyOrderRequest.StockName,
                    Quantity = buyOrderRequest.Quantity,
                    StockSymbol = buyOrderRequest.StockSymbol
                };

                return View("Index.cshtml", stockTrade);

            }
            var buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);


            return RedirectToAction("Orders");
        }

        [Route("/Trade/Sell")]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
        {
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            ModelState.Clear();
            TryValidateModel(sellOrderRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade()
                {
                    StockName = sellOrderRequest.StockName,
                    Quantity = sellOrderRequest.Quantity,
                    StockSymbol = sellOrderRequest.StockSymbol
                };

                return View("Index.cshtml", stockTrade);

            }
            var sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);

            return RedirectToAction("Orders");
        }

        [Route("/Trade/Orders")]
        public async Task<IActionResult> Orders()
        {

            List<BuyOrderResponse> buyOrderResponsesList = await _stocksService.GetAllBuyOrders();
            List<SellOrderResponse> sellOrderResponsesList = await _stocksService.GetAllSellOrders();

            Orders orders = new Orders()
            {
                BuyOrders = buyOrderResponsesList,
                SellOrders = sellOrderResponsesList
            };

            return View(orders);
        }

        [Route("/Trade/OrdersPDF")]
        public async Task<IActionResult> OrdersPDF()
        {

            List<IOrderResponse> orders = new List<IOrderResponse>();

            orders.AddRange(await _stocksService.GetAllBuyOrders());
            orders.AddRange(await _stocksService.GetAllSellOrders());

            orders = orders.OrderByDescending(x => x.DateAndTimeOfOrder).ToList();

            return new ViewAsPdf("OrdersPDF", orders, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Right = 20, Bottom = 20, Left = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };

        }
    }
}
