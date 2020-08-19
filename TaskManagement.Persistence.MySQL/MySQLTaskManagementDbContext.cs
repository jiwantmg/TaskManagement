using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TaskManagement.Persistence.MySQL
{
    public class MySQLTaskManagementDbContext: TaskManagementDbContext
    {
        public MySQLTaskManagementDbContext(
            DbContextOptions options
        )
            :base(options)
        {
            
        }
    }
}