using System;
using System.Collections.Generic;

namespace TaskManagement.Tenant
{
    public class TenantMapping
    {
        public string Default { get; set; }
        public Dictionary<string, string> Tenants { get; set; } = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
    }
}