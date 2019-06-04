using System;
using System.Collections.Generic;

namespace MMM_Bracket.API.Resources
{
    public class BattleResource
    {
        public BattleResource()
        {
            Participants = new HashSet<ParticipantResource>();
        }

        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Location { get; set; }
        public int RoundNumber { get; set; }

        public ICollection<ParticipantResource> Participants { get; set; }
    }
}
