using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Repositories;
using MMM_Bracket.API.Persistence.Contexts;

namespace MMM_Bracket.API.Persistence.Repositories
{
  public class CategoryRepository : BaseRepository, ICategoryRepository
  {
    public CategoryRepository(mmm_bracketContext context) : base(context)
    {
    }

    public IQueryable<Category> GetAllCategories()
    {
      return _context.Categories.Include("Animals").AsQueryable();

    }

    public async Task<IEnumerable<Category>> GetAllCategoriesByYear(int year)
    {
      var categories = await GetAllCategories()
              .Where(x => x.Year == year)
              .Include(a => a.Animals)
              .ToListAsync();

      categories.ForEach(x => x.Animals = x.Animals.OrderBy(a => a.InitialSeed).ToList());

      return categories;
    }

    public async Task<Category> GetCategoryById(int id)
    {
      return await _context.Categories.Include("Animals").FirstAsync(i => i.Id == id);
    }
  }
}