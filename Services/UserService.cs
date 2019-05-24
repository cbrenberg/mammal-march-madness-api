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
    return await _userRepository.Authenticate(username, password);
  }
  public async Task<User> GetUserById(int id)
  {
    return await _userRepository.GetById(id);
  }
  public async Task<User> GetUserByUsername(string username)
  {
    return await _userRepository.GetByUsername(username);
  }


}