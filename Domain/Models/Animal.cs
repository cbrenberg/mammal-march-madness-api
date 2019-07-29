using System;
using System.Collections.Generic;
using static MMM_Bracket.API.Services.BracketService;

namespace MMM_Bracket.API.Domain.Models
{
  public partial class Animal
  {
    public Animal()
    {
      Participants = new HashSet<Participant>();
    }

    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public int InitialSeed { get; set; }

    public virtual Category Category { get; set; }
    public virtual ICollection<Participant> Participants { get; set; }
    
        //TODO: convert to async Task<bool>
    public static bool AreCorrectlyMatchedInRound(Animal animal, Animal competitor, Round round)
        {
            return GetSeedForMatching(round, animal.InitialSeed) == GetSeedForMatching(round, competitor.InitialSeed);
        }

    //TODO: convert to async Task<int> 
    private static int GetSeedForMatching(Round round, int seed, int currentIteration = 0)
    {
            //highest possible seed value allowed for current iteration
            int cutoffSeed = (int)(16 / Math.Pow(2d, currentIteration));

            //basically finds the reverse index of anything higher than the cutoff value (counts from end of list)
            int nextSeed = seed > cutoffSeed ? (cutoffSeed*2 - seed + 1) : seed;

            //end recursion after number of iterations equal to round
            if (currentIteration == (int)round)
            {
                return nextSeed;
            }

            //recurse with new seed and incremented iteration
            return GetSeedForMatching(round, nextSeed, currentIteration + 1);
    }
  }
}
