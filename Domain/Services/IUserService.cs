using System.Threading.Tasks;
using MMM_Bracket.API.Resources;

namespace MMM_Bracket.API.Domain.Services
{
  public interface IUserService
  {
    bool IsValidUser(string username, string password);
    Task<UserResource> Authenticate(string username, string password);
    Task<UserResource> GetUserById(int id);
    Task<UserResource> GetUserByUsername(string username);
    Task<UserResource> SaveRefreshToken(int id, string newRefreshToken);
    // Task<User> UpdateUser(string id);

  }
}
