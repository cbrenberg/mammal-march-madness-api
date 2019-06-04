using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Persistence.Contexts;

namespace MMM_Bracket.API.Persistence.Repositories
{
  public class ResultsRepository : BaseRepository, IResultsRepository
  {
    public ResultsRepository(mmm_bracketContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Battle>> ListAsync()
    {
      return await _context.Battles.Include(x => x.Participants).ThenInclude(x => x.Animal).ToListAsync();
    }

    public async Task<Battle> GetById(int id)
    {
      return await _context.Battles.Include("Participants").FirstAsync(i => i.Id == id);
    }
  }
}