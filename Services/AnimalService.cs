using System.Collections.Generic;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Domain.Services;

namespace MMM_Bracket.API.Services
{
  public class AnimalService : IAnimalService
  {

    private readonly IAnimalRepository _animalsRepository;

    public AnimalService(IAnimalRepository animalsRepository)
    {
      this._animalsRepository = animalsRepository;
    }
    public async Task<IEnumerable<Animal>> ListAsync()
    {
      return await _animalsRepository.ListAsync();
    }
  }
}