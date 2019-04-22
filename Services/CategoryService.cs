using System.Collections.Generic;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Domain.Services;

namespace MMM_Bracket.API.Services
{
  public class CategoryService : ICategoryService
  {

    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
      this._categoryRepository = categoryRepository;
    }
    public async Task<IEnumerable<Category>> ListAsync()
    {
      return await _categoryRepository.ListAsync();
    }

    public async Task<Category> GetById(int id)
    {
      return await _categoryRepository.GetById(id);
    }
  }
}