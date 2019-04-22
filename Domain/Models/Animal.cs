using System;
using System.Collections.Generic;

namespace MMM_Bracket.API.Domain.Models
{
  public partial class Animal
  {
    public Animal()
    {
      // Participants = new HashSet<Participants>();
    }

    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public int InitialSeed { get; set; }

    public virtual Category Category { get; set; }
    // public virtual ICollection<Participants> Participants { get; set; }
  }
}
