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
  public class ParticipantsController : ControllerBase
  {
    private readonly IParticipantService _participantService;
    private readonly IMapper _mapper;

    public ParticipantsController(IParticipantService participantService, IMapper mapper)
    {
      _participantService = participantService;
      _mapper = mapper;
    }


    // GET api/animals
    [HttpGet]
    public async Task<IEnumerable<ParticipantResource>> GetAllAsync()
    {
      var participants = await _participantService.ListAsync();
      var resources = _mapper.Map<IEnumerable<Participant>, IEnumerable<ParticipantResource>>(participants);

      return resources;
    }

    // GET api/animals/5
    [HttpGet("{id}")]
    public async Task<ParticipantResource> Get(int id)
    {
      var participant = await _participantService.GetById(id);
      var resource = _mapper.Map<Participant, ParticipantResource>(participant);

      return resource;
    }

    // // POST api/animals
    // [HttpPost]
    // public void Post([FromBody] string value)
    // {
    // }

    // // PUT api/animals/5
    // [HttpPut("{id}")]
    // public void Put(int id, [FromBody] string value)
    // {
    // }

    // // DELETE api/animals/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
  }
}
