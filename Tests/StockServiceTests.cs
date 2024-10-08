using ServiceContracts.DTO;
using ServiceContracts;
using Services;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class StockServiceTests
    {
        private readonly IStocksService _stocksService;
        public StockServiceTests()
        {
            _stocksService = new StocksService(new StocksProDbContext(new DbContextOptionsBuilder<StocksProDbContext>().Options)); ;
        }
        #region CreateBuyOrder
        [Fact]
        public async Task CreateBuyOrder_RequestIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _stocksService.CreateBuyOrder(null);
            });

        }

        /// <summary>
        /// Test Case 2:
        /// When Quantity is set to 0, it should throw an ArgumentException.
        /// </summary>
        [Fact]
        public async Task CreateBuyOrder_QuantityIsZero_ShouldThrowArgumentException()
        {
            // Arrange
            var buyOrderRequest = new BuyOrderRequest
            {
                Quantity = 0, // Invalid quantity
                Price = 100,  // Valid price
                StockSymbol = "AAPL", // Valid stock symbol
                DateAndTimeOfOrder = DateTime.UtcNow // Valid date
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () 
                => await _stocksService.CreateBuyOrder(buyOrderRequest));
        }

        /// <summary>
        /// Test Case 3:
        /// When Quantity is set to 100001, it should throw an ArgumentException.
        /// </summary>
        [Fact]
        public async Task CreateBuyOrder_QuantityExceedsMaximum_ShouldThrowArgumentException()
        {
            // Arrange
            var buyOrderRequest = new BuyOrderRequest
            {
                Quantity = 100001, // Invalid quantity
                Price = 100,       // Valid price
                StockSymbol = "GOOGL",     // Valid stock symbol
                DateAndTimeOfOrder = DateTime.UtcNow // Valid date
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () 
                => await _stocksService.CreateBuyOrder(buyOrderRequest));
        }

        /// <summary>
        /// Test Case 4:
        /// When Price is set to 0, it should throw an ArgumentException.
        /// </summary>
        [Fact]
        public async Task CreateBuyOrder_PriceIsZero_ShouldThrowArgumentException()
        {
            // Arrange
            var buyOrderRequest = new BuyOrderRequest
            {
                Quantity = 10,     // Valid quantity
                Price = 0,         // Invalid price
                StockSymbol = "MSFT",      // Valid stock symbol
                DateAndTimeOfOrder = DateTime.UtcNow // Valid date
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () 
                => await _stocksService.CreateBuyOrder(buyOrderRequest));
        }


        /// <summary>
        /// Test Case 5:
        /// When Price is set to 10001, it should throw an ArgumentException.
        /// </summary>
        [Fact]
        public async Task CreateBuyOrder_PriceExceedsMaximum_ShouldThrowArgumentException()
        {
            // Arrange
            var buyOrderRequest = new BuyOrderRequest
            {
                Quantity = 50,       // Valid quantity
                Price = 10001,       // Invalid price
                StockSymbol = "TSLA",         // Valid stock symbol
                DateAndTimeOfOrder = DateTime.UtcNow // Valid date
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _stocksService.CreateBuyOrder(buyOrderRequest));
        }

        /// <summary>
        /// Test Case 6:
        /// When stockSymbol is set to null, it should throw an ArgumentException.
        /// </summary>
        [Fact]
        public async Task CreateBuyOrder_StockSymbolIsNull_ShouldThrowArgumentException()
        {
            // Arrange
            var buyOrderRequest = new BuyOrderRequest
            {
                Quantity = 10,               // Valid quantity
                Price = 100,                 // Valid price
                StockSymbol = null,                  // Invalid stock symbol
                DateAndTimeOfOrder = DateTime.UtcNow // Valid date
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async ()
                => await _stocksService.CreateBuyOrder(buyOrderRequest));
        }

        /// <summary>
        /// Test Case 7:
        /// When dateAndTimeOfOrder is set to "1999-12-31", it should throw an ArgumentException.
        /// </summary>
        [Fact]
        public async Task CreateBuyOrder_DateAndTimeOfOrderIsBefore2000_ShouldThrowArgumentException()
        {
            // Arrange
            var buyOrderRequest = new BuyOrderRequest
            {
                Quantity = 10,                          // Valid quantity
                Price = 100,                            // Valid price
                StockSymbol = "NFLX",                           // Valid stock symbol
                DateAndTimeOfOrder = new DateTime(1999, 12, 31) // Invalid date
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () 
                => await _stocksService.CreateBuyOrder(buyOrderRequest));
        }

        /// <summary>
        /// Test Case 8:
        /// When all valid values are supplied, it should return a BuyOrderResponse with a valid BuyOrderID.
        /// </summary>
        [Fact]
        public async Task CreateBuyOrder_AllValidValues_ShouldReturnBuyOrderResponseWithValidBuyOrderID()
        {
            // Arrange
            var buyOrderRequest = new BuyOrderRequest
            {
                Quantity = 100,                      // Valid quantity
                Price = 250,                          // Valid price
                StockSymbol = "AMZN",
                StockName = "AMAZON CORP",// Valid stock symbol
                DateAndTimeOfOrder = new DateTime(2023, 1, 1) // Valid date
            };

            // Act
            var buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);

            // Assert
            Assert.NotNull(buyOrderResponse);
            Assert.IsType<BuyOrderResponse>(buyOrderResponse);
            Assert.NotEqual(Guid.Empty, buyOrderResponse.BuyOrderID); // Ensure BuyOrderID is a valid GUID
        }

        #endregion CreateBuyOrder

        #region CreateSellOrder

        /// <summary>
        /// Test Case 1:
        /// When SellOrderRequest is null, it should throw an ArgumentNullException.
        /// </summary>
        [Fact]
        public async Task CreateSellOrder_RequestIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            SellOrderRequest nullRequest = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _stocksService.CreateSellOrder(nullRequest);
            });
        }

        /// <summary>
        /// Test Case 2:
        /// When SellOrderQuantity is set to 0, it should throw an ArgumentException.
        /// </summary>
        [Fact]
        public async Task CreateSellOrder_QuantityIsZero_ShouldThrowArgumentException()
        {
            // Arrange
            var sellOrderRequest = new SellOrderRequest
            {
                Quantity = 0, // Invalid quantity
                Price = 100,  // Valid price
                StockSymbol = "AAPL", // Valid stock symbol
                DateAndTimeOfOrder = DateTime.UtcNow // Valid date
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        /// <summary>
        /// Test Case 3:
        /// When SellOrderQuantity exceeds the maximum limit (100,000), it should throw an ArgumentException.
        /// </summary>
        [Fact]
        public async Task CreateSellOrder_QuantityExceedsMaximum_ShouldThrowArgumentException()
        {
            // Arrange
            var sellOrderRequest = new SellOrderRequest
            {
                Quantity = 100001, // Invalid quantity
                Price = 100,       // Valid price
                StockSymbol = "GOOGL", // Valid stock symbol
                DateAndTimeOfOrder = DateTime.UtcNow // Valid date
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        /// <summary>
        /// Test Case 4:
        /// When SellOrderPrice is set to 0, it should throw an ArgumentException.
        /// </summary>
        [Fact]
        public async Task CreateSellOrder_PriceIsZero_ShouldThrowArgumentException()
        {
            // Arrange
            var sellOrderRequest = new SellOrderRequest
            {
                Quantity = 10, // Valid quantity
                Price = 0,     // Invalid price
                StockSymbol = "MSFT", // Valid stock symbol
                DateAndTimeOfOrder = DateTime.UtcNow // Valid date
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        /// <summary>
        /// Test Case 5:
        /// When SellOrderPrice exceeds the maximum limit (10,000), it should throw an ArgumentException.
        /// </summary>
        [Fact]
        public async Task CreateSellOrder_PriceExceedsMaximum_ShouldThrowArgumentException()
        {
            // Arrange
            var sellOrderRequest = new SellOrderRequest
            {
                Quantity = 10,    // Valid quantity
                Price = 10001,    // Invalid price
                StockSymbol = "TSLA", // Valid stock symbol
                DateAndTimeOfOrder = DateTime.UtcNow // Valid date
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>( async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        /// <summary>
        /// Test Case 6:
        /// When StockSymbol is null, it should throw an ArgumentException.
        /// </summary>
        [Fact]
        public async Task CreateSellOrder_StockSymbolIsNull_ShouldThrowArgumentException()
        {
            // Arrange
            var sellOrderRequest = new SellOrderRequest
            {
                Quantity = 10,    // Valid quantity
                Price = 100,      // Valid price
                StockSymbol = null, // Invalid stock symbol
                DateAndTimeOfOrder = DateTime.UtcNow // Valid date
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        /// <summary>
        /// Test Case 7:
        /// When DateAndTimeOfOrder is before 2000-01-01, it should throw an ArgumentException.
        /// </summary>
        [Fact]
        public async Task CreateSellOrder_DateIsBeforeMinimum_ShouldThrowArgumentException()
        {
            // Arrange
            var sellOrderRequest = new SellOrderRequest
            {
                Quantity = 10,    // Valid quantity
                Price = 100,      // Valid price
                StockSymbol = "FB", // Valid stock symbol
                DateAndTimeOfOrder = new DateTime(1999, 12, 31) // Invalid date
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        /// <summary>
        /// Test Case 8:
        /// When all values are valid, it should successfully create a sell order and return a SellOrderResponse with a valid SellOrderID.
        /// </summary>
        [Fact]
        public async Task CreateSellOrder_AllValidInputs_ShouldReturnSellOrderResponse()
        {
            // Arrange
            var sellOrderRequest = new SellOrderRequest
            {
                Quantity = 100,              // Valid quantity
                Price = 250,                 // Valid price
                StockSymbol = "AMZN",       // Valid stock symbol
                StockName = "AMAZON CORP",  // Valid stock Name
                DateAndTimeOfOrder = DateTime.UtcNow // Valid date
            };

            // Act
            var response = await _stocksService.CreateSellOrder(sellOrderRequest);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<SellOrderResponse>(response);
            Assert.IsType<Guid>(response.SellOrderID);
            Assert.NotEqual(Guid.Empty, response.SellOrderID);
        }

        #endregion CreateSellOrder

        #region GetAllBuyOrders

        [Fact]
        public async Task GetAllBuyOrders_InitialCall()
        {
            // Act
            var buyOrderResponseList = await _stocksService.GetAllBuyOrders();

            int expected = 0;
            // Assert
            Assert.Equal(buyOrderResponseList.Count, expected);
        }
        [Fact]
        public async Task GetAllBuyOrders_ShouldHaveAllBuyOrders()
        {
            // Arrange
            var buyOrderRequest1 = new BuyOrderRequest
            {
                Quantity = 100,                      // Valid quantity
                Price = 250,                          // Valid price
                StockSymbol = "AMZN",
                StockName = "AMAZON CORP",// Valid stock symbol
                DateAndTimeOfOrder = new DateTime(2023, 1, 1) // Valid date
            };

            var buyOrderRequest2 = new BuyOrderRequest
            {
                Quantity = 1001,                      // Valid quantity
                Price = 2503,                          // Valid price
                StockSymbol = "MSFT",
                StockName = "Microsoft",// Valid stock symbol
                DateAndTimeOfOrder = new DateTime(2023, 2, 1) // Valid date
            };



            var buyOrderResponse1 = await _stocksService.CreateBuyOrder(buyOrderRequest1);
            var buyOrderResponse2 = await _stocksService.CreateBuyOrder(buyOrderRequest2);

            List<BuyOrderResponse> expectedList = new List<BuyOrderResponse>();
            expectedList.Add(buyOrderResponse1);
            expectedList.Add(buyOrderResponse2);


            // Act
            var buyOrderResponseList = await _stocksService.GetAllBuyOrders();

            // Assert
            foreach (BuyOrderResponse buyOrderResponse in expectedList)
            {
                Assert.Contains(buyOrderResponse, buyOrderResponseList);
            }
        }

        #endregion GetAllBuyOrders

        #region GetAllSellOrders

        [Fact]
        public async Task GetAllSellOrders_InitialCall()
        {
            // Act
            var sellOrderResponseList = await _stocksService.GetAllSellOrders();

            int expected = 0;
            // Assert
            Assert.Equal(sellOrderResponseList.Count, expected);
        }
        [Fact]
        public async Task GetAllSellOrders_ShouldHaveAllSellOrders()
        {
            // Arrange
            var sellOrderRequest1 = new SellOrderRequest
            {
                Quantity = 100,                      // Valid quantity
                Price = 250,                          // Valid price
                StockSymbol = "AMZN",
                StockName = "AMAZON CORP",// Valid stock symbol
                DateAndTimeOfOrder = new DateTime(2023, 1, 1) // Valid date
            };

            var sellOrderRequest2 = new SellOrderRequest
            {
                Quantity = 1001,                      // Valid quantity
                Price = 2503,                          // Valid price
                StockSymbol = "MSFT",
                StockName = "Microsoft",// Valid stock symbol
                DateAndTimeOfOrder = new DateTime(2023, 2, 1) // Valid date
            };



            var sellOrderResponse1 = await _stocksService.CreateSellOrder(sellOrderRequest1);
            var sellOrderResponse2 = await _stocksService.CreateSellOrder(sellOrderRequest2);

            List<SellOrderResponse> expectedList = new List<SellOrderResponse>();
            expectedList.Add(sellOrderResponse1);
            expectedList.Add(sellOrderResponse2);


            // Act
            var sellOrderResponseList = await _stocksService.GetAllSellOrders();

            // Assert
            foreach (SellOrderResponse sellOrderResponse in expectedList)
            {
                Assert.Contains(sellOrderResponse, sellOrderResponseList);
            }
        }

        #endregion GetAllSellOrders

    }
}






