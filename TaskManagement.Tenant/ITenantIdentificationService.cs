using Microsoft.AspNetCore.Http;

namespace TaskManagement.Tenant
{
    public interface ITenantIdentificationService
    {
        string GetCurrentTenant(HttpContext httpContext);
    }
}