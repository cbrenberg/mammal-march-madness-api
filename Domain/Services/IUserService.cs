using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Domain.Services
{
  public interface IUserService
  {
    bool IsValidUser(string username, string password);
    Task<User> Authenticate(string username, string password);

    //TODO:
    //AddUser
    //PasswordHasher
  }
}
