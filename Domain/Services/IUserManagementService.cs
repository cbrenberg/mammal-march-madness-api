using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Domain.Services
{
  public interface IUserManagementService
  {

    //TODO implement User Management Service in conjunction with user management repository class and database user credentials
    bool IsValidUser(string username, string password);
  }
}
