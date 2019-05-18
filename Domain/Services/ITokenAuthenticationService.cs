using System.Collections.Generic;
using System.Security.Claims;
using MMM_Bracket.API.Resources;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Domain.Services
{
  public interface ITokenAuthenticationService
  {
    string CreateTokenForValidUser(UserResource user);

    string GenerateToken(IEnumerable<Claim> claims);
  }
}
