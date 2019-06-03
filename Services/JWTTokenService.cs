using MMM_Bracket.API.Domain.Services;
using MMM_Bracket.API.Domain.Models.Configuration;
using MMM_Bracket.API.Resources;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MMM_Bracket.API.Services
{
    public class JWTTokenService : IJWTTokenService
    {
        private readonly JWTSettings _jwtSettings;

        public JWTTokenService(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string CreateAccessTokenForValidUserResource(UserResource authenticatedUser)
        {
            Claim[] publicClaims =
            {
        new Claim("Id", authenticatedUser.Id.ToString()),
        new Claim("Username", authenticatedUser.Username),
        new Claim("FirstName", authenticatedUser.FirstName),
        new Claim("IsAdmin", authenticatedUser.IsAdmin)
      };

            string token = GenerateAccessTokenWithClaims(publicClaims);
            return token;
        }

        public string GenerateAccessTokenWithClaims(IEnumerable<Claim> publicClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                publicClaims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessExpiration),
                signingCredentials: credentials
            );
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            RandomNumberGenerator.Create().GetBytes(randomNumber);
            //TODO: add refresh token expiration
            string refreshToken = Convert.ToBase64String(randomNumber);
            string refreshExpiration = DateTimeOffset.UtcNow.AddHours(2).ToUnixTimeMilliseconds().ToString();

            return String.Join("::", refreshToken, refreshExpiration);
        }
    }
}
