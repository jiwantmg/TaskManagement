using System;
using System.Collections.Generic;
using TaskManagement.Domain.Dtos;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.Interfaces
{
    public interface IJwtProvider
    {
        AuthDto Create(AppUser user, string role, string audience = null,
            IDictionary<string, IEnumerable<string>> claims = null);
    }
}