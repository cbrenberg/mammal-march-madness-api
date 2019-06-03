using System.Collections.Generic;

namespace MMM_Bracket.API.Resources
{
  public class CategoryResource
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }

    public ICollection<AnimalResource> Animals { get; set; }
  }
}