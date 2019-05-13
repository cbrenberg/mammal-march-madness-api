using MMM_Bracket.API.Resources;

namespace MMM_Bracket.API.Domain.Services
{
  public interface ITokenAuthenticationService
  {
    bool IsValidToken(TokenRequestResource request, out string token);
  }
}
