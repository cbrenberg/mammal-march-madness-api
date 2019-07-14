using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Services;
using MMM_Bracket.API.Resources;

namespace MMM_Bracket.API.Controllers
{
    [Route("api/[controller]")]
  [ApiController]
  public class ResultsController : ControllerBase
  {
    private readonly IResultsService _resultsService;
    private readonly IMapper _mapper;

    public ResultsController(IResultsService resultsService, IMapper mapper)
    {
        _resultsService = resultsService;
        _mapper = mapper;
    }

    // GET api/results
    [HttpGet]
    public async Task<IEnumerable<BattleResource>> GetAllAsync()
    {
      var battles = await _resultsService.ListAsync();
      var resources = _mapper.Map<IEnumerable<Battle>, IEnumerable<BattleResource>>(battles);

      return resources;
    }

    // GET api/results/{battleId}
    [HttpGet("{battleId}")]
    public async Task<BattleResource> GetBattleResultsByBattleId(int battleId)
    {
      var battle = await _resultsService.GetByBattleId(battleId);
      var resource = _mapper.Map<Battle, BattleResource>(battle);

      return resource;
    }

    // GET api/results/official/{year}
    [HttpGet("official/{year}")]
    public async Task<IEnumerable<BattleResource>> GetOfficialResultsForYear(int year)
    {
        var officialResults = await _resultsService.ListAsync(year);
        var resource = _mapper.Map<IEnumerable<Battle>, IEnumerable<BattleResource>>(officialResults);
        return resource;
    }

    // // POST api/results
    // [HttpPost]
    // public void Post([FromBody] string value)
    // {
    // }

    // // PUT api/results/5
    // [HttpPut("{id}")]
    // public void Put(int id, [FromBody] string value)
    // {
    // }

    // // DELETE api/results/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
  }
}
