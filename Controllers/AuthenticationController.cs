using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MMM_Bracket.API.Domain.Services;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Resources;
using JWT;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{
  private ITokenAuthenticationService _tokenAuthService;
  private IMapper _mapper;
  private IUserService _userService;
  private IConfiguration _config;

  public AuthorizationController(ITokenAuthenticationService tokenAuthService, IUserService userService, IMapper mapper, IConfiguration config)
  {
    _tokenAuthService = tokenAuthService;
    _mapper = mapper;
    _userService = userService;
    _config = config;
  }

  [AllowAnonymous]
  [HttpPost, Route("request")]
  public async Task<ActionResult> RequestTokenAtLogin([FromBody] TokenRequestResource request)//TODO rename tokenrequestresource to logincredentialsresource
  {
    if (!ModelState.IsValid)
    {
      return BadRequest("Invalid Request");
    }

    var authorizedUser = await getAuthorizedUserFromTokenRequest(request);

    if (authorizedUser != null)
    {
      //TODO create and save refresh token to DB
      string refreshToken = _tokenAuthService.GenerateRefreshToken();
      var userResource = _mapper.Map<User, UserResource>(authorizedUser);
      string token = _tokenAuthService.CreateAccessTokenForValidUser(userResource);
      object result = new { token = token, refreshToken = refreshToken };
      return new ObjectResult(result);
    }

    return Unauthorized("Invalid Credentials");
  }

  private async Task<User> getAuthorizedUserFromTokenRequest(TokenRequestResource request)
  {
    return await _userService.Authenticate(request.Username, request.Password);
  }

  public class RefreshTokenRequestParams
  {
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
  }

  [AllowAnonymous]
  [HttpPost, Route("refresh")]
  public async Task<ActionResult> RequestRefreshToken([FromBody] RefreshTokenRequestParams tokenParams)
  {
    string expiredToken = tokenParams.AccessToken;
    string clientRefreshToken = tokenParams.RefreshToken;

    ClaimsPrincipal principal = ValidateAndGetPrincipalFromExpiredToken(expiredToken);
    string username = principal.Claims.First(x => x.Type == "Username").Value.ToString();
    //TODO: get username from claims principal

    string refreshTokenFromDatabase = await getStoredRefreshTokenForUser(username);

    if (clientRefreshToken != refreshTokenFromDatabase)
    {
      throw new SecurityTokenException("Invalid Refresh Token");
    }

    var newJwtToken = _tokenAuthService.GenerateAccessTokenWithClaims(principal.Claims);

    string newRefreshToken = _tokenAuthService.GenerateRefreshToken();
    //TODO: DELETE and SAVE refresh token to DB
    var result = new { token = newJwtToken, refreshToken = newRefreshToken };
    return new ObjectResult(result);
  }

  private ClaimsPrincipal ValidateAndGetPrincipalFromExpiredToken(string token)
  {
    var tokenSettings = _config.GetSection("JWTSettings").Get<JWTSettings>();
    var secret = Encoding.ASCII.GetBytes(tokenSettings.SecretKey);
    var tokenValidationParameters = new TokenValidationParameters
    {
      ValidateAudience = true,
      ValidateIssuer = true,
      ValidIssuer = tokenSettings.Issuer,
      ValidAudience = tokenSettings.Audience,
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(secret),
      ValidateLifetime = false,
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    SecurityToken securityToken;
    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
    var jwtSecurityToken = securityToken as JwtSecurityToken;
    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
    {
      throw new SecurityTokenException("Invalid token.");
    };
    return principal;
  }

  private async Task<string> getStoredRefreshTokenForUser(string username)
  {
    User user = await _userService.GetUserByUsername(username);
    string storedRefreshToken = user.RefreshToken;
    return storedRefreshToken;
  }
}