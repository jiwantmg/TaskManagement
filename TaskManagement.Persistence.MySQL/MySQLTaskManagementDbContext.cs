using System;
using Finbuckle.MultiTenant;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TaskManagement.Persistence.MySQL
{
    public class MySQLTaskManagementDbContext: TaskManagementDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MySQLTaskManagementDbContext(
            DbContextOptions options,
            IHttpContextAccessor httpContextAccessor
        )
            :base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Console.WriteLine(_httpContextAccessor.HttpContext.GetMultiTenantContext()?.TenantInfo.ConnectionString);
            
            optionsBuilder.UseMySql(
                _httpContextAccessor.HttpContext.GetMultiTenantContext()?.TenantInfo.ConnectionString,
                //"Server=localhost;Database=taskmanagement_abc;User=sha256user;Password=System@123321",
                b => b.MigrationsAssembly("TaskManagement.Persistence.MySQL")
            );
            base.OnConfiguring(optionsBuilder);
        }
    }
}