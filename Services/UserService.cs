using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Services;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Domain.Models;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;
  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public bool IsValidUser(string username, string password)
  {
    if (Authenticate(username, password) != null)
    {
      return true;
    }
    return false;
  }
  public async Task<User> Authenticate(string username, string password)
  {
    //TODO validate user credentials from DB
    return await _userRepository.Authenticate(username, password);
  }


}