using MMM_Bracket.API.Domain.Services;
using MMM_Bracket.API.Domain.Models;
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
  public class TokenAuthenticationService : ITokenAuthenticationService
  {
    private readonly JWTSettings _jwtSettings;

    public TokenAuthenticationService(IOptions<JWTSettings> jwtSettings)// TODO rename to JWTTokenService
    {
      _jwtSettings = jwtSettings.Value;
    }

    public string CreateTokenForValidUser(UserResource authenticatedUser) //TODO change param to type User
    {
      Claim[] claims =
      {
        new Claim("Username", authenticatedUser.Username),
        new Claim("FirstName", authenticatedUser.FirstName),
        new Claim("IsAdmin", authenticatedUser.IsAdmin),
        new Claim("Id", authenticatedUser.Id.ToString()) //TODO add public claims from user param
      };

      string token = GenerateToken(claims);
      return token;
    }

    public string GenerateToken(IEnumerable<Claim> claims)
    {
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var jwtToken = new JwtSecurityToken(
          _jwtSettings.Issuer,
          _jwtSettings.Audience,
          claims,
          expires: DateTime.Now.AddMinutes(_jwtSettings.AccessExpiration),
          signingCredentials: credentials
      );
      string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

      return token;
    }
  }
}
