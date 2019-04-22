using System;
using System.Collections.Generic;

namespace MMM_Bracket.API.Domain.Models
{
  public partial class Categories
  {
    public Categories()
    {
      Animals = new HashSet<Animal>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }

    public virtual ICollection<Animal> Animals { get; set; }
  }
}
