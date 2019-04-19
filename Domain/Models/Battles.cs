using System;
using System.Collections.Generic;

namespace MMM_Bracket.API.Domain.Models
{
  public partial class Battles
  {
    public Battles()
    {
      Participants = new HashSet<Participants>();
    }

    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string Location { get; set; }
    public int RoundNumber { get; set; }

    public virtual ICollection<Participants> Participants { get; set; }
  }
}
