using PetShoes.Stock.Api.Core.Application.AppStock.ViewModel;

namespace PetShoes.Stock.Api.Core.Application.AppStock.Mapping
{
    public static class StockMapping
    {
        public static StockViewModel ToViewModel(this Domain.Entities.Stock stock)
        {
            return new StockViewModel
            {
                Id = stock.Id,
                ProductId = stock.ProductId,
                Color = stock.Color,
                Size = stock.Size,
                Quantity = stock.Quantity,
                Price = stock.Price
            };
        }
    }
}
