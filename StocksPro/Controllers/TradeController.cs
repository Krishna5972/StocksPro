using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Models;

namespace StocksPro.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly IConfiguration _configuration;

        public TradeController(IFinnhubService finnhubService,IConfiguration configuration)
        {
            _finnhubService =  finnhubService;
            _configuration = configuration;
        }




        [Route("/")]
        [Route("/Trade")]
        public async Task<IActionResult> Index()
        {
            string symbol = _configuration.GetSection("TradingOptions")["DefaultStockSymbol"];
            Dictionary<string, object> quoteData = await _finnhubService.GetStockPriceQuote(symbol);
            Dictionary<string,object> profileData = await _finnhubService.GetCompanyProfile(symbol);

            StockTrade stockTrade = new StockTrade()
            {
                StockName= profileData["name"].ToString(),
                StockSymbol = symbol,
                Price = Convert.ToDouble(quoteData["c"].ToString())
            };

            ViewBag.FinnhubToken = _configuration["FinHubApiKey"];
            return View(stockTrade);
        }
    }
}
