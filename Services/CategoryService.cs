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
      _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesByYear(int year)
    {
      return await _categoryRepository.GetAllCategoriesByYear(year);
    }

    public async Task<Category> GetCategoryById(int id)
    {
      return await _categoryRepository.GetCategoryById(id);
    }
  }
}