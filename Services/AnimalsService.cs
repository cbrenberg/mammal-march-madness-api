using System.Collections.Generic;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Domain.Services;

namespace MMM_Bracket.API.Services
{
  public class AnimalsService : IAnimalsService
  {

    private readonly IAnimalsRepository _animalsRepository;

    public AnimalsService(IAnimalsRepository animalsRepository)
    {
      this._animalsRepository = animalsRepository;
    }
    public async Task<IEnumerable<Animals>> ListAsync()
    {
      return await _animalsRepository.ListAsync();
    }
  }
}