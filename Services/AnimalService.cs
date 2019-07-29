using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Domain.Services;

namespace MMM_Bracket.API.Services
{
  public class AnimalService : IAnimalService
  {

    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }
    
    public async Task<IEnumerable<Animal>> ListAsync()
    {
      return await _animalRepository.ListAsync();
    }

    public async Task<Animal> GetById(int id)
    {
      return await _animalRepository.GetById(id);
    }
  }
}