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
  public class TokenAuthenticationService : IAuthenticationService
  {
    private readonly IUserManagementService _userManagementService;
    private readonly JWTSettings _jwtSettings;

    public TokenAuthenticationService(IUserManagementService service, IOptions<JWTSettings> jwtSettings)
    {
      _userManagementService = service;
      _jwtSettings = jwtSettings.Value;
    }
    public bool IsAuthenticated(TokenRequestResource request, out string token)
    {

      token = string.Empty;
      if (!_userManagementService.IsValidUser(request.Username, request.Password)) return false;

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
      token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
      return true;
    }
  }
}
