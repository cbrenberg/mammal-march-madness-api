using System.Collections.Generic;
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

    public async Task<IEnumerable<Category>> ListAsync()
    {
      return await _context.Categories.Include("Animals").ToListAsync();
    }

    public async Task<Category> GetById(int id)
    {
      return await _context.Categories.Include("Animals").FirstAsync(i => i.Id == id);
    }
  }
}