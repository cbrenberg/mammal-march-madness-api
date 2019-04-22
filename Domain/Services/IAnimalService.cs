using System.Collections.Generic;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Domain.Services
{
  public interface IAnimalService
  {
    Task<IEnumerable<Animal>> ListAsync();
  }
}