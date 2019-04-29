using System;
using System.Collections.Generic;
using System.Linq;
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
  public class AnimalsController : ControllerBase
  {
    private readonly IAnimalService _animalService;
    private readonly IMapper _mapper;

    public AnimalsController(IAnimalService animalService, IMapper mapper)
    {
      _animalService = animalService;
      _mapper = mapper;
    }


    // GET api/animals
    [HttpGet]
    public async Task<IEnumerable<AnimalResource>> GetAllAsync()
    {
      var animals = await _animalService.ListAsync();
      var resources = _mapper.Map<IEnumerable<Animal>, IEnumerable<AnimalResource>>(animals);

      return resources;
    }

    // GET api/animals/5
    [HttpGet("{id}")]
    public async Task<AnimalResource> Get(int id)
    {
      var animal = await _animalService.GetById(id);
      var resource = _mapper.Map<Animal, AnimalResource>(animal);

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
