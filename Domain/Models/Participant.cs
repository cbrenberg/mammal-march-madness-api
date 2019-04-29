using System;
using System.Collections.Generic;

namespace MMM_Bracket.API.Domain.Models
{
  public partial class Participant
  {
    public Participant()
    {
      BracketPicks = new HashSet<BracketPicks>();
    }

    public int Id { get; set; }
    public int AnimalId { get; set; }
    public int BattleId { get; set; }
    public bool IsWinner { get; set; }

    public virtual Animal Animal { get; set; }
    public virtual Battles Battle { get; set; }
    public virtual ICollection<BracketPicks> BracketPicks { get; set; }
  }
}
