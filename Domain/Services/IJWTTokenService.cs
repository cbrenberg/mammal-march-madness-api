using System.Collections.Generic;
using System.Security.Claims;
using MMM_Bracket.API.Resources;

namespace MMM_Bracket.API.Domain.Services
{
    public interface IJWTTokenService
    {
        string CreateAccessTokenForValidUserResource(UserResource user);

        string GenerateAccessTokenWithClaims(IEnumerable<Claim> claims);
        string GenerateRefreshToken();

        bool IsRefreshTokenExpired(string refreshToken);
    }
}
