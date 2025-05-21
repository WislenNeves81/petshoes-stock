using MyProfit.Foundation.Redis.Repositories.Interfaces;
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
        private readonly ICacheRepository _cacheRepository;

        public StockAppService(IStockRepository stockRepository, 
                                ICacheRepository cacheRepository)
        {
            _stockRepository = stockRepository;
            _cacheRepository = cacheRepository;
        }
        public async Task<StockViewModel> InsertAsync(StockInput shoeInput)
        {
            var stock = new Domain.Entities.Stock(shoeInput.ProductId,
                                                   shoeInput.Size,
                                                   shoeInput.Quantity);

            var stockExist =  await _stockRepository
                                        .GetStockByProductIdAsync(stock.ProductId)
                                        .ConfigureAwait(false);

            if (stockExist is not null)
                return default!;

            //TODO :: IMPLEMENTAR A PRÁTICA ANTI-CORRUPÇÃO

            await _stockRepository
                        .InsertAsync(stock)
                        .ConfigureAwait(false);

            var stockViewModel = stock.ToViewModel();

            var keyShoeCatalog = $"stock:productId:{stock.ProductId}:stockId:{stock.Id}";

            await _cacheRepository
                     .InsertAsync<StockViewModel>(keyShoeCatalog, stockViewModel)
                     .ConfigureAwait(false);

            //TODO :: IMPLEMENTAR A PRÁTICA ANTI-CORRUPÇÃO

            return stockViewModel;
        }
        public async Task<StockViewModel> GetStockByIdAsync(Guid itemStockId)
        {
            var stockItem = await _stockRepository
                                    .GetStockByIdAsync(itemStockId)
                                    .ConfigureAwait(false);
            if (stockItem is null)
                return default!;

            return stockItem.ToViewModel();
        }
        public async Task<StockViewModel> UpdateAsync(Guid itemStockId, StockInput stockInput)
        {
            var itemStock = await _stockRepository
                                        .GetStockByIdAsync(itemStockId)
                                        .ConfigureAwait(false);

            if (itemStock == null)
                throw new Exception("Produto não encontrado");


            itemStock.Update(stockInput.Size,
                             stockInput.Quantity);

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
