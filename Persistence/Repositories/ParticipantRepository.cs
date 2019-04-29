using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Persistence.Contexts;

namespace MMM_Bracket.API.Persistence.Repositories
{
  public class ParticipantRepository : BaseRepository, IParticipantRepository
  {
    public ParticipantRepository(mmm_bracketContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Participant>> ListAsync()
    {
      return await _context.Participants.Include("Animal").Include("Battle").ToListAsync();
    }

    public async Task<Participant> GetById(int id)
    {
      return await _context.Participants.Include("Animal").Include("Battle").FirstAsync(i => i.Id == id);
    }
  }
}