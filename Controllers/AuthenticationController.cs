using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Models.Configuration;
using MMM_Bracket.API.Domain.Services;
using MMM_Bracket.API.Resources;

namespace MMM_Bracket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJWTTokenService _tokenAuthService;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

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

            UserResource authorizedUser = await GetAuthorizedUserFromTokenRequest(request);

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

        private async Task<UserResource> GetAuthorizedUserFromTokenRequest(LoginCredentialsResource request)
        {
            return await _userService.Authenticate(request.Username, request.Password);
        }


        [AllowAnonymous]
        [HttpPost, Route("refresh")]
        public async Task<ActionResult> RequestRefreshToken([FromBody] RefreshTokenRequestParams tokenParams)
        {
            UserResource userWithNewRefreshToken;
            string newRefreshToken;
            string newJwtToken;

            try
            {
                string expiredTokenFromClient = tokenParams.AccessToken;
                string refreshTokenFromClient = tokenParams.RefreshToken;

                if (_tokenAuthService.IsRefreshTokenExpired(refreshTokenFromClient))
                {
                    throw new SecurityTokenExpiredException("Refresh Token Is Expired");
                }

                ClaimsPrincipal principal = GetValidatedClaimsPrincipalFromExpiredToken(expiredTokenFromClient);
                string username = principal.FindFirstValue("Username");
                string refreshTokenFromDatabase = await GetStoredRefreshTokenForUser(username);

                if (refreshTokenFromClient != refreshTokenFromDatabase)
                {
                    throw new SecurityTokenValidationException("Invalid Refresh Token");
                }

                IEnumerable<Claim> publicClaims = ExtractPublicClaims(principal);

                newJwtToken = _tokenAuthService.GenerateAccessTokenWithClaims(publicClaims);

                int id = Convert.ToInt32(principal.FindFirstValue("Id"));
                newRefreshToken = _tokenAuthService.GenerateRefreshToken();

                userWithNewRefreshToken = await _userService.SaveRefreshToken(id, newRefreshToken);

            }
            catch (Exception e)
            {
                return StatusCode(401, $"Unable to issue refresh token: {e.Message}");
            }

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

            var principal = _tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = (JwtSecurityToken)securityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenDecryptionFailedException("Invalid token.");
            }
            return principal;
        }

        private async Task<string> GetStoredRefreshTokenForUser(string username)
        {
            UserResource user = await _userService.GetUserByUsername(username);
            string storedRefreshToken = user.RefreshToken;
            return storedRefreshToken;
        }

        private IEnumerable<Claim> ExtractPublicClaims(ClaimsPrincipal principal)
        {
            Claim[] publicClaims = new Claim[4];
            publicClaims[0] = principal.FindFirst("Id");
            publicClaims[1] = principal.FindFirst("Username");
            publicClaims[2] = principal.FindFirst("FirstName");
            publicClaims[3] = principal.FindFirst("IsAdmin");

            return publicClaims;
        }

        [AllowAnonymous]
        [HttpPost, Route("register")]
        public async Task<ActionResult> RegisterNewUser([FromBody] RegistrationRequestResource request)
        {

            User newUser = new User
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                FirstName = request.FirstName
            };

            try
            {
                bool success = await _userService.RegisterNewUser(newUser);
                if (success == false) throw new ApplicationException("Failed during registration");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Could not register new user: {e.Message}");
            }

            return StatusCode(200, "Successfully registered. Proceed to login.");
        }
    }
}
