
using Microsoft.AspNetCore.Mvc;
using PetShoes.Stock.Api.Core.Application.AppStock.Input;
using PetShoes.Stock.Api.Core.Application.AppStock.Interface;
using PetShoes.Stock.Api.Core.Application.AppStock.Mapping;
using PetShoes.Stock.Api.Core.Application.AppStock.ViewModel;
using PetShoes.Stock.Api.Core.Domain.Interfaces;


namespace PetShoes.Stock.Api.Core.Application.AppStock
{
    public class StockAppService : IStockAppService
    {
        private readonly IStockRepository _stockRepository;

        public StockAppService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public async Task<StockViewModel> InsertAsync(StockInput shoeInput)
        {
            var stock = new Domain.Entities.Stock(shoeInput.ProductId,
                                   shoeInput.Color,
                                   shoeInput.Size,
                                   shoeInput.Quantity,
                                   shoeInput.Price);

            await _stockRepository
                        .InsertAsync(stock)
                        .ConfigureAwait(false);

            var stockViewModel = stock.ToViewModel();

            return stockViewModel;
        }
        public async Task<StockViewModel> GetStockByIdAsync(Guid itemStockId)
        {
            var stockItem = await _stockRepository
                                    .GetStockByIdAsync(itemStockId)
                                    .ConfigureAwait(false);
            if (stockItem == null)
                throw new Exception("Produto não encontrado");

            return stockItem.ToViewModel();
        }
        public async Task<StockViewModel> UpdateAsync(Guid itemStockId, StockInput stockInput)
        {
            var itemStock = await _stockRepository
                                        .GetStockByIdAsync(itemStockId)
                                        .ConfigureAwait(false);

            if (itemStock == null)
                throw new Exception("Produto não encontrado");


            itemStock.Update(stockInput.Color,
                             stockInput.Size,
                             stockInput.Quantity,
                             stockInput.Price);

            await _stockRepository
                        .UpdateAsync(itemStock)
                        .ConfigureAwait(false);

            return itemStock.ToViewModel();
        }
        public async Task DeleteAsync(Guid itemStockId)
        {
            var shoe = await _stockRepository
                                .GetStockByIdAsync(itemStockId)
                                .ConfigureAwait(false);
            if (shoe == null)
                throw new Exception("Produto não encontrado");

            await _stockRepository
                        .DeleteAsync(itemStockId)
                        .ConfigureAwait(false);

        }

    }
}
