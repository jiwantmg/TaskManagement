using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace TaskManagement.Tenant
{
    public sealed class HostTenantIdentificationService : ITenantIdentificationService
    {
        private readonly TenantMapping _tenants;

        public HostTenantIdentificationService(IConfiguration configuration)
        {
            _tenants = configuration.GetTenantMapping();
        }

        public HostTenantIdentificationService(TenantMapping tenants)
        {
            _tenants = tenants;
        }

        public IEnumerable<string> GetAllTenants()
        {
            return _tenants.Tenants.Values;
        }

        public string GetCurrentTenant(HttpContext context)
        {
            if (!_tenants.Tenants.TryGetValue(context.Request.Host.Host, out var tenant))
            {
                tenant = _tenants.Default;
            }

            return tenant;
        }
    }
}