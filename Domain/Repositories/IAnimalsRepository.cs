using System.Collections.Generic;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Domain.Repositories
{
  public interface IAnimalsRepository
  {
    Task<IEnumerable<Animals>> ListAsync();
  }
}