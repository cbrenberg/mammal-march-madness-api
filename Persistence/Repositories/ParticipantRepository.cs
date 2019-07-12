using System.Collections.Generic;
using System.Linq;
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

    public async Task<IEnumerable<Participant>> GetByBattleId(int battleId)
    {
      return await _context.Participants.Where(x => x.BattleId == battleId).Include("Animal").Include("Battle").ToListAsync();
    }
  }
}