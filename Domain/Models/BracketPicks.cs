using System;
using System.Collections.Generic;

namespace MMM_Bracket.API.Domain.Models
{
  public partial class BracketPicks
  {
    public int UserId { get; set; }
    public int WinnerId { get; set; }
    public int Id { get; set; }

    public virtual Users User { get; set; }
    public virtual Participant Winner { get; set; }
  }
}
