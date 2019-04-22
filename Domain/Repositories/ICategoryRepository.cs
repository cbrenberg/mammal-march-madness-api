using System.Collections.Generic;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Domain.Repositories
{
  public interface ICategoryRepository
  {
    Task<IEnumerable<Category>> ListAsync();

    Task<Category> GetById(int id);
  }
}