using Microsoft.AspNetCore.Http;

namespace TaskManagement.Tenant
{
    public class TenantService: ITenantService
    {
        private readonly HttpContext _httpContext;
        private readonly ITenantIdentificationService _service;

        public TenantService(IHttpContextAccessor accessor, ITenantIdentificationService service)
        {
            _httpContext = accessor.HttpContext;
            _service = service;
        }
        
        public string GetCurrentTenant()
        {
            return _service.GetCurrentTenant(_httpContext);
        }
    }
}