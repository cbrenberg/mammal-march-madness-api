using System;
using System.Collections.Generic;

namespace MMM_Bracket.API.Domain.Models
{
  public partial class Categories
  {
    public Categories()
    {
      Animals = new HashSet<Animals>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }

    public virtual ICollection<Animals> Animals { get; set; }
  }
}
