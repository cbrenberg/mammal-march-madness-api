using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using Newtonsoft.Json;
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

  private JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

  public AuthorizationController(ITokenAuthenticationService tokenAuthService, IUserService userService, IMapper mapper, IConfiguration config)
  {
    _tokenAuthService = tokenAuthService;
    _mapper = mapper;
    _userService = userService;
    _config = config;
  }

  [AllowAnonymous]
  [HttpPost, Route("request")]
  public async Task<ActionResult> RequestTokenAtLogin([FromBody] LoginCredentialsResource request)//TODO rename tokenrequestresource to logincredentialsresource
  {
    if (!ModelState.IsValid)
    {
      return BadRequest("Invalid Request");
    }

    var authorizedUser = await getAuthorizedUserFromTokenRequest(request);

    if (authorizedUser != null)
    {
      string refreshToken = _tokenAuthService.GenerateRefreshToken();
      //TODO save refresh token to DB

      var userResource = _mapper.Map<User, UserResource>(authorizedUser);
      string token = _tokenAuthService.CreateAccessTokenForValidUser(userResource);
      object result = new { accessToken = token, refreshToken = refreshToken };
      return new ObjectResult(result);
    }

    return Unauthorized("Invalid Credentials");
  }

  private async Task<User> getAuthorizedUserFromTokenRequest(LoginCredentialsResource request)
  {
    return await _userService.Authenticate(request.Username, request.Password);
  }

  public class RefreshTokenRequestParams
  {
    [JsonProperty("accessToken")]
    public string AccessToken { get; set; }
    [JsonProperty("refreshToken")]
    public string RefreshToken { get; set; }
  }

  [AllowAnonymous]
  [HttpPost, Route("refresh")]
  public async Task<ActionResult> RequestRefreshToken([FromBody] RefreshTokenRequestParams tokenParams)
  {
    string expiredTokenFromClient = tokenParams.AccessToken;
    string refreshTokenFromClient = tokenParams.RefreshToken;

    ClaimsPrincipal principal = GetValidatedClaimsPrincipalFromExpiredToken(expiredTokenFromClient);
    // string username = principal.Claims.First(x => x.Type == "Username").Value.ToString();
    string username = principal.FindFirst("Username").Value.ToString();
    string refreshTokenFromDatabase = await getStoredRefreshTokenForUser(username);

    if (refreshTokenFromClient != refreshTokenFromDatabase)
    {
      throw new SecurityTokenException("Invalid Refresh Token");
    }

    var publicClaims = extractPublicClaims(principal);

    string newJwtToken = _tokenAuthService.GenerateAccessTokenWithClaims(publicClaims);

    string newRefreshToken = _tokenAuthService.GenerateRefreshToken();
    //TODO: DELETE and SAVE new refresh token to DB
    var result = new { accessToken = newJwtToken, refreshToken = newRefreshToken };
    return new ObjectResult(result);
  }

  private ClaimsPrincipal GetValidatedClaimsPrincipalFromExpiredToken(string token)
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

    SecurityToken securityToken;
    var principal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out securityToken);
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

  private IEnumerable<Claim> extractPublicClaims(ClaimsPrincipal principal)
  {
    Claim[] publicClaims = new Claim[4];
    publicClaims[0] = principal.FindFirst("Id");
    publicClaims[1] = principal.FindFirst("Username");
    publicClaims[2] = principal.FindFirst("FirstName");
    publicClaims[3] = principal.FindFirst("IsAdmin");

    return publicClaims;
  }
}