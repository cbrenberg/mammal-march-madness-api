using System.Collections.Generic;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Domain.Services;

namespace MMM_Bracket.API.Services
{
  public class ResultsService : IResultsService
    {

    private readonly IResultsRepository _resultsRepository;

    public ResultsService(IResultsRepository resultsRepository)
    {
            _resultsRepository = resultsRepository;
    }
    public async Task<IEnumerable<Battle>> ListAsync()
    {
      return await _resultsRepository.ListAsync();
    }

    public async Task<Battle> GetById(int id)
    {
      return await _resultsRepository.GetById(id);
    }
  }
}