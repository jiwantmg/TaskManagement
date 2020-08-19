using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Dtos;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Helpers;

namespace TaskManagement.Application.Services
{
    public class JwtProvider: IJwtProvider
    {
        private readonly AppSettings _appSettings;
        public JwtProvider(
            IOptions<AppSettings> appSettings
            )
        {
            _appSettings = appSettings.Value;
        }
        public AuthDto Create(AppUser user, string role, string audience = null, 
            IDictionary<string, IEnumerable<string>> claims = null)
        {
            return new AuthDto
            {    
                    AccessToken = GenerateJwtToken(user)
            };
        }

        private string GenerateJwtToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName), 
                }),
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}