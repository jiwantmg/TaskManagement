using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Persistence.MySQL
{
    public static class MySQLServiceCollectionExtension
    {
        public static IServiceCollection AddMySqlDbContext(
            this IServiceCollection serviceCollection,
            IConfiguration config = null)
        {
            serviceCollection.AddDbContext<TaskManagementDbContext, MySQLTaskManagementDbContext>(option =>
            {
                option.UseMySql(
                    config.GetConnectionString("TaskManagementDbContext"),
                    b => b.MigrationsAssembly("TaskManagement.Persistence.MySQL")
                );
            });

            return serviceCollection;
        }
    }
}