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
  public class CategoriesController : ControllerBase
  {
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
      _categoryService = categoryService;
      _mapper = mapper;
    }


    // GET api/animals
    [HttpGet]
    public async Task<IEnumerable<CategoryResource>> GetAllAsync()
    {
      var categories = await _categoryService.ListAsync();
      var resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);

      return resources;
    }

    // GET api/animals/5
    [HttpGet("{id}")]
    public async Task<CategoryResource> Get(int id)
    {
      var category = await _categoryService.GetById(id);
      var resource = _mapper.Map<Category, CategoryResource>(category);

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
