using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Persistence.MySQL;

namespace TaskManagement.Api
{
    public static class PersistenceServiceCollectionExtensions
    {
        public static IServiceCollection AddConfiguredDbContext(
            this IServiceCollection serviceCollection,
            IConfiguration config = null
        )
        {
            var persistenceConfig = config?.GetSection("Persistence")?.Get<PersistenceConfiguration>();
            if (persistenceConfig?.Provider == "MySQL")
            {
                serviceCollection.AddMySqlDbContext();
            }

            return serviceCollection;
        }
    }

    public class PersistenceConfiguration
    {
        public string Provider { get; set; }
    }
}