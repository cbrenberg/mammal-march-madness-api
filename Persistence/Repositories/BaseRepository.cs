using MMM_Bracket.API.Persistence.Contexts;

namespace MMM_Bracket.API.Persistence.Repositories
{
  public abstract class BaseRepository
  {
    protected readonly mmm_bracketContext _context;

    public BaseRepository(mmm_bracketContext context)
    {
      _context = context;
    }
  }
}