using System.Collections.Generic;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Domain.Services;

namespace MMM_Bracket.API.Services
{
  public class ParticipantService : IParticipantService
  {

    private readonly IParticipantRepository _participantRepository;

    public ParticipantService(IParticipantRepository participantRepository)
    {
      this._participantRepository = participantRepository;
    }
    public async Task<IEnumerable<Participant>> ListAsync()
    {
      return await _participantRepository.ListAsync();
    }

    public async Task<Participant> GetById(int id)
    {
      return await _participantRepository.GetById(id);
    }
  }
}