using System.Collections.Generic;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Domain.Repositories
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> ListAsync();

        Task<Animal> GetById(int id);
    }
}