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
  public class CategoriesController : ControllerBase
  {
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
      _categoryService = categoryService;
      _mapper = mapper;
    }


    // GET api/categories
    [HttpGet]
    public async Task<IEnumerable<CategoryResource>> GetAllAsync([FromQuery] int year)
    {
      var categories = await _categoryService.GetAllCategoriesByYear(year);
      var resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);

      return resources;
    }

    // GET api/categories/5
    [HttpGet("{id}")]
    public async Task<CategoryResource> Get(int id)
    {
      var category = await _categoryService.GetCategoryById(id);
      var resource = _mapper.Map<Category, CategoryResource>(category);

      return resource;
    }

    // // POST api/categories
    // [HttpPost]
    // public void Post([FromBody] string value)
    // {
    // }

    // // PUT api/categories/5
    // [HttpPut("{id}")]
    // public void Put(int id, [FromBody] string value)
    // {
    // }

    // // DELETE api/categories/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
  }
}
