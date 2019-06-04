using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Domain.Repositories
{
  public interface ICategoryRepository
  {
        IQueryable<Category> GetAllCategories();

        Task<IEnumerable<Category>> GetAllCategoriesByYear(int year);

        Task<Category> GetCategoryById(int id);
  }
}