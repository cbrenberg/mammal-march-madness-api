using System.Collections.Generic;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Domain.Repositories
{
  public interface IUserRepository
  {
    Task<User> Authenticate(string username, string password);
    Task<IEnumerable<User>> ListAsync();
    Task<User> GetById(int id);
    Task<User> GetByUsername(string username);
    Task<User> SaveRefreshToken(int id, string newRefreshToken);
    //TODO:
    // AddUser(User user);
  }
}