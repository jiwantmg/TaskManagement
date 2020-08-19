using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TaskManagement.Persistence.MySQL
{
    public class MySQLContextFactory: IDesignTimeDbContextFactory<MySQLTaskManagementDbContext>
    {
        public MySQLTaskManagementDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile("appsettings.local.json", true)
                .Build();
            
            var builder = new DbContextOptionsBuilder<TaskManagementDbContext>();

            builder.UseMySql(
                config.GetConnectionString(nameof(TaskManagementDbContext)),
                b => b.MigrationsAssembly("TaskManagement.Persistence.MySQL")
            );
            
            return new MySQLTaskManagementDbContext(builder.Options);
        }
    }
}