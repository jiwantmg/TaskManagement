using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Persistence.MySQL
{
    public static class MySQLServiceCollectionExtension
    {
        public static IServiceCollection AddMySqlDbContext(
            this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<TaskManagementDbContext, MySQLTaskManagementDbContext>(option => {});

            return serviceCollection;
        }
    }
}