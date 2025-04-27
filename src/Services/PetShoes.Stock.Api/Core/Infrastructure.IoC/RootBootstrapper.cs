using PetShoes.Stock.Api.Core.Infrastructure.IoC.Application;
using PetShoes.Stock.Api.Core.Infrastructure.IoC.Repository;

namespace PetShoes.Stock.Api.Core.Infrastructure.IoC
{
    public class RootBootstrapper
    {
        public void BootstrapperRegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            new RepositoryBootstrapper().ChildServiceRegister(services, configuration);
            new ApplicationBootstrapper().ChildServiceRegister(services);
        }
    }
}
