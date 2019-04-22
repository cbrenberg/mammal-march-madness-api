using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMM_Bracket.API.Domain.Services;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AnimalsController : ControllerBase
  {
    private readonly IAnimalService _animalService;

    public AnimalsController(IAnimalService animalService)
    {
      _animalService = animalService;
    }


    // GET api/animals
    [HttpGet]
    public async Task<IEnumerable<Animal>> GetAllAsync()
    {
      var animals = await _animalService.ListAsync();

      return animals;
    }

    // // GET api/animals/5
    // [HttpGet("{id}")]
    // public ActionResult<string> Get(int id)
    // {
    //   return "value";
    // }

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
