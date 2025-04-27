using PetShoes.Stock.Api.Core.Application.AppStock;
using PetShoes.Stock.Api.Core.Application.AppStock.Interface;

namespace PetShoes.Stock.Api.Core.Infrastructure.IoC.Application
{
    internal class ApplicationBootstrapper
    {
        internal void ChildServiceRegister(IServiceCollection service)
        {
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            service.AddScoped<IStockAppService, StockAppService>();
        }
    }
}
