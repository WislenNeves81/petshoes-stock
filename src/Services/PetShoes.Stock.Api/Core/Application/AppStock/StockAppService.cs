using Marraia.Notifications.Interfaces;
using MyProfit.Foundation.Redis.Repositories.Interfaces;
using PetShoes.Stock.Api.Core.Application.AppStock.Input;
using PetShoes.Stock.Api.Core.Application.AppStock.Interface;
using PetShoes.Stock.Api.Core.Application.AppStock.Mapping;
using PetShoes.Stock.Api.Core.Application.AppStock.ViewModel;
using PetShoes.Stock.Api.Core.Domain.Entities.ValueObjects;
using PetShoes.Stock.Api.Core.Domain.Interfaces;


namespace PetShoes.Stock.Api.Core.Application.AppStock
{
    public class StockAppService : IStockAppService
    {
        private readonly IStockRepository _stockRepository;
        private readonly ICacheRepository _cacheRepository;
        private readonly ISmartNotification _smartNotification;

        public StockAppService(IStockRepository stockRepository, 
                                ICacheRepository cacheRepository,
                                ISmartNotification smartNotification)
        {
            _stockRepository = stockRepository;
            _cacheRepository = cacheRepository;
            _smartNotification = smartNotification;
        }
        public async Task<StockViewModel> InsertAsync(StockInput shoeInput)
        {
            var stock = new Domain.Entities.Stock(shoeInput.ProductId,
                                                   shoeInput.Size,
                                                   shoeInput.Quantity);

            var stockExist = await _stockRepository
                                        .GetStockByProductIdAsync(stock.ProductId)
                                        .ConfigureAwait(false);

            if (stockExist is not null)
                return default!;

            if (!ValidateStockInput(shoeInput))
            {
                _smartNotification.NewNotificationConflict("Invalid stock input data.");
                return default!;
            }

            await _stockRepository
                        .InsertAsync(stock)
                        .ConfigureAwait(false);

            var stockViewModel = stock.ToViewModel();

            var keyShoeCatalog = $"stock:productId:{stock.ProductId}:stockId:{stock.Id}";

            await _cacheRepository
                     .InsertAsync<StockViewModel>(keyShoeCatalog, stockViewModel)
                     .ConfigureAwait(false);

            if (!ValidateStockViewModel(stockViewModel))
            {
                _smartNotification.NewNotificationConflict("Invalid stock view model data.");
                return default!;
            }

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
            {
                _smartNotification.NewNotificationConflict($"O item não foi encontrado no estoque.");
                return default!;
            }

            itemStock.Update(stockInput.Quantity);

            await _stockRepository
                       .UpdateAsync(itemStock)
                       .ConfigureAwait(false);
            
            var keyStock = $"stock:productId:{itemStock.ProductId}:stockId:{itemStock.Id}";

            var stockItem = await GetStockByCacheAsync(keyStock).ConfigureAwait(false);

            if (StockValidation(stockItem, itemStock.Quantity))
            {
                stockItem.UpdateQuantity(stockInput.Quantity);

                var keyShoeCatalog = $"stock:productId:{stockItem.ProductId}:stockId:{itemStock.Id}";

                await _cacheRepository
                         .InsertAsync(keyShoeCatalog, stockItem)
                         .ConfigureAwait(false);
            }

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

        #region 
        public async Task<StockValueObject> GetStockByCacheAsync(string keyStock)
        {
            var currentStock = await _cacheRepository
                                        .GetByKeyAsync<StockValueObject>(keyStock)
                                        .ConfigureAwait(false);

            return currentStock;
        }
        public bool StockValidation(StockValueObject stockItem, int quantity)
        {
            if (stockItem == null)
                _smartNotification.NewNotificationConflict($"O item não foi encontrado no estoque.");
            if (stockItem!.Quantity < quantity)
                _smartNotification.NewNotificationConflict($"O item {stockItem.ProductId} não possui estoque suficiente. Estoque atual: {stockItem.Quantity} - Quantidade solicitada: {quantity}");
            return true;
        }
        private bool ValidateStockInput(StockInput input)
        {
            return input != null && input.ProductId != Guid.Empty && input.Quantity > 0 && input.Size > 0;
        }

        private bool ValidateStockViewModel(StockViewModel viewModel)
        {
            return viewModel != null && viewModel.Id != Guid.Empty && viewModel.ProductId != Guid.Empty;
        }
        #endregion

    }
}
