namespace PetShoes.Stock.Api.Core.Domain.Interfaces
{
    public interface IStockRepository
    {
        Task InsertAsync(Entities.Stock stock);
        Task<Entities.Stock> GetStockByIdAsync(Guid itemStockId);
        Task<Entities.Stock> UpdateAsync(Entities.Stock itemStockId);
        Task DeleteAsync(Guid itemStockId);

    }
}
