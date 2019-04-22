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
  public class CategoriesController : ControllerBase
  {
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
      _categoryService = categoryService;
    }


    // GET api/animals
    [HttpGet]
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
      var categories = await _categoryService.ListAsync();

      return categories;
    }

    // GET api/animals/5
    [HttpGet("{id}")]
    public async Task<Category> Get(int id)
    {
      var category = await _categoryService.GetById(id);

      return category;
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
