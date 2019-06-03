using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using MMM_Bracket.API.Domain.Services;
using MMM_Bracket.API.Domain.Models.Configuration;
using MMM_Bracket.API.Resources;
using JWT;

namespace MMM_Bracket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IJWTTokenService _tokenAuthService;
        private IUserService _userService;
        private IConfiguration _config;

        private JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

        public AuthenticationController(IJWTTokenService tokenAuthService, IUserService userService, IConfiguration config)
        {
            _tokenAuthService = tokenAuthService;
            _userService = userService;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public async Task<ActionResult> RequestTokenAtLogin([FromBody] LoginCredentialsResource request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }

            UserResource authorizedUser = await getAuthorizedUserFromTokenRequest(request);

            if (authorizedUser != null)
            {
                string refreshToken = _tokenAuthService.GenerateRefreshToken();
                UserResource updatedUserResource = await _userService.SaveRefreshToken(authorizedUser.Id, refreshToken);

                string accessToken = _tokenAuthService.CreateAccessTokenForValidUserResource(updatedUserResource);
                object result = new { accessToken, refreshToken };
                return new ObjectResult(result);
            }

            return Unauthorized("Invalid Credentials");
        }

        private async Task<UserResource> getAuthorizedUserFromTokenRequest(LoginCredentialsResource request)
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
            UserResource userWithNewRefreshToken = null;
            string newRefreshToken = "";
            string newJwtToken = "";

            try
            {
                string expiredTokenFromClient = tokenParams.AccessToken;
                string refreshTokenFromClient = tokenParams.RefreshToken;

                ClaimsPrincipal principal = GetValidatedClaimsPrincipalFromExpiredToken(expiredTokenFromClient);
                string username = principal.FindFirstValue("Username");
                string refreshTokenFromDatabase = await getStoredRefreshTokenForUser(username);

                if (refreshTokenFromClient != refreshTokenFromDatabase)
                {
                    throw new SecurityTokenException("Invalid Refresh Token");
                }

                IEnumerable<Claim> publicClaims = extractPublicClaims(principal);

                newJwtToken = _tokenAuthService.GenerateAccessTokenWithClaims(publicClaims);

                int id = Convert.ToInt32(principal.FindFirstValue("Id"));
                newRefreshToken = _tokenAuthService.GenerateRefreshToken();

                userWithNewRefreshToken = await _userService.SaveRefreshToken(id, newRefreshToken);

            }
            catch (Exception e)
            {
                Console.Write("Unable to issue refresh token: ", e);
                
            }

            if (userWithNewRefreshToken != null)
            {
                var result = new { accessToken = newJwtToken, refreshToken = newRefreshToken };
                return new ObjectResult(result);
            }
                
            return StatusCode(500, "Unable to issue refresh token");
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

            var principal = _tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = (JwtSecurityToken)securityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }
            return principal;
        }

        private async Task<string> getStoredRefreshTokenForUser(string username)
        {
            UserResource user = await _userService.GetUserByUsername(username);
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
}
