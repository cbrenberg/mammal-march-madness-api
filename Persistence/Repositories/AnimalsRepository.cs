using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Persistence.Contexts;

namespace MMM_Bracket.API.Persistence.Repositories
{
  public class AnimalsRepository : BaseRepository, IAnimalsRepository
  {
    public AnimalsRepository(mmm_bracketContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Animals>> ListAsync()
    {
      return await _context.Animals.ToListAsync();
    }
  }
}