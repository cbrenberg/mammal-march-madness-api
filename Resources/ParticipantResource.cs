using System.Collections.Generic;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Resources
{
  public class ParticipantResource
  {
    // public ParticipantResource()
    // {
    //   BracketPicks = new HashSet<BracketPicks>();
    // }

    public int Id { get; set; }
    // public int AnimalId { get; set; }
    public int BattleId { get; set; }
    public bool IsWinner { get; set; }

    public AnimalResource Animal { get; set; }
    // public Battles Battle { get; set; }
    // public virtual ICollection<BracketPicks> BracketPicks { get; set; }
  }
}
