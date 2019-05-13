using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Persistence.Contexts;

namespace MMM_Bracket.API.Persistence.Repositories
{
  public class UserRepository : BaseRepository, IUserRepository
  {
    public UserRepository(mmm_bracketContext context) : base(context)
    {
    }

    public async Task<User> Authenticate(string username, string password)
    {
      User user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username && x.Password == password);

      if (user == null)
      {
        return null;
      }

      user.Password = null;

      return user;
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
      return await _context.Users.ToListAsync();
    }

    public async Task<User> GetById(int id)
    {
      return await _context.Users.FindAsync(id);
    }
  }
}