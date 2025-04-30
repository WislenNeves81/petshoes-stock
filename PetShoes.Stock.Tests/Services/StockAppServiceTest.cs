using Bogus;
using FluentAssertions;
using MyProfit.Foundation.Redis.Repositories.Interfaces;
using NSubstitute;
using PetShoes.Stock.Api.Core.Application.AppStock;
using PetShoes.Stock.Api.Core.Application.AppStock.Input;
using PetShoes.Stock.Api.Core.Application.AppStock.ViewModel;
using PetShoes.Stock.Api.Core.Domain.Interfaces;
using PetShoes.Stock.Tests.Services.Generate;

namespace PetShoes.Stock.Tests.Services
{
    public class StockAppServiceTest
    {
        private StockAppService _stockAppService;
        private IStockRepository _stockRepository;
        private Faker _faker;

        private const int defaultReceived = 1;

        public StockAppServiceTest()
        {
            _stockRepository = Substitute.For<IStockRepository>();
            var cacheRepositoryMock = Substitute.For<ICacheRepository>();
            _stockAppService = new StockAppService(_stockRepository, cacheRepositoryMock);

            _faker = new Faker();
        }
        [Fact]
        public async Task GetStockIdAsync_When_Returns_Value()
        {
            // Arrange
            var stockId = _faker.Random.Guid();
            var stock = GenerateFakerShoe.CreateShoeObject(stockId);
            _stockRepository.GetStockByIdAsync(Arg.Any<Guid>()).Returns(stock);

            // Act
            var result = await _stockAppService.GetStockByIdAsync(stockId);

            // Assert
            result.Should().BeOfType<StockViewModel>();

            await _stockRepository
                    .Received(defaultReceived)
                    .GetStockByIdAsync(Arg.Any<Guid>());
        }
        [Fact]
        public async Task GetStockIdAsync_When_Do_Not_Returns_Value()
        {
            //Arrange
            var stockId = _faker.Random.Guid();
            _stockRepository.GetStockByIdAsync(Arg.Any<Guid>())!.Returns(default(Api.Core.Domain.Entities.Stock));

            //Act
            var result = await _stockAppService.GetStockByIdAsync(stockId);

            //Assert
            result.Should().BeNull();

        }
        [Fact]
        public async Task InsertStockAsync_When_Product_Exists()
        {
            // Arrange  
            var stockId = _faker.Random.Guid();
            var stock = GenerateFakerShoe.CreateShoeObject(stockId);

            var stockInput = new StockInput()
            {
                ProductId = stock.ProductId,
                Color = stock.Color,
                Size = stock.Size,
                Quantity = stock.Quantity,
                Price = stock.Price

            };

            _stockRepository.GetStockByProductIdAsync(Arg.Any<Guid>()).Returns(stock);

            // Act  
            var result = await _stockAppService.InsertAsync(stockInput);

            // Assert  
            result.Should().BeNull();

            await _stockRepository
                    .Received(defaultReceived)
                    .GetStockByProductIdAsync(Arg.Any<Guid>());

        }
    }
}
