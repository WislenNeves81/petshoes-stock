using Marraia.MongoDb.Repositories;
using Marraia.MongoDb.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PetShoes.Stock.Api.Core.Domain.Interfaces;

namespace PetShoes.Stock.Api.Core.Repository
{
    public class StockRespository : MongoDbRepositoryStandard<Domain.Entities.Stock, Guid>, IStockRepository
    {
        public StockRespository(IMongoContext context) : base(context) { }
        public async Task InsertAsync(Domain.Entities.Stock stock)
        {
            await Collection
                  .InsertOneAsync(stock)
                  .ConfigureAwait(false);
        }
        public async Task<Domain.Entities.Stock> GetStockByIdAsync(Guid itemStockId)
        {
            return await Collection
                            .AsQueryable()
                            .Where(item => item.Id == itemStockId)
                            .FirstOrDefaultAsync();
        }

        public async Task<Domain.Entities.Stock> UpdateAsync(Domain.Entities.Stock itemStockId)
        {
            await Collection
                   .ReplaceOneAsync(c => c.Id == itemStockId.Id, itemStockId)
                   .ConfigureAwait(false);

            return itemStockId;
        }

        public async Task DeleteAsync(Guid itemStockId)
        {
            await Collection
                    .UpdateOneAsync(c => c.Id == itemStockId,
                                    Builders<Domain.Entities.Stock>.Update.Set(itemStockId => itemStockId.Active, false))
                    .ConfigureAwait(false);
        }
    }
}
