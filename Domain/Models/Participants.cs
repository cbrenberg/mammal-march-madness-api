﻿using System;
using System.Collections.Generic;

namespace MMM_Bracket.API.Domain.Models
{
  public partial class Participants
  {
    public Participants()
    {
      BracketPicks = new HashSet<BracketPicks>();
    }

    public int Id { get; set; }
    public int AnimalId { get; set; }
    public int BattleId { get; set; }
    public bool IsWinner { get; set; }

    public virtual Animals Animal { get; set; }
    public virtual Battles Battle { get; set; }
    public virtual ICollection<BracketPicks> BracketPicks { get; set; }
  }
}