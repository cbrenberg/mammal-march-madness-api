using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Domain.Repositories
{
  public interface IUserManagementRepository
  {

    //TODO implement GetUser method with database
    //TODO implement UserManagementRepository class
    // Task<User> GetUser(string username, string password);
    bool IsValidUser(string username, string password);
  }
}
