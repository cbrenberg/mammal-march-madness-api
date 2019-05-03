using MMM_Bracket.API.Resources;

namespace MMM_Bracket.API.Domain.Services
{
  public interface IAuthenticationService
  {
    bool IsAuthenticated(TokenRequestResource request, out string token);
  }
}
