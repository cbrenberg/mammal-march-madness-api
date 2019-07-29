using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Domain.Services
{
    public interface IAnimalService
    {
        Task<IEnumerable<Animal>> ListAsync();

        Task<Animal> GetById(int id);
    }
}