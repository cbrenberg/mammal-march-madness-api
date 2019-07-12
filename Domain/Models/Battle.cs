using System;
using System.Collections.Generic;

namespace MMM_Bracket.API.Domain.Models
{
    public partial class Battle
    {
        public Battle()
        {
            Participants = new HashSet<Participant>();
        }

        public Battle(int roundNumber)
        {
            this.RoundNumber = roundNumber;
        }

        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Location { get; set; }
        public int RoundNumber { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }
    }
}
