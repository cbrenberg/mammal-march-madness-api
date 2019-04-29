using System.Collections.Generic;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Domain.Repositories
{
  public interface IParticipantRepository
  {
    Task<IEnumerable<Participant>> ListAsync();

    Task<Participant> GetById(int id);
  }
}