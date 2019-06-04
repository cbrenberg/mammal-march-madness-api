using System.Collections.Generic;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Domain.Repositories
{
  public interface IResultsRepository
  {
    Task<IEnumerable<Battle>> ListAsync();

    Task<Battle> GetById(int id);
  }
}