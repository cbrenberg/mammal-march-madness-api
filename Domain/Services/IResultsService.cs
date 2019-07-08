using System.Collections.Generic;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Domain.Services
{
    public interface IResultsService
    {
        Task<IEnumerable<Battle>> ListAsync();

        Task<IEnumerable<Battle>> ListAsync(int year);
    
        Task<Battle> GetByBattleId(int battleId);
    }
}