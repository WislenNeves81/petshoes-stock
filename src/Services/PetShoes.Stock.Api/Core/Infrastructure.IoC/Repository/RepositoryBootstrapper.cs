using Marraia.MongoDb.Configurations;
using MyProfit.Foundation.Redis.Configurations;
using PetShoes.Stock.Api.Core.Domain.Interfaces;
using PetShoes.Stock.Api.Core.Repository;

namespace PetShoes.Stock.Api.Core.Infrastructure.IoC.Repository
{
    internal class RepositoryBootstrapper
    {
        internal void ChildServiceRegister(IServiceCollection service, IConfiguration configuration)
        {
            service.AddMongoDb();
            service.AddRedis(configuration);
            service.AddScoped<IStockRepository, StockRespository>();
        }
    }
}
