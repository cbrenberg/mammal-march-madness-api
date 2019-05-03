using MMM_Bracket.API.Domain.Services;

public class UserManagementService : IUserManagementService
{
  public bool IsValidUser(string username, string password)
  {
    return true;
  }
}