using PetShoes.Stock.Api.Core.Application.AppStock.Input;
using PetShoes.Stock.Api.Core.Application.AppStock.ViewModel;

namespace PetShoes.Stock.Api.Core.Application.AppStock.Interface
{
    public interface IStockAppService
    {
        Task<StockViewModel> InsertAsync(StockInput shoeInput);
        Task<StockViewModel> GetStockByIdAsync(Guid id);
        Task<StockViewModel> UpdateAsync(Guid id, StockInput stockInput);
        Task DeleteAsync(Guid id);
    }
}
