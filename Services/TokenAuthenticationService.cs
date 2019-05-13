using MMM_Bracket.API.Domain.Services;
using MMM_Bracket.API.Resources;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MMM_Bracket.API.Services
{
  public class TokenAuthenticationService : ITokenAuthenticationService
  {
    private readonly IUserService _userService;
    private readonly JWTSettings _jwtSettings;

    public TokenAuthenticationService(IUserService userService, IOptions<JWTSettings> jwtSettings)
    {
      _userService = userService;
      _jwtSettings = jwtSettings.Value;
    }
    public bool IsValidToken(TokenRequestResource request, out string token)
    {
      token = string.Empty;
      if (!_userService.IsValidUser(request.Username, request.Password))
      {
        return false;
      }
      else
      {
        token = createTokenFromRequestResource(request);
        return true;
      }
    }

    private string createTokenFromRequestResource(TokenRequestResource request)
    {
      var claim = new[]
      {
        new Claim(ClaimTypes.Name, request.Username)
      };
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var jwtToken = new JwtSecurityToken(
          _jwtSettings.Issuer,
          _jwtSettings.Audience,
          claim,
          expires: DateTime.Now.AddMinutes(_jwtSettings.AccessExpiration),
          signingCredentials: credentials
      );
      string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

      return token;
    }
  }
}
