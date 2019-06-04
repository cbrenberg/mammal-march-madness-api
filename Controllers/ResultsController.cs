using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MMM_Bracket.API.Domain.Services;
using MMM_Bracket.API.Domain.Models;
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

    // GET api/results/5
    [HttpGet("{id}")]
    public async Task<BattleResource> Get(int id)
    {
      var battle = await _resultsService.GetById(id);
      var resource = _mapper.Map<Battle, BattleResource>(battle);

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
