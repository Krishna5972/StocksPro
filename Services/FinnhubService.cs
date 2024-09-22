using Microsoft.Extensions.Configuration;
using ServiceContracts;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Services
{
    public class FinnhubService : IFinnhubService
    {

        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;


        public FinnhubService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            string URI = $"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinHubApiKey"]}";

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, URI);
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                Stream stream = httpResponseMessage.Content.ReadAsStream();
                StreamReader streamReader = new StreamReader(stream);
                string response = streamReader.ReadToEnd();

                Dictionary<string, object>? responseDict = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if (responseDict is null)
                    throw new InvalidOperationException("API returned empty values");
                if (responseDict.ContainsKey("error"))
                    throw new InvalidOperationException("API call returned error");

                return responseDict;

            }
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            string URI = $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinHubApiKey"]}";

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, URI);
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                Stream stream = httpResponseMessage.Content.ReadAsStream();
                StreamReader streamReader = new StreamReader(stream);
                string response = streamReader.ReadToEnd();

                Dictionary<string, object>? responseDict = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if (responseDict is null)
                    throw new InvalidOperationException("API returned empty values");
                if (responseDict.ContainsKey("error"))
                    throw new InvalidOperationException("API call returned error");

                return responseDict;

            }
        }
    }
}
